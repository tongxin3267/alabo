using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.Partner {

    /// <summary>
    ///     Partner等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "Partner等级", Icon = "fa fa-user-times",
        Description = "Partner等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级",
        SideBarType = SideBarType.PartnerSideBar)]
    public class PartnerGradeConfig : BaseGradeConfig, IAutoConfig {

        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "Partner等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365444");

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<PartnerGradeConfig>();
            if (list.Count < 1) {
                var configs = new List<PartnerGradeConfig>();
                var config = new PartnerGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-abcd-9845-10054a36e000"),
                    Name = "投资合伙人",
                    Icon = "/wwwroot/static/images/GradeIcon/Partner01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new PartnerGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-abcd-9845-1a3d4a36e001"),
                    Name = "核心合伙人",
                    Icon = "/wwwroot/static/images/GradeIcon/Partner02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new PartnerGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-abcd-9845-1a3d4a36e002"),
                    Name = "创始合伙人",
                    Icon = "/wwwroot/static/images/GradeIcon/Partner03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                config = new PartnerGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-abcd-9845-1a3d4a36e003"),
                    Name = "团队合伙人",
                    Icon = "/wwwroot/static/images/GradeIcon/Partner03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                config = new PartnerGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-abcd-9845-1a3d4a36e004"),
                    Name = "资源合伙人",
                    Icon = "/wwwroot/static/images/GradeIcon/Partner03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                config = new PartnerGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-abcd-9845-1a3d4a36e005"),
                    Name = "高管合伙人",
                    Icon = "/wwwroot/static/images/GradeIcon/Partner03.png",
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
}