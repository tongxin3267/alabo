using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.AutoConfigs.Services;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Helpers;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Framework.Basic.Grades.Domain.Configs {

    [NotMapped]
    [ClassProperty(Name = "用户类型", Icon = "fa fa-users",
        Description = "用户类型", PageType = ViewPageType.List, SortOrder = 11, SideBarType = SideBarType.ControlSideBar,
        Validator = "SELECT * FROM User_UserType where UserTypeId='{0}'",
        GroupName = "基本信息,加盟信息,高级选项", ValidateMessage = "该用户类型下存在用户")]
    public class UserTypeConfig : AutoConfigBase, IAutoConfig {

        public UserTypeConfig() {
            if (TypeClass != UserTypeEnum.Customer) {
                Id = TypeClass.GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
            } else {
                Id = Guid.NewGuid();
            }
        }

        #region

        /// <summary>
        ///     图标
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 5, IsImagePreview = true, GroupTabId = 1)]
        [Display(Name = "图标")]
        public string Icon { get; set; }

        /// <summary>
        ///     类型名称
        /// </summary>
        [Main]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2, ListShow = true, GroupTabId = 1)]
        [Display(Name = "类型名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(8, ErrorMessage = "不能超过8个字符")]
        [HelpBlock("系统对应的类型，有会员，省代理")]
        public string Name { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 2, EnumUniqu = true, ListShow = true,
            DataSource = "Alabo.Framework.Core.Enums.Enum.UserTypeEnum", GroupTabId = 1)]
        [Display(Name = "系统类型")]
        [HelpBlock("系统对应的类型，有会员，省代理")]
        public UserTypeEnum TypeClass { get; set; } = UserTypeEnum.Member;

        /// <summary>
        ///     会员类型是否可以在前台申请，MayApply枚举,可以申请CanApply = 0,不可以申请NotApply = 2,
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 2, ListShow = false, GroupTabId = 1)]
        [Display(Name = "前台是否可以申请")]
        [HelpBlock("前台是否可以申请")]
        public bool ApplyState { get; set; } = false;

        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2, ListShow = false, GroupTabId = 1)]
        [Display(Name = "前台后台显示名称")]
        [HelpBlock("在代理商后台中显示的名称，可以通过/usertype/index来查看")]
        public string FrontName { get; set; }

        /// <summary>
        ///     类型优先级，数据越高，代表级别越高
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, SortOrder = 2, ListShow = true, GroupTabId = 1)]
        [HelpBlock("系统对应的类型，有会员，省代理")]
        [Display(Name = "优先级")]
        public int Priority { get; set; } = 1000;

        [Display(Name = "推荐方式")]
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 2,
            DataSource = "Alabo.Framework.Core.Enums.Enum.RecommendModel",
            ListShow = false, GroupTabId = 1)]
        [HelpBlock("根据会员类型的推荐方式进行推荐")]
        public RecommendModel RecommendModel { get; set; }

        /// <summary>
        ///     区域标示，区域是否唯一限制,AgentsIdentity枚举，非区域会员NotRegionalPartners = 0,是区域会员RegionalPartners = 1,
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 2, ListShow = false, GroupTabId = 1)]
        [HelpBlock("区域唯一")]
        [Display(Name = "区域唯一")]
        public bool AgentsIdentitys { get; set; } = false;

        [Field(ControlsType = ControlsType.Numberic, SortOrder = 4, ListShow = false, GroupTabId = 2)]
        [HelpBlock("加盟费用")]
        [Display(Name = "加盟费用")]
        public decimal JoinPrice { get; set; } = 0.0m;

        /// <summary>
        ///     用户类型简介
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, SortOrder = 5, ListShow = false, GroupTabId = 1)]
        [Display(Name = "详细介绍")]
        [HelpBlock("详细介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     招募书
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, SortOrder = 7, ListShow = false, GroupTabId = 2)]
        [HelpBlock("招募书")]
        [Display(Name = "招募书")]
        public string Book { get; set; }

        /// <summary>
        ///     加盟条件
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, SortOrder = 8, ListShow = false, GroupTabId = 2)]
        [HelpBlock("加盟条件")]
        [Display(Name = "加盟条件")]
        public string Conditions { get; set; }

        /// <summary>
        ///     加盟收益
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, SortOrder = 9, ListShow = false, GroupTabId = 2)]
        [HelpBlock("加盟收益")]
        [Display(Name = "加盟收益")]
        public string Earnings { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 10000,
            Width = "110", GroupTabId = 3)]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public new long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     通用状态 状态：0正常,1冻结,2删除
        ///     实体的软删除通过此字段来实现
        ///     软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        [Field(GroupTabId = 3, ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 10003, Width = "110", DataSource = "Alabo.Domains.Enums.Status")]
        public new Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     备注，此备注一般表示管理员备注，前台会员不可以修改
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, ListShow = true, EditShow = true, Row = 5, SortOrder = 10004,
            GroupTabId = 3)]
        public new string Remark { get; set; }

        /// <summary>

        #endregion

        public void SetDefault() {
            var list = Ioc.Resolve<IAlaboAutoConfigService>().GetList<UserTypeConfig>();
            if (list == null || list.Count == 0) {
                var configs = new List<UserTypeConfig>();
                var config = new UserTypeConfig();
                foreach (UserTypeEnum item in Enum.GetValues(typeof(UserTypeEnum))) {
                    if (item.IsDefault()) {
                        config = new UserTypeConfig {
                            TypeClass = item,
                            Icon = item.GetFieldAttribute().Icon
                        };
                        if (config.TypeClass == UserTypeEnum.Customer) {
                            config.Id = Guid.NewGuid();
                        } else {
                            config.Id = item.GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
                        }

                        config.Name = item.GetDisplayName();
                        config.Name = item.GetDisplayName();
                        config.FrontName = item.GetDisplayName() + "中心";
                        configs.Add(config);
                    }
                }

                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}