using Microsoft.AspNetCore.Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Entities.Extensions;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Product.Domain.Repositories;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Core.Files;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Runtime;

namespace Alabo.App.Shop.Product.Domain.Services {

    /// <summary>
    /// </summary>
    public class ProductAdminService : ServiceBase, IProductAdminService {
        /// <summary>
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>

        public ViewProductEdit GetViewProductEdit(long productId, long storeId) {
            var viewProduct = new ViewProductEdit {
                //管理员获取商品 Service<IProductService>().图片不需要处理
                Product = GetSingle(productId)
            };

            if (viewProduct.Product != null) {
                // viewProduct.ThreeAddress = Resolve<IRegionService>().GetThreeAddress(viewProduct.Product.RegionId);
                viewProduct.ProductDetail = viewProduct.Product.Detail;
                viewProduct.ProductDetail.ProductDetailExtension = viewProduct.ProductDetail.Extension.DeserializeJson<ProductDetailExtension>();
                viewProduct.Category = Resolve<ICategoryService>().GetSingle(viewProduct.Product.CategoryId);
                viewProduct.CategoryId = viewProduct.Category.Id;
                // 将商品值赋值到View中
                viewProduct = AutoMapping.SetValue(viewProduct.Product, viewProduct);
                //将商品详情值赋值到AutoConfig中
                viewProduct = AutoMapping.SetValue(viewProduct.ProductDetail, viewProduct);

                viewProduct.PriceStyleId = viewProduct.Product.PriceStyleId;
                // viewProduct.DeliveryTemplate = Service<IDeliveryTemplateService>().GetSingle(e => e.Id == viewProduct.ProductDetail.DeliveryTemplateId);//运费模板
                viewProduct.Category = Resolve<ICategoryService>().GetSingle(viewProduct.Product.CategoryId); //商品类型
                viewProduct.Classes = Resolve<IRelationIndexService>().GetRelationIds<ProductClassRelation>(productId); //商品分类
                viewProduct.ProductStatus = viewProduct.Product.ProductStatus;
                viewProduct.Tags = Resolve<IRelationIndexService>().GetRelationIds<ProductTagRelation>(productId);

                if (viewProduct.Product.Id > 0) {
                    viewProduct.Store = Resolve<IShopStoreService>().GetSingle(e => e.Id == storeId); //供应商
                    //viewProduct.ClassesStore = Service<IRelationIndexService>().GetRelationIds<StoreClassRelation>(productId);///店铺分类
                    viewProduct.ProductSkus = Resolve<IProductSkuService>().GetList(o => o.ProductId == viewProduct.Product.Id)?.ToList();
                }
            } else {
                var config = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetValue<ProductConfig>();

                var maxId = "" + (Resolve<IProductService>().MaxId() + 1);
                maxId = maxId.PadLeft(3, '0');
                viewProduct.Product = new Entities.Product {
                    Bn = config.Bn + maxId,
                    PurchasePrice = config.PurchasePrice,
                    MarketPrice = config.MarketPrice,
                    CostPrice = config.CostPrice,
                    Price = config.Price,
                    Weight = config.Weight,
                    Stock = config.Stock
                };
                viewProduct.ProductDetail = new ProductDetail();
                viewProduct.Category = new Category.Domain.Entities.Category();
                viewProduct.ProductStatus = ProductStatus.Online;
                viewProduct.Product.ProductExtensions = new ProductExtensions {
                    ProductSkus = new List<ProductSku>(),
                    ProductCategory = new Category.Domain.Entities.Category(),
                    ProductThums = new List<ProductThum>()
                };
                viewProduct.PriceStyleId = Resolve<IAutoConfigService>().GetList<PriceStyleConfig>().FirstOrDefault().Id;
                viewProduct.ProductStatus = ProductStatus.Online;
                var Categorys = Resolve<ICategoryService>().GetList(r => r.Status == Status.Normal);
                if (Categorys.Count() > 0) {
                    viewProduct.CategoryId = Categorys.FirstOrDefault().Id;
                }
            }

            viewProduct.Intro = viewProduct.ProductDetail.Intro;
            viewProduct.MobileIntro = viewProduct.ProductDetail.MobileIntro;
            viewProduct.MetaTitle = viewProduct.ProductDetail.MetaTitle;
            viewProduct.MetaKeywords = viewProduct.ProductDetail.MetaKeywords;
            viewProduct.MetaDescription = viewProduct.ProductDetail.MetaDescription;
            viewProduct.Images = viewProduct.ProductDetail.ImageJson.DeserializeJson<List<ProductThum>>().Select(o => o.OriginalUrl).JoinToString(",");

            viewProduct.ProductConfig = Resolve<IAutoConfigService>().GetValue<ProductConfig>();
            viewProduct.PriceStyleItems = Resolve<IAutoConfigService>().GetList<PriceStyleConfig>(r => r.Status == Status.Normal).OrderBy(r => r.SortOrder).ToList();

            if (viewProduct.Product.StoreId > 0) {
                viewProduct.Store = Resolve<IShopStoreService>().GetSingle(r => r.Id == viewProduct.Product.StoreId);
            }

            if (viewProduct.Product.PriceStyleId.IsGuidNullOrEmpty()) {
                viewProduct.PriceStyleConfig = viewProduct.PriceStyleItems.FirstOrDefault();
            } else {
                viewProduct.PriceStyleConfig = viewProduct.PriceStyleItems.FirstOrDefault(r => r.Id == viewProduct.Product.PriceStyleId);
            }

            return viewProduct;
        }

