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
        /// �鿴 �˻�,�˿�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Refund> Get([FromQuery]long orderId) {
            var order = Resolve<IOrderService>().GetSingle(orderId);
            //δ����
            if (order == null) {
                return ApiResult.Failure<Refund>("����������");
            }

            //var res = Resolve<IRefundService>().GetById(order.OrderExtension.RefundInfo.Id);
            //�������³ɹ����˿���Ϣ����ɹ�
            return ApiResult.Success<Refund>(order.OrderExtension.RefundInfo);
        }

        /// <summary>
        /// �����˻�,�˿�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ZKCloud.Open.ApiBase.Models.ApiResult> Apply([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            //δ����
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.WaitingSellerSendGoods) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.Refund;
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.WaitingReceiptProduct) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.AfterSale;
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("����״̬�Ѹ���!"));
            }
            parameter.Process = Domain.Enums.RefundStatus.BuyerApplyRefund;
            var res = Resolve<IRefundService>().AddOrUpdate(parameter);
            order.OrderExtension.RefundInfo = parameter;

            order.Extension = order.OrderExtension.ToJsons();

            var update = Resolve<IOrderService>().Update(order);

            //�������³ɹ����˿���Ϣ����ɹ�
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");
            //���붩��������¼
            var orderAction = new OrderAction {
                Intro = $"��Ա����{parameter.Reason},ԭ��Ϊ:{parameter.Description}",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);
            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// �̼Ҵ��� �˿�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Process([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);
            pay.PayExtension = pay.Extensions.ToObject<PayExtension>();
            //�Ȱ�΢�Ž��ԭ·�˻�

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity == null) {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�˿�״̬���쳣!"));
            }

            if (pay.Amount < parameter.Amount) {
                //�˿���ܴ���֧�����
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�˿���ܴ��ڶ������!"));
            }
            //�˿������ �ֶ��˿�Ľ��
            entity.Amount = parameter.Amount;
            //�ٸ��¶���״̬���˻�״̬
            var refundFree = Resolve<IPayService>().Refund(ref pay, Convert.ToInt32(parameter.Amount * 100), order.OrderExtension?.RefundInfo?.Id.ToString());

            //������
            if (!refundFree.Item1.Succeeded) {
                return ApiResult.Failure($"�˿�ʧ��!refundFree.Item1:ErrorMessages=>{refundFree.Item1.ErrorMessages.Join()}/ReturnMessage=>{refundFree.Item1.ReturnMessage},refundFree.Item2:{refundFree.Item2}");
            }
            //δ����
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.AfterSale) {
                if (order.OrderExtension.RefundInfo.Process == Domain.Enums.RefundStatus.WaitSaleAllow) {
                    entity.Process = Domain.Enums.RefundStatus.Sucess;
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�ۺ�״̬�ѱ�����,����ʧ��!"));
                }
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.Refund) {
                //�˿������ ͬ��֮����� ֱ���˿�,�˿��ֱ�����
                if (order.OrderExtension.RefundInfo.Process == Domain.Enums.RefundStatus.BuyerApplyRefund) {
                    entity.Process = Domain.Enums.RefundStatus.Sucess;
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�ۺ�״̬�ѱ�����,����ʧ��!"));
                }
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("����״̬�Ѹ���,����ʧ��!"));
            }
            //�˿���ɺ󽫸��Ķ���״̬
            order.OrderStatus = Order.Domain.Enums.OrderStatus.Refunded;

            entity.ProcessMessage = parameter.ProcessMessage;

            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("�˿�ʧ��!");

            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// �̼Ҵ��� ͬ����
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
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�ۺ�״̬�ѱ�����,����ʧ��!"));
                }
                entity.Address = parameter.Address;
            }
            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");
            //���붩��������¼
            var orderAction = new OrderAction {
                Intro = $"ϵͳ��̨ͬ���˿����룬�Ѵ���",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// �̼Ҵ��� �ܾ���
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
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�ۺ�״̬�Ѹ���!"));
                }
            }
            //δ����
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.AfterSale) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingReceiptProduct;
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.Refund) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingSellerSendGoods;
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("����״̬�Ѹ���!"));
            }
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");

            //���붩��������¼
            var orderAction = new OrderAction {
                Intro = $"ϵͳ��̨�ܾ��˿����룬�Ѵ���",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await System.Threading.Tasks.Task.FromResult(result);
        }

        /// <summary>
        /// �û�ȡ������
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RefuseCancel([FromBody]Refund parameter) {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity != null) {
                if (entity.Process == Domain.Enums.RefundStatus.BuyerApplyRefund) {
                    entity.Process = Domain.Enums.RefundStatus.Closed;//�û�ȡ��
                } else {
                    return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("�ۺ�״̬�Ѹ���!"));
                }
            }
            //δ����
            if (order.OrderStatus == Order.Domain.Enums.OrderStatus.AfterSale) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingReceiptProduct;
            } else if (order.OrderStatus == Order.Domain.Enums.OrderStatus.Refund) {
                order.OrderStatus = Order.Domain.Enums.OrderStatus.WaitingSellerSendGoods;
            } else {
                return await System.Threading.Tasks.Task.FromResult(ApiResult.Failure("����״̬�Ѹ���!"));
            }
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");
            //���붩��������¼
            var orderAction = new OrderAction {
                Intro = $"�û�ȡ���˿�����",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}