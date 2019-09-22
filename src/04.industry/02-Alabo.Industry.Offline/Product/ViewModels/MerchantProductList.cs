using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Offline.Merchants.Domain.Services;
using Alabo.App.Offline.Product.Domain.CallBacks;
using Alabo.App.Offline.Product.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Offline.Product.Domain.Entities
{
    /// <summary>
    /// 商品列表
    /// </summary>
    [ClassProperty(Name = "商品列表", PageType = ViewPageType.List)]
    public class MerchantProductList : UIBase, IAutoTable<MerchantProductList>
    {
        /// <summary>
        /// 商品id
        /// </summary>
        [Display(Name = "商品id")]
        public string Id { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string MerchantStoreId { get; set; }

        /// <summary>
        /// 缩略图的URL,通过主图生成
        /// </summary>
        [Display(Name = "缩略图")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true)]
        public string Name { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string MerchantStoreName { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        [Display(Name = "商品分类")]
        public long ClassId { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        [Display(Name = "商品分类")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true)]
        public string ClassName { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        [Display(Name = "商品单位")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string Unit { get; set; }

        /// <summary>
        /// sku id
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// Sku名称
        /// </summary>
        [Display(Name = "规格名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string SkuName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal Price { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        [Display(Name = "销售数量")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public long SoldCount { get; set; }

        /// <summary>
        /// 商品库存
        /// </summary>
        [Display(Name = "商品库存")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public long Stock { get; set; }

        /// <summary>
        /// 商品Sku
        /// </summary>
        [Display(Name = "商品Sku")]
        public List<MerchantProductSku> Skus { get; set; } = new List<MerchantProductSku>();

        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("编辑", "/User/Merchant/Product/Edit",TableActionType.ColumnAction),
                ToLinkAction("删除商品", "/Api/MerchantProduct/Delete",ActionLinkType.Delete,TableActionType.ColumnAction)
            };

            return list;
        }

        /// <summary>
        /// page table
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<MerchantProductList> PageTable(object query, AutoBaseModel autoModel)
        {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId");
            dic = dic.RemoveKey("type");
            //express
            var allStores = Resolve<IMerchantStoreService>().GetList().ToList();
            var allRelations = Resolve<IRelationService>().GetClass(typeof(MerchantProductClassRelation).FullName).ToList();
            var model = ToQuery<MerchantProductList>();
            var expressionQuery = new ExpressionQuery<MerchantProduct>();
            if (!string.IsNullOrWhiteSpace(model.ClassName))
            {
                var relation = allRelations.Find(r => r.Name.Contains(model.ClassName));
                if (relation != null)
                {
                    expressionQuery.And(e => e.ClassId == relation.Id);
                }
            }
            //query
            var apiService = Resolve<Alabo.App.Core.Api.Domain.Service.IApiService>();
            var list = Resolve<IMerchantProductService>().GetPagedList<MerchantProductList>(dic.ToJson(), expressionQuery.BuildExpression());
            list.ForEach(item =>
            {
                //store
                var store = allStores.Find(s => s.Id == item.MerchantStoreId.ToObjectId());
                item.MerchantStoreName = store?.Name;
                item.ThumbnailUrl = apiService.ApiImageUrl(item.ThumbnailUrl);

                //relation
                var relation = allRelations.Find(r => r.Id == item.ClassId);
                item.ClassName = relation?.Name;

                //item.sku
                var sku = item.Skus.FirstOrDefault();
                if (sku != null)
                {
                    item.SkuName = sku.Name;
                    item.Price = sku.Price;
                }
                item.Stock = item.Skus.Sum(s => s.Stock);
            });

            return ToPageResult(list);
        }
    }
}