using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.Queries.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alabo.Industry.Cms.LightApps.Domain.Services
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public class LightAppService : ServiceBase, ILightAppService
    {
        public LightAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ServiceResult Add(string tableName, string dataJson)
        {
            var modelBaseJson = new AutoDataBaseEntity().ToJsons();

            var rsJoinJson = $"{dataJson.TrimEnd('}')},{modelBaseJson.TrimStart('{')}";
            // dynamic object
            var model = LightAppStore.JsonToDynamicObject(rsJoinJson);
            LightAppStore.GetCollection(tableName).InsertOne(model);
            return ServiceResult.Success;
        }

        public long Count(string tableName)
        {
            return LightAppStore.Count(tableName);
        }

        public ServiceResult Delete(string tableName, ObjectId id)
        {
            var rs = LightAppStore.DeleteOne(tableName, id);

            return rs ? ServiceResult.Success : ServiceResult.Failed;
        }

        /// <summary>
        ///     按 字段 和 字段值 取List
        ///     fieldName || fieldValue 为空则获取数据表的所有数据.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public List<dynamic> GetList(string tableName, string fieldName = "", string fieldValue = "")
        {
            // return all data.
            if (fieldName.IsNullOrEmpty() || fieldValue.IsNullOrEmpty())
                return LightAppStore.GetCollection(tableName).AsQueryable().ToList();

            var dic = new Dictionary<string, string>
            {
                {fieldName, fieldValue}
            };
            var rsList = GetList(tableName, dic).ToList();

            return rsList;
        }

        public List<dynamic> GetList(string tableName, object query)
        {
            throw new NotImplementedException();
        }

        public dynamic GetSingle(string tableName, Dictionary<string, string> query)
        {
            if (tableName.IsNullOrEmpty())
            {
                tableName = query.GetValue("TableName");
                query.Remove("TableName");
            }

            var listOp = new List<(string Field, Operator op, string Value)>();
            foreach (var item in query)
            {
                var rs = LightAppStore.ComparisonParse(item);
                listOp.Add(rs);
            }

            var rsList = LightAppStore.QuerySingle(tableName, listOp);

            return rsList;
        }

        public List<dynamic> GetList(string tableName, Dictionary<string, string> query)
        {
            if (tableName.IsNullOrEmpty())
            {
                tableName = query.GetValue("TableName");
                query.Remove("TableName");
            }

            var listOp = new List<(string Field, Operator op, string Value)>();

            foreach (var item in query)
            {
                var rs = LightAppStore.ComparisonParse(item);
                listOp.Add(rs);
            }

            var rsList = LightAppStore.QueryList(tableName, listOp).ToList();

            return rsList;
        }

        public List<dynamic> GetListByClassId(string tableName, long classId)
        {
            return GetList(tableName, "ClassId", classId.ToString());
        }

        public List<dynamic> GetListByUserId(string tableName, long userId)
        {
            return GetList(tableName, "UserId", userId.ToString());
        }

        public PagedList<dynamic> GetPagedList(string tableName, object paramater)
        {
            var pageSize = 0;
            var pageIndex = 0;
            var dictionary = paramater.DeserializeJson<Dictionary<string, string>>();
            if (dictionary != null)
            {
                var pagedInput = AutoMapping.SetValue<PagedInputDto>(dictionary);
                if (pagedInput != null)
                {
                    pageSize = (int) pagedInput.PageSize;
                    pageIndex = (int) pagedInput.PageIndex;
                }
            }

            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            dictionary.Remove("PageSize");
            dictionary.Remove("PageIndex");

            var listOp = new List<(string Field, Operator op, string Value)>();
            foreach (var item in dictionary)
            {
                var rs = LightAppStore.ComparisonParse(item);
                listOp.Add(rs);
            }

            var filter = LightAppStore.FilterParser(listOp);
            var fetchList = LightAppStore.GetCollection(tableName).Find(filter)
                .Skip(pageSize * (pageIndex - 1)).Limit(pageSize)
                .ToList();
            var totalCount = LightAppStore.GetCollection(tableName).Find(filter).CountDocuments();

            var pagedList = PagedList<dynamic>.Create(fetchList, totalCount, pageSize, pageIndex);

            return pagedList;
        }

        public dynamic GetSingle(string tableName, ObjectId id)
        {
            return LightAppStore.GetSingle(tableName, "_id", id.ToString());
        }

        public dynamic GetSingle(string tableName, string fieldName, string fieldValue)
        {
            return LightAppStore.GetSingle(tableName, fieldName, fieldValue);
        }

        public ServiceResult Update(string tableName, string dataJson, ObjectId id)
        {
            var modelBaseJson = new AutoDataBaseEntity().ToJsons();

            var rsJson = $"{dataJson.TrimEnd('}')},{modelBaseJson.TrimStart('{')}";

            // dynamic object
            var model = LightAppStore.JsonToDynamicObject(rsJson);

            var filter = Builders<dynamic>.Filter.Eq("_id", id);
            LightAppStore.GetCollection(tableName).FindOneAndReplace<dynamic>(filter, model);

            return ServiceResult.Success;
        }

        public IList<dynamic> GetList(Dictionary<string, string> dictionary)
        {
            //var predicate = LinqHelper.DictionaryToLinq<object>(dictionary);
            return GetList(null);
        }
    }
}