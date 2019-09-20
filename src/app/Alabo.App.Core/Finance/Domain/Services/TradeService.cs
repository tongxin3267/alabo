using System;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.Recharge;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.ViewModels.Recharge;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Mapping;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    /// </summary>
    public class TradeService : ServiceBase<Trade, long>, ITradeService {

        public TradeService(IUnitOfWork unitOfWork, IRepository<Trade, long> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        ///     获取单个trade
        /// </summary>
        /// <param name="tradeId"></param>
        public Trade GetSingle(long tradeId) {
            var trade = Resolve<ITradeService>().GetSingle(e => e.Id == tradeId);
            return trade;
        }

        #region 充值

        public ServiceResult Delete(long userId, long id) {
            throw new NotImplementedException();
        }

        public ViewAdminRecharge GetAdminRecharge(long id) {
            throw new NotImplementedException();
        }

        public PagedList<RechargeOutput> GetPageList(object query) {
            var model = Resolve<ITradeService>().GetPagedList(query, u => u.Type == TradeType.Recharge);
            var result = new PagedList<RechargeOutput>();
            foreach (var item in model) {
                var view = AutoMapping.SetValue<RechargeOutput>(item);
                result.Add(view);
            }

            return result;
        }

        public Dictionary<string, object> GetRechargeMoneys() {
            throw new NotImplementedException();
        }

        public ListOutput GetUserList(RechargeApiInput parameter) {
            throw new NotImplementedException();
        }

        #endregion 充值
    }
}