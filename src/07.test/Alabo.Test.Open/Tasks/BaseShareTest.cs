using Alabo.Framework.Core.Enums.Enum;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Test.Base.Core.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Share.Rewards.Domain.Services;
using Alabo.App.Share.TaskExecutes;
using Alabo.App.Share.TaskExecutes.Domain.Services;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Services;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Users.Entities;
using Xunit;

namespace Alabo.Test.Open.Tasks {

    /// <summary>
    ///     规则：1.配置必须提前配置好
    ///     2. 资产分配为100%人民币
    /// </summary>
    public abstract class BaseShareTest : CoreTest {

        /// <summary>
        ///     测试模块配置Id 注册配置Id
        ///     与后台对应
        /// </summary>
        public long RegModuleConfigId { get; set; }

        /// <summary>
        ///     测试模块商城订单Id
        ///     与后台对应
        /// </summary>
        public long ShopModuleConfigId { get; set; }

        /// <summary>
        ///     配置类型
        /// </summary>
        public Type ModuleType { get; set; }

        public Type ConfigType { get; set; }

        protected ITaskActuator TaskActuator {
            get {
                var taskActuator = Services.GetService<ITaskActuator>();
                return taskActuator;
            }
        }

        /// <summary>
        ///     添加会员注册类型的分润订单
        ///     分润基数为1
        /// </summary>
        /// <param name="user">用户</param>
        protected ShareOrder UserRegShareOrder(User user) {
            var shareOrder = new ShareOrder {
                EntityId = user.Id,
                UserId = user.Id,
                Amount = 1,
                TriggerType = TriggerType.UserReg
            };
            Resolve<IShareOrderService>().Add(shareOrder);
            return shareOrder;
        }

        /// <summary>
        ///     获取商城订单
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="address"></param>
        protected Tuple<ShareOrder, Order> GetShopOrder(User user, UserAddress address = null) {
            // 复制id=465的订单
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == 7);
            if (order == null) {
                throw new InvalidExpressionException("基础订单不存在，请重新复制订单");
            }

            // 添加订单
            var newOrder = AutoMapping.SetValue<Order>(order);
            newOrder.Id = 0;
            newOrder.UserId = user.Id;
            newOrder.OrderExtension.User = user;

            if (address != null) {
                newOrder.AddressId = address.Id.ToString();
                newOrder.OrderExtension.UserAddress = address;
            }

            newOrder.Extension = newOrder.OrderExtension.ToJson();
            Resolve<IOrderService>().Add(newOrder);

            // 添加订单商品
            var orderProductList = Resolve<IOrderProductService>().GetList(r => r.OrderId == order.Id);
            foreach (var item in orderProductList) {
                var orderProduct = AutoMapping.SetValue<OrderProduct>(item);
                orderProduct.Id = 0;
                orderProduct.OrderId = newOrder.Id;
                Resolve<IOrderProductService>().Add(orderProduct);
            }

            var shareOrder = new ShareOrder {
                EntityId = newOrder.Id,
                UserId = user.Id,
                Amount = newOrder.PaymentAmount,
                TriggerType = TriggerType.Order
            };
            Resolve<IShareOrderService>().Add(shareOrder);

            return Tuple.Create(shareOrder, order);
        }

        /// <summary>
        ///     获取分润会员的分润账户
        /// </summary>
        /// <param name="shareUserId"></param>
        protected Account GetShareAccount(long shareUserId) {
            var account = Resolve<IAccountService>().GetAccount(shareUserId, Currency.Cny);
            return account;
        }

        protected List<decimal> Ratios(long moduleConfigId) {
            //var find = Resolve<IRewardService>().GetShareBaseConfig(moduleConfigId);
            //if (find == null) {
            //    return null;
            //}

            var list = new List<decimal>();
            //var stringList = find.DistriRatio.Split(",");
            //foreach (var item in stringList) {
            //    list.Add(item.ConvertToDecimal(0));
            //}

            return list;
        }

        /// <summary>
        ///     断言分润结果
        /// </summary>
        /// <param name="input"></param>
        protected void AssertResult(ShareRewardInput input) {
            //分润金额
            Assert.Equal(input.BeforeAccount.Amount + input.ShareAmount, input.AfterAccount.Amount);

            //历史金额
            Assert.Equal(input.BeforeAccount.HistoryAmount + input.ShareAmount, input.AfterAccount.HistoryAmount);

            // 判断分润参数
            var reward = Resolve<IRewardService>().GetSingle(r =>
                r.OrderId == input.ShareOrder.Id && r.ModuleConfigId == input.ModuleConfigId);
            Assert.NotNull(reward);

            //判断分润金额
            Assert.Equal(input.ShareAmount, reward.Amount);
            //判断账号金额
            // Assert.Equal(input.ShareAmount + input.BeforeAccount.Amount, reward.AfterAmount);
            Assert.Equal(reward.MoneyTypeId, input.BeforeAccount.MoneyTypeId);
            Assert.Equal(reward.MoneyTypeId, input.AfterAccount.MoneyTypeId);
            Assert.Equal(reward.OrderUserId, input.OrderUserId);

            // 判断分润模块设置
            var moduleAttribute = Resolve<ITaskModuleConfigService>().GetModuleAttribute(ModuleType);
            Assert.NotNull(moduleAttribute);
            Assert.Equal(reward.ModuleId, moduleAttribute.Id);

            //// 判断财务记录
            //var bill = Service<IBillService>()(r =>
            //    r.UserId == input.ShareUserId && r.Type == BillActionType.FenRun && r.EntityId == input.ShareOrder.Id);
            //Assert.NotNull(bill);

            ////判断分润金额
            //Assert.Equal(input.ShareAmount, bill.Amount);
            ////判断账号金额

            ////  Assert.Equal(input.ShareAmount + input.BeforeAccount.Amount, bill.AfterAmount);

            //Assert.Equal(bill.MoneyTypeId, input.BeforeAccount.MoneyTypeId);
            //Assert.Equal(bill.MoneyTypeId, input.AfterAccount.MoneyTypeId);

            //Assert.Equal(bill.OtherUserId, input.OrderUserId);
            //Assert.Equal(AccountFlow.Income, bill.Flow);
        }

        protected void AssertReward(ShareOrder shareOrder, long shareUserId, Account beforeAccount,
            Account afterAccount,
            decimal shareAmount, long moduleConfigId, Type type) {
        }
    }

    public class ShareRewardInput {

        public ShareRewardInput() {
        }

        public ShareRewardInput(ShareOrder shareOrder, Account beforeAccount, Account afterAccount, long shareUserId,
            decimal shareAmount, long moduleConfigId, long orderUserId) {
            ShareOrder = shareOrder;
            BeforeAccount = beforeAccount;
            ModuleConfigId = moduleConfigId;
            AfterAccount = afterAccount;
            ShareAmount = shareAmount;
            ShareUserId = shareUserId;
            OrderUserId = orderUserId;
        }

        public ShareOrder ShareOrder { get; set; }

        /// <summary>
        ///     分润之前账号
        /// </summary>
        public Account BeforeAccount { get; set; }

        /// <summary>
        ///     分润之后账号
        /// </summary>
        public Account AfterAccount { get; set; }

        /// <summary>
        ///     分润用户Id
        /// </summary>
        public long ShareUserId { get; set; }

        /// <summary>
        ///     分润金额
        /// </summary>
        public decimal ShareAmount { get; set; }

        /// <summary>
        ///     分润配置Id
        /// </summary>

        public long ModuleConfigId { get; set; }

        public long OrderUserId { get; set; }
    }
}