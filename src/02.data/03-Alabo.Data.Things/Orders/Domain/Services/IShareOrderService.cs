using System;
using System.Collections.Generic;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Entities.Extensions;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Data.Things.Orders.Domain.Services {

    /// <summary>
    ///     Interface IShareOrderService
    /// </summary>
    public interface IShareOrderService : IService<ShareOrder, long> {

        /// <summary>
        ///     获取s the un handled identifier list.
        /// </summary>
        IList<long> GetUnHandledIdList();

        /// <summary>
        ///     获取单条记录
        /// </summary>
        /// <param name="id">Id标识</param>
        ShareOrder GetSingle(long id);

        /// <summary>
        ///     获取s the single native.
        /// </summary>
        /// <param name="id">Id标识</param>
        ShareOrder GetSingleNative(long id);

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        PagedList<ShareOrder> GetPageList(object query);

        /// <summary>
        ///     删除订单
        /// </summary>
        /// <param name="id">Id标识</param>
        Tuple<ServiceResult, ShareOrder> Delete(long id);

        /// <summary>
        ///     添加订单
        /// </summary>
        /// <param name="shareOrder">The share order.</param>
        ServiceResult AddSingle(ShareOrder shareOrder);

        /// <summary>
        /// 测试模型View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ShareOrder GetTestView(object id);

        /// <summary>
        /// 更新或添加视图
        /// </summary>
        /// <param name="shareOrder"></param>
        /// <returns></returns>
        ServiceResult AddOrUpdateTest(ShareOrder shareOrder);

        /// <summary>
        ///     Adds the task message.
        ///     添加分润订单执行结果
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="taskMessage">The task message.</param>
        void AddTaskMessage(long orderId, TaskMessage taskMessage);
    }
}