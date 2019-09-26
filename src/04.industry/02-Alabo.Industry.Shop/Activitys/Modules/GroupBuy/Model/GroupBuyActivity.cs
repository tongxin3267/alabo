using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Enum;
using Alabo.Industry.Shop.Activitys.Dtos;
using Alabo.Industry.Shop.Activitys.Extensions;
using Alabo.Industry.Shop.Products.Domain.Configs;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Mapping;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Model
{

    /// <summary>
    ///     拼图
    ///     人多优惠大
    ///     行为类活动
    ///     参考：拼多多
    /// </summary>
    /// ]
    [ActivityModule(Name = "拼团", ActivityType = ActivityType.BehaviorActivity, Icon = "fa fa-recycle",
        IsSupportSigleProduct = true, IsSupportSku = true,
        BackGround = "dashboard-stat yellow-crusta  ",
        Intro = "配置拼团策略，人多优惠大。",
        FatherId = 130001
    )]
    [ClassProperty(Name = "拼团")]
    public class GroupBuyActivity : BaseViewModel, IActivity
    {

        /// <summary>
        ///     拼团人数
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [Range(2, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        [Display(Name = "拼团达标人数")]
        [HelpBlock("请输入拼团人数，拼图的达标人数")]
        public long BuyerCount { get; set; } = 2;


        /// <summary>
        /// 拼团价格设置
        /// Mark=1表示可以批量填充数据
        /// </summary>
        [Field(ControlsType = ControlsType.JsonList, EditShow = true, Mark = "1")]
        [Display(Name = "拼团商品价格设置")]
        [HelpBlock("拼团价支持多模式,如:拼团价为100元，商城模式为50%现金+50%积分，则可用50元+50积分购买")]
        public IList<GroupBuySkuProduct> SkuProducts { get; set; } = new List<GroupBuySkuProduct>();

        public AutoForm GetAutoForm(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Sets the default.
        ///     设置默认值
        /// </summary>
        public object GetDefaultValue(ActivityEditInput activityEdit, Activity activity)
        {
            var groupBuyActivity = new GroupBuyActivity();
            if (activityEdit.ProductId > 0)
            {
                var product = Resolve<IProductService>().GetSingle(r => r.Id == activityEdit.ProductId);
                if (product != null)
                {
                    var productSkus = Resolve<IProductSkuService>().GetList(r => r.ProductId == product.Id);
                    // 新增活动
                    if (activity.Id <= 0)
                    {
                        activity.Name = "拼团" + product.Name;
                        productSkus.Foreach(r =>
                        {
                            var buySkuProduct = AutoMapping.SetValue<GroupBuySkuProduct>(r);
                            groupBuyActivity.SkuProducts.Add(buySkuProduct);
                        });
                    }
                    else
                    {
                        var groupBuyData = activity.Value.ToObject<GroupBuyActivity>();
                        groupBuyActivity = AutoMapping.SetValue(groupBuyData, groupBuyActivity);
                        groupBuyActivity.SkuProducts = new List<GroupBuySkuProduct>();
                        // 编辑活动，sku使用
                        productSkus.Foreach(r =>
                        {
                            var buySkuProduct = AutoMapping.SetValue<GroupBuySkuProduct>(r);
                            var dataSku = groupBuyData.SkuProducts.FirstOrDefault(e => e.Id == r.Id);
                            if (dataSku != null)
                            {
                                buySkuProduct.FenRunPrice = dataSku.FenRunPrice;
                                buySkuProduct.Price = dataSku.Price;
                                buySkuProduct.GroupBuyDisplayPrice = dataSku.GroupBuyDisplayPrice;
                                groupBuyActivity.SkuProducts.Add(buySkuProduct);
                            }
                        });
                    }
                }
            }

            // groupBuyActivity.SkuJson = groupBuyActivity.SkuProducts.ToJson();
            return groupBuyActivity;
        }

        /// <summary>
        ///     Sets the value.
        ///     根据前端页面表单HttpContext设置内容
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public ServiceResult SetValue(HttpContext httpContext)
        {
            var activityModule = AutoMapping.SetValue<GroupBuyActivity>(httpContext); // 活动模块
            activityModule = JsonMapping.HttpContextToExtension<GroupBuyActivity>(activityModule, httpContext);
            var priceSytles =
                Resolve<IAutoConfigService>()
                    .GetList<PriceStyleConfig>(r => r.Status == Status.Normal); // 状态不正常的商品，可能不支持价格类型
            var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes(); // 所有货币类型
            var productId = httpContext.Request.Form["ProductId"].ConvertToLong();
            var product = Resolve<IProductService>().GetSingle(r => r.Id == productId);
            if (product == null)
            {
                return ServiceResult.FailedWithMessage("商品不存在");
            }

            var priceStyleConfig = Resolve<IAutoConfigService>().GetList<PriceStyleConfig>()
                .FirstOrDefault(r => r.Id == product.PriceStyleId);
            if (product.MinCashRate == 0)
            {
                product.MinCashRate = priceStyleConfig.MinCashRate;
            }

            activityModule.SkuProducts.Foreach(r =>
            {
                r.GroupBuyDisplayPrice = Resolve<IProductAdminService>().Resolve<IProductService>()
                    .GetDisplayPrice(r.Price, priceStyleConfig.Id, priceStyleConfig.MinCashRate);
                // 如果商品中有价格参数，则使员商品中的最小抵现价格
                if (priceStyleConfig != null)
                {
                    var moneyConfig = moneyTypes.FirstOrDefault(e => e.Id == priceStyleConfig.MoneyTypeId);
                    if (moneyConfig?.RateFee == 0)
                    {
                        moneyConfig.RateFee = 1;
                    }
                    // 如果不是现金商品
                    if (priceStyleConfig.PriceStyle != PriceStyle.CashProduct)
                    {
                        r.MinPayCash = Math.Round(r.Price * priceStyleConfig.MinCashRate, 2); // 最低可使用的现金资产
                        r.MaxPayPrice = Math.Round(r.Price * (1 - priceStyleConfig.MinCashRate) / moneyConfig.RateFee,
                            2); // 最高可使用的现金资产
                    }
                    else
                    {
                        r.MaxPayPrice = 0; // 现金商品，最高可使用的虚拟资产为0
                        r.MinPayCash = r.Price; //现金商品，最低使用的现金为价格
                    }
                }
            });

            var result = ServiceResult.Success;
            result.ReturnObject = activityModule;
            return result;
        }

        public ServiceResult SetValueOfRule(object rules)
        {
            return new ServiceResult(true);
        }
    }

    /// <summary>
    ///     拼团商品价格设置
    /// </summary>
    [ClassProperty(Name = "拼团商品价格设置")]
    public class GroupBuySkuProduct : BaseViewModel
    {

        /// <summary>
        ///     商品skuId
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "商品sku")]
        public long Id { get; set; }

        /// <summary>
        ///     商品Id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "商品序号")]
        public long ProductId { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = false, SortOrder = 1)]
        [Display(Name = "商品货号")]
        [JsonIgnore]
        public string Bn { get; set; }

        /// <summary>
        ///     规格属性说明,属性、规格的文字说明 比如：绿色 XL
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = false, SortOrder = 1)]
        [Display(Name = "规格")]
        [JsonIgnore]
        public string PropertyValueDesc { get; set; }

        /// <summary>
        ///     拼团价 用户购买的价格，和订单相关，生成订单的时候，使用这个价格
        /// </summary>
        [Field(ControlsType = ControlsType.Decimal, ListShow = true, EditShow = true, SortOrder = 5)]
        [Display(Name = "拼团价")]
        public decimal Price { get; set; }

        /// <summary>
        ///     价格显示方式，最终页面上的显示价格 页面上显示价格
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = false, SortOrder = 2)]
        [Display(Name = "实际价格")]
        [JsonIgnore]
        public string DisplayPrice { get; set; }

        /// <summary>
        ///     价格显示方式，最终页面上的显示价格 页面上显示价格
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = false, SortOrder = 3)]
        [Display(Name = "拼团方式")]
        public string GroupBuyDisplayPrice { get; set; }

        /// <summary>
        ///     分润价格
        /// </summary>
        [Field(ControlsType = ControlsType.Decimal, ListShow = true, EditShow = true, SortOrder = 6)]
        [Display(Name = "拼团分润价")]
        public decimal FenRunPrice { get; set; }

        /// <summary>
        ///     Gets or sets the maximum pay amount. 最高可支付的金额
        ///     虚拟资产，最高可使用虚拟资产支付的价格
        /// </summary>
        /// <value> The maximum pay amount. </value>
        [Display(Name = "最高可支付价格")]
        public decimal MaxPayPrice { get; set; }

        /// <summary>
        ///     Gets or sets the minimum pay cash. 现金的最低支付额度
        /// </summary>
        /// <value> The minimum pay cash. </value>
        [Display(Name = "最小可支付价格")]
        public decimal MinPayCash { get; set; }
    }
}