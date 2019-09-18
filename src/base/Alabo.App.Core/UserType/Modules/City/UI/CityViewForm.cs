using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Validations;

namespace Alabo.App.Core.UserType.Modules.City.UI {

    /// <summary>
    /// 城市合伙人
    /// </summary>
    [ClassProperty(Name = "城市合伙人", Description = "城市合伙人", GroupName = "基本信息,高级选项")]
    public class CityViewForm : UIBase, IAutoForm, IAutoTable<CityViewForm> {
        public long Id { get; set; }

        /// <summary>
        ///     用户类型ID
        /// </summary>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365102");

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户名称")]
        public long UserId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入合伙人名称，名称不能为空并且不超过8个字符")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, IsMain = true,
            SortOrder = 0, Link = "/Admin/Basic/Edit?Service=ICityService&View=GetView&Id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     相关的实体Id，比如如果是省代理，城市代理，区域代理，则使用Common_Region中的Id
        ///     如果是商圈，则使用Common_Cicle的Id
        ///     如果时候供应商，则使用ZKShop_Store 的Id
        ///     所属区域ID
        ///     一个区域只能有个
        ///     广东省 省代理只有一个
        ///     东莞市 市代理也只有一个
        /// </summary>
        [Display(Name = "所属城市")]
        [Required]
        [Field(ControlsType = ControlsType.CityDropList, GroupTabId = 1, Width = "120", SortOrder = 600)]
        public long EntityId { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人ID")]
        [HelpBlock("填写正确的推荐人用户名")]
        [Field(ListShow = false, EditShow = false, SortOrder = 700)]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     推荐人
        /// </summary>
        [Display(Name = "推荐人用户名")]
        [HelpBlock("请输入合伙人推荐人的用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ValidType = ValidType.ParentUserName, Width = "150",
            ListShow = true, EditShow = true, SortOrder = 3)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     城市名字的显示名称
        /// </summary>
        [Field(TableDispalyStyle = TableDispalyStyle.Code, ListShow = true, EditShow = false, Width = "160",
            SortOrder = 4)]
        [Display(Name = "所属城市")]
        public string RegionName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "合伙人等级")]
        [HelpBlock("请设置合伙人等级")]
        [Field(ControlsType = ControlsType.RadioButton,
            DataSource = "Alabo.App.Core.UserType.Modules.City.CityGradeConfig", GroupTabId = 1, Width = "180",
            ListShow = false, EditShow = true, SortOrder = 1)]
        public Guid GradeId { get; set; } = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a368000");

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        /// <value>The name of the grade.</value>
        [Display(Name = "等级名称")]
        //[HelpBlock(helpText: "请设置合伙人等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, GroupTabId = 1, Width = "180",
            ListShow = true, EditShow = false, SortOrder = 1)]
        public string GradeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("只有通过审核的情况下才能进行操作")]
        [Field(ControlsType = ControlsType.RadioButton, GroupTabId = 1, IsTabSearch = true,
            IsShowAdvancedSerach = false,
            DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", Width = "150", ListShow = true,
            EditShow = true, SortOrder = 1005)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     类型的组织架构图
        ///     计算分润时，可加快速度
        /// </summary>
        [Display(Name = "类型的组织架构图")]
        public string ParentMap { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Display(Name = "扩展属性")]
        public string Extensions { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, EditShow = false,
            SortOrder = 1006)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(IsShowBaseSerach = true, PlaceHolder = "请输入用户名", DataField = "UserId",
            ValidType = ValidType.UserName, ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110",
            ListShow = true, EditShow = true, SortOrder = 2)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "介绍")]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2500)]
        public string Intro { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [HelpBlock("请输入详细的地址")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 601)]
        public string Address { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2501)]
        public string Remark { get; set; }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("城市合伙人", "/CityView/list")
            };
            return list;
        }

        /// <summary>
        /// 转换成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var cityId = ToId<long>(id);
            var cityView = Resolve<ICityService>().GetView(cityId);
            var model = AutoMapping.SetValue<CityViewForm>(cityView);
            return ToAutoForm(model);
        }

        public PageResult<CityViewForm> PageTable(object query, AutoBaseModel autoModel) {
            var reuslt = Resolve<ICityService>().GetPageList(query);
            return null;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var cityView = AutoMapping.SetValue<CityView>(model);
            var result = Resolve<ICityService>().AddOrUpdate(cityView);
            return result;
        }
    }
}