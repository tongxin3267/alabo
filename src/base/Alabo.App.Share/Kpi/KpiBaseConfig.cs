using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Open.Tasks.Base;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Open.Kpi {

    public abstract class KpiBaseConfig : BaseViewModel, IModuleConfig {

        /// <summary>
        /// 模块ID与，远程OpenID对应
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = false, SortOrder = 1)]
        public long Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        ///<summary>
        ///触发类型
        ///</summary>>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = false)]
        public TriggerType TriggerType { get; set; } = TriggerType.Order;

        /// <summary>
        /// 分润比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false, SortOrder = 1)]
        [HelpBlock(
            "需要给N代分润则填写N个比例,以英文符号,分隔开。比如：分润比例设置成0.1 表示一代分润比例为10%；分润比例设置成0.1,0.05  表示一代分润比例为10%,二代为5%；分润比例设置成0.1,0.05,0.03  表示一代分润比例为10%,二代为5%,三代为3%；请根据您的资质，在合法合情范围内设置。设置不能超过三级")]
        [Display(Name = "分润比例", Description = "填写分润比例。需要给N代分润则填写N个比例,以英文符号,分隔开")]
        public virtual string DistriRatio { get; set; } = "0.0,0.0";

        /// <summary>
        /// 最小触发金额
        /// </summary>
        public BaseRule BaseRule { get; set; }

        /// <summary>
        /// 分润用户，收益用户
        /// </summary>
        public ShareUser ShareUser { get; set; }

        /// <summary>
        /// 交易用户、触发用户、下单用户的用户类型
        /// </summary>
        public OrderUser OrderUser { get; set; }

        /// <summary>
        /// 商品范围
        /// </summary>
        public ProductRule ProductRule { get; set; }

        /// <summary>
        /// 分期规则
        /// </summary>
        public StageRule StageRule { get; set; }

        /// <summary>
        /// 模板信息
        /// </summary>
        public TemplateRule TemplateRule { get; set; }

        /// <summary>
        /// 是否锁定
        /// 维度锁定以后，不能编辑，只能查看
        /// </summary>
        public bool IsLock { get; set; } = false;
    }
}