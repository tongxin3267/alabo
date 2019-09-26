using System.Collections.Generic;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Repositories;

namespace Alabo.Data.Things.Orders.Domain.Repositories {

    /// <summary>
    ///     分润订单
    /// </summary>
    public interface IShareOrderRepository : IRepository<ShareOrder, long> {

        /// <summary>
        ///     获取s the un handled identifier list.
        /// </summary>
        IList<long> GetUnHandledIdList();

        /// <summary>
        ///     Errors the order.
        /// </summary>
        /// <param name="shareOrderId">The share order identifier.</param>
        /// <param name="message">The message.</param>
        void ErrorOrder(long shareOrderId, string message);

        /// <summary>
        /// 更新成功
        /// </summary>
        /// <param name="shareOrderId"></param>
        void SuccessOrder(long shareOrderId);

        /// <summary>
        ///     获取s the single native.
        /// </summary>
        /// <param name="shareOrderId">The share order identifier.</param>
        ShareOrder GetSingleNative(long shareOrderId);

        /// <summary>
        ///     更新分润执行结果
        ///     价格类型的分润结果
        /// </summary>
        /// <param name="resultList">The result list.</param>
        void UpdatePriceTaskResult(IEnumerable<ShareResult> resultList);

        /// <summary>
        ///     更新分润模块执行次数
        /// </summary>
        /// <param name="shareOrderId">The share order identifier.</param>
        /// <param name="count">The count.</param>
        void UpdateExcuteCount(long shareOrderId, long count);

        /// <summary>
        ///     执行分润执行结果
        /// </summary>
        /// <param name="resultList">The result list.</param>
        void UpdateUpgradeTaskResult(IEnumerable<UserGradeChangeResult> resultList);

        /// <summary>
        ///     根据EntityIds 获取分润订单
        /// </summary>
        /// <param name="EntityIds"></param>
        List<ShareOrder> GetList(List<long> EntityIds);
    }
}