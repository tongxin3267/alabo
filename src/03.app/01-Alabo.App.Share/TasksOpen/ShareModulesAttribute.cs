using System;
using Alabo.App.Core.Tasks.Domain.Enums;

namespace Alabo.App.Share.Tasks {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ShareModulesAttribute : Attribute {
        public string Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模块的详细说明
        /// </summary>
        public string Intro { get; set; } = "请在模块特性里头填写活动详情";

        public string Icon { get; set; } = "icon-social-dribbble ";

        /// <summary>
        /// 分润方式
        /// </summary>
        public FenRunResultType FenRunResultType { get; set; } = FenRunResultType.Price;

        /// <summary>
        /// 图标对应的背景颜色
        /// </summary>
        public string BackGround { get; set; } = "bg-green-seagreen";

        /// <summary>
        /// 排序，从小到大排列
        /// </summary>
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        /// 依赖关系图
        /// </summary>
        public RelationshipType RelationshipType { get; set; }

        /// <summary>
        /// 分润规则的配置帮助说明
        /// </summary>
        public string ConfigHelp { get; set; }

        /// <summary>
        /// 是否开启报单
        /// 是否显示报单
        /// </summary>
        public bool IsSupportDeclarationTriggerType { get; set; } = false;

        /// <summary>
        /// 配置是否继承自ShareBaseConfig
        /// </summary>
        public bool IsIncludeShareModuleConfig { get; set; } = false;
    }
}