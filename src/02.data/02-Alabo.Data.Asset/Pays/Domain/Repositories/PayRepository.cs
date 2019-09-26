using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Linq.Dynamic;
using System;
using System.Collections.Generic;
using Convert = System.Convert;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    /// <summary>
    ///     Class PayRepository.
    /// </summary>
    public class PayRepository : RepositoryEfCore<Pay, long>, IPayRepository {

        public PayRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     根据实体Id列表获取订单金额
        /// </summary>
        /// <param name="entityIdList">The entity identifier list.</param>
        public IEnumerable<PayShopOrderInfo> GetOrderPayAccount(List<object> entityIdList) {
            var result = new List<PayShopOrderInfo>();
            // 读取未付支付订单的金额
            var sql =
                $"select paymentAmount,AccountPay from Shop_Order  where OrderStatus=1 and id  in  ({entityIdList.ToSqlString()})";
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    var payShopInfoItem = new PayShopOrderInfo {
                        PaymentAmount = reader["PaymentAmount"].ToDecimal(),
                        AccountPay = reader["AccountPay"].ToStr()
                    };
                    payShopInfoItem.AccountPayPair =
                        payShopInfoItem.AccountPay.DeserializeJson<List<KeyValuePair<Guid, decimal>>>();
                    result.Add(payShopInfoItem);
                }
            }

            return result;
        }

        /// <summary>
        ///     支付完成后更新订单状态
        ///     更新支付订单状态
        /// </summary>
        /// <param name="entityIdList">The entity identifier list.</param>
        /// <param name="pay">The pay.</param>
        /// <param name="isPaySucess">是否支出成功</param>
        public ServiceResult AfterPay(List<object> entityIdList, Pay pay, bool isPaySucess) {
            var sqlList = new List<string>();
            var sql = string.Empty;
            var result = ServiceResult.Success;

            //扣除解冻后的虚拟资产(支付成功后，冻结资产减少）
            foreach (var payPair in pay.AccountPayPair) {
                if (payPair.Value > 0) {
                    var accountSql =
                        $"select FreezeAmount from Asset_Account where MoneyTypeId='{payPair.Key}' and UserId={pay.UserId}";
                    var freezeAmount = RepositoryContext.ExecuteScalar(accountSql).ToDecimal();
                    if (freezeAmount < payPair.Value) {
                        return ServiceResult.FailedWithMessage("冻结账户余额不足");
                    }

                    // 扣除冻结资产
                    sql =
                        $"update Asset_Account set FreezeAmount=FreezeAmount-{payPair.Value} where UserId={pay.UserId} and MoneyTypeId='{payPair.Key}'";
                    sqlList.Add(sql);
                    // 财务记录
                    var intro =
                        $"支付订单(编号{pay.PayExtension.TradeNo})后，减少冻结资产,减少金额{payPair.Value},冻结金额账后{freezeAmount - payPair.Value}";
                    sql =
                        $@"INSERT INTO [dbo].[Asset_Bill]([UserId] ,[OtherUserId] ,[Type]  ,[Flow] ,[MoneyTypeId],[Amount] ,[AfterAmount],[Intro] ,[CreateTime] ,[EntityId])
                                 VALUES
                                 ({pay.UserId},0,{Convert.ToInt16(BillActionType.Shopping)},{
                                Convert.ToInt16(AccountFlow.Spending)
                            },'{payPair.Key}',{-payPair.Value},{freezeAmount - payPair.Value},'{intro}',GETDATE(),{
                                pay.Id
                            })";
                    sqlList.Add(sql);
                }
            }

            //支付成功
            if (isPaySucess) {
                // 更新支付账单状态
                sql =
                    $"UPDATE [dbo].[Asset_Pay] SET [Message] = '{pay.Message}',[PayType]={Convert.ToInt16(pay.PayType)}  ,[ResponseSerial] ='{pay.ResponseSerial}' ,[Status] =2 ,[ResponseTime] = '{pay.ResponseTime}' where id={pay.Id} and Status=1";
                sqlList.Add(sql);
                // 插入分润订单
                if (pay.Type == CheckoutType.Order) {
                    //更新支付状态
                    var orderStatus = 2; // 代发货
                    if (pay.PayExtension.IsGroupBuy) {
                        orderStatus = 10; // 如果是团购商品，状态改成待分享
                    }

                    sql =
                        $"update  Shop_Order set OrderStatus={orderStatus},PayId='{pay.Id}'  where OrderStatus=1 and id  in  ({entityIdList.ToSqlString()})";
                    sqlList.Add(sql);

                    foreach (var item in entityIdList) {
                        // 如果是管理员代付
                        var orderUserId = pay.UserId;
                        if (pay.PayExtension?.OrderUser?.Id >= 0) {
                            // 订单Id使用实际订单Id
                            orderUserId = pay.PayExtension.OrderUser.Id;
                        }
                        if (pay.PayType == PayType.AdminPay) {
                            var order = EntityDynamicService.GetSingleOrder((long)item);
                            orderUserId = order.UserId;
                        }

                        // 通过支付记录，修改分入订单的触发类型
                        var triggerType = TriggerType.Order;
                        if (Convert.ToInt16(pay.PayExtension.TriggerType) > 0) {
                            triggerType = pay.PayExtension.TriggerType;
                        }

                        //TODO 2019年9月22日  订单完成后分润 重构
                        //var shareOrderConfig = Ioc.Resolve<IAutoConfigService>().GetValue<ShopOrderShareConfig>();
                        //int pending = (int)ShareOrderSystemStatus.Pending;
                        //if (shareOrderConfig.OrderSuccess) {
                        //    pending = (int)ShareOrderSystemStatus.OrderPendingSucess;
                        //}

                        // 分润订单, UserId 有等于 OrderId的可能, 用 EntityId = {trade.Id}) AND TriggerType != 1 联合进行限制
                        sql = $" IF NOT EXISTS(SELECT * FROM Task_ShareOrder WHERE EntityId = {(long)item} AND TriggerType != 1) " +
                            "INSERT INTO [dbo].[Task_ShareOrder]([UserId] ,[Amount] ,[EntityId],[Parameters] ,[Status],[SystemStatus] , [TriggerType] ,[Summary] ,[CreateTime] ,[UpdateTime],[Extension],[ExecuteCount]) " +
                            $"VALUES ({orderUserId} ,{pay.Amount},{(long)item},'{string.Empty}' ,{(int)ShareOrderStatus.Pending} ,{(int)ShareOrderSystemStatus.Pending} ,{(int)triggerType} ,'{string.Empty}' ,'{DateTime.Now}' ,'{DateTime.Now}','',0)";
                        sqlList.Add(sql);

                        // 订单操作记录
                        sql =
                            "INSERT INTO [dbo].[Shop_OrderAction] ([OrderId] ,[ActionUserId] ,[Intro]  ,[Extensions]  ,[CreateTime],[OrderActionType])" +
                            $"VALUES({(long)item},{pay.UserId},'会员支付订单，支付方式为{pay.PayType.GetDisplayName()},支付现金金额为{pay.Amount}','','{DateTime.Now}',102)";
                        sqlList.Add(sql);

                        #region 如果是拼团购买

                        //更新活动记录状态
                        if (pay.PayExtension.IsGroupBuy) {
                            sql = $"update Shop_ActivityRecord set Status=2 where OrderId={item} and Status=1";
                            sqlList.Add(sql);

                            sql =
                                $"  select count(id) from Shop_ActivityRecord where ParentId = (select ParentId from Shop_ActivityRecord where OrderId = {item}) and ParentId> 0";
                            var gourpBuyCount = RepositoryContext.ExecuteScalar(sql).ConvertToLong();
                            if (gourpBuyCount > 0 && gourpBuyCount + 1 == pay.PayExtension.BuyerCount &&
                                pay.PayExtension.BuyerCount > 0) {
                                // 拼团结束(修改订单状态)
                                var parentId = RepositoryContext
                                    .ExecuteScalar($"select ParentId from Shop_ActivityRecord where OrderId={item}")
                                    .ConvertToLong();

                                //将订单状态从待分享修改成待发货
                                sql =
                                    $"update Shop_Order set OrderStatus=2 where Id in( select OrderId from Shop_ActivityRecord where (ParentId={parentId} or Id={parentId}) and Status=2) and OrderStatus=10";
                                sqlList.Add(sql);
                                //修改活动记录支付状态，为成功
                                sql =
                                    $"update  Shop_ActivityRecord set Status=5 where Id in( select Id from Shop_ActivityRecord where (ParentId={parentId} or Id={parentId}) and Status=2)";
                                sqlList.Add(sql);
                            }
                        }

                        #endregion 如果是拼团购买
                    }
                }

                #region 支付时附加的Sql后操作

                var excecuteSqlList = pay.PayExtension?.ExcecuteSqlList;
                if (excecuteSqlList != null) {
                    // 动态调用执行
                    var resolveResult = DynamicService.ResolveMethod(excecuteSqlList.ServiceName, excecuteSqlList.Method, entityIdList);
                    if (resolveResult.Item1.Succeeded) {
                        var afterSqlList = (IList<string>)resolveResult.Item2;
                        if (afterSqlList.Count > 0) {
                            sqlList.AddRange(afterSqlList);
                        }
                    }
                }

                #endregion 支付时附加的Sql后操作
            }

            //支付失败
            else {
                if (pay.Type == CheckoutType.Order) {
                    foreach (var payPair in pay.AccountPayPair) {
                        if (payPair.Value > 0) {
                            // 减少冻结资产
                            var accountSql =
                                $"select Amount from Asset_Account where MoneyTypeId='{payPair.Key}' and UserId={pay.UserId}";
                            var amount = RepositoryContext.ExecuteScalar(accountSql).ToDecimal();
                            // 扣除冻结资产
                            sql =
                                $"update Asset_Account set amount=amount+{payPair.Value} where UserId={pay.UserId} and MoneyTypeId='{payPair.Key}'";
                            sqlList.Add(sql);
                            // 财务记录
                            var intro =
                                $"支付订单(编号{pay.PayExtension.TradeNo})失败后，解冻金额，金额{payPair.Value},解冻账后{amount + payPair.Value}";
                            sql =
                                $@"INSERT INTO [dbo].[Asset_Bill]([UserId] ,[OtherUserId] ,[Type]  ,[Flow] ,[MoneyTypeId],[Amount] ,[AfterAmount],[Intro] ,[CreateTime] ,[EntityId])
                                 VALUES
                                 ({pay.UserId},0,{Convert.ToInt16(BillActionType.Shopping)},{
                                        Convert.ToInt16(AccountFlow.Income)
                                    },'{payPair.Key}',{payPair.Value},{amount + payPair.Value},'{intro}',GETDATE(),{
                                        pay.Id
                                    })";
                            sqlList.Add(sql);
                        }
                    }
                }
                //更新数据库
            }

            var count = RepositoryContext.ExecuteSqlList(sqlList);
            if (count <= 0) {
                //      Ioc. Resolve<IPayService>().Log("订单支付后，数据库相关处理失败", LogsLevel.Error);
                return ServiceResult.FailedWithMessage("订单支付后，数据库相关处理失败");
            }

            // 支付成功后处理
            var afterSuccess = pay.PayExtension?.AfterSuccess;
            if (afterSuccess != null) {
                // 动态调用执行
                DynamicService.ResolveMethod(afterSuccess.ServiceName, afterSuccess.Method, entityIdList);
            }

            return result;
        }
    }
}