using System;
using System.Collections.Generic;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.Orders.Dtos;

namespace Alabo.Industry.Shop.OrderActions.Domain.Repositories {

    internal class OrderActionRepository : RepositoryEfCore<OrderAction, long>, IOrderActionRepository {

        /// <summary>
        ///     Deletes the cart buy order.
        ///     订单生成成功以后，删除购物车数据
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DeleteCartBuyOrder() {
            throw new NotImplementedException();
        }

        public List<OrderToExcel> GetOrderListToExcel() {
            var sql = $@"select
orde.Id '订单号',
users.[Name] '用户名',
users.Mobile '电话',
shopProduct.Name '品名', --品名
sku.PropertyValueDesc '规格',
sku.DisplayPrice '单价',
orde.TotalCount '购买数量',--数量
orde.TotalAmount '总金额',--金额
pay.Amount '支付金额',--支付金额
orde.CreateTime '下单时间',--创建时间
pay.ResponseTime '支付时间',--支付时间

CASE pay.PayType
WHEN 1 then '余额支付'
WHEN 3 then '支付宝手机支付'
WHEN 7 then '微信支付'
WHEN 12 then '微信小程序支付'
WHEN 99 then '管理员代付'
WHEN 109 then '线下转账给卖家'
else cast(pay.PayType AS  varchar(20)) --'未支付'
END AS '支付方式',

CASE pay.Status
WHEN 1 then '等待处理'
WHEN 2 then '处理成功'
WHEN 3 then '处理失败'
else '未知'
END AS '支付状态',
--pay.PayType '支付方式',--支付方式
CASE orde.OrderStatus
WHEN 1 then '待付款'
WHEN 2 then '待发货'
WHEN 3 then '待收货'
WHEN 4 then '待评价'
WHEN 10 then '待分享'
WHEN 50 then '退款/退货'
WHEN 51 then '待退款'
WHEN 100 then '订单完成'
WHEN 200 then '订单关闭'
WHEN 201 then '订单关闭，已退款'
else '未知'
END AS '订单状态'
--订单状态
from [dbo].[Shop_Order] orde
left join [dbo].[Asset_Pay] pay on orde.PayId=pay.Id
LEFT JOIN [dbo].[Shop_OrderProduct] ordeProduct on ordeProduct.OrderId=orde.Id
left join [dbo].[Shop_Product] shopProduct on shopProduct.Id=ordeProduct.ProductId
LEFT JOIN [dbo].[Shop_Store] shop on shop.Id=orde.StoreId
LEFT JOIN [dbo].[User_User] users on users.Id=orde.UserId
LEFT JOIN  [dbo].[Shop_ProductSku] sku on sku.ProductId=shopProduct.Id
where orde.OrderType=1 --过滤购买会员等级的商品
and pay.PayType<>1  AND pay.PayType<>0 --去掉余额付款
and orde.OrderStatus not in(201,50,51)-- 关闭已退款，申请退款，退货退款";

            List<OrderToExcel> result = new List<OrderToExcel>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    OrderToExcel userProductCount = new OrderToExcel {
                        OrderId = reader["订单号"].ToString(),
                        UserName = reader["用户名"].ToString(),
                        Mobile = reader["电话"].ToString(),
                        ProductName = reader["品名"].ToString(),
                        ProductSku = reader["规格"].ToString(),
                        PayPrice = reader["支付金额"].ToString(),
                        Count = reader["购买数量"].ToString(),
                        TotalPrice = reader["总金额"].ToString(),
                        CreateTime = reader["下单时间"].ToString(),
                        PayTime = reader["支付时间"].ToString(),
                        Price = reader["单价"].ToString(),
                        PayType = reader["支付方式"].ToString(),
                        PayStatus = reader["支付状态"].ToString(),
                        OrderStatus = reader["订单状态"].ToString(),
                    };
                    result.Add(userProductCount);
                }
            }
            return result;
        }

        public OrderActionRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}