using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.AutoConfigs.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Callbacks {

    /// <summary>
    ///     用户等级设置
    /// </summary>
    /// <seealso cref="Alabo.App.Core.User.BaseGradeConfig" />
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

        /// <summary>
        ///     Gets or sets the discount.
        /// </summary>
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
            var list = Ioc.Resolve<IAlaboAutoConfigService>().GetList<UserGradeConfig>();
            if (list.Count < 1) {
                var configs = new List<UserGradeConfig>();
                var config = new UserGradeConfig {
                    Id = Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366000"),
                    Name = "红钻会员",
                    Icon = @"https://s-open.qiniuniu99.com//wwwroot/uploads/api/2019-06-13/5d0231d97d430d0ffc85242b.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new UserGradeConfig {
                    Id = Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366001"),
                    Name = "金钻会员",
                    Icon = @"https://s-open.qiniuniu99.com//wwwroot/uploads/api/2019-06-13/5d0231d97d430d0ffc85242c.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new UserGradeConfig {
                    Id = Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366002"),
                    Name = "紫钻会员",
                    Icon = @"https://s-open.qiniuniu99.com//wwwroot/uploads/api/2019-06-13/5d0231d97d430d0ffc85242d.png ",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,

                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(list)
                };
                Ioc.Resolve<IAlaboAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }

    /// <summary>
    /// </summary>
    public class GradeHelper {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        /// <value>
        ///     Id标识
        /// </value>
        [Display(Name = "Id标识")]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [Display(Name = "名称")]
        public string Name { get; set; }
    }
}