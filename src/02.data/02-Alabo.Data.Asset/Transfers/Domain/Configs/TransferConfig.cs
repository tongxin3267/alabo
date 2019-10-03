using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Alabo.App.Asset.Transfers.Domain.Configs
{
    /// <summary>
    ///     账户转账设置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "账户转账设置", Icon = "fa fa-exchange", Description = "账户转账设置", PageType = ViewPageType.List,
        SortOrder = 23, SideBarType = SideBarType.TransferSideBar)]
    public class TransferConfig : AutoConfigBase, IAutoConfig
    {
        /// <summary>
        ///     转账名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "25%")]
        [Display(Name = "转账名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     转出货币类型 付款货币类型
        ///     将MoneyTypeConfig 配置构建为下拉菜单
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, DataSource =
            "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig")]
        [HelpBlock("比如现金转积分，则转出货币类型为现金")]
        [Display(Name = "付款货币类型")]
        //[Required]
        public Guid OutMoneyTypeId { get; set; }

        /// <summary>
        ///     转入货币类型 收款货币类型
        ///     将MoneyTypeConfig 配置构建为下拉菜单
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, DataSource =
            "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig")]
        [HelpBlock("比如现金转积分，则转入货币类型为积分")]
        [Display(Name = "收款货币类型")]
        //[Required]
        public Guid InMoneyTypeId { get; set; }

        /// <summary>
        ///     转换费率
        ///     将MoneyTypeConfig 配置构建为下拉菜单
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "转换费率")]
        [HelpBlock("不同货币类型的转换费率，如果为空时根据货币本身的费率进行转换.1表示等比转换。例如转账金额为100，如果是0.90则表示收到的金额为90，如果,0.11表示收到的金额为110")]
        [Required]
        public decimal Fee { get; set; }

        /// <summary>
        ///     手续费
        ///     将MoneyTypeConfig 配置构建为下拉菜单
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "手续费")]
        [HelpBlock("手续费转出账户的比例,例如转出金额100，手续费0.05 实际转出到账95，手续费5")]
        [Required]
        public decimal ServiceFee { get; set; }

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "可转他人")]
        [HelpBlock("表示该类型能不能转账给其他人，会员体外可转账")]
        [Required]
        public bool CanTransferOther { get; set; }

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "可转自己")]
        [HelpBlock("表示该类型能不能转账给其自己，会员体系内转账")]
        [Required]
        public bool CanTransferSelf { get; set; }

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "推荐关系限制")]
        [HelpBlock("开启以后。只有在组织架构图上，树形结构有推荐关系的才可以转账")]
        public bool IsRelation { get; set; } = false;

        /// <summary>
        ///     转账说明
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [Display(Name = "转账说明")]
        public string Intro { get; set; }

        /// <summary>
        ///     转账说明
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [Display(Name = "转账必须是多少的倍数，0表示没有倍数限制")]
        public long Multiple { get; set; } = 0;

        /// <summary>
        ///     设置默认值
        /// </summary>
        public void SetDefault()
        {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<TransferConfig>();
            if (list.Count == 0)
            {
                var configs = new List<TransferConfig>();
                var config = new TransferConfig();

                var moneyTypes = Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();

                //现金转现金(体外转）
                var cnyType = moneyTypes.FirstOrDefault(r => r.Currency == Currency.Cny);
                if (cnyType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "现金转现金(体外转）",
                        CanTransferOther = true,
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000001"),
                        Fee = 1,
                        OutMoneyTypeId = cnyType.Id,
                        InMoneyTypeId = cnyType.Id,
                        Intro = "现金转现金，可以转账给其他会员，不能转账给自己，转账费率为1:1"
                    };
                    configs.Add(config);
                }

                //积分转转积分(体外转）
                var pointType = moneyTypes.FirstOrDefault(r => r.Currency == Currency.Point);
                if (pointType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "积分转积分(体外转）",
                        CanTransferOther = true,
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000002"),
                        Fee = 1,
                        OutMoneyTypeId = pointType.Id,
                        InMoneyTypeId = pointType.Id,
                        Intro = "积分转现积分，可以转账给其他会员，不能转账给自己，转账费率为1:1"
                    };
                    configs.Add(config);
                }

                //虚拟币转虚拟币(体外转）
                var virtualType = moneyTypes.FirstOrDefault(r => r.Currency == Currency.Virtual);
                if (pointType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "虚拟币转虚拟币(体外转）",
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000003"),
                        CanTransferOther = true,
                        Fee = 1,
                        OutMoneyTypeId = virtualType.Id,
                        InMoneyTypeId = virtualType.Id,
                        Intro = "虚拟币转现虚拟币，可以转账给其他会员，不能转账给自己，转账费率为1:1"
                    };
                    configs.Add(config);
                }

                //授信转授信(体外转）
                var creditType = moneyTypes.FirstOrDefault(r => r.Currency == Currency.Credit);
                if (pointType != null && creditType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "授信转授信(体外转）",
                        CanTransferOther = true,
                        Fee = 1,
                        OutMoneyTypeId = creditType.Id,
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000004"),
                        InMoneyTypeId = creditType.Id,
                        Intro = "授信转授信，可以转账给其他会员，不能转账给自己，转账费率为1:1"
                    };
                    configs.Add(config);
                }

                //现金转积分(体内体外转）
                if (pointType != null && cnyType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "现金转积分(体内体外转）",
                        CanTransferOther = true,
                        CanTransferSelf = true,
                        Fee = 10,
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000005"),
                        OutMoneyTypeId = cnyType.Id,
                        InMoneyTypeId = pointType.Id,
                        Intro = "授信转授信，可以转账给其他会员，能转账给自己，转账费率为1:10，表示1单位现金可转10单位积分"
                    };
                    configs.Add(config);
                }

                //现金转授信(体内）
                if (creditType != null && cnyType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "现金转授信(体内）",
                        CanTransferOther = false,
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000006"),
                        CanTransferSelf = true,
                        Fee = 10,
                        OutMoneyTypeId = cnyType.Id,
                        InMoneyTypeId = creditType.Id,
                        Intro = "现金转授信(体内），不可以转账给其他会员，只能转账给自己，转账费率为1:1，表示1单位现金可转10单位积分"
                    };
                    configs.Add(config);
                }

                //现金转虚拟币(体内）
                if (virtualType != null && cnyType != null)
                {
                    config = new TransferConfig
                    {
                        Name = "现金转虚拟币(体内）",
                        CanTransferOther = false,
                        CanTransferSelf = true,
                        Fee = 10,
                        Id = Guid.Parse("E0000000-1479-49BD-BFC7-E00000000007"),
                        OutMoneyTypeId = cnyType.Id,
                        InMoneyTypeId = virtualType.Id,
                        Intro = "现金转虚拟币(体内），不可以转账给其他会员，只能转账给自己，转账费率为1:10，表示1单位现金可转10单位虚拟币"
                    };
                    configs.Add(config);
                }

                
                var autoConfig = new AutoConfig
                {
                    Type = config.GetType().FullName,
                    // AppName = typeclassProperty.AppName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}