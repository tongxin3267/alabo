using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Alabo.App.Core.Reports.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Reports.Domain.Services {

    public interface IReportService : IService<Report, ObjectId> {

        void AddOrUpdate<T>(T reporyModel) where T : class, IReportModel;

        /// <summary>
        ///     更新配置的值
        ///     ///
        /// </summary>
        /// <param name="value"></param>
        ServiceResult AddOrUpdate<T>(object value) where T : class, IReportModel;

        /// <summary>
        ///     更新配置的值
        /// </summary>
        /// <param name="report"></param>
        void AddOrUpdate(Report report);

        IEnumerable<Type> GetAllTypes();

        /// <summary>
        ///     根据Key获取通用配置
        /// </summary>
        /// <param name="key">完整的命名空间：Alabo.App.Core.Finance.Domain.CallBacks.WithdRawReport</param>
        Report GetReport(string key);

        T GetValue<T>() where T : class, IReportModel;

        List<T> GetList<T>(Func<T, bool> predicate = null) where T : new();

        IEnumerable<SelectListItem> GetList<T>(Func<T, bool> predicate, Func<T, object> textSelector,
            Func<T, object> valueSelector) where T : class, IReportModel;

        object GetValue(string key);

        object GetValue(Type type, Guid id);

        List<JObject> GetList(string key);

        List<object> GetObjectList(Type type);
    }
}