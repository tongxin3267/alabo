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

namespace Alabo.App.Core.UserType.Modules.Employees {

    /// <summary>
    ///     员工等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "员工等级", Icon = "fa fa-user-times",
        Description = "员工等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级", SideBarType = SideBarType.EmployeesSideBar)]
    public class EmployeesGradeConfig : BaseGradeConfig, IAutoConfig {

        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "员工等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365333");

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<EmployeesGradeConfig>();
            if (list.Count < 1) {
                var configs = new List<EmployeesGradeConfig>();
                var config = new EmployeesGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a365000"),
                    Name = "员工默认等级",
                    Icon = "/wwwroot/static/images/GradeIcon/Employees01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new EmployeesGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a365001"),
                    Name = "黄金员工",
                    Icon = "/wwwroot/static/images/GradeIcon/Employees02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new EmployeesGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a365002"),
                    Name = "钻石员工",
                    Icon = "/wwwroot/static/images/GradeIcon/Employees03.png",
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