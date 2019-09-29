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
        ///     �鿴 �˻�,�˿�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Refund> Get([FromQuery] long orderId)
        {
            var order = Resolve<IOrderService>().GetSingle(orderId);
            //δ����
            if (order == null) return ApiResult.Failure<Refund>("����������");

            //var res = Resolve<IRefundService>().GetById(order.OrderExtension.RefundInfo.Id);
            //�������³ɹ����˿���Ϣ����ɹ�
            return ApiResult.Success(order.OrderExtension.RefundInfo);
        }

        /// <summary>
        ///     �����˻�,�˿�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Apply([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            //δ����
            if (order.OrderStatus == OrderStatus.WaitingSellerSendGoods)
                order.OrderStatus = OrderStatus.Refund;
            else if (order.OrderStatus == OrderStatus.WaitingReceiptProduct)
                order.OrderStatus = OrderStatus.AfterSale;
            else
                return await Task.FromResult(ApiResult.Failure("����״̬�Ѹ���!"));
            parameter.Process = RefundStatus.BuyerApplyRefund;
            var res = Resolve<IRefundService>().AddOrUpdate(parameter);
            order.OrderExtension.RefundInfo = parameter;

            order.Extension = order.OrderExtension.ToJsons();

            var update = Resolve<IOrderService>().Update(order);

            //�������³ɹ����˿���Ϣ����ɹ�
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");
            //���붩��������¼
            var orderAction = new OrderAction
            {
                Intro = $"��Ա����{parameter.Reason},ԭ��Ϊ:{parameter.Description}",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);
            return await Task.FromResult(result);
        }

        /// <summary>
        ///     �̼Ҵ��� �˿�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Process([FromBody] Refund parameter)
        {
            var order = Resolve<IOrderService>().GetSingle(parameter.OrderId);
            var pay = Resolve<IPayService>().GetSingle(s => s.Id == order.PayId);
            pay.PayExtension = pay.Extensions.ToObject<PayExtension>();
            //�Ȱ�΢�Ž��ԭ·�˻�

            var entity = Resolve<IRefundService>().GetByIdNoTracking(order.OrderExtension?.RefundInfo?.Id);
            if (entity == null) return await Task.FromResult(ApiResult.Failure("�˿�״̬���쳣!"));

            if (pay.Amount < parameter.Amount) //�˿���ܴ���֧�����
                return await Task.FromResult(ApiResult.Failure("�˿���ܴ��ڶ������!"));
            //�˿������ �ֶ��˿�Ľ��
            entity.Amount = parameter.Amount;
            //�ٸ��¶���״̬���˻�״̬
            var refundFree = Resolve<IPayService>().Refund(ref pay, Convert.ToInt32(parameter.Amount * 100),
                order.OrderExtension?.RefundInfo?.Id.ToString());

            //������
            if (!refundFree.Item1.Succeeded)
                return ApiResult.Failure(
                    $"�˿�ʧ��!refundFree.Item1:ErrorMessages=>{refundFree.Item1.ErrorMessages.Join()}/ReturnMessage=>{refundFree.Item1.ReturnMessage},refundFree.Item2:{refundFree.Item2}");
            //δ����
            if (order.OrderStatus == OrderStatus.AfterSale)
            {
                if (order.OrderExtension.RefundInfo.Process == RefundStatus.WaitSaleAllow)
                    entity.Process = RefundStatus.Sucess;
                else
                    return await Task.FromResult(ApiResult.Failure("�ۺ�״̬�ѱ�����,����ʧ��!"));
            }
            else if (order.OrderStatus == OrderStatus.Refund)
            {
                //�˿������ ͬ��֮����� ֱ���˿�,�˿��ֱ�����
                if (order.OrderExtension.RefundInfo.Process == RefundStatus.BuyerApplyRefund)
                    entity.Process = RefundStatus.Sucess;
                else
                    return await Task.FromResult(ApiResult.Failure("�ۺ�״̬�ѱ�����,����ʧ��!"));
            }
            else
            {
                return await Task.FromResult(ApiResult.Failure("����״̬�Ѹ���,����ʧ��!"));
            }

            //�˿���ɺ󽫸��Ķ���״̬
            order.OrderStatus = OrderStatus.Refunded;

            entity.ProcessMessage = parameter.ProcessMessage;

            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("�˿�ʧ��!");

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     �̼Ҵ��� ͬ����
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
                    return await Task.FromResult(ApiResult.Failure("�ۺ�״̬�ѱ�����,����ʧ��!"));
                entity.Address = parameter.Address;
            }

            var res = Resolve<IRefundService>().Update(entity);
            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");
            //���붩��������¼
            var orderAction = new OrderAction
            {
                Intro = "ϵͳ��̨ͬ���˿����룬�Ѵ���",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     �̼Ҵ��� �ܾ���
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
                    return await Task.FromResult(ApiResult.Failure("�ۺ�״̬�Ѹ���!"));
            }

            //δ����
            if (order.OrderStatus == OrderStatus.AfterSale)
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
            else if (order.OrderStatus == OrderStatus.Refund)
                order.OrderStatus = OrderStatus.WaitingSellerSendGoods;
            else
                return await Task.FromResult(ApiResult.Failure("����״̬�Ѹ���!"));
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");

            //���붩��������¼
            var orderAction = new OrderAction
            {
                Intro = "ϵͳ��̨�ܾ��˿����룬�Ѵ���",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     �û�ȡ������
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
                    entity.Process = RefundStatus.Closed; //�û�ȡ��
                else
                    return await Task.FromResult(ApiResult.Failure("�ۺ�״̬�Ѹ���!"));
            }

            //δ����
            if (order.OrderStatus == OrderStatus.AfterSale)
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
            else if (order.OrderStatus == OrderStatus.Refund)
                order.OrderStatus = OrderStatus.WaitingSellerSendGoods;
            else
                return await Task.FromResult(ApiResult.Failure("����״̬�Ѹ���!"));
            //entity.Address = parameter.Address;
            var res = Resolve<IRefundService>().Update(entity);

            order.OrderExtension.RefundInfo = entity;
            order.Extension = order.OrderExtension.ToJsons();
            var update = Resolve<IOrderService>().Update(order);
            var result = res && update ? ApiResult.Success() : ApiResult.Failure("����ʧ��!");
            //���붩��������¼
            var orderAction = new OrderAction
            {
                Intro = "�û�ȡ���˿�����",
                ActionUserId = parameter.UserId,
                OrderId = parameter.OrderId,
                OrderActionType = OrderActionType.UserRefund
            };
            Resolve<IOrderActionService>().Add(orderAction);

            return await Task.FromResult(result);
        }
    }
}