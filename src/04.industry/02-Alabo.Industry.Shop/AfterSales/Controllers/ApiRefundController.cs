using System;
using System.Threading.Tasks;
using Alabo.App.Asset.Pays.Domain.Entities.Extension;
using Alabo.App.Asset.Pays.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using Alabo.Industry.Shop.AfterSales.Domain.Enums;
using Alabo.Industry.Shop.AfterSales.Domain.Services;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Enums;
using Alabo.Industry.Shop.OrderActions.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.AfterSales.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Refund/[action]")]
    public class ApiRefundController : ApiBaseController<Refund, ObjectId>
    {
        /// <summary>
        ///     查看 退货,退款
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Refund> Get([FromQuery] long orderId)
        {
            var order = Resolve<IOrderService>().GetSingle(orderId);
            //未发货
            if (order == null) return ApiResult.Failure<Refund>("订单不存在");

            //var res = Resolve<IRefundService>().GetById(order.OrderExtension.RefundInfo.Id);
            //订单更新成功且退款信息插入成功
            return ApiResult.Success(order.OrderExtension.RefundInfo);
        }

        /// <summary>
        ///     申请退货,退款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Apply([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            //未发货
            if (order.OrderStatus == OrderStatus.WaitingSellerSendGoods)
                order.OrderStatus = OrderStatus.Refund;
            else if (order.OrderStatus == OrderStatus.WaitingReceiptProduct)
                order.OrderStatus = OrderStatus.AfterSale;
            else
                return await Task.FromResult(ApiResult.Failure("订单状态已更改!"));
            parameter.Process = RefundStatus.BuyerApplyRefund;
            var res = Resolve<IRefundService>().AddOrUpdate(parameter);
            order.OrderExtension.RefundInfo = parameter;

            order.Extension = order.OrderExtension.ToJsons();

            var update = Resolve<IOrderService>().Update(order);

            //订单更新成功且退款信息插入成功
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("申请失败!");
            //插入订单操作记录
            var orderAction = new OrderAction
            {
                Intro = $"会员申请{parameter.Reason},原因为:{parameter.Description}",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);
            return await Task.FromResult(result);
        }

        /// <summary>
        ///     商家处理 退款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Process([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);
            pay.PayExtension = pay.Extensions.ToObject<PayExtension>();
            //先把微信金额原路退回

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity == null) return await Task.FromResult(ApiResult.Failure("退款状态以异常!"));

            if (pay.Amount < parameter.Amount) //退款金额不能大于支付金额
                return await Task.FromResult(ApiResult.Failure("退款金额不能大于订单金额!"));
            //退款金额等于 手动退款的金额
            entity.Amount = parameter.Amount;
            //再更新订单状态和退货状态
            var refundFree = Resolve<IPayService>().Refund(ref pay, Convert.ToInt32(parameter.Amount * 100),
                order.OrderExtension?.RefundInfo?.Id.ToString());

            //请求结果
            if (!refundFree.Item1.Succeeded)
                return ApiResult.Failure(
                    $"退款失败!refundFree.Item1:ErrorMessages=>{refundFree.Item1.ErrorMessages.Join()}/ReturnMessage=>{refundFree.Item1.ReturnMessage},refundFree.Item2:{refundFree.Item2}");
            //未发货
            if (order.OrderStatus == OrderStatus.AfterSale)
            {
                if (order.OrderExtension.RefundInfo.Process == RefundStatus.WaitSaleAllow)
                    entity.Process = RefundStatus.Sucess;
                else
                    return await Task.FromResult(ApiResult.Failure("售后状态已被更改,操作失败!"));
            }
            else if (order.OrderStatus == OrderStatus.Refund)
            {
                //退款可以在 同意之后或者 直接退款,退款后直接完成
                if (order.OrderExtension.RefundInfo.Process == RefundStatus.BuyerApplyRefund)
                    entity.Process = RefundStatus.Sucess;
                else
                    return await Task.FromResult(ApiResult.Failure("售后状态已被更改,操作失败!"));
            }
            else
            {
                return await Task.FromResult(ApiResult.Failure("订单状态已更改,操作失败!"));
            }

            //退款完成后将更改订单状态
            order.OrderStatus = OrderStatus.Refunded;

            entity.ProcessMessage = parameter.ProcessMessage;

            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("退款失败!");

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     商家处理 同意退
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AllowProcess([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null)
            {
                if (entity.Process == RefundStatus.BuyerApplyRefund)
                    entity.Process = RefundStatus.WaitSaleAllow;
                else
                    return await Task.FromResult(ApiResult.Failure("售后状态已被更改,操作失败!"));
                entity.Address = parameter.Address;
            }

            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("操作失败!");
            //插入订单操作记录
            var orderAction = new OrderAction
            {
                Intro = "系统后台同意退款申请，已处理",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     商家处理 拒绝退
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RefuseProcess([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null)
            {
                if (entity.Process == RefundStatus.BuyerApplyRefund)
                    entity.Process = RefundStatus.WaitSaleRefuse;
                else
                    return await Task.FromResult(ApiResult.Failure("售后状态已更改!"));
            }

            //未发货
            if (order.OrderStatus == OrderStatus.AfterSale)
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
            else if (order.OrderStatus == OrderStatus.Refund)
                order.OrderStatus = OrderStatus.WaitingSellerSendGoods;
            else
                return await Task.FromResult(ApiResult.Failure("订单状态已更改!"));
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("操作失败!");

            //插入订单操作记录
            var orderAction = new OrderAction
            {
                Intro = "系统后台拒绝退款申请，已处理",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     用户取消申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RefuseCancel([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null)
            {
                if (entity.Process == RefundStatus.BuyerApplyRefund)
                    entity.Process = RefundStatus.Closed; //用户取消
                else
                    return await Task.FromResult(ApiResult.Failure("售后状态已更改!"));
            }

            //未发货
            if (order.OrderStatus == OrderStatus.AfterSale)
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
            else if (order.OrderStatus == OrderStatus.Refund)
                order.OrderStatus = OrderStatus.WaitingSellerSendGoods;
            else
                return await Task.FromResult(ApiResult.Failure("订单状态已更改!"));
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("操作失败!");
            //插入订单操作记录
            var orderAction = new OrderAction
            {
                Intro = "用户取消退款申请",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await Task.FromResult(result);
        }
    }
}