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
                return ServiceResult.FailedWithMessage("�ֻ������ʽ����");
            }
            if (order.Name.Length <= 1 && order.Name.Length > 8) {
                return ServiceResult.FailedWithMessage("������ʽ����ȷ����������ȷ������");
            }
            var product = Resolve<IProductService>().GetSingle(r => r.Id == order.ProductId);
            if (product == null) {
                return ServiceResult.FailedWithMessage("��Ʒ������");
            }
            var productSku = Resolve<IProductSkuService>().GetSingle(r => r.Id == order.ProductSkuId);
            if (productSku == null) {
                return ServiceResult.FailedWithMessage("��ƷSku������");
            }
            if (productSku.ProductId != order.ProductId) {
                return ServiceResult.FailedWithMessage("��Ʒ��Ϣ����");
            }
            if (order.BuyCount < 0) {
                return ServiceResult.FailedWithMessage("������������С��1");
            }
            order.RegionFullName = Resolve<IRegionService>().GetFullName(order.RegionId) + $" {product.Name}";
            order.ProductName = $"{product.Name} {productSku.PropertyValueDesc}";
            order.TotalPrice = productSku.Price * order.BuyCount;

            if (Add(order)) {
                Resolve<IOpenService>().SendRaw(order.Mobile,
                    $"��ϲ��������{order.ProductName}���µ��ɹ������������ֻ���ͨ�����ǻᾡ�콫���ı����͵������У���л��������ƽ̨��֧��");
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedWithMessage("�µ�ʧ��");
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
                var str = $"<img src='{avator}' class='BuyList_image' width='32px' height='32px' style='border-radius:32px;'/><span>{xing}{sex.GetDisplayName()} {k}���ӹ���ɹ�</span>";
                list.Add(str);
            }

            return list;
        }

        public List<string> BuyListRcently(long productId) {
            int k = 1;
            var product = Resolve<IProductService>().GetSingle(r => r.Id == productId);
            if (product == null) {
                throw new ValidException("��Ʒ������");
            }

            var productSkus = Resolve<IProductSkuService>().GetList(r => r.ProductId == product.Id);

            var list = new List<string>();
            for (int i = 0; i < 10; i++) {
                var index = RandomHelper.Number(0, productSkus.Count);
                var xing = RandomHelper.XingRandom();
                var mobile = RandomHelper.MobileHiden();
                k = k + RandomHelper.Number(1, 5);
                var sku = productSkus[index];
                var str = $"{xing}**({mobile}) {k}���Ӷ�����{product.Name } {sku.PropertyValueDesc}";
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