        /// <summary>
        /// </summary>
        /// <param name="viewProduct"></param>
        /// <param name="httpRequest"></param>

        public ServiceResult AddOrUpdate(ViewProductEdit viewProduct, HttpRequest httpRequest) {
            var result = MappingProductValue(viewProduct, httpRequest, out viewProduct); // 商品属性值处理
            if (!result.Succeeded) {
                return result;
            }

            var context = Repository<IProductRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                viewProduct.ProductDetail.ProductId = viewProduct.Product.Id;
                viewProduct.Product.RegionId = httpRequest.Form["Country"].ConvertToLong(1);
                if (viewProduct.Product.Id == 0) {
                    Resolve<IProductService>().Add(viewProduct.Product); // 添加zkshop_product 表
                    viewProduct.ProductDetail.ProductId = viewProduct.Product.Id;

                    Resolve<IProductDetailService>().Add(viewProduct.ProductDetail); // 添加zkshop_productdetai表
                } else {
                    viewProduct.Product.StoreId = viewProduct.StoreId; //供应商Id单独处理
                    Resolve<IProductService>().Update(viewProduct.Product); // 更新zkshop_product 表
                    Resolve<IProductDetailService>().UpdateNoTracking(viewProduct.ProductDetail); // 更新zkshop_productdetai表
                }

                // 更新商品Sku
                var skuResult = Resolve<IProductSkuService>().AddUpdateOrDelete(viewProduct.Product, viewProduct.ProductSkus); // 更新zkshop_productsku表
                if (!skuResult.Succeeded) {
                    throw new ArgumentException(skuResult.ToString());
                }

                //添加商品分类和标签
                Resolve<IRelationIndexService>().AddUpdateOrDelete<ProductClassRelation>(viewProduct.Product.Id, viewProduct.Classes.ToStr());
                Resolve<IRelationIndexService>().AddUpdateOrDelete<ProductTagRelation>(viewProduct.Product.Id, viewProduct.Tags.ToStr());

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            //删除缓存
            var cacheKey = "ApiProduct_" + viewProduct.Product.Id;
            ObjectCache.Remove(cacheKey);
            // 更新商品Sku价格
            Resolve<IProductSkuService>().AutoUpdateSkuPrice(viewProduct.Product.Id);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        public void Delete(long id) {
            var context = Repository<IProductRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<IProductService>().Delete(r => r.Id == id);
                Resolve<IProductDetailService>().Delete(r => r.ProductId == id);
                Resolve<IProductSkuService>().Delete(r => r.ProductId == id);
                Resolve<IRelationIndexService>().Delete(r => r.EntityId == id && r.Type == typeof(ProductClassRelation).FullName); //删除商品分类和标签
                Resolve<IRelationIndexService>().Delete(r => r.EntityId == id && r.Type == typeof(ProductTagRelation).FullName);

                context.SaveChanges();
                context.CommitTransaction();
            } catch {
                context.RollbackTransaction();
            } finally {
                context.DisposeTransaction();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="view"></param>

        public ViewProductEdit GetPageView(ViewProductEdit view) {
            //var brandList = Service<IStoreBrandService>().GetListFromCache();
            //view.BrandItems = SelectListItems.FromIEnumerable(brandList, r => r.Name, r => r.Id);
            view.ProductConfig = Resolve<IAutoConfigService>().GetValue<ProductConfig>();
            view.PriceStyleItems = Resolve<IAutoConfigService>().GetList<PriceStyleConfig>(r => r.Status == Status.Normal).OrderBy(r => r.SortOrder).ToList();
            view.StoreId = view.Product.StoreId;
            if (view.Product.StoreId > 0) {
                view.Store = Resolve<IShopStoreService>().GetSingle(r => r.Id == view.Product.StoreId);
            }

            if (view.Product.PriceStyleId.IsGuidNullOrEmpty()) {
                view.PriceStyleConfig = view.PriceStyleItems.FirstOrDefault();
            } else {
                view.PriceStyleConfig = view.PriceStyleItems.FirstOrDefault(r => r.Id == view.Product.PriceStyleId);
            }

            return view;
        }

        /// <summary>
        ///     获取商品列表
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewProductList> GetViewProductList(object query) {
            Dictionary<string, string> dic = query.ToObject<Dictionary<string, string>>();
            if (dic.TryGetValue("StoreName", out string storeName)) {
                if (!storeName.IsNullOrEmpty()) {
                    var store = Resolve<IShopStoreService>().GetSingle(r => r.Name.Contains(storeName));
                    if (store != null) {
                        dic.Add("StoreId", store.Id.ToString());
                    }
                }
            }
            var pageList = Resolve<IProductService>().GetPagedList<ViewProductList>(dic.ToJson());
            var storesId = pageList.Select(r => r.StoreId).Distinct();
            var stores = Resolve<IShopStoreService>().GetList(r => storesId.Contains(r.Id));
            pageList.ForEach(r => {
                r.StoreName = stores.FirstOrDefault(e => e.Id == r.StoreId)?.Name;
            });
            return pageList;
        }

        /// <summary>
        /// 判断类目下是否存在商品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool CheckCategoryHasProduct(Guid categoryId) {
            var query = Repository<IProductRepository>().RepositoryContext.Query<Alabo.App.Shop.Product.Domain.Entities.Product>();
            query = query.Where(o => o.CategoryId == categoryId && o.ProductStatus == ProductStatus.Online);
            return query.Count() > 0;
            //return false;
        }

        /// <summary>
        /// Mappings the product value.
        /// 商品属性值处理
        /// </summary>
        /// <param name="viewProduct">The 视图 product.</param>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="updateView">The 更新 视图.</param>
        private ServiceResult MappingProductValue(ViewProductEdit viewProduct, HttpRequest httpRequest,
            out ViewProductEdit updateView) {
            updateView = new ViewProductEdit();
            if (viewProduct.Product.PurchasePrice > viewProduct.Product.CostPrice) {
                return ServiceResult.FailedWithMessage("进货价不能大于成本价");
            }
            //如果是后台添加商品,则商品关联平台店铺
            if (viewProduct.IsAdminAddProduct) {
                var planform = Resolve<IShopStoreService>().PlanformStore();
                if (planform == null) {
                    return ServiceResult.FailedWithMessage("请先添加平台店铺");
                }

                viewProduct.Product.StoreId = planform.Id;
            }

            if (viewProduct.Product.StoreId <= 0) {
                return ServiceResult.FailedWithMessage("请为商品选择店铺");
            }

            if (viewProduct.SkuJson.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("请设置商品Sku");
            }

            var category = Resolve<ICategoryService>().GetSingle(viewProduct.CategoryId);
            if (category == null) {
                return ServiceResult.FailedWithMessage("未选择商品类目，或者商品类目已不存在");
            }

            if (Repository<IProductRepository>().Count(r => r.Bn == viewProduct.Product.Bn && r.Id != viewProduct.Product.Id) > 0) {
                return ServiceResult.FailedWithMessage("该货号已存在，请使用其他货号");
            }

            #region product表属性处理

            /// 商品表类目,价格模式，状态，显示价格处理
            //viewProduct.Product.CategoryId = (Guid)viewProduct.CategoryId;
            //viewProduct.Product.PriceStyleId = (Guid)viewProduct.PriceStyleId;

            //viewProduct.Product.RegionId = viewProduct.RegionId;

            // 对商品进行动态赋值
            viewProduct.Product = AutoMapping.SetValue(viewProduct, viewProduct.Product); // 动态赋值
            viewProduct.Product.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(viewProduct.Product.Price, viewProduct.Product.PriceStyleId, viewProduct.Product.MinCashRate);
            viewProduct.Product.ModifiedTime = DateTime.Now;

            #endregion product表属性处理

            #region productDetail表属性，处理

            if (viewProduct.ProductDetail == null) {
                viewProduct.ProductDetail = new ProductDetail();
            }

            viewProduct.ProductDetail = AutoMapping.SetValue(viewProduct, viewProduct.ProductDetail); // 动态赋值
            viewProduct.ProductDetail.Extension = viewProduct.ProductDetail.ProductDetailExtension.ToJson(); // 扩展属性序列

            ///商品属性值处理
            viewProduct.ProductDetail.PropertyJson = Resolve<ICategoryService>().AddOrUpdateOrDeleteProductCategoryData(viewProduct.Product, httpRequest);
            if (viewProduct.ProductDetail.PropertyJson.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("商品属性值处理失败，不能为空");
            }
            // 商品图片处理
            viewProduct.ProductDetail.ImageJson = CreateImage(viewProduct.Product, viewProduct.Images);

            #endregion productDetail表属性，处理

            updateView = viewProduct;
            return ServiceResult.Success;
        }

        /// <summary> 根据图片原始地址获取List<ProductThum>，同时自动生成多张图片 </summary>
        /// <param name="images">原始图片地址，多个用,隔开</param>

        private string CreateImage(Entities.Product product, string images) {
            var list = new List<ProductThum>();
            if (images.IsNullOrEmpty()) {
                return list.ToJson();
            }

            var config = Resolve<IAutoConfigService>().GetValue<ProductConfig>();
            if (config.WidthThanHeight <= 0) {
                throw new InvalidOperationException("缩略图高宽比例必须>0.");
            }

            if (config.ThumbnailWidth <= 0) {
                throw new InvalidOperationException("列表页缩略图宽度必须>0.");
            }

            if (config.ShowCaseWidth <= 0) {
                throw new InvalidOperationException("详情页橱窗图宽度必须>0.");
            }

            var i = 0;
            foreach (var item in images.SplitList(new[] { ',' })) {
                if (!item.IsNullOrEmpty()) {
                    var savePath = "";
                    var thum = new ProductThum();
                    var originalPath = "";
                    var suffixIndex = item.LastIndexOf(".", StringComparison.Ordinal);
                    var suffix = item.Substring(suffixIndex, item.Length - suffixIndex); //后缀名
                    if (item.Contains("X") || item.StartsWith("http")) //如果不是新增，上送的图片会带有X,处理为原图片
                    {
                        thum.OriginalUrl = item.Split('_')[0];
                        int width = decimal.ToInt16(config.ThumbnailWidth); //宽度
                        int height = decimal.ToInt16(config.ThumbnailWidth * config.WidthThanHeight); //高度

                        thum.ThumbnailUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        width = decimal.ToInt16(config.ShowCaseWidth); //宽度
                        height = decimal.ToInt16(config.ShowCaseWidth * config.WidthThanHeight); //高度
                        thum.ShowCaseUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        originalPath = Path.Combine(FileHelper.RootPath, thum.OriginalUrl.TrimStart('/'));
                    } else {
                        thum.OriginalUrl = item;
                        //缩略图片处理
                        int width = decimal.ToInt16(config.ThumbnailWidth); //宽度
                        int height = decimal.ToInt16(config.ThumbnailWidth * config.WidthThanHeight); //高度

                        thum.ThumbnailUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        originalPath = Path.Combine(FileHelper.RootPath, thum.OriginalUrl.TrimStart('/'));
                        //替换第二个路径前的/，避免生成相对路径
                        savePath = Path.Combine(FileHelper.RootPath, thum.ThumbnailUrl.TrimStart('/'));
                        //替换第二个路径前的/，避免生成相对路径
                        if (!File.Exists(savePath)) //图片不存在才处理
                        {
                            ImageHelper.ImageCompression(originalPath, savePath, width, height); //改成绝对路径
                        }

                        //详情页图片处理
                        width = decimal.ToInt16(config.ShowCaseWidth); //宽度
                        height = decimal.ToInt16(config.ShowCaseWidth * config.WidthThanHeight); //高度
                        thum.ShowCaseUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        savePath = Path.Combine(FileHelper.RootPath, thum.ShowCaseUrl.TrimStart('/'));
                        //替换第二个路径前的/，避免生成相对路径
                        if (!File.Exists(savePath)) //图片不存在才处理
                        {
                            ImageHelper.ImageCompression(originalPath, savePath, width, height); //改成绝对路径
                        }
                    }

                    //生成小图片
                    if (i == 0 && !item.StartsWith("http")) {
                        product.ThumbnailUrl = thum.ThumbnailUrl;
                        product.SmallUrl = $@"{thum.OriginalUrl}_50X50{suffix}";
                        savePath = Path.Combine(FileHelper.RootPath, product.SmallUrl.TrimStart('/')); //替换第二个路径前的/，避免生成相对路径
                        if (!File.Exists(savePath)) //图片不存在才处理
                        {
                            ImageHelper.ImageCompression(originalPath, savePath, 50, 50); //改成绝对路径
                        }

                        i++;
                    }

                    list.Add(thum);
                }
            }

            return list.ToJson();
        }

        /// <summary>
        ///     获得商品详情
        /// </summary>
        /// <param name="id">主键ID</param>
        public Entities.Product GetSingle(long id) {
            var product = Resolve<IProductService>().GetSingle(r => r.Id == id);
            if (product == null) {
                return product;
            }

            product.Detail = Resolve<IProductDetailService>().GetSingle(e => e.ProductId == product.Id); //商品SKU
            if (product.Detail == null) {
                return null;
            }

            product.Detail.ProductDetailExtension = product.Detail.Extension.DeserializeJson<ProductDetailExtension>();
            product.ProductExtensions = new ProductExtensions {
                ProductSkus = Resolve<IProductSkuService>().GetList(e => e.ProductId == product.Id).ToList(), //商品SKU                                                                               // ProductBrand = Service<IStoreBrandService>().GetSingle(e => e.Id == product.BrandId),// 商品品牌
                Store = Resolve<IShopStoreService>().GetSingle(e => e.Id == product.StoreId) // 商品所属店铺
            };
            product.ProductExtensions.ProductCategory = product.Detail.PropertyJson.DeserializeJson<Category.Domain.Entities.Category>();
            product.ProductExtensions.ProductThums = product.Detail.ImageJson.DeserializeJson<List<ProductThum>>();
            // product.ThumbnailUrl = Service<IApiService>().ApiImageUrl(product.ThumbnailUrl);
            //  product.SmallUrl = Service<IApiService>().ApiImageUrl(product.SmallUrl);

            return product;
        }

        public ProductAdminService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}