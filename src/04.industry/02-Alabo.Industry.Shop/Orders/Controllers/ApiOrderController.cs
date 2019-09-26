using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebUis.Domain.Services;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Orders.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Orders.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Order/[action]")]
    public class ApiOrderController : ApiBaseController<Domain.Entities.Order, long> {

        public ApiOrderController() : base() {
            BaseService = Resolve<IOrderService>();
        }

        /// <summary>
        /// 代付款订单支付
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "代付款订单支付")]
        [ApiAuth]
        public ApiResult<BuyOutput> Pay(long id, long loginUserId) {
            if (id <= 0) {
                return ApiResult.Failure<BuyOutput>("Id不正确");
            }
            var result = Resolve<IOrderBuyServcie>().Pay(id, loginUserId);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<BuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }
            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     订单取消
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "订单取消")]
        [ApiAuth]
        public ApiResult Cancel(long id, long loginUserId) {
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id && e.UserId == loginUserId);
            if (order == null) {
                return ApiResult.Failure("订单不存在");
            }
            if (order.OrderStatus != OrderStatus.WaitingBuyerPay) {
                return ApiResult.Failure("该状态不可取消!");
            }
            order.OrderStatus = OrderStatus.Closed;
            Resolve<IOrderService>().Update(order);
            return ApiResult.Success("订单取消成功");
        }

        /// <summary>
        ///     店铺订单详情
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="loginUserId"></param>

        [HttpGet]
        [Display(Description = "订单详情")]
        [ApiAuth]
        public ApiResult<OrderShowOutput> Show(long id, long loginUserId) {
            var orderShow = Resolve<IOrderService>().Show(id);
            if (orderShow == null) {
                return ApiResult.Failure<OrderShowOutput>("订单不存在");
            }

            return ApiResult.Success(orderShow);
        }

        /// <summary>
        ///     用户订单详情
        /// </summary>
        [HttpGet]
        [Display(Description = "订单详情")]
        [ApiAuth]
        public ApiResult GetOrder(long id, long loginUserId) {
            var orderShow = Resolve<IOrderService>().GetOrderSingle(id, loginUserId);
            if (orderShow == null) {
                return ApiResult.Failure("订单不存在");
            }

            return ApiResult.Success(orderShow);
        }

        /// <summary>
        /// 获取快递公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Express() {
            var result = Resolve<IOrderService>().GetExpress();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取快递列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetExpressList(long orderId) {
            var result = Resolve<IOrderService>().GetExpressList(orderId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取物流信息
        /// </summary>
        /// <param name="expressId"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuth]
        public ApiResult GetExpressInfo(string expressId) {
            var result = Resolve<IOrderService>().GetExpressInfo(expressId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 发货 只更改订单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "发货")]
        [ApiAuth]
        public ApiResult Delivery([FromBody]OrderInput model) {
            var result = Resolve<IOrderService>().Deliver(model);
            if (result.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure(result.ReturnMessage);
        }

        /// <summary>
        /// 发货 只支持一个快递
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "发货")]
        [ApiAuth]
        public ApiResult Deliver([FromBody]Deliver model) {
            var result = Resolve<IOrderService>().Deliver(model);
            if (result.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure(result.ReturnMessage);
        }

        /// <summary>
        /// 增加快递
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "增加快递")]
        [ApiAuth]
        public ApiResult AddExpress([FromBody]Deliver model) {
            if (model.ExpressName.IsNullOrEmpty()) {
                return ApiResult.Failure("快递公司不能为空");
            }

            if (model.ExpressNum.IsNullOrEmpty()) {
                return ApiResult.Failure("快递单号不能为空");
            }
            var result = Resolve<IOrderService>().AddExpress(model);
            if (result.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure(result.ReturnMessage);
        }

        /// <summary>
        ///     获取价格
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "获取价格")]
        [ApiAuth]
        public ApiResult<StoreOrderPrice> GetPrice([FromBody] UserOrderInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<StoreOrderPrice>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IOrderBuyServcie>().GetPrice(parameter);
            if (!result.Item1.Succeeded) {
                if (result.Item1.Id.ToInt16() == -1) {
                    return ApiResult.Failure<StoreOrderPrice>(result.Item1.ToString(), MessageCodes.ReremoteRequest);
                }

                return ApiResult.Failure<StoreOrderPrice>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        public StoreOrderPrice GetPriceSync(UserOrderInput parameter) {
            //if (!this.IsFormValid()) {
            //    return ApiResult.Failure<StoreOrderPrice>(this.FormInvalidReason(),
            //        MessageCodes.ParameterValidationFailure);
            //}

            var result = Resolve<IOrderBuyServcie>().GetPrice(parameter);
            //if (!result.Item1.Succeeded) {
            //    if (result.Item1.Id.ToInt16() == -1) {
            //        return ApiResult.Failure<StoreOrderPrice>(result.Item1.ToString(), MessageCodes.ReremoteRequest);
            //    }

            //    return ApiResult.Failure<StoreOrderPrice>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            //}

            return result.Item2;
        }

        /// <summary>
        ///     立即购买,商品够，提交订单时候用
        ///     包括购物车购买
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "立即购买，商品购买，提交订单时候用，包括购物车购买")]
        [ApiAuth]
        public ApiResult<BuyOutput> Buy([FromBody] BuyInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<BuyOutput>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            IList<StoreOrderItem> StoreOrders = new List<StoreOrderItem>();

            var result = Resolve<IOrderBuyServcie>().Buy(parameter);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<BuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     我的订单
        ///     获取订单列表，包括供应商订单，会员订单，后台订单
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "我的订单")]
        [ApiAuth]
        public ApiResult<PagedList<OrderListOutput>> Index([FromQuery] OrderListInput parameter) {
            parameter.OrderType = OrderType.Normal;
            var orderPageList = Resolve<IOrderService>().GetPageList(parameter);
            return ApiResult.Success(orderPageList);
        }

        /// <summary>
        ///     买家的订单
        ///     只供买家使用,请勿滥用
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "买家的订单")]
        [ApiAuth]
        public ApiResult<PagedList<OrderListOutput>> BuyOrderList([FromQuery] OrderListInput parameter) {
            parameter.UserId = parameter.LoginUserId;
            parameter.OrderType = OrderType.Normal;
            var orderPageList = Resolve<IOrderService>().GetPageList(parameter);
            return ApiResult.Success(orderPageList);
        }

        /// <summary>
        ///     商品的SkuId，确认订单页面，获取商品的购买信息
        ///     每次修改价格，通过此方法计算
        ///     在/order/buy 页面中使用
        ///     /ordery
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "商品的SKUid，确认订单页面，获取商品购买信息，每次修改价格通过此方法计算，在/order/buy页面使用")]
        [ApiAuth]
        public ApiResult<StoreProductSku> BuyInfo([FromBody] BuyInfoInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<StoreProductSku>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            parameter.IsBuy = true; // 为购买，生成sign 插入缓存
            var result = Resolve<IOrderBuyServcie>().BuyInfo(parameter);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<StoreProductSku>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        /// 用于租户向主库自动下订单.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public StoreProductSku BuyInfoSync(BuyInfoInput parameter) {
            parameter.IsBuy = true;
            var result = Resolve<IOrderBuyServcie>().BuyInfo(parameter);

            return result.Item2;
        }

        /// <summary>
        ///     用户评论
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "用户评论")]
        [ApiAuth]
        public ApiResult Rate([FromBody] RateInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IOrderService>().Rate(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     收货确认
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "收货确认")]
        [ApiAuth]
        public ApiResult Confirm([FromBody] ConfirmInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IOrderService>().Confirm(parameter);
            return ToResult(result);
        }

        /// <summary>
        /// 收货确认, 只用于租户同步信息到主库订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult ConfirmToMaster([FromBody] ConfirmInput parameter) {
            var rs = Resolve<IOrderService>().ConfirmToMaster(parameter);

            return ApiResult.Success(rs);
        }

        [HttpGet]
        [Display(Description = "我的订单")]
        [ApiAuth]
        public ApiResult OrderList([FromQuery] AdminOrderList parameter) {
            var orderList = Resolve<IOrderService>().GetPagedList(this.Query);
            return ApiResult.Success(orderList);
        }

        /// <summary>
        /// 获取订单邮费
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuth]
        [Display(Description = "获取订单邮费")]
        public ApiResult<OrderExpressViewModel> GetExpressAmount(long orderId) {
            var result = Resolve<IOrderService>().GetExpressAmount(orderId);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<OrderExpressViewModel>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        /// 修改订单邮费
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ApiAuth]
        [Display(Description = "修改订单邮费")]
        public ApiResult UpdateExpressAmount([FromBody] OrderExpressViewModel orderExpressInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<IOrderService>().UpdateExpressAmount(orderExpressInput);

            return ToResult(result);
        }

        /// <summary>
        /// 支付货款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ApiAuth]
        [Display(Description = "支付货款")]
        public ApiResult PayGoodsAmount([FromBody] PayGoodsAmountInput input) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var order = Resolve<IOrderService>().GetSingle(s => s.Id == input.OrderId);
            order.OrderExtension.IsSupplierView = true;
            order.OrderExtension.PayGoods = input;
            order.Extension = order.OrderExtension.ToJsons();
            order.OrderStatus = OrderStatus.Remited;
            var result = Resolve<IOrderService>().Update(order);
            if (result) {
                var store = Resolve<IShopStoreService>().GetSingle(u => u.Id == order.StoreId);
                var user = Resolve<IUserService>().GetSingle(u => u.Id == store.UserId);
                Resolve<IOpenService>().SendRawAsync(user.Mobile, "您有一个新订单，请尽快安排发货，并登录企牛牛供应商后台进行发货操作");
                return ApiResult.Success();
            }

            return ApiResult.Failure("操作失败,请稍后再试!");
        }

        [HttpGet]
        public ApiResult GetOrderList() {
            var result = Resolve<IAdminTableService>()
                .ToExcel("Alabo.App.Shop.Order.Domain.Dtos.OrderToExcel", "IOrderActionService", "GetOrdersToExcel", ObjectExtension.ToJson(QueryDictionary()));
            if (result.Item1.Succeeded) {
                //return File(result.Item2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                var fileBytes = System.IO.File.ReadAllBytes(result.Item2);
                File(fileBytes, "application/x-msdownload", result.Item3);
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure("操作失败,请稍后再试!");
        }
    }
}