using System.Collections.Generic;
using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public class ShareOrderReportRepository : RepositoryMongo<ShareOrderReport, ObjectId>, IShareOrderReportRepository {

        public ShareOrderReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public IList<ShareOrderReportItem> GetShareOrderReportItems(long shareOrderId) {
            // 升级点不统计
            var sql =
                $"select UserId,MoneyTypeId,Amount from Share_Reward where OrderId={shareOrderId} and MoneyTypeId!='E97CCD1E-1478-49BD-BFC7-E73A5D699006'  ";
            IList<ShareOrderReportItem> list = new List<ShareOrderReportItem>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    var shareOrder = new ShareOrderReportItem {
                        UserId = reader["UserId"].ConvertToLong(0),
                        MoneyTypeId = reader["MoneyTypeId"].ToGuid(),
                        Amount = reader["Amount"].ConvertToDecimal(0)
                    };
                    list.Add(shareOrder);
                }
            }

            return list;
        }
    }
}