using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    public interface IPayRepository : IRepository<Pay, long> {

        /// <summary>
        ///     根据实体Id列表获取订单金额
        /// </summary>
        /// <param name="entityIdList"></param>
        IEnumerable<PayShopOrderInfo> GetOrderPayAccount(List<object> entityIdList);

        /// <summary>
        ///     支付完成后更新订单状态
        ///     更新支付订单状态
        ///     整个系统所有的支付：支付完成以后的事物处理
        ///     线上商城，线下商城、会员权益等等，无论何种支付方式
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <param name="pay"></param>
        /// <param name="isPaySucess">是否支出成功</param>
        ServiceResult AfterPay(List<object> entityIdList, Pay pay, bool isPaySucess);
    }
}