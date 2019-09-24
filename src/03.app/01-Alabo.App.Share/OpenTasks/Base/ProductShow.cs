using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Tasks.Base {

    /// <summary>
    /// 分润在商品上面的展示
    /// </summary>
    public class ProductShow {

        /// <summary>
        ///活动显示： 商品详情页
        /// 商品详情页显示
        /// 比如：促销，限时打折、包邮、新店开张等
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false)]
        [HelpBlock(" 比如：促销，限时打折、包邮、新店开张等")]
        //[Display(Name = "商品详情页标签")]
        public string ProductTag { get; set; }

        /// <summary>
        /// 价格标签的颜色
        /// </summary>
        [Field(ControlsType = ControlsType.Color, ListShow = true, EditShow = false)]
        [HelpBlock("商品详情标签颜色")]
        //[Display(Name = "商品详情标签颜色")]
        public string ProductTagColor { get; set; }

        /// <summary>
        /// 价格促销模板
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false)]
        [HelpBlock("商品详情模板")]
        //[Display(Name = "商品详情模板")]
        public string ProductTagTemplate { get; set; }

        /// <summary>
        /// 商品详情页显示位置,1都显示,2仅pc,3仅phone,4都不显示
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = false)]
        [HelpBlock("默认为1，1都显示,2仅pc,3仅phone,4都不显示")]
        //[Display(Name = "显示位置")]
        public ViewLocation ProductViewLocation { get; set; }
    }
}