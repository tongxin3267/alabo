using System;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Configs
{
    /// <summary>
    /// 目标设置
    /// </summary>
    [ClassProperty(Name = "目标设置", Icon = "fa fa-external-link", Description = "目标设置", SortOrder = 23, PageType = ViewPageType.List)]
    public class TargetConfig : AutoConfigBase, IAutoConfig
    {
        public void SetDefault() {
            throw new NotImplementedException();
        }
    }
}