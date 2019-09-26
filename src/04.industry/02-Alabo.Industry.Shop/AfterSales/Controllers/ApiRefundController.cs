using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using System.Threading.Tasks;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Shop.AfterSale.Domain.Entities;
using Alabo.App.Shop.AfterSale.Domain.Services;
using Alabo.App.Shop.Order.Domain.Services;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using Alabo.App.Core.Finance.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Extensions;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Domains.Enums;

namespace Alabo.App.Shop.AfterSale.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Refund/[action]")]
    public class ApiRefundController : ApiBaseController<Refund, ObjectId> {

        public ApiRefundController() : base() {
        }

        /// <summary>
        /// 查看 退货,退款
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Refund> Get([FromQuery]long orderId) {
            var order = Resolve<IOrderService>().GetSingle(orderId);
            //未发货
            if (order == null) {
                return ApiResult.Failure<Refund>("订单不存在");
            }

            //var res = Resolve<IRefundService>().GetById(order.OrderExtension.RefundInfo.Id);
            //订单更新成功且退款信息插入成功
            return ApiResult.Success<Refund>(order.OrderExtension.RefundInfo);
        }

        /// <summary>
        /// 申请退货,退款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ZKCloud.Open.ApiBase.Models.ApiResult> Apply([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            //未发货
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.WaitingSellerSendGoods) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.Refund;
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.WaitingReceiptProduct) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.AfterSale;
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("订单状态已更改!"));
            }
            parameter.Process = Domain.Enums.RefundStatus.BuyerApplyRefund;
            var res = Resolve<IRefundService>().AddOrUpdate(parameter);
            order.OrderExtension.RefundInfo = parameter;

            order.Extension = order.OrderExtension.ToJsons();

            var update = Resolve<IOrderService>().Update(order);

            //订单更新成功且退款信息插入成功
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("申请失败!");
            //插入订单操作记录
            var orderAction = new OrderAction {
                Intro = $"会员申请{parameter.Reason},原因为:{parameter.Description}",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);
            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// 商家处理 退款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Process([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);
            pay.PayExtension = pay.Extensions.ToObject<PayExtension>();
            //先把微信金额原路退回

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity == null) {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("退款状态以异常!"));
            }

            if (pay.Amount < parameter.Amount) {
                //退款金额不能大于支付金额
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("退款金额不能大于订单金额!"));
            }
            //退款金额等于 手动退款的金额
            entity.Amount = parameter.Amount;
            //再更新订单状态和退货状态
            var refundFree = Resolve<IPayService>().Refund(ref pay, Convert.ToInt32(parameter.Amount * 100), order.OrderExtension?.RefundInfo?.Id.ToString());

            //请求结果
            if (!refundFree.Item1.Succeeded) {
                return ApiResult.Failure($"退款失败!refundFree.Item1:ErrorMessages=>{refundFree.Item1.ErrorMessages.Join()}/ReturnMessage=>{refundFree.Item1.ReturnMessage},refundFree.Item2:{refundFree.Item2}");
            }
            //未发货
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.AfterSale) {
                if (order.OrderExtension.RefundInfo.Process == Domain.Enums.RefundStatus.WaitSaleAllow) {
                    entity.Process = Domain.Enums.RefundStatus.Sucess;
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("售后状态已被更改,操作失败!"));
                }
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.Refund) {
                //退款可以在 同意之后或者 直接退款,退款后直接完成
                if (order.OrderExtension.RefundInfo.Process == Domain.Enums.RefundStatus.BuyerApplyRefund) {
                    entity.Process = Domain.Enums.RefundStatus.Sucess;
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("售后状态已被更改,操作失败!"));
                }
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("订单状态已更改,操作失败!"));
            }
            //退款完成后将更改订单状态
            order.OrderStatus = Order.Domain.Enums.OrderStatus.Refunded;

            entity.ProcessMessage = parameter.ProcessMessage;

            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("退款失败!");

            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// 商家处理 同意退
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ZKCloud.Open.ApiBase.Models.ApiResult> AllowProcess([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null) {
                if (entity.Process == Domain.Enums.RefundStatus.BuyerApplyRefund) {
                    entity.Process = Domain.Enums.RefundStatus.WaitSaleAllow;
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("售后状态已被更改,操作失败!"));
                }
                entity.Address = parameter.Address;
            }
            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("操作失败!");
            //插入订单操作记录
            var orderAction = new OrderAction {
                Intro = $"系统后台同意退款申请，已处理",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// 商家处理 拒绝退
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RefuseProcess([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null) {
                if (entity.Process == Domain.Enums.RefundStatus.BuyerApplyRefund) {
                    entity.Process = Domain.Enums.RefundStatus.WaitSaleRefuse;
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("售后状态已更改!"));
                }
            }
            //未发货
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.AfterSale) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingReceiptProduct;
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.Refund) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingSellerSendGoods;
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("订单状态已更改!"));
            }
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("操作失败!");

            //插入订单操作记录
            var orderAction = new OrderAction {
                Intro = $"系统后台拒绝退款申请，已处理",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// 用户取消申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RefuseCancel([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null) {
                if (entity.Process == Domain.Enums.RefundStatus.BuyerApplyRefund) {
                    entity.Process = Domain.Enums.RefundStatus.Closed;//用户取消
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("售后状态已更改!"));
                }
            }
            //未发货
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.AfterSale) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingReceiptProduct;
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.Refund) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingSellerSendGoods;
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("订单状态已更改!"));
            }
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("操作失败!");
            //插入订单操作记录
            var orderAction = new OrderAction {
                Intro = $"用户取消退款申请",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}