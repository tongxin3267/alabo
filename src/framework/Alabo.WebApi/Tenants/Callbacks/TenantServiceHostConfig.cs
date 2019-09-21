using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Tenants.Callbacks {
    /// <summary>
    /// 后台Api服务器
    /// </summary>

    [NotMapped]
    [ClassProperty(Name = "后台Api服务器(ZKCloud)", Icon = "fa fa-cny", Description = "支付方式",
        PageType = ViewPageType.List)]
    public class TenantServiceHostConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        /// Id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public Guid Id { get; set; }

        /// <summary>
        ///     货币名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, IsMain = true, ListShow = true, Width = "110",
            IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        [Required]
        [Display(Name = "Api服务器地址")]
        [HelpBlock("租户Api后台网址服务器，对应后台.net core 程序，一个Api服务器地址可以对应多个地址")]
        [Main]
        public string Url { get; set; }

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<TenantServiceHostConfig>();
            if (list == null || list.Count == 0) {
                var configs = new List<TenantServiceHostConfig>();
                var config = new TenantServiceHostConfig {
                    Id = Guid.Parse("72bb65e6-3000-414d-972e-1a3g4h366000"),
                    Url = "http://s-test.qiniuniu99.com/"
                };
                configs.Add(config);

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