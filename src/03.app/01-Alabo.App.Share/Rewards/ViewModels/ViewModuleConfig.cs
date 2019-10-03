using Alabo.App.Share.OpenTasks;
using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.Rewards.Domain.Enums;
using Alabo.App.Share.TaskExecutes;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.Share.Models;

namespace Alabo.App.Share.Rewards.ViewModels
{
    public class ViewModuleConfig : BaseViewModel
    {
        private object _configuration;
        public long Id { get; set; }

        /// <summary>
        ///     是否锁定
        ///     维度锁定以后，不能编辑，只能查看
        /// </summary>
        public bool IsLock { get; set; } = false;

        /// <summary>
        ///     模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     配置名称
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = "配置名称不能超过20个字")]
        public string Name { get; set; }

        [Display(Name = "触发类型")] public TriggerType TriggerType { get; set; } = TriggerType.Order;

        public virtual string DistriRatio { get; set; } = "0.0,0.0";

        public ShareModule ShareModule { get; set; } = new ShareModule();

        public BaseRule BaseRule { get; set; } = new BaseRule();

        /// <summary>
        ///     分润用户，收益用户
        /// </summary>
        public ShareUser ShareUser { get; set; } = new ShareUser();

        /// <summary>
        ///     交易用户、触发用户、下单用户的用户类型
        /// </summary>
        public OrderUser OrderUser { get; set; } = new OrderUser();

        /// <summary>
        ///     资产Json数据格式
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "不同的分润维度，可以分配到不同的资产账户，总比例必须等于1",
            ListShow = false, EditShow = true, JsonCanAddOrDelete = true, ExtensionJson = "RuleItems")]
        public string RuleJson { get; set; }

        /// <summary>
        ///     资产分配规则
        /// </summary>

        public IList<AssetsRule> RuleItems { get; set; } = new List<AssetsRule>();

        /// <summary>
        ///     商品范围
        /// </summary>
        public ProductRule ProductRule { get; set; } = new ProductRule();

        /// <summary>
        ///     商品详情页面的显示
        /// </summary>
        public ProductShow ProductShow { get; set; } = new ProductShow();

        /// <summary>
        ///     封顶方式
        /// </summary>
        public LimitRule LimitRule { get; set; } = new LimitRule();

        /// <summary>
        ///     分期规则
        /// </summary>
        public StageRule StageRule { get; set; } = new StageRule();

        /// <summary>
        ///     模板信息
        /// </summary>
        public TemplateRule TemplateRule { get; set; } = new TemplateRule();

        public TaskModuleAttribute ModuleAttribute { get; set; }

        public ShareModulesAttribute SAttribute { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }

        public string Intro { get; set; }

        public bool IsCopy { get; set; } = false;

        /// <summary>
        ///     配置对象
        /// </summary>
        public object Configuration
        {
            get
            {
                if (_configuration == null) {
                    if (ConfigurationValue != null && ModuleAttribute != null) {
                        _configuration = ConfigurationValue.ConvertToModuleConfig(ModuleAttribute.ConfigurationType);
                    }
                }

                return _configuration;
            }
        }

        /// <summary>
        ///     配置内容项目，为对应config的json数据
        /// </summary>
        public string ConfigurationValue { get; set; }

        /// <summary>
        ///     分润用户等级ID
        /// </summary>
        public Guid ShareGradeId { get; set; }

        /// <summary>
        ///     订单用户等级Id
        /// </summary>
        public Guid OrderUserGradeId { get; set; }

        public Guid ShareUserTypeId { get; set; }

        public Guid OrderUserTypeId { get; set; }

        /// <summary>
        ///     是否限制分润用户的用户类型
        ///     IsLimitShareUserType
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLimitShareUserType { get; set; } = false;

        /// <summary>
        ///     是否限制分润用户的用户类型等级
        /// </summary>
        public bool IsLimitShareUserGrade { get; set; } = false;

        public PriceType AmountType { get; set; } = PriceType.Price;

        /// <summary>
        ///     是否限制交易用户、触发用户、下单用户的用户类型
        ///     IsLimitShareUserType
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLimitOrderUserType { get; set; } = false;

        /// <summary>
        ///     是否限制分润用户的用户类型等级
        /// </summary>
        public bool IsLimitOrderUserGrade { get; set; } = false;

        public T GetConfigration<T>() where T : class, IModuleConfig
        {
            if (Configuration == null) {
                return default;
            }

            return Configuration as T;
        }
    }
}