using System.Collections.Generic;
using _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Repositories {

    public interface IShareOrderReportRepository : IRepository<ShareOrderReport, ObjectId> {

        /// <summary>
        ///     获取分润数据统计详情
        /// </summary>
        /// <param name="shareOrderId"></param>
        IList<ShareOrderReportItem> GetShareOrderReportItems(long shareOrderId);
    }
}