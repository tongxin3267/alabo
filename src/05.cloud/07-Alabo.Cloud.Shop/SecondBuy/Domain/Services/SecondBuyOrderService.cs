using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Market.SecondBuy.Domain.Entities;
using Alabo.Domains.Entities;
using System.Collections.Generic;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Shop.Product.Domain.Services;
using System.Text.RegularExpressions;
using Alabo.Domains.Base.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Randoms;

namespace Alabo.App.Market.SecondBuy.Domain.Services {

    public class SecondBuyOrderService : ServiceBase<SecondBuyOrder, ObjectId>, ISecondBuyOrderService {

        public SecondBuyOrderService(IUnitOfWork unitOfWork, IRepository<SecondBuyOrder, ObjectId> repository) : base(
            unitOfWork, repository) {
        }

        public ServiceResult Buy(SecondBuyOrder order) {
            if (!CheckMobile(order.Mobile)) {
                return ServiceResult.FailedWithMessage("手机号码格式错误");
            }
            if (order.Name.Length <= 1 && order.Name.Length > 8) {
                return ServiceResult.FailedWithMessage("姓名格式不正确，请输入正确的姓名");
            }
            var product = Resolve<IProductService>().GetSingle(r => r.Id == order.ProductId);
            if (product == null) {
                return ServiceResult.FailedWithMessage("商品不存在");
            }
            var productSku = Resolve<IProductSkuService>().GetSingle(r => r.Id == order.ProductSkuId);
            if (productSku == null) {
                return ServiceResult.FailedWithMessage("商品Sku不存在");
            }
            if (productSku.ProductId != order.ProductId) {
                return ServiceResult.FailedWithMessage("商品信息错误");
            }
            if (order.BuyCount < 0) {
                return ServiceResult.FailedWithMessage("购买数量不能小于1");
            }
            order.RegionFullName = Resolve<IRegionService>().GetFullName(order.RegionId) + $" {product.Name}";
            order.ProductName = $"{product.Name} {productSku.PropertyValueDesc}";
            order.TotalPrice = productSku.Price * order.BuyCount;

            if (Add(order)) {
                Resolve<IOpenService>().SendRaw(order.Mobile,
                    $"恭喜您订购的{order.ProductName}已下单成功，请您保持手机畅通，我们会尽快将您的宝贝送到您手中，感谢您对我们平台的支持");
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedWithMessage("下单失败");
            }
        }

        public List<string> BuyList(long productId) {
            int k = 1;
            var list = new List<string>();
            for (int i = 0; i < 10; i++) {
                var avator = RandomHelper.Avator;
                var xing = RandomHelper.XingRandom();
                var sex = RandomHelper.GetSex();
                k = k + RandomHelper.Number(1, 5);
                var str = $"<img src='{avator}' class='BuyList_image' width='32px' height='32px' style='border-radius:32px;'/><span>{xing}{sex.GetDisplayName()} {k}分钟购买成功</span>";
                list.Add(str);
            }

            return list;
        }

        public List<string> BuyListRcently(long productId) {
            int k = 1;
            var product = Resolve<IProductService>().GetSingle(r => r.Id == productId);
            if (product == null) {
                throw new ValidException("商品不存在");
            }

            var productSkus = Resolve<IProductSkuService>().GetList(r => r.ProductId == product.Id);

            var list = new List<string>();
            for (int i = 0; i < 10; i++) {
                var index = RandomHelper.Number(0, productSkus.Count);
                var xing = RandomHelper.XingRandom();
                var mobile = RandomHelper.MobileHiden();
                k = k + RandomHelper.Number(1, 5);
                var sku = productSkus[index];
                var str = $"{xing}**({mobile}) {k}分钟订购了{product.Name } {sku.PropertyValueDesc}";
                list.Add(str);
            }

            return list;
        }

        private bool CheckMobile(string mobile) {
            if (mobile.IsNullOrEmpty()) {
                return false;
            }

            if (mobile.Length != 11) {
                return false;
            }

            var rg = new Regex(@"^0?(13[0-9]|15[0-9]|18[0-9]|17[0-9]|19[0-9]|16[0-9]|14[0-9])[0-9]{8}$");
            var m = rg.Match(mobile);
            return m.Success;
        }
    }
}