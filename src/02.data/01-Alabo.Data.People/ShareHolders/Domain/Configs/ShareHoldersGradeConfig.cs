using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Data.People.Users;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;

namespace Alabo.Data.People.ShareHolders.Configs
{
    /// <summary>
    ///     股东等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "股东等级", Icon = "fa fa-user-times",
        Description = "股东等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级", SideBarType = SideBarType.ShareHoldersSideBar)]
    public class ShareHoldersGradeConfig : BaseGradeConfig, IAutoConfig
    {
        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "股东等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365222");

        public void SetDefault()
        {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<ShareHoldersGradeConfig>();
            if (list.Count < 1)
            {
                var configs = new List<ShareHoldersGradeConfig>();
                var config = new ShareHoldersGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f500"),
                    Name = "投资股东",
                    Icon = "/wwwroot/static/images/GradeIcon/RewardHolders01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new ShareHoldersGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f501"),
                    Name = "运营股东",
                    Icon = "/wwwroot/static/images/GradeIcon/RewardHolders02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new ShareHoldersGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f502"),
                    Name = "核心股东",
                    Icon = "/wwwroot/static/images/GradeIcon/RewardHolders03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                config = new ShareHoldersGradeConfig
                {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f502"),
                    Name = "原始股东",
                    Icon = "/wwwroot/static/images/GradeIcon/RewardHolders04.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig
                {
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
    [ClassProperty(Name = "股东等级", Icon = "fa fa-database", Description = "股东等级", PageType = ViewPageType.List, Mark = 0,
        SideBarType = SideBarType.ShareHoldersSideBar)]
    //SideBar = "/Core/UserType/SideBar/RewardHoldersSideBar")]
    public class ShareHoldersGradeRelation : IRelation
    {
    }
}