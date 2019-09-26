using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Industry.Offline.Order.Domain.Dtos;
using Alabo.Industry.Offline.Order.Domain.Entities;
using Alabo.Industry.Offline.Order.ViewModels;
using Alabo.Industry.Offline.Product.Domain.Services;
using Alabo.Maps;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Order.Domain.Services
{
    public class MerchantCartService : ServiceBase<MerchantCart, ObjectId>, IMerchantCartService
    {
        public MerchantCartService(IUnitOfWork unitOfWork, IRepository<MerchantCart, ObjectId> repository)
            : base(unitOfWork, repository)
        {
        }

        /// <summary>
        ///     Add cart
        /// </summary>
        public ServiceResult AddCart(MerchantCartInput input)
        {
            //Check product
            var serviceResult = ServiceResult.Success;
            var product = Resolve<IMerchantProductService>()
                .GetSingle(r =>
                    r.Id == input.MerchantProductId.ToObjectId()); //&& r.MerchantStoreId == input.MerchantStoreId
            if (product == null) return ServiceResult.FailedWithMessage("��Ʒ������");
            if (input.Count <= 0) return ServiceResult.FailedWithMessage("��Ʒ�����������0");
            //stock
            var sku = product.Skus?.Find(s => s.SkuId == input.SkuId);
            if (sku == null) return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku������");
            if (sku.Stock <= 0) return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku:{sku.Name}����治��");
            if (input.Count > sku.Stock)
                return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku:{sku.Name}����������������Ʒ�������");

            //Get cart
            var cartSingle = Resolve<IMerchantCartService>().GetSingle(c => c.UserId == input.UserId
                                                                            //&& c.MerchantProductId == input.MerchantProductId
                                                                            && c.SkuId == input.SkuId
                                                                            && c.Status == Status.Normal);
            if (cartSingle == null)
            {
                var cart = new MerchantCart
                {
                    //MerchantStoreId = input.MerchantStoreId,
                    UserId = input.UserId,
                    Count = input.Count,
                    ProductName = product.Name,
                    MerchantProductId = product.Id.ToString(),
                    Price = sku != null ? sku.Price : 0,
                    SkuId = sku.SkuId,
                    SkuName = sku.Name
                };

                Add(cart);

                return serviceResult;
            }

            // ��������
            cartSingle.Count += input.Count;
            //���������Ƿ񳬹���������
            if (cartSingle.Count > sku.Stock)
                return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku:{sku.Name}����������������Ʒ�������");

            Update(cartSingle);

            ////�����stock
            //MerchantProduct skus = product;
            //skus.Stock -= input.Count;
            //sku.Stock -= input.Count;
            //Resolve<IMerchantProductService>().Update(skus);

            return ServiceResult.Success;
        }

        /// <summary>
        ///     Get cart
        /// </summary>
        public Tuple<ServiceResult, MerchantCartOutput> GetCart(long userId, string merchantStoreId)
        {
            //get merchant store
            //var merchantStore = Resolve<IMerchantStoreService>().GetSingle(s => s.Id == merchantStoreId.ToObjectId());
            //if (merchantStore == null)
            //{
            //    return Tuple.Create(ServiceResult.FailedWithMessage("�ŵ겻����"), new MerchantCartOutput());
            //}

            var viewCarts = Resolve<IMerchantCartService>()
                .GetList(e => e.UserId == userId && e.Status == Status.Normal).ToList();
            var carts = viewCarts.MapToList<MerchantCartViewModel>();
            if (carts.Count <= 0)
                return Tuple.Create(ServiceResult.FailedWithMessage("���Ĺ��ﳵ�տ���Ҳ"), new MerchantCartOutput());

            return Resolve<IMerchantOrderService>().CountPrice(carts);
        }

        /// <summary>
        ///     get cart by ids
        /// </summary>
        /// <param name="cartIds"></param>
        /// <returns></returns>
        public List<MerchantCartViewModel> GetCart(List<ObjectId> cartIds)
        {
            if (cartIds.Count <= 0) return new List<MerchantCartViewModel>();
            var viewCarts = Resolve<IMerchantCartService>().GetList(e => cartIds.Contains(e.Id)).ToList();
            return viewCarts.MapToList<MerchantCartViewModel>();
        }

        /// <summary>
        ///     Remove cart
        /// </summary>
        public ServiceResult RemoveCart(MerchantCartInput input)
        {
            //Get cart
            var cart = Resolve<IMerchantCartService>().GetList(c => c.UserId == input.UserId
                                                                    && c.Id == input.Id.ToObjectId()
                                                                    && c.Status == Status.Normal);
            cart.Foreach(z =>
            {
                z.Status = Status.Deleted;
                Update(z);
            });

            return ServiceResult.Success;
        }

        /// <summary>
        ///     Update cart
        /// </summary>
        public ServiceResult UpdateCart(MerchantCartInput input)
        {
            var cart = Resolve<IMerchantCartService>().GetSingle(c => c.UserId == input.UserId
                                                                      && c.Id == input.Id.ToObjectId()
                                                                      && c.Status == Status.Normal);
            cart.Count = input.Count;
            //��֤��Ʒ��sku����
            var product = Resolve<IMerchantProductService>()
                .GetSingle(r =>
                    r.Id == input.MerchantProductId.ToObjectId()); //&& r.MerchantStoreId == input.MerchantStoreId
            if (product == null) return ServiceResult.FailedWithMessage("��Ʒ������");
            if (input.Count <= 0) return ServiceResult.FailedWithMessage("��Ʒ�����������0");
            //stock
            var sku = product.Skus?.Find(s => s.SkuId == input.SkuId);
            if (sku == null) return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku������");
            if (input.Count > sku.Stock)
                return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku:{sku.Name}����������������Ʒ�������");
            var cartSingle = Resolve<IMerchantCartService>().GetSingle(c => c.UserId == input.UserId
                                                                            //&& c.MerchantProductId == input.MerchantProductId
                                                                            && c.SkuId == input.SkuId
                                                                            && c.Status == Status.Normal);
            // ��������
            cartSingle.Count += input.Count;
            //���������Ƿ񳬹���������
            if (cartSingle.Count > sku.Stock)
                return ServiceResult.FailedWithMessage($"��Ʒ:{product.Name}��Sku:{sku.Name}����������������Ʒ�������");
            Update(cart);

            return ServiceResult.Success;
        }
    }
}