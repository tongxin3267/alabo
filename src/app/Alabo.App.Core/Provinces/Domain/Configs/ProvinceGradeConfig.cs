﻿using Newtonsoft.Json;
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

namespace Alabo.App.Core.UserType.Modules.Province {

    /// <summary>
    ///     省代理等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "省代理等级", Icon = "fa fa-user-times",
        Description = "省代理等级", PageType = ViewPageType.List, SortOrder = 12,
        Validator = "SELECT 1 FROM User_UserType WHERE GradeId='{0}'",
        ValidateMessage = "该等级下存在用户或者为默认等级", SideBarType = SideBarType.ProvinceSideBar)]
    public class ProvinceGradeConfig : BaseGradeConfig, IAutoConfig {

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<ProvinceGradeConfig>();
            if (list.Count < 1) {
                var configs = new List<ProvinceGradeConfig>();
                var config = new ProvinceGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f100"),
                    Name = "默认省代理等级",
                    Icon = "/wwwroot/static/images/GradeIcon/Province01.png",
                    IsDefault = true
                };
                list.Add(config);

                config = new ProvinceGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f101"),
                    Name = "黄金省代理",
                    Icon = "/wwwroot/static/images/GradeIcon/Province02.png",
                    IsDefault = false,
                    Contribute = 10000
                };
                list.Add(config);

                config = new ProvinceGradeConfig {
                    Id = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f102"),
                    Name = "钻石省代理",
                    Icon = "/wwwroot/static/images/GradeIcon/Province03.png",
                    IsDefault = false,
                    Contribute = 100000
                };
                list.Add(config);

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,
                    // // AppName = typeclassProperty.AppName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(list)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}