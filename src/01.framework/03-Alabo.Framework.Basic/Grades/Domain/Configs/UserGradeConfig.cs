using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Framework.Basic.Grades.Domain.Configs {

    /// <summary>
    ///     用户等级设置
    /// </summary>
    /// <seealso cref="BaseGradeConfig" />
    /// <seealso cref="IAutoConfig" />
    [NotMapped]
    [ClassProperty(Name = "用户等级", Icon = "fa fa-user-times",
        Description = "用户等级", PageType = ViewPageType.List, SortOrder = 12,
        SideBarType = SideBarType.UserSideBar,
        ValidateMessage = "该会员等级下存在用户，或者该等级为默认等级不能删除")]
    public class UserGradeConfig : BaseGradeConfig, IAutoConfig {

        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        /// <value>
        ///     The user type identifier.
        /// </value>
        //[Field("用户类型", ControlsType.DropdownList, ListShow = false,EditShow =false, Width = "10%", DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1, GroupTabId = 1)]
        [Display(Name = "用户类型")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365000");

        /// <summary>
        ///     Gets or sets the with draw discount.
        /// </summary>
        /// <value>
        ///     The with draw discount.
        /// </value>
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = false, Width = "10%", GroupTabId = 1)]
        [HelpBlock("在提现手续费的基础上进行折扣优惠，如0.5即20%，则提现手续费打5折")]
        [Range(0.00, 1, ErrorMessage = "折扣范围必须在0~1之间")]
        [Display(Name = "提现手续费减免")]
        public decimal WithDrawDiscount { get; set; } = 0;

        /// <value>
        ///     The discount.
        /// </value>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "10%", GroupTabId = 1)]
        [HelpBlock("商城购物折扣,0.1表示折扣为10%，1表示折扣为0，不同的会员等级折扣不一样")]
        [Range(0.00, 1, ErrorMessage = "折扣范围必须在0~1之间")]
        [Display(Name = "折扣")]
        public decimal Discount { get; set; } = 1;

        [Field(ControlsType = ControlsType.Switch, ListShow = true, GroupTabId = 1)]
        [HelpBlock("用户端是否可见")]
        [Display(Name = "是否可见")]
        public bool Visible { get; set; } = true;

        /// <summary>
        ///     Sets the default.
        /// </summary>
        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            if (list.Count < 1) {
                var config = new UserGradeConfig {
                    Id = Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366000"),
                    Name = "会员默认等级",
                    Icon = "/wwwroot/static/images/GradeIcon/User01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new UserGradeConfig {
                    Id = Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366001"),
                    Name = "钻石会员",
                    Icon = "/wwwroot/static/images/GradeIcon/User02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new UserGradeConfig {
                    Id = Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366002"),
                    Name = "黄金会员",
                    Icon = "/wwwroot/static/images/GradeIcon/User03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(list)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}