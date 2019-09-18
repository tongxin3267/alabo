using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Tasks.Domain.Services {

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