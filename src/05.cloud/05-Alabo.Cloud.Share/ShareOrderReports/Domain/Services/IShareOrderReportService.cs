using System.Collections.Generic;
using _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Services {

    public interface IShareOrderReportService : IService<ShareOrderReport, ObjectId> {

        /// <summary>
        ///     ͳ������
        /// </summary>
        void Report(bool isAppend);

        PagedList<ShareOrderReport> GetBonusPoolPageList(object query);

        /// <summary>
        ///     ��������
        /// </summary>
        /// <param name="query"></param>
        List<ShareOrderReportItem> GetBonusPoolItems(object query);
    }
}