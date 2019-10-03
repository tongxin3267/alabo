using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Reports.Domain.Entities;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alabo.Framework.Reports.Domain.Services
{
    public class ReportService : ServiceBase<Report, ObjectId>, IReportService
    {
        private static readonly string _ReportCacheKey = "ReportCacheKey_";

        public ReportService(IUnitOfWork unitOfWork, IRepository<Report, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        public void AddOrUpdate<T>(T userReport) where T : class, IReport
        {
            var report = new Report();

            var configProperty = typeof(T).GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
            report.LastUpdated = DateTime.Now;
            report.Type = typeof(T).FullName;
            ////report.AppName = configProperty?.AppName;
            report.Summary = configProperty?.Name;
            report.Value = JsonConvert.SerializeObject(userReport);
            AddOrUpdate(report);
        }

        /// <summary>
        ///     更新配置的值
        /// </summary>
        /// <param name="value"></param>
        public ServiceResult AddOrUpdate<T>(object value) where T : class, IReport
        {
            var report = Resolve<IReportService>().GetReport(typeof(T).FullName);
            var typeclassProperty = typeof(T).GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
            if (typeclassProperty == null) {
                return ServiceResult.FailedWithMessage("未设置ClassPropertyAttribute特性");
            }

            if (report == null) {
                report = new Report
                {
                    // AppName = typeclassProperty.AppName,
                    Type = typeof(T).FullName,

                    Summary = typeclassProperty.Description.IsNullOrEmpty()
                        ? typeclassProperty.Name
                        : typeclassProperty.Description
                };
            }

            report.Value = value.ToJson();
            report.LastUpdated = DateTime.Now;
            AddOrUpdate(report);
            return ServiceResult.Success;
        }

        public void AddOrUpdate(Report report)
        {
            if (report == null) {
                throw new ArgumentNullException(nameof(report));
            }

            Report find = null;
            if (!report.Id.IsObjectIdNullOrEmpty()) {
                find = GetSingle(e => e.Id == report.Id);
            }

            if (find == null) {
                find = GetSingle(e => e.Type == report.Type);
            }

            if (find == null)
            {
                find = new Report
                {
                    AppName = report.AppName,
                    Type = report.Type,
                    Summary = report.Summary,
                    Value = report.Value,
                    LastUpdated = DateTime.Now
                };
                Add(find);
                var cacheAllKey = _ReportCacheKey + "_alltypes";
                ObjectCache.Remove(cacheAllKey);
            }
            else
            {
                find.AppName = report.AppName;
                find.Type = report.Type;
                find.Value = report.Value;
                find.Summary = report.Summary;
                find.LastUpdated = DateTime.Now;
                Update(find);
            }

            var cacheKey = _ReportCacheKey + report.Type;
            ObjectCache.Remove(cacheKey);
        }

        /// <summary>
        ///     获取统计信息
        /// </summary>
        /// <param name="key"></param>
        public Report GetReport(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            var cacheKey = _ReportCacheKey + key;
            if (!ObjectCache.TryGet(cacheKey, out Report report))
            {
                report = GetSingle(e => e.Type == key);
                if (report != null) {
                    ObjectCache.Set(cacheKey, report);
                }
            }

            return report;
        }

        public T GetValue<T>() where T : class, IReport
        {
            var report = GetReport(typeof(T).FullName);
            if (report == null) {
                return Activator.CreateInstance(typeof(T)) as T;
            }

            var result = JsonConvert.DeserializeObject<T>(report.Value);
            return result;
        }

        public object GetValue(string key)
        {
            var types = GetAllTypes();
            foreach (var item in types) {
                if (item.FullName == key) {
                    return GetValue(item);
                }
            }

            return null;
        }

        public IEnumerable<Type> GetAllTypes()
        {
            var cacheKey = _ReportCacheKey + "_alltypes";
            if (!ObjectCache.TryGetPublic(cacheKey, out IEnumerable<Type> types))
            {
                //因为遍历所有程序集，速度会有影响
                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IReport)) && t.FullName.EndsWith("Report")));
                //排序，根据SordOrder从小到大排列
                types = types.OrderBy(r =>
                    r.GetTypeInfo().GetAttribute<ClassPropertyAttribute>() != null
                        ? r.GetTypeInfo().GetAttribute<ClassPropertyAttribute>().SortOrder
                        : 1);
                if (types != null) {
                    ObjectCache.Set(cacheKey, types);
                }
            }

            return types;
        }

        public List<JObject> GetList(string key)
        {
            var list = new List<JObject>();
            var dataReport = GetReport(key);
            if (dataReport != null) {
                list = JsonConvert.DeserializeObject<List<JObject>>(dataReport.Value);
            }

            return list;
        }

        public List<T> GetList<T>(Func<T, bool> predicate = null) where T : new()
        {
            var report = GetReport(typeof(T).FullName);
            var t = new T();
            var reportlist = new List<T>();
            if (report != null) {
                if (report.Value != null)
                {
                    reportlist = report.Value.Deserialize(t);
                    if (predicate != null) {
                        return reportlist.Where(predicate).ToList();
                    }
                }
            }

            return reportlist;
        }

        public IEnumerable<SelectListItem> GetList<T>(Func<T, bool> predicate, Func<T, object> textSelector,
            Func<T, object> valueSelector) where T : class, IReport
        {
            var report = GetReport(typeof(T).FullName);
            var values = new List<T>();
            if (report != null)
            {
                var request = JsonConvert.DeserializeObject<List<JObject>>(report.Value);
                foreach (var item in request)
                {
                    var data = (T) Activator.CreateInstance(typeof(T));
                    PropertyDescription.SetValue(data, item);
                    values.Add(data);
                }
            }
            else
            {
                return null;
            }

            return FromIEnumerable(values.Where(predicate).ToList(), textSelector, valueSelector);
        }

        /// <summary>
        ///     获取值的类型
        /// </summary>
        /// <param name="type">值类型</param>
        /// <param name="id">如果是列表页面需要传入ID,编辑页面不需要传入</param>
        public object GetValue(Type type, Guid id)
        {
            var report = GetReport(type.FullName);
            var values = new List<object>();
            var data = Activator.CreateInstance(type);
            if (report != null)
            {
                var reportDescription = new ClassDescription(data.GetType());
                if (reportDescription.ClassPropertyAttribute.PageType == ViewPageType.List)
                {
                    var request = JsonConvert.DeserializeObject<List<JObject>>(report.Value);
                    foreach (var item in request)
                    {
                        PropertyDescription.SetValue(data, item);
                        if (data.GetType().GetProperty("Id").GetValue(data).ToString() == id.ToString()) {
                            return data;
                        }
                    }
                }
                else
                {
                    var request = JsonConvert.DeserializeObject<JObject>(report.Value);
                    PropertyDescription.SetValue(data, request);
                    return data;
                }

                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(type);
        }

        public List<object> GetObjectList(Type type)
        {
            var list = new List<object>();
            var report = GetReport(type.FullName);
            if (report != null)
            {
                var request = JsonConvert.DeserializeObject<List<JObject>>(report.Value);
                foreach (var item in request)
                {
                    var data = Activator.CreateInstance(type);
                    PropertyDescription.SetValue(data, item);
                    list.Add(data);
                }
            }

            return list;
        }

        private object GetValue(Type type)
        {
            var report = GetReport(type.FullName);
            if (report != null)
            {
                var reportDescription = new ClassDescription(report.GetType());
                ///如果是编辑页面获取数据库里头的字，如果列表页面使用 GetList<T>来获取值
                ///如果列表页面编辑的时候，应该要传入ID
                if (reportDescription.ClassPropertyAttribute.PageType == ViewPageType.Edit)
                {
                    var data = Activator.CreateInstance(type);
                    var request = JsonConvert.DeserializeObject<JObject>(report.Value);
                    PropertyDescription.SetValue(data, request);
                    return data;
                }

                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(type);
        }

        public Type GetType(string key)
        {
            var types = GetAllTypes();
            foreach (var item in types) {
                if (item.FullName == key) {
                    return item;
                }
            }

            return null;
        }

        public static IEnumerable<SelectListItem> FromIEnumerable<T>(
            IEnumerable<T> elements,
            Func<T, object> textSelector, Func<T, object> valueSelector)
        {
            foreach (var element in elements) {
                yield return new SelectListItem
                {
                    Text = textSelector(element)?.ToString(),
                    Value = valueSelector(element)?.ToString()
                };
            }
        }
    }
}