using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Domains.Query.Dto;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Mapping;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Shop.Order.ViewModels.OrderEdit;
using Alabo.Core.Extensions;
using Alabo.Helpers;
using Alabo.App.Shop.Order.Domain.Entities.Extensions;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Shop.AfterSale.Domain.Services;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;

namespace Alabo.App.Shop.Order.Controllers {

    [ApiExceptionFilter]
    [Route("Api/OrderAdmin/[action]")]
    public class ApiOrderAdminController : ApiBaseController<Domain.Entities.Order, long> {

        /// <summary>
        /// ctor
        /// </summary>
        public ApiOrderAdminController() : base() {
            BaseService = Resolve<IOrderService>();
        }

        // 留言，退换货，退款 ，查看物流，确认收货，完成，评价，支付，
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "订单详情")]
        [ApiAuth]
        public ApiResult<OrderShowOutput> Show(long id, long loginUserId) {
            var orderShow = Resolve<IOrderService>().GetSingleAdmin(id, loginUserId);
            if (orderShow == null) {
                return ApiResult.Failure<OrderShowOutput>("订单不存在");
            }

            return ApiResult.Success(orderShow);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "订单详情")]
        [ApiAuth]
        public ApiResult<OrderShowOutput> SupplierShow(long id, long loginUserId) {
            var store = Resolve<IStoreService>().GetSingle(u => u.UserId == loginUserId);
            var order = Resolve<IOrderService>().GetSingle(u => u.Id == id);
            if (order?.StoreId != store?.Id) {
                return ApiResult.Failure<OrderShowOutput>("你无权查看该订单");
            }

            var orderShow = Resolve<IOrderService>().GetSingleAdmin(id, loginUserId);

            if (orderShow == null) {
                return ApiResult.Failure<OrderShowOutput>("订单不存在");
            }

            return ApiResult.Success(orderShow);
        }

        /// <summary>
        /// 管理员代付
        /// </summary>
        /// <param name="orderEditPay"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "管理员代付")]
        [ApiAuth]
        public ApiResult Pay([FromBody] OrderEditPay orderEditPay) {
            var user = Resolve<IUserService>().GetUserDetail(orderEditPay.UserId);
            if (user == null) {
                return ApiResult.Failure("用户已经不存在");
            }

            if (user.Detail.PayPassword != orderEditPay.PayPassword.ToMd5HashString()) {
                return ApiResult.Failure("支付密码不正确");
            }

            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderEditPay.OrderId);
            if (order == null) {
                return ApiResult.Failure("订单不存在");
            }

            if (order.OrderStatus != OrderStatus.WaitingBuyerPay) {
                return ApiResult.Failure("订单已付款或关闭，请刷新");
            }

            IList<Domain.Entities.Order> orderList = new List<Domain.Entities.Order>
            {
                order
            };

            var reduceMoneys = new List<OrderMoneyItem>();
            order.OrderExtension.ReduceAmounts.Foreach(r => {
                reduceMoneys.Add(new OrderMoneyItem {
                    MoneyId = r.MoneyTypeId,
                    MaxPayPrice = r.ForCashAmount
                });
            });
            var singlePayInput = new SinglePayInput {
                Orders = orderList,
                User = user,
                ReduceMoneys = reduceMoneys,
                IsAdminPay = true
            };
            var payResult = Resolve<IOrderAdminService>().AddSinglePay(singlePayInput);
            if (!payResult.Item1.Succeeded) {
                return ApiResult.Failure(payResult.Item1.ToString());
            }

            var payInput = AutoMapping.SetValue<PayInput>(payResult.Item2);
            payInput.LoginUserId = user.Id;
            payInput.PayId = payResult.Item2.Id;

            var result = Resolve<IPayService>().Pay(payInput);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure(result.ToString());
            }

            return ApiResult.Success("支付成功!");
        }

        /// <summary>
        ///     店家取消订单
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="storeId"></param>
        [HttpGet]
        [Display(Description = "订单取消")]
        [ApiAuth]
        public ApiResult StoreCancel(long id, long storeId) {
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id && e.StoreId == storeId && e.OrderStatus == OrderStatus.WaitingBuyerPay);
            if (order == null) {
                return ApiResult.Failure("订单不存在");
            }

            order.OrderStatus = OrderStatus.Closed;
            Resolve<IOrderService>().Update(order);
            return ApiResult.Success("订单取消成功");
        }

        /// <summary>
        ///     管理员取消订单
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="userId"></param>
        [HttpGet]
        [Display(Description = "订单取消")]
        [ApiAuth]
        public ApiResult AdminCancel(long id, long userId) {
            var isAdmin = Resolve<IUserService>().IsAdmin(userId);
            if (!isAdmin) {
                return ApiResult.Failure("权限不足");
            }

            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id && e.OrderStatus == OrderStatus.WaitingBuyerPay);
            if (order == null) {
                return ApiResult.Failure("订单不存在");
            }

            order.OrderStatus = OrderStatus.Closed;
            Resolve<IOrderService>().Update(order);
            return ApiResult.Success("订单取消成功");
        }

        /// <summary>
        ///     发货
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Display(Description = "发货")]
        [ApiAuth]
        public ApiResult Delivery([FromBody] OrderEditDelivery model) {
            var rs = Ioc.Resolve<IOrderActionService>().Delivery(model);
            if (rs.Succeeded) {
                return ApiResult.Success("发货成功");
            } else {
                return ApiResult.Failure(rs.ReturnMessage);
            }
        }

        /// <summary>
        ///     备注 管理员备忘
        /// </summary>
        /// <param name="modelIn"></param>
        [HttpPost]
        [Display(Description = "备注")]
        [ApiAuth]
        public ApiResult Remark([FromBody] OrderRemark modelIn) {
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == modelIn.OrderId);

            if (order == null) {
                return ApiResult.Failure("订单不存在");
            }

            if (order.OrderExtension.OrderRemark == null) {
                order.OrderExtension.OrderRemark = new OrderRemark();
            }

            order.OrderExtension.OrderRemark.PlatplatformRemark = modelIn.PlatplatformRemark;
            order.Extension = order.OrderExtension.ToJson();
            var result = Resolve<IOrderService>().Update(order);
            if (result) {
                return ApiResult.Success("备注成功!");
            }

            return ApiResult.Failure("备注失败!");
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "删除订单")]
        [ApiAuth]
        public ApiResult Delete([FromBody] OrderActionViewIn modelIn) {
            var rs = Ioc.Resolve<IOrderActionService>().Delete(modelIn);
            return ToResult(rs);
        }

        /// <summary>
        /// 取消订单 (2019.03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "取消订单")]
        [ApiAuth]
        public ApiResult Cancel([FromBody] OrderActionViewIn modelIn) {
            var rs = Ioc.Resolve<IOrderActionService>().Cancel(modelIn);
            return ToResult(rs);
        }

        /// <summary>
        /// 卖家评价
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "卖家评价")]
        [ApiAuth]
        public ApiResult Rate([FromBody] OrderRateInfo modelIn) {
            var rs = Ioc.Resolve<IOrderActionService>().Rate(modelIn);
            return ToResult(rs);
        }

        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "修改地址")]
        [ApiAuth]
        public ApiResult Address([FromBody] AddressEditViewIn modelIn) {
            var rs = Ioc.Resolve<IOrderActionService>().Address(modelIn, modelIn.OrderId);
            return ToResult(rs);
        }

        /// <summary>
        ///     虚拟的订单
        ///     只供管理员使用,请勿滥用
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "管理员查看虚拟订单")]
        [ApiAuth]
        public ApiResult<PagedList<OrderListOutput>> VirtualOrderList([FromQuery] OrderListInput parameter) {
            parameter.UserId = parameter.LoginUserId;
            parameter.OrderType = OrderType.VirtualOrder;
            var orderPageList = Resolve<IOrderService>().GetPageList(parameter);
            return ApiResult.Success(orderPageList);
        }

        /// <summary>
        ///     临时用于管理端查看退款原因 后续写入操作记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuth]
        public ApiResult GetRefundInfo(long orderId) {
            var order = Resolve<IOrderService>().GetSingle(u => u.Id == orderId);
            if (order == null) {
                return ApiResult.Failure("不存在的订单");
            }

            //if (order.OrderStatus != OrderStatus.AfterSale || order.OrderStatus != OrderStatus.Refund) {
            //    return ApiResult.Failure("该订单不是退款订单");
            //}

            var refund = Resolve<IRefundService>().GetSingle(u => u.OrderId == orderId);
            if (refund == null) {
                return ApiResult.Failure("不存在的退款订单");
            }
            return ApiResult.Success(refund);
        }
    }
}