using Alabo.App.Share.Rewards.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.OpenTasks.Base
{
    public class ProductRule
    {
        /// <summary>
        ///     价格类型
        /// </summary>
        /// >
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = false)]
        [HelpBlock("0:售价 1：分润价 2：纯利润")]
        //[Display(Name = "价格类型", Description = "0:售价 1：分润价 2：纯利润")]
        public PriceType AmountType { get; set; } = PriceType.FenRun;

        /// <summary>
        ///     商品模式
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = false)]
        [HelpBlock("默认为所有商品")]
        //[Display(Name = "商品模式", Description = "默认为所有商品")]
        public ProductModelType ProductModel { get; set; } = ProductModelType.ShoppingMall;

        /// <summary>
        ///     商品价格模式的配置Id
        ///     与PriceStyleConfig 对应
        /// </summary>
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = false)]
        [HelpBlock(" 请选择所属商城")]
        //[Display(Name = "所属商城")]
        public Guid PriceStyleId { get; set; } = Guid.Parse("e0000000-1478-49bd-bfc7-e73a5d699000");

        /// <summary>
        ///     产品线
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false)]
        [HelpBlock("商品模式为产品线模式时才有效,请填写产品线ID,以英文符号,分隔开。不填写则无限制。")]
        //[Display(Name = "产品线", Description = "商品模式为产品线模式时才有效,请填写产品线ID,以英文符号,分隔开。不填写则无限制。")]
        public string ProductLines { get; set; }

        /// <summary>
        ///     设置为false，该用户没有购买该供应商的商品也可以产生分润;如果设置为true，分润用户必须购买该供应商的商品后，才可以分润
        /// </summary>
        [Display(Name = "分润用户需购买过该供应商商品")]
        public bool IsLimitStoreBuy { get; set; } = false;
    }
}