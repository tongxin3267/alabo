using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Enum;
using Alabo.Industry.Shop.Activitys.Dtos;
using Alabo.Industry.Shop.Activitys.Extensions;
using Alabo.Mapping;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.Activitys.Modules.ProductNumberLimit.Model
{

    /// <summary>
    ///     拼图
    ///     人多优惠大
    ///     行为类活动
    ///     参考：拼多多
    /// </summary>
    /// ]
    [ActivityModule(Name = "商品购买数量限制", ActivityType = ActivityType.BehaviorActivity, Icon = "fa fa-recycle",
        IsSupportSigleProduct = true, IsSupportSku = true,
        BackGround = "dashboard-stat yellow-crusta  ",
        Intro = "商品最大购买数",
        FatherId = 130001
    )]
    [ClassProperty(Name = "商品购买数量限制")]
    public class ProductNumberLimitActivity : BaseViewModel, IActivity
    {

        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [Range(2, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        [HelpBlock("商品Id")]
        public long ProudctId { get; set; }

        /// <summary>
        ///     最大数量
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [Range(2, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        [HelpBlock("请输入限制购买数")]
        public long MaxCount { get; set; } = 1;

        public AutoForm GetAutoForm(object obj)
        {
            throw new System.NotImplementedException();
        }

        public object GetDefaultValue(ActivityEditInput activityEdit, Activity Activity)
        {
            return null;
        }

        /// <summary>
        ///     Sets the value.
        ///     根据前端页面表单HttpContext设置内容
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public ServiceResult SetValue(HttpContext httpContext)
        {
            var productNumberLimit = AutoMapping.SetValue<ProductNumberLimitActivity>(httpContext); // 活动模块
            //activityModule = JsonMapping.HttpContextToExtension<GroupBuyActivity>(activityModule, httpContext);
            //var priceSytles =
            //    Resolve<IAutoConfigService>()
            //        .GetList<PriceStyleConfig>(r => r.Status == Status.Normal); // 状态不正常的商品，可能不支持价格类型
            //var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes(); // 所有货币类型
            //var productId = httpContext.Request.Form["ProductId"].ConvertToLong();
            //var product = Resolve<IProductService>().GetSingle(r => r.Id == productId);
            //if (product == null) {
            //    return ServiceResult.FailedWithMessage("商品不存在");
            //}

            //var priceStyleConfig = Resolve<IAutoConfigService>().GetList<PriceStyleConfig>()
            //    .FirstOrDefault(r => r.Id == product.PriceStyleId);
            //if (product.MinCashRate == 0) {
            //    product.MinCashRate = priceStyleConfig.MinCashRate;
            //}

            var result = ServiceResult.Success;
            result.ReturnObject = productNumberLimit;
            return result;
        }

        public ServiceResult SetValueOfRule(object rules)
        {
            return new ServiceResult(true);
        }
    }
}