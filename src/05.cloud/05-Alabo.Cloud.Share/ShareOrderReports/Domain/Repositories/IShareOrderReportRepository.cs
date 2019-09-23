using System.Collections.Generic;
using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public interface IShareOrderReportRepository : IRepository<ShareOrderReport, ObjectId> {

        /// <summary>
        ///     获取分润数据统计详情
        /// </summary>
        /// <param name="shareOrderId"></param>
        IList<ShareOrderReportItem> GetShareOrderReportItems(long shareOrderId);
    }
}