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

namespace Alabo.App.Core.UserType.Modules.StrategicPartners {

    /// <summary>
    ///     合作伙伴等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "合作伙伴等级", Icon = "fa fa-user-times",
        Description = "合作伙伴等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级",
        SideBarType = SideBarType.VentureCompanySideBar)]
    public class StrategicPartnersGradeConfig : BaseGradeConfig, IAutoConfig {

        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "合作伙伴等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365555");

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<StrategicPartnersGradeConfig>();
            if (list.Count < 1) {
                var configs = new List<StrategicPartnersGradeConfig>();
                var config = new StrategicPartnersGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f600"),
                    Name = "合作伙伴默认等级",
                    Icon = "/wwwroot/static/images/GradeIcon/StrategicPartners01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new StrategicPartnersGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f601"),
                    Name = "黄金合作伙伴",
                    Icon = "/wwwroot/static/images/GradeIcon/StrategicPartners02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new StrategicPartnersGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f602"),
                    Name = "钻石合作伙伴",
                    Icon = "/wwwroot/static/images/GradeIcon/StrategicPartners03.png",
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
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }

    /// <summary>
    ///     控制面板中 商品分类
    ///     Alabo.App.Shop.Product.Domain.CallBacks.ProductCalssRelation
    /// </summary>
    [ClassProperty(Name = "合作伙伴等级", Icon = "fa fa-database", Description = "合作伙伴等级", PageType = ViewPageType.List,
        Mark = 0,
        SideBarType = SideBarType.PartnerSideBar)]
    //SideBar = "/Core/UserType/SideBar/StrategicPartnersSideBar")]
    public class StrategicPartnersGradeRelation : IRelation {
    }
}