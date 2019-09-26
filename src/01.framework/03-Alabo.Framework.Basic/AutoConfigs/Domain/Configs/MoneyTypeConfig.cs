using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.UI.Enum;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.CallBacks {

    /// <summary>
    ///     货币类型配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "货币类型", Icon = "fa fa-cny", Description = "支付方式",
        PageType = ViewPageType.List, SortOrder = 20,
        Validator = "select 1 from Asset_Account where MoneyTypeId ='{0}'", ValidateMessage = "当前有账户正在使用当前货币",
        SideBarType = SideBarType.ControlSideBar)]
    public class MoneyTypeConfig : AutoConfigBase, IAutoConfig {

        /// <summary>
        ///     人民币
        /// </summary>
        public static readonly Guid CNY = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000");

        /// <summary>
        ///     积分
        /// </summary>
        public static readonly Guid Point = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699002");

        /// <summary>
        ///     虚拟币
        /// </summary>
        public static readonly Guid Virtual = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699003");

        /// <summary>
        ///     授信
        /// </summary>
        public static readonly Guid Credit = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699005");

        /// <summary>
        ///     消费额
        /// </summary>
        public static readonly Guid ShopAmount = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699756");

        /// <summary>
        /// 优惠券
        /// </summary>
        public static readonly Guid Preferential = Guid.Parse("e97ccd1e-1478-49bd-bfc7-e73a5d699009");

        /// <summary>
        ///     设置默认值
        /// </summary>
        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            if (list == null || list.Count == 0) {
                var configs = new List<MoneyTypeConfig>();
                var config = new MoneyTypeConfig();
                foreach (Currency item in Enum.GetValues(typeof(Currency))) {
                    config = new MoneyTypeConfig {
                        Currency = item,
                        Unit = item.GetFieldAttribute().Mark
                    };
                    var color = item.GetFieldAttribute().Selection;
                    if (!color.IsNullOrEmpty()) {
                        config.BackGroudColor = (ColorLibrary)Enum.Parse(typeof(ColorLibrary), color);
                    }

                    if (item.GetFieldAttribute().SortOrder > 0) {
                        SortOrder = item.GetFieldAttribute().SortOrder;
                    }

                    if (config.Currency == Currency.Custom) {
                        config.Id = Guid.NewGuid();
                    } else {
                        config.Id = item.GetFieldAttribute().GuidId.ToGuid();
                    }

                    config.Name = item.GetDisplayName();
                    if (item.IsDefault()) {
                        config.Status = Status.Normal;
                    } else {
                        config.Status = Status.Freeze;
                    }

                    configs.Add(config);
                }

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,
                    //// AppName = typeclassProperty.AppName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }

        #region

        /// <summary>
        ///     货币类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 1, EnumUniqu = true, ListShow = true,
            DataSource = "Alabo.Framework.Core.Enums.Enum.Currency")]
        [Display(Name = "货币类型")]
        [HelpBlock("除自定义货币以外，一种货币类型只能添加一次。如需添加非系统指定货币，可选择自定义类型。常见货币类型：人民币，积分、卡券、红包、虚拟币、授信、美元等")]
        public Currency Currency { get; set; }

        /// <summary>
        ///     货币名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, IsMain = true, ListShow = true, Width = "110", IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        [Required]
        [Display(Name = "货币名称")]
        [Main]
        public string Name { get; set; }

        /// <summary>
        ///     是否默认 0：不是，1：是默认
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 3, ListShow = true, IsShowAdvancedSerach = true)]
        [Display(Name = "是否默认")]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        ///     是否可提现 0：不是，1：是默认
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 3)]
        [Display(Name = "是否可提现")]
        public bool IsWithDraw { get; set; } = false;

        /// <summary>
        ///     是否可以充值
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 3)]
        [Display(Name = "是否可以充值")]
        public bool IsRecharge { get; set; }

        /// <summary>
        ///     背景颜色
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 3, ListShow = true,
            DataSource = "Alabo.Framework.Core.UI.Enum.ColorLibrary", DisplayMode = DisplayMode.Text)]
        [Display(Name = "背景颜色")]
        [HelpBlock("目前不是所有的下拉框的颜色都支持，请认真验证")]
        public ColorLibrary BackGroudColor { get; set; } = ColorLibrary.Blue;

        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4, ListShow = true)]
        [Required]
        [Display(Name = "货币单位")]
        [HelpBlock("常用货币单位:元、积分、豆、币、珠、磅")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "货币名称的长度为1-8之间")]
        public string Unit { get; set; } = "币";

        /// <summary>
        ///     货币费率
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 5, ListShow = true)]
        [HelpBlock("与人民币的转换费率，比如美元与人民币，积分与虚拟币。示列积分与人人民币：比如费率为0.5,人民币1元=2积分，费率为2，人民币1元=0.5积分")]
        [Display(Name = "费率")]
        [Required]
        [Range(0.01, 10, ErrorMessage = "费率格式不正确")]
        public decimal RateFee { get; set; } = 1;

        /// <summary>
        ///     Gets or sets the fee rate.
        ///     服务费比例
        /// </summary>
        /// <value>The fee rate.</value>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 3, EditShow = true)]
        [Display(Name = "服务费比例")]
        [HelpBlock("虚拟资产服务费比例，在使用虚拟资产进行商品购买时候的比例。比如某订单使用了100积分进行购买，服务费比例是0.1,则该订单的服务费为100*0.1=10元")]
        [Range(typeof(decimal), "0.00", "1", ErrorMessage = "服务费比例")]
        public decimal ServiceRateFee { get; set; } = 0m;

        /// <summary>
        ///     货币额度
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 5)]
        [Display(Name = "额度")]
        public long Limit { get; set; }

        /// <summary>
        ///     货币符号
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 6)]
        [Display(Name = "货币符号")]
        [HelpBlock(
            "常用标志：￥、฿、 Bs、Br、₵ 、₡、₫ 、€、ƒ 、Ft、Rs、₭ 、kr、￡、₤ 、Lm、₥、₦ 、₱ 、P 、Q、 R、 Sk、Rp、৲৳、R$、S、〒、₮、₩、 ¥、 NT、￥       ")]
        public string Symbol { get; set; }

        /// <summary>
        ///     货币代码
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 7)]
        [Display(Name = "货币代码")]
        public string Code { get; set; }

        /// <summary>
        ///     货币图标
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 8)]
        [Display(Name = "货币图标")]
        public string Icon { get; set; }

        /// <summary>
        ///     货币说明
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 10)]
        [Display(Name = "货币说明")]
        public string Intro { get; set; }

        /// <summary>
        ///     会员中心是否显示
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 10, ListShow = true)]
        [Display(Name = "前台是否显示")]
        public bool IsShowFront { get; set; } = true;

        #endregion
    }
}