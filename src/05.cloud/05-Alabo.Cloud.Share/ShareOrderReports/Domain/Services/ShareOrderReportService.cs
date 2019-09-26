using System;
using System.Collections.Generic;
using _05_Alabo.Cloud.Share.BonusPools.Domain.Configs;
using _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using MongoDB.Bson;

namespace _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Services {

    public class ShareOrderReportService : ServiceBase<ShareOrderReport, ObjectId>, IShareOrderReportService {

        public ShareOrderReportService(IUnitOfWork unitOfWork, IRepository<ShareOrderReport, ObjectId> repository) :
            base(unitOfWork, repository) {
        }

        public List<ShareOrderReportItem> GetBonusPoolItems(object query) {
            var dictionary = query.ToObject<Dictionary<string, string>>();
            var id = dictionary.TryGetValue("Id", out var objecValue);
            var shareOrderReport = GetSingle(r => r.Id == objecValue.ToObjectId());
            return shareOrderReport.UserItems;
        }

        public PagedList<ShareOrderReport> GetBonusPoolPageList(object query) {
            return GetPagedList(query);
        }

        /// <summary>
        ///     ����ͳ��
        /// </summary>
        /// <param name="isAppend">�Ƿ�Ϊ����ģʽ���������ȫ��ͳ�����е�</param>
        public void Report(bool isAppend) {
            var config = Resolve<IAutoConfigService>().GetValue<BonusPoolConfig>();

            var maxShareOrderId = config.LastShareOrderId;
            if (isAppend) {
                maxShareOrderId = 0;
            }

            // Ŀǰֻͳ���̳Ƕ���
            var shareOrderList = Resolve<IShareOrderService>()
                .GetList(r => r.Id >= maxShareOrderId && r.TriggerType == TriggerType.Order);
            foreach (var shareOrder in shareOrderList) {
                // TODO �ع�ͳ�� 2019��9��24��
                //var shareOrderItems = Ioc.Resolve<IShareOrderRepository>()
                //    .GetShareOrderReportItems(shareOrder.Id);
                //var shareOrderReport = new ShareOrderReport {
                //    Id = shareOrder.Id.ToString().ConvertToObjectId(),
                //    UserId = shareOrder.UserId,
                //    ShareOrderId = shareOrder.Id,
                //    Amount = shareOrder.Amount,
                //    TotalFenRun = shareOrderItems.Sum(r => r.Amount),
                //    UserItems = shareOrderItems.ToList()
                //};
                //if (shareOrderReport.TotalFenRun > 0) {
                //    shareOrderReport.TotalRatio =
                //        Math.Round(shareOrderReport.TotalFenRun / shareOrderReport.Amount, 4); // ����
                //}

                //var find = Resolve<IShareOrderReportService>().GetSingle(r => r.ShareOrderId == shareOrder.Id);
                //if (find == null) {
                //    Add(shareOrderReport);
                //} else {
                //    find.Amount = shareOrderReport.Amount;
                //    find.TotalFenRun = shareOrderReport.TotalFenRun;
                //    find.UserItems = shareOrderReport.UserItems;
                //    shareOrderReport.TotalRatio = shareOrderReport.TotalRatio;
                //    Update(find);
                //}
            }

            //ͳ��
            config.LastReportTime = DateTime.Now;
            config.TotalAmount = 0m;
            var reportList = GetList();
            foreach (var report in reportList) {
                // ������ܽ��
                // �������
                var baseAmount = report.Amount * config.BaseRadio - report.TotalFenRun; // �ܶ������*��׼����-�ܷ����������
                if (baseAmount > 0) {
                    config.TotalAmount += baseAmount;
                }

                // ID ͳ��
                if (report.ShareOrderId > config.LastShareOrderId) {
                    config.LastShareOrderId = report.ShareOrderId;
                }

                //��󲦱�
                if (report.TotalRatio >= config.MaxRadio) {
                    config.MaxRadio = Math.Round(report.TotalRatio, 4);
                }

                //��С����
                if (report.TotalRatio <= config.MinRadio) {
                    config.MinRadio = Math.Round(report.TotalRatio, 4);
                }

                //��󲦱�
                if (report.Amount >= config.MaxAmount) {
                    config.MaxAmount = report.Amount;
                }

                // ����������
                if (report.TotalFenRun >= config.MaxShareAmount) {
                    config.MaxShareAmount = report.TotalFenRun;
                }
            }

            Resolve<IAutoConfigService>().AddOrUpdate<BonusPoolConfig>(config);
        }
    }
}