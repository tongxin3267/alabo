using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Open.Tasks.Base;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Tasks.Base
{
    /// <summary>
    /// 分润编辑详情
    /// 对应前台的admin-reward-edit组件
    /// </summary>
    public class RewardRuleOutput
    {
        /// <summary>
        /// 配置Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        ///     模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// 基本信息
        /// </summary>
        public RewardEditOutputBase Base { get; set; } = new RewardEditOutputBase();

        /// <summary>
        /// 自动分润表格
        /// </summary>
        public AutoForm AutoForm { get; set; }

        /// <summary>
        /// 分润范围
        /// </summary>
        public RewardEditOutputRange Range { get; set; } = new RewardEditOutputRange();

        /// <summary>
        /// 资产分配规则
        /// </summary>
        public IList<AssetsRule> RuleItems { get; set; } = new List<AssetsRule>();

        /// <summary>
        /// 用户与等级设置
        /// </summary>
        public RewardEditOutputUserRange UserConfig { get; set; } = new RewardEditOutputUserRange();

        /// <summary>
        /// 是否锁定
        /// 维度锁定以后，不能编辑，只能查看
        /// </summary>
        public bool IsLock { get; set; } = false;

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }
    }

    /// <summary>
    /// 基础信息
    /// </summary>
    public class RewardEditOutputBase
    {
        /// <summary>
        /// 维度名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     配置名称
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = "配置名称不能超过20个字")]
        public string Name { get; set; }

        /// <summary>
        /// 简要说明
        /// </summary>
        [Display(Name = "简要说明")]
        public string Summary { get; set; } = "简要说明";

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 模板信息
        /// </summary>
        public TemplateRule TemplateRule { get; set; } = new TemplateRule();
    }

    public class RewardEditOutputUserRange
    {
        /// <summary>
        /// 交易用户、触发用户、下单用户的用户类型
        /// </summary>
        public OrderUser OrderUser { get; set; }=new OrderUser();

        /// <summary>
        /// 用户与等级
        /// </summary>
        public ShareUser ShareUser { get; set; } = new ShareUser();
    }

    /// <summary>
    /// 分润范围
    /// </summary>

    public class RewardEditOutputRange
    {
      

        /// <summary>
        /// 价格限制方式
        /// </summary>

        [Display(Name = "价格限制方式")]
        public PriceLimitType PriceLimitType { get; set; } = PriceLimitType.OrderPrice;

        /// <summary>
        /// 最小触发金额
        /// </summary>
        [Display(Name = "最小触发金额")]
        public decimal MinimumAmount { get; set; } = 0.0m;

        /// <summary>
        /// 最大触发金额
        /// </summary>
        [Display(Name = "最大触发金额")]
        public decimal MaxAmount { get; set; } = 0.0m;

        ///<summary>
        ///触发类型
        ///</summary>>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = false)]
        public TriggerType TriggerType { get; set; } = TriggerType.Order;

        /// <summary>
        /// 商品范围
        /// </summary>
        public ProductRule ProductRule { get; set; } = new ProductRule();
    }
}