using System;
using System.Collections.Generic;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Entities.Extensions;
using Alabo.Data.Things.Orders.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.Data.Things.Orders.Domain.Services {

    /// <summary>
    ///     Class ShareOrderService.
    /// </summary>
    public class ShareOrderService : ServiceBase<ShareOrder, long>, IShareOrderService {

        /// <summary>
        ///     The share order repository
        /// </summary>
        private readonly IShareOrderRepository _shareOrderRepository;

        public ShareOrderService(IUnitOfWork unitOfWork, IRepository<ShareOrder, long> repository) : base(unitOfWork,
            repository) {
            _shareOrderRepository = Repository<IShareOrderRepository>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShareOrderService" /> class.
        /// </summary>
        /// <summary>
        ///     获取s the un handled identifier list.
        /// </summary>
        public IList<long> GetUnHandledIdList() {
            return _shareOrderRepository.GetUnHandledIdList();
        }

        /// <summary>
        ///     添加订单
        /// </summary>
        /// <param name="shareOrder">The share order.</param>
        public ServiceResult AddSingle(ShareOrder shareOrder) {
            if (shareOrder.UserName.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("用户名不能为空");
            }

            var user = Resolve<IUserService>().GetSingle(shareOrder.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常");
            }

            shareOrder.UserId = user.Id;
            if (Add(shareOrder)) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("添加失败");
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id">Id标识</param>
        public Tuple<ServiceResult, ShareOrder> Delete(long id) {
            var result = ServiceResult.Success;
            var model = Resolve<IShareOrderService>().GetSingle(u => u.Id == id);
            if (model == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("删除失败"), new ShareOrder());
            }

            var context = Repository<IShareOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            Resolve<IShareOrderService>().Delete(u => u.Id == id);
            context.CommitTransaction();
            return Tuple.Create(result, model);
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ShareOrder> GetPageList(object query) {
            var list = Resolve<IShareOrderService>().GetPagedList<ShareOrder>(query);
            return list;
        }

        /// <summary>
        ///     Adds the task message.
        ///     添加分润订单执行结果
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="taskMessage">The message.</param>
        public void AddTaskMessage(long orderId, TaskMessage taskMessage) {
            var shareOrder = GetSingle(orderId);
            if (shareOrder != null) {
                shareOrder.ShareOrderExtension.TaskMessage.Add(taskMessage);
                shareOrder.Extension = shareOrder.ShareOrderExtension.ToJson();
                Update(shareOrder); // 更新数据库
            }
        }

        /// <summary>
        ///     获取单条记录
        /// </summary>
        /// <param name="id">Id标识</param>
        public ShareOrder GetSingle(long id) {
            var shareOrder = GetSingle(r => r.Id == id);
            if (shareOrder != null) {
                shareOrder.ShareOrderExtension = shareOrder.Extension.DeserializeJson<ShareOrderExtension>();
                if (shareOrder.ShareOrderExtension == null) {
                    shareOrder.ShareOrderExtension = new ShareOrderExtension();
                }
            }

            return shareOrder;
        }

        /// <summary>
        ///     获取s the single native.
        /// </summary>
        /// <param name="id">Id标识</param>
        public ShareOrder GetSingleNative(long id) {
            var shareOrder = _shareOrderRepository.GetSingleNative(id);
            return shareOrder;
        }

        public ShareOrder GetTestView(object id) {
            var view = Resolve<IShareOrderService>().GetSingle(id);
            if (view == null) {
                view = new ShareOrder();
            }

            return view;
        }

        /// <summary>
        ///  添加订单
        /// </summary>
        /// <param name="shareOrder"></param>
        /// <returns></returns>
        public ServiceResult AddOrUpdateTest(ShareOrder shareOrder) {
            var result = Resolve<IShareOrderService>().AddSingle(shareOrder);
            return result;
        }
    }
}