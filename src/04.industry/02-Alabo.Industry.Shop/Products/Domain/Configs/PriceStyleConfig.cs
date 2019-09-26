using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using System.Reflection;

using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Product.Domain.CallBacks {

    /// <summary>
    ///     商城配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "商城配置", GroupName = "Shop", Icon = "fa fa-fire",
        PageType = ViewPageType.List, Description = "商城配置,不同的商城使用不同的购买模式购买",
        SideBarType = SideBarType.ProductSideBar, SortOrder = 400)]
    public class PriceStyleConfig : AutoConfigBase, IAutoConfig {

        /// <summary>
        ///     名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true, Width = "12%")]
        [StringLength(12, MinimumLength = 2, ErrorMessage = "商城模式名称长度在2-8之间")]
        [Required(ErrorMessage = "名称不能为空")]
        [Display(Name = "名称")]
        [Main]
        [HelpBlock("商城模式名称长度在2-12之间")]
        public string Name { get; set; }

        /// <summary>
        ///     系统类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 1, ListShow = true,
            DataSource = "Alabo.App.Shop.Product.Domain.Enums.PriceStyle")]
        [Display(Name = "系统类型")]
        [HelpBlock("附加货币。常见货币类型：人民币，积分、卡券、红包、虚拟币、授信、美元等")]
        public PriceStyle PriceStyle { get; set; } = PriceStyle.CashProduct;

        [Field(ControlsType = ControlsType.TextBox, SortOrder = 3, ListShow = false, EditShow = false)]
        [Display(Name = "商城首页标识")]
        [HelpBlock(
            "商城首页网址=/Mall/+商城首页标识,系统类型为非自定义类型时商城首页为系统指定,请不要更改，比如积分商城首页为/Mall/Point。自定义类型时候商城首页手动指定并在视图目录下创建相应的视图,")]
        public string IndexUrl { get; set; }

        /// <summary>
        ///     现金最低比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4, ListShow = true)]
        [Required]
        [Display(Name = "现金最低比例")]
        [Main]
        [HelpBlock("商品价格模式的现金最低比例,当与最高比例相同时，以现金最低比例为参考。如 100元最低比例为0.9，则要求现金最低90元")]
        [Range(typeof(decimal), "0.00", "1", ErrorMessage = "现金比例格式不正确")]
        public decimal MinCashRate { get; set; }

        ///// <summary>
        ///// 现金最高比例
        ///// </summary>
        //[Field("现金最高比例", ControlsType.TextBox, SortOrder = 3, ListShow = true)]
        //[Required]
        //[Display(Name = "现金最高比例"), Main]
        //[HelpBlock("商品价格模式的现金最高比例")]
        //[Range(typeof(decimal), "0.00", "1", ErrorMessage = "现金比例格式不正确")]
        //public decimal MaxCashRate { get; set; } = 1;

        /// <summary>
        ///     附加货币，不附加货币时为空
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 4, ListShow = false,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig")]
        [Display(Name = "附加货币")]
        [HelpBlock("附加货币。常见货币类型：人民币，积分、卡券、红包、虚拟币、授信、美元等")]
        public Guid MoneyTypeId { get; set; }

        //[Field("开启高级编辑器", ControlsType.Switch, SortOrder = 6, ListShow = true)]
        //[Required]
        //[Display(Name = "开启高级编辑器"), Main]
        //[HelpBlock("如果商城模式需要特殊字段时候，可以使用高级编辑器，需要开启设置所属高级编辑器")]
        //public bool IsUserAdvanceEditor { get; set; } = false;

        //[Field("所属高级编辑器", ControlsType.DropdownList, SortOrder = 6, ListShow = false, DataSource = "Alabo.Framework.Basic.Relations.Domain.Entities.AdvanceEditor")]
        //[Display(Name = "所属高级编辑器")]
        //[HelpBlock("如果商城模式需要特殊字段时候，可以使用高级编辑器，需要开启开启高级编辑器.<a href='/Admin/AdvanceEditor/index'>配置高级编辑器</a>")]
        //public long AdvanceEditorId { get; set; }

        //[Field("编辑器名称", ControlsType.TextBox, SortOrder = 6, ListShow = false)]
        //[Display(Name = "编辑器名称")]
        //[Required]
        //[StringLength(8, MinimumLength = 2, ErrorMessage = "名称长度为2-8字符")]
        //[HelpBlock("配置编辑的名称，便于更容易理解编辑器,名称长度为2-8字符")]
        //public string AdvanceEditorName { get; set; } = "行业数据";

        ///// <summary>
        ///// 是否支持前台发布
        ///// </summary>
        //[Field("供应商发布权重", ControlsType.Numberic, SortOrder = 8, ListShow = false)]
        //[Display(Name = "供应商发布权重")]
        //[HelpBlock("供应商发布权重，如果供应商权限权重大于改值时，供应商可以使用该商城，否则不可以使用,为0的时候，供应商不支持发布")]
        //public int FrontPublishLevel { get; set; } = 0;

        ///// <summary>
        ///// 说明
        ///// </summary>
        //[Field("说明", ControlsType.TextArea, SortOrder = 11, ListShow = false)]
        //[Display(Name = "货币说明")]
        //public string Intro { get; set; }

        //[Field("是否显示运费模板", ControlsType.Switch, SortOrder = 5, ListShow = true)]
        //[Display(Name = "是否显示运费模板"), Main]
        //[HelpBlock("是否显示运费模板")]
        //public bool IsShowDeliveryTemplateId { get; set; } = true;

        public void SetDefault() {
            var list = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<PriceStyleConfig>();
            var moneyTypelist = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            if (moneyTypelist.Count == 0) {
                new MoneyTypeConfig().SetDefault();
            }

            if (list.Count == 0) {
                var configs = new List<PriceStyleConfig>();
                var config = new PriceStyleConfig();
                MoneyTypeConfig mt = null;
                moneyTypelist = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
                foreach (PriceStyle item in Enum.GetValues(typeof(PriceStyle))) {
                    if (Convert.ToInt32(item) >= 0) {
                        config = new PriceStyleConfig {
                            //Intro = item.GetDisplayName(),
                            // MaxCashRate = 1.00m,
                            MinCashRate = 0.50m,

                            Name = item.GetDisplayName()
                        };
                        if (item == PriceStyle.Customer) {
                            config.Id = Guid.NewGuid();
                        } else {
                            config.Id = item.GetFieldAttribute().GuidId.ToGuid();
                            config.IndexUrl = item.GetFieldAttribute().Mark; ///首页
                        }

                        switch (item) {
                            case PriceStyle.CashAndCredit: //现金+授信
                                config.PriceStyle = PriceStyle.CashAndCredit;
                                config.Status = Status.Normal;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Voucher);
                                config.MinCashRate = 0.5m;
                                break;

                            case PriceStyle.CashAndPoint: //现金+积分
                                config.PriceStyle = PriceStyle.CashAndPoint;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Point);
                                config.MinCashRate = 0.5m;
                                break;

                            case PriceStyle.CashAndVirtual: //现金+虚拟币
                                config.PriceStyle = PriceStyle.CashAndVirtual;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Virtual);
                                config.MinCashRate = 0.5m;
                                break;

                            case PriceStyle.CashProduct: //全部由现金购买
                                config.MinCashRate = 1;
                                config.PriceStyle = PriceStyle.CashProduct; //全部现金购买的商品 MoneyTypeId 为空
                                config.SortOrder = 1;
                                config.MinCashRate = 1;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Cny);
                                break;

                            case PriceStyle.CreditProduct: //全部由授信购买
                                config.PriceStyle = PriceStyle.CreditProduct;
                                config.MinCashRate = 0;
                                config.Status = Status.Freeze;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Credit);
                                break;

                            case PriceStyle.Customer:
                                config.PriceStyle = PriceStyle.Customer;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Custom);
                                break;

                            case PriceStyle.PointProduct: //积分购买
                                config.PriceStyle = PriceStyle.PointProduct;
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Point);
                                config.MinCashRate = 0;
                                break;

                            case PriceStyle.VirtualProduct: //全部由虚拟币购买
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Virtual);
                                config.PriceStyle = PriceStyle.VirtualProduct;
                                config.MinCashRate = 0;
                                break;

                            case PriceStyle.ShopAmount:     // 消费额
                                mt = moneyTypelist.FirstOrDefault(p => p.Currency == Currency.Withdrawal);
                                config.PriceStyle = PriceStyle.ShopAmount;
                                config.Status = Status.Normal;
                                config.MinCashRate = 0;
                                break;
                        }

                        if (mt == null) {
                            continue;
                        }

                        config.MoneyTypeId = mt.Id;
                        if (Convert.ToInt32(item) > 1000) {
                            config.Status = Status.Freeze;
                        }

                        configs.Add(config);
                    }
                }

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,

                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}