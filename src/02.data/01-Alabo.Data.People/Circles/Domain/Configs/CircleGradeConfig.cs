using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.Data.People.Users;
using Alabo.Reflections;

namespace ZKCloud.App.Core.UserType.Modules.Circle
{
    /// <summary>
    ///     商圈等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "商圈等级", Icon = "fa fa-user-times",
        Description = "商圈等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级",
        SideBarType = SideBarType.CircleSideBar)]
    //SideBar = "/Core/UserType/SideBar/CircleSideBar")]
    public class CircleGradeConfig : BaseGradeConfig, IAutoConfig
    {
        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "商圈等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365104");

        public void SetDefault()
        {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<CircleGradeConfig>();
            if (list.Count < 1)
            {
                var configs = new List<CircleGradeConfig>();
                var config = new CircleGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a367000"),
                    Name = "商圈默认等级",
                    Icon = "/wwwroot/static/images/GradeIcon/Circle01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new CircleGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a367001"),
                    Name = "商圈会员",
                    Icon = "/wwwroot/static/images/GradeIcon/Circle02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new CircleGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a367002"),
                    Name = "商圈会员",
                    Icon = "/wwwroot/static/images/GradeIcon/Circle03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig
                {
                    Type = config.GetType().FullName,
                    // AppName = typeclassProperty.AppName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(list)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}