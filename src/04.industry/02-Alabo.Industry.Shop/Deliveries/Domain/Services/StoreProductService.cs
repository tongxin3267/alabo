using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Categories.Domain.Services;
using Alabo.Industry.Shop.Deliveries.Domain.Dtos;
using Alabo.Industry.Shop.Products.Domain.Configs;
using Alabo.Industry.Shop.Products.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Domain.Repositories;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.ViewModels;

namespace Alabo.Industry.Shop.Deliveries.Domain.Services
{

    public class StoreProductService : ServiceBase, IStoreProductService
    {

        public StoreProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 商品保存
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ServiceResult EditProduct(ProductEditOuput parameter)
        {

            #region 基础数据赋值

            parameter.Product.CategoryId = parameter.Category.Id;
            if (parameter.Product.Id == 0) {
                parameter.Product.StoreId = parameter.Store.Id;
            }

            parameter.Product.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(parameter.Product.Price, parameter.Product.PriceStyleId, parameter.Product.MinCashRate);
            parameter.Product.ModifiedTime = DateTime.Now;
            parameter.ProductDetail.ProductId = parameter.Product.Id;
            //如果是管理员就直接上架或者下架            
            if (Resolve<IUserService>().IsAdmin(parameter.Store.UserId))
            {
                if (parameter.OnSale)
                {
                    parameter.Product.ProductStatus = ProductStatus.Online;
                }
                else
                {
                    parameter.Product.ProductStatus = ProductStatus.SoldOut;
                }
            }
            else
            {
                //如果是供应商直接状态为审核状态
                parameter.Product.ProductStatus = ProductStatus.Auditing;
            }
            if (parameter.Product.PriceStyleId.IsGuidNullOrEmpty())
            {
                // 默认使用现金商城，后期从前端传值过来
                var priceStyleConfig = Resolve<IAutoConfigService>()
                    .GetList<PriceStyleConfig>(r => r.PriceStyle == PriceStyle.CashProduct);
                parameter.Product.PriceStyleId = priceStyleConfig.FirstOrDefault().Id;
            }
            parameter.ProductDetail.PropertyJson = parameter.Category.ToJson();

            #endregion 基础数据赋值

            #region 基础安全验证

            if (parameter.ProductSkus == null || parameter.ProductSkus.Count == 0)
            {
                return ServiceResult.FailedWithMessage("商品Sku不能为空");
            }
            //if (parameter.Product.PurchasePrice > parameter.Product.CostPrice) {
            //    return ServiceResult.FailedWithMessage("进货价不能大于成本价");
            //}
            //尝试过特性,属性set函数 均无效
            if (parameter.Product.CostPrice > parameter.Product.Price) {
                return ServiceResult.FailedWithMessage("商品销售价必须为大于等于成本价");
            }

            if (parameter.Product.CostPrice > parameter.Product.MarketPrice) {
                return ServiceResult.FailedWithMessage("商品市场价必须为大于等于成本价");
            }




            //判断sku的价格是否低于成本
            foreach (var item in parameter.ProductSkus)
            {
                if (item.CostPrice > item.Price) {
                    return ServiceResult.FailedWithMessage("商品SKU销售价必须为大于等于成本价");
                }

                if (item.CostPrice > item.MarketPrice) {
                    return ServiceResult.FailedWithMessage("商品SKU市场价必须为大于等于成本价");
                }

                if (item.FenRunPrice > item.CostPrice || item.FenRunPrice > item.MarketPrice || item.FenRunPrice > item.MarketPrice) {
                    return ServiceResult.FailedWithMessage("分润价格不能大于其他价格");
                }
            }

            if (parameter.Product.StoreId <= 0)
            {
                return ServiceResult.FailedWithMessage("请为商品选择店铺");
            }
            var category = Resolve<ICategoryService>().GetSingle(parameter.Product.CategoryId);
            if (category == null)
            {
                return ServiceResult.FailedWithMessage("未选择商品类目，或者商品类目已不存在");
            }
            if (Repository<IProductRepository>()
                    .Count(r => r.Bn == parameter.Product.Bn && r.Id != parameter.Product.Id) >
                0)
            {
                return ServiceResult.FailedWithMessage("该货号已存在，请使用其他货号");
            }

            #endregion 基础安全验证

            #region 商品图片处理

            parameter.ProductDetail.ImageJson = Resolve<IProductApiService>().CreateImage(parameter.Product, parameter.Images);

            #endregion 商品图片处理

            #region 数据保存

            var context = Repository<IProductRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                parameter.ProductDetail.Extension = parameter.ProductDetail.ProductDetailExtension.ToJson();

                if (parameter.Product.Id == 0)
                {
                    Resolve<IProductService>().Add(parameter.Product); // 添加Shop_product 表
                    parameter.ProductDetail.ProductId = parameter.Product.Id;
                    Resolve<IProductDetailService>().Add(parameter.ProductDetail); // 添加Shop_productdetai表
                }
                else
                {
                    Resolve<IProductService>().Update(parameter.Product); // 更新Shop_product 表
                    Resolve<IProductDetailService>().UpdateNoTracking(parameter.ProductDetail); // 更新Shop_productdetai表
                }

                // 更新商品Sku
                var skuResult = Resolve<IProductSkuService>().AddUpdateOrDelete(parameter.Product, parameter.ProductSkus); // 更新Shop_productsku表
                if (!skuResult.Succeeded)
                {
                    throw new ArgumentException(skuResult.ToString());
                }

                //添加商品分类和标签
                Resolve<IRelationIndexService>().AddUpdateOrDelete<ProductClassRelation>(parameter.Product.Id, string.Join(',', parameter.ProductDetail.ProductDetailExtension.StoreClass));
                Resolve<IRelationIndexService>().AddUpdateOrDelete<ProductTagRelation>(parameter.Product.Id, string.Join(',', parameter.ProductDetail.ProductDetailExtension.Tags));

                context.SaveChanges();
                context.CommitTransaction();
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }

