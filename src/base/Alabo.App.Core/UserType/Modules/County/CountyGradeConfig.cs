using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.County {

    /// <summary>
    ///     县代理等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "县代理等级", Icon = "fa fa-user-times",
        Description = "县代理等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级",
        SideBarType = SideBarType.CountySideBar)]
    public class CountyGradeConfig : BaseGradeConfig, IAutoConfig {

        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "县代理等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365103");

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<CountyGradeConfig>();
            if (list.Count < 1) {
                var configs = new List<CountyGradeConfig>();
                var config = new CountyGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a369000"),
                    Name = "县代理默等级",
                    Icon = "/wwwroot/static/images/GradeIcon/County01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new CountyGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a369001"),
                    Name = "黄金县代理",
                    Icon = "/wwwroot/static/images/GradeIcon/County02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new CountyGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a369002"),
                    Name = "钻石县代理",
                    Icon = "/wwwroot/static/images/GradeIcon/County03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,
                    //// AppName = typeclassProperty.AppName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(list)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }

    /// <summary>
    ///     控制面板中 商品分类
    ///     Alabo.App.Shop.Product.Domain.CallBacks.ProductCalssRelation
    /// </summary>
    [ClassProperty(Name = "县代理等级", Icon = "fa fa-database", Description = "县代理等级", PageType = ViewPageType.List,
        Mark = 0,
        SideBarType = SideBarType.CountySideBar)]
    //SideBar = "/Core/UserType/SideBar/CountySideBar")]
    public class CountyGradeRelation : IRelation {
    }
}