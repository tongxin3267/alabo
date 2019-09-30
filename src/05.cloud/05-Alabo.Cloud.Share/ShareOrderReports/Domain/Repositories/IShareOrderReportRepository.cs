using _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;
using System.Collections.Generic;

namespace _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Repositories
{
    public interface IShareOrderReportRepository : IRepository<ShareOrderReport, ObjectId>
    {
        /// <summary>
        ///     ��ȡ��������ͳ������
        /// </summary>
        /// <param name="shareOrderId"></param>
        IList<ShareOrderReportItem> GetShareOrderReportItems(long shareOrderId);
    }
}