            #endregion 数据保存

            #region 保存后操作

            //删除缓存
            var cacheKey = "ApiProduct_" + parameter.Product.Id;
            ObjectCache.Remove(cacheKey);
            // 更新商品Sku价格
            //Resolve<IProductSkuService>().AutoUpdateSkuPrice(parameter.Product.Id);
            Resolve<IProductService>().Log($"成功添加一件商品,商品货号：{parameter.Product.Bn}");
            return ServiceResult.Success;

            #endregion 保存后操作
        }

        public ProductEditOuput GetProductView(ProductEditInput parameter)
        {

            #region 安全验证

            var productEditOuput = new ProductEditOuput();
            if (parameter == null)
            {
                throw new System.Exception("参数不能为空");
            }
            var user = Resolve<IUserService>().GetNomarlUser(parameter.UserId);
            if (user == null)
            {
                throw new System.Exception("用户不存在");
            }

            var store = Resolve<IShopStoreService>().GetUserStore(user.Id);
            if (store == null)
            {
                throw new System.Exception("当前用户不存在店铺");
            }
            productEditOuput.Store = store;

            if (parameter.ProductId != 0)
            {
                productEditOuput.Setting = new ProductViewEditSetting()
                {
                    Title = "商品编辑",
                    IsAdd = false,
                };
            }

            productEditOuput.Relation = GetRelationView(store, parameter.UserId);

            #endregion 安全验证

            var productView = Resolve<IProductAdminService>().GetViewProductEdit(parameter.ProductId, 0);
            if (productView == null)
            {
                throw new System.Exception("商品不存在");
            }
            productEditOuput.Product = productView.Product;
            productEditOuput.ProductDetail = productView.ProductDetail;

            // 新加商品, 返回默认配置值.
            if (parameter.ProductId == 0)
            {
                if (parameter.CategoryId.IsGuidNullOrEmpty())
                {
                    throw new System.Exception("商品添加是请传入类目ID");
                }
                var category = Resolve<ICategoryService>().GetSingle(parameter.CategoryId);
                productEditOuput.Category = category;
                if (productEditOuput.Category == null)
                {
                    throw new System.Exception("类目不存在");
                }
                productEditOuput.Product.DeliveryTemplateId = productEditOuput.Relation.DeliveryTemplates.FirstOrDefault()?.Key.ToStr();
            }
            else
            {
                if (productEditOuput.Product.Id <= 0)
                {
                    throw new System.Exception("商品不存在");
                }

                productEditOuput.ProductSkus = Resolve<IProductSkuService>()
                    .GetList(o => o.ProductId == parameter.ProductId)?.ToList();
                if (productEditOuput.ProductSkus == null)
                {
                    throw new System.Exception("商品SKU不存在");
                }


                var outCategory = productView.ProductDetail.PropertyJson.ToObject<Category>();
                //var categoryEntity = productView.Category;


                //var checkCategory = outCategory.SalePropertys?[0]?.PropertyValues;
                if (outCategory != null && outCategory.SalePropertys != null && outCategory.SalePropertys.Count > 0 && outCategory.SalePropertys[0].PropertyValues != null)
                {
                    outCategory.SalePropertys[0].PropertyValues = outCategory.SalePropertys[0].PropertyValues.Where(s => s != null).ToList();
                    //    productView?.Category?.SalePropertys?[0]?.PropertyValues.Select(s => new CategoryPropertyValue()
                    //{
                    //    Id = s.Id,
                    //    CreateTime = s.CreateTime,
                    //    Image = s.Image,
                    //    IsCheck = (checkCategory.FirstOrDefault(c => c.Id == s.Id)?.IsCheck).Value,
                    //    //ObjectCache = s.ObjectCache,
                    //    PropertyId = s.PropertyId,
                    //    SortOrder = s.SortOrder,
                    //    ValueAlias = checkCategory.FirstOrDefault(c => c.Id == s.Id)?.ValueAlias,
                    //    ValueName = s.ValueName,
                    //    Version = s.Version
                    //}).ToList();
                }

                //productView?.Category?.SalePropertys?.Where(s => s != null)?.Select(s=>return new );
                productEditOuput.Category = outCategory;
                if (productEditOuput.Category == null)
                {
                    throw new System.Exception("该商品类目不存在");
                }

                // 商品分类，和商品标签，商品副标题会通过此处来回绑定
                if (productView.ProductDetail != null)
                {
                    productView.ProductDetail.ProductDetailExtension =
                        productView.ProductDetail.Extension.ToObject<ProductDetailExtension>();
                    //回绑图片
                    productEditOuput.Images = productView.ProductDetail.ImageJson.DeserializeJson<List<ProductThum>>()
                        .Select(o => o.OriginalUrl).ToList();
                }

                if (productEditOuput.Product.ProductStatus != ProductStatus.Online)
                {
                    productEditOuput.OnSale = false;
                }
                else
                {
                    productEditOuput.OnSale = true;
                }
            }
            return productEditOuput;
        }

        #region 获取级联相关数据

        private StoreRelation GetRelationView(Entities.Store store, long userId)
        {
            StoreRelation relation = new StoreRelation();
            var templates = Resolve<IDeliveryTemplateService>().GetList(x => x.StoreId == store.Id);
            templates.Foreach(r =>
            {
                relation.DeliveryTemplates.Add(new KeyValue(r.Id, r.TemplateName));
            });

            relation.Classes = Resolve<IRelationService>()
                .GetClass(typeof(ProductClassRelation).Name, userId).ToList();
            //ProductTagRelation
            relation.Tags = Resolve<IRelationService>()
                .GetKeyValues(typeof(ProductTagRelation).Name, userId).ToList();

            return relation;
        }

        #endregion 获取级联相关数据
    }
}