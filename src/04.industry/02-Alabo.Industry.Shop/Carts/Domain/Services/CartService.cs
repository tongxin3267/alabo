using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Industry.Shop.Carts.Domain.Entities;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Products.Domain.Repositories;
using Alabo.Industry.Shop.Products.Domain.Services;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Carts.Domain.Services
{
    public class CartService : ServiceBase<Cart, ObjectId>, ICartService
    {
        private readonly IProductSkuRepository _productSkuRepository;

        public CartService(IUnitOfWork unitOfWork, IRepository<Cart, ObjectId> repository) : base(unitOfWork,
            repository)
        {
            _productSkuRepository = Repository<IProductSkuRepository>();
        }

        /// <summary>
        ///     添加购物车
        /// </summary>
        /// <param name="orderProductInput"></param>
        public ServiceResult AddCart(OrderProductInput orderProductInput)
        {
            var serviceResult = ServiceResult.Success;
            var product = Resolve<IProductService>().GetSingle(r => r.Id == orderProductInput.ProductId);
            var productSku = Resolve<IProductSkuService>().GetSingle(r => r.Id == orderProductInput.ProductSkuId);
            var storeId = _productSkuRepository.GetStoreIdByProductSkuId(orderProductInput.ProductSkuId);

            if (product == null) return ServiceResult.FailedWithMessage("商品不存在");

            if (productSku == null) return ServiceResult.FailedWithMessage("商品SKU错误");

            if (storeId.IsObjectIdNullOrEmpty()) return ServiceResult.FailedWithMessage("storeId错误");

            var cartSingle = Resolve<ICartService>().GetSingle(u =>
                u.ProductId == orderProductInput.ProductId && u.ProductSkuId == orderProductInput.ProductSkuId
                                                           && u.UserId == orderProductInput.LoginUserId &&
                                                           u.Status == Status.Normal);

            if (cartSingle == null)
            {
                var cart = new Cart
                {
                    StoreId = storeId,
                    UserId = orderProductInput.LoginUserId,
                    Count = orderProductInput.Count,
                    ProductName = product.Name,
                    ProductId = product.Id,
                    Price = product.Price,
                    ProductSkuId = productSku.Id,
                    PropertyValueDesc = productSku.PropertyValueDesc
                };

                Add(cart);
                return serviceResult;
            }
            //TODO:修改商品数量在这里操作
            //var count = cartSingle.Count + orderProductInput.Count;
            //TODO:判断商品最大购买和最小购买
            //var key = ProductActivityType.BuyPermission.GetDisplayResourceTypeName();
            //var buy = Resolve<IActivityService>().GetSingle(s => s.ProductId == orderProductInput.ProductId && s.Key == key);
            //if (count > buy?.MaxStock)
            //    return ServiceResult.FailedWithMessage("商品数量不能高于最大购买数量!");
            //else if (count < buy?.MaxStock)
            //    return ServiceResult.FailedWithMessage("商品数量不能低于最低购买数量!");

            // 数量递增
            cartSingle.Count += orderProductInput.Count;
            Update(cartSingle);

            return ServiceResult.Success;
        }

        /// <summary>
        ///     获取用户的购物车    快递模板费用未处理
        /// </summary>
        /// <param name="UserId">用户Id</param>
        public Tuple<ServiceResult, StoreProductSku> GetCart(long UserId)
        {
            var storeItems = Resolve<IShopStoreService>().GetStoreItemListFromCache();
            var temp = storeItems.Select(r => r.StoreId);
            var viewCarts = Resolve<ICartService>().GetList(e =>
                e.UserId == UserId && e.Status == Status.Normal && temp.Contains(e.StoreId)); // 读取购物车数据，是正常店铺的
            var orderProductInputList = new List<OrderProductInput>();
            foreach (var item in viewCarts)
            {
                var orderProductInput = new OrderProductInput
                {
                    Count = item.Count,
                    LoginUserId = UserId,
                    StoreId = item.StoreId,
                    ProductId = item.ProductId,
                    ProductSkuId = item.ProductSkuId
                };
                orderProductInputList.Add(orderProductInput);
            }

            if (orderProductInputList.Count == 0)
                return Tuple.Create(ServiceResult.FailedWithMessage("商品数量为0"), new StoreProductSku());

            var buyInput = new BuyInfoInput
            {
                LoginUserId = UserId,
                ProductJson = orderProductInputList.ToJsons()
            };
            return Resolve<IOrderBuyServcie>().BuyInfo(buyInput);
        }

        /// <summary>
        ///     移除购物车
        /// </summary>
        /// <param name="orderProductInput"></param>
        public ServiceResult RemoveCart(OrderProductInput orderProductInput)
        {
            var serviceResult = ServiceResult.Success;
            //var storeId = _productSkuRepository.GetStoreIdByProductSkuId(orderProductInput.ProductSkuId);
            //var cars = Resolve<IUserActionService>().GetList(e =>
            //    e.UserId == orderProductInput.LoginUserId && e.EntityId == storeId &&
            //    e.Type == UserActionType.ProductCart);
            //foreach (var car in cars) {
            //    var productInput = car.Extensions.DeserializeJson<OrderProductInput>();
            //    if (productInput.ProductSkuId == orderProductInput.ProductSkuId) {
            //        if (!Resolve<IUserActionService>().Delete(e => e.Id.Equals(car.Id))) {
            //            return ServiceResult.FailedWithMessage("移除购物车,到数据库失败");
            //        }
            //    }
            //}
            var cart = Resolve<ICartService>().GetList(u =>
                u.ProductSkuId == orderProductInput.ProductSkuId && u.UserId == orderProductInput.LoginUserId &&
                u.Status == Status.Normal);
            cart.Foreach(z =>
            {
                z.Status = Status.Deleted;
                Update(z);
            });

            return serviceResult;
        }

        /// <summary>
        ///     更新购物车
        /// </summary>
        /// <param name="orderProductInput"></param>
        public ServiceResult UpdateCart(OrderProductInput orderProductInput)
        {
            var serviceResult = ServiceResult.Success;
            //var storeId = _productSkuRepository.GetStoreIdByProductSkuId(orderProductInput.ProductSkuId);
            //var cars = Resolve<IUserActionService>().GetList(e =>
            //    e.UserId.Equals(orderProductInput.LoginUserId) && e.EntityId.Equals(storeId) &&
            //    e.Type.Equals(UserActionType.ProductCart));
            //foreach (var car in cars) {
            //    var productInput = car.Extensions.DeserializeJson<OrderProductInput>();
            //    if (productInput.ProductSkuId == orderProductInput.ProductSkuId) {
            //        productInput.Count = orderProductInput.Count;
            //        car.Extensions = productInput.ToJson();
            //        if (!Resolve<IUserActionService>().Update(car)) {
            //            return ServiceResult.FailedWithMessage("更新购物车到据库失败");
            //        }
            //    }
            //}
            var cart = Resolve<ICartService>().GetSingle(u =>
                u.ProductId == orderProductInput.ProductId && u.ProductSkuId == orderProductInput.ProductSkuId &&
                u.UserId == orderProductInput.LoginUserId && u.Status == Status.Normal);
            cart.Count = orderProductInput.Count;
            Update(cart);
            return serviceResult;
        }
    }
}