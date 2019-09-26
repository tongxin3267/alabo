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
        /// �������֧��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "�������֧��")]
        [ApiAuth]
        public ApiResult<BuyOutput> Pay(long id, long loginUserId) {
            if (id <= 0) {
                return ApiResult.Failure<BuyOutput>("Id����ȷ");
            }
            var result = Resolve<IOrderBuyServcie>().Pay(id, loginUserId);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<BuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }
            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     ����ȡ��
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "����ȡ��")]
        [ApiAuth]
        public ApiResult Cancel(long id, long loginUserId) {
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id && e.UserId == loginUserId);
            if (order == null) {
                return ApiResult.Failure("����������");
            }
            if (order.OrderStatus != OrderStatus.WaitingBuyerPay) {
                return ApiResult.Failure("��״̬����ȡ��!");
            }
            order.OrderStatus = OrderStatus.Closed;
            Resolve<IOrderService>().Update(order);
            return ApiResult.Success("����ȡ���ɹ�");
        }

        /// <summary>
        ///     ���̶�������
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="loginUserId"></param>

        [HttpGet]
        [Display(Description = "��������")]
        [ApiAuth]
        public ApiResult<OrderShowOutput> Show(long id, long loginUserId) {
            var orderShow = Resolve<IOrderService>().Show(id);
            if (orderShow == null) {
                return ApiResult.Failure<OrderShowOutput>("����������");
            }

            return ApiResult.Success(orderShow);
        }

        /// <summary>
        ///     �û���������
        /// </summary>
        [HttpGet]
        [Display(Description = "��������")]
        [ApiAuth]
        public ApiResult GetOrder(long id, long loginUserId) {
            var orderShow = Resolve<IOrderService>().GetOrderSingle(id, loginUserId);
            if (orderShow == null) {
                return ApiResult.Failure("����������");
            }

            return ApiResult.Success(orderShow);
        }

        /// <summary>
        /// ��ȡ��ݹ�˾
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Express() {
            var result = Resolve<IOrderService>().GetExpress();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetExpressList(long orderId) {
            var result = Resolve<IOrderService>().GetExpressList(orderId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ������Ϣ
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
        /// ���� ֻ���Ķ���״̬
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "����")]
        [ApiAuth]
        public ApiResult Delivery([FromBody]OrderInput model) {
            var result = Resolve<IOrderService>().Deliver(model);
            if (result.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure(result.ReturnMessage);
        }

        /// <summary>
        /// ���� ֻ֧��һ�����
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "����")]
        [ApiAuth]
        public ApiResult Deliver([FromBody]Deliver model) {
            var result = Resolve<IOrderService>().Deliver(model);
            if (result.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure(result.ReturnMessage);
        }

        /// <summary>
        /// ���ӿ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "���ӿ��")]
        [ApiAuth]
        public ApiResult AddExpress([FromBody]Deliver model) {
            if (model.ExpressName.IsNullOrEmpty()) {
                return ApiResult.Failure("��ݹ�˾����Ϊ��");
            }

            if (model.ExpressNum.IsNullOrEmpty()) {
                return ApiResult.Failure("��ݵ��Ų���Ϊ��");
            }
            var result = Resolve<IOrderService>().AddExpress(model);
            if (result.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure(result.ReturnMessage);
        }

        /// <summary>
        ///     ��ȡ�۸�
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "��ȡ�۸�")]
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
        ///     ��������,��Ʒ�����ύ����ʱ����
        ///     �������ﳵ����
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "����������Ʒ�����ύ����ʱ���ã��������ﳵ����")]
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
        ///     �ҵĶ���
        ///     ��ȡ�����б�������Ӧ�̶�������Ա��������̨����
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "�ҵĶ���")]
        [ApiAuth]
        public ApiResult<PagedList<OrderListOutput>> Index([FromQuery] OrderListInput parameter) {
            parameter.OrderType = OrderType.Normal;
            var orderPageList = Resolve<IOrderService>().GetPageList(parameter);
            return ApiResult.Success(orderPageList);
        }

        /// <summary>
        ///     ��ҵĶ���
        ///     ֻ�����ʹ��,��������
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "��ҵĶ���")]
        [ApiAuth]
        public ApiResult<PagedList<OrderListOutput>> BuyOrderList([FromQuery] OrderListInput parameter) {
            parameter.UserId = parameter.LoginUserId;
            parameter.OrderType = OrderType.Normal;
            var orderPageList = Resolve<IOrderService>().GetPageList(parameter);
            return ApiResult.Success(orderPageList);
        }

        /// <summary>
        ///     ��Ʒ��SkuId��ȷ�϶���ҳ�棬��ȡ��Ʒ�Ĺ�����Ϣ
        ///     ÿ���޸ļ۸�ͨ���˷�������
        ///     ��/order/buy ҳ����ʹ��
        ///     /ordery
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "��Ʒ��SKUid��ȷ�϶���ҳ�棬��ȡ��Ʒ������Ϣ��ÿ���޸ļ۸�ͨ���˷������㣬��/order/buyҳ��ʹ��")]
        [ApiAuth]
        public ApiResult<StoreProductSku> BuyInfo([FromBody] BuyInfoInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<StoreProductSku>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            parameter.IsBuy = true; // Ϊ��������sign ���뻺��
            var result = Resolve<IOrderBuyServcie>().BuyInfo(parameter);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<StoreProductSku>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        /// �����⻧�������Զ��¶���.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public StoreProductSku BuyInfoSync(BuyInfoInput parameter) {
            parameter.IsBuy = true;
            var result = Resolve<IOrderBuyServcie>().BuyInfo(parameter);

            return result.Item2;
        }

        /// <summary>
        ///     �û�����
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "�û�����")]
        [ApiAuth]
        public ApiResult Rate([FromBody] RateInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IOrderService>().Rate(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     �ջ�ȷ��
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "�ջ�ȷ��")]
        [ApiAuth]
        public ApiResult Confirm([FromBody] ConfirmInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IOrderService>().Confirm(parameter);
            return ToResult(result);
        }

        /// <summary>
        /// �ջ�ȷ��, ֻ�����⻧ͬ����Ϣ�����ⶩ��
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult ConfirmToMaster([FromBody] ConfirmInput parameter) {
            var rs = Resolve<IOrderService>().ConfirmToMaster(parameter);

            return ApiResult.Success(rs);
        }

        [HttpGet]
        [Display(Description = "�ҵĶ���")]
        [ApiAuth]
        public ApiResult OrderList([FromQuery] AdminOrderList parameter) {
            var orderList = Resolve<IOrderService>().GetPagedList(this.Query);
            return ApiResult.Success(orderList);
        }

        /// <summary>
        /// ��ȡ�����ʷ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuth]
        [Display(Description = "��ȡ�����ʷ�")]
        public ApiResult<OrderExpressViewModel> GetExpressAmount(long orderId) {
            var result = Resolve<IOrderService>().GetExpressAmount(orderId);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<OrderExpressViewModel>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        /// �޸Ķ����ʷ�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ApiAuth]
        [Display(Description = "�޸Ķ����ʷ�")]
        public ApiResult UpdateExpressAmount([FromBody] OrderExpressViewModel orderExpressInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<IOrderService>().UpdateExpressAmount(orderExpressInput);

            return ToResult(result);
        }

        /// <summary>
        /// ֧������
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ApiAuth]
        [Display(Description = "֧������")]
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
                Resolve<IOpenService>().SendRawAsync(user.Mobile, "����һ���¶������뾡�찲�ŷ���������¼��ţţ��Ӧ�̺�̨���з�������");
                return ApiResult.Success();
            }

            return ApiResult.Failure("����ʧ��,���Ժ�����!");
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

            return ApiResult.Failure("����ʧ��,���Ժ�����!");
        }
    }
}