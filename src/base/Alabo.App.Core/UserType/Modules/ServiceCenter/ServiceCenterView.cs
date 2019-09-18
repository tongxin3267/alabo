using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping.Dynamic;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;

namespace Alabo.App.Core.UserType.Modules.ServiceCenter {

    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    [ClassProperty(Name = "管理中心", Icon = "fa fa-puzzle-piece", Description = "管理中心",
        GroupName = "基本信息,高级选项", SideBarType = SideBarType.ServiceCenterSideBar)]
    public class ServiceCenterView : UIBase, IAutoTable<ServiceCenterView> {

        /// <summary>
        ///     表单HttpContext对象
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        [BsonIgnore]
        [DynamicIgnore]
        public HttpContext HttpContext { get; set; }

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        /// <value>
        ///     Id标识
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     用户类型ID
        /// </summary>
        /// <value>
        ///     The user type identifier.
        /// </value>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; } = UserTypeEnum.ServiceCenter.GetFieldId();

        /// <summary>
        ///     用户Id
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        [Display(Name = "用户名称")]
        public long UserId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入合伙人名称，名称不能为空并且不超过20个字符")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, IsMain = true,
            SortOrder = 0, Link = "/Admin/Basic/Edit?Service=IServiceCenterService&View=GetView&Id=[[Id]]")]
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
        /// <value>
        ///     The entity identifier.
        /// </value>
        [Display(Name = "所在城市")]
        [Required]
        [Field(ControlsType = ControlsType.CityDropList, GroupTabId = 1, Width = "120", SortOrder = 600)]
        public long EntityId { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        /// <value>
        ///     The parent user identifier.
        /// </value>
        [Display(Name = "推荐人ID")]
        [HelpBlock("填写正确的推荐人用户名")]
        [Field(ListShow = false, EditShow = false, SortOrder = 700)]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     推荐人
        /// </summary>
        /// <value>
        ///     The name of the parent user.
        /// </value>
        [Display(Name = "推荐人")]
        [HelpBlock("请输入合伙人推荐人的用户名")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ValidType = ValidType.ParentUserName, Width = "150",
            ListShow = true, EditShow = true, SortOrder = 3)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     城市名字的显示名称
        /// </summary>
        /// <value>
        ///     The name of the region.
        /// </value>
        [Field(ListShow = true, EditShow = false, Width = "120", SortOrder = 4)]
        [Display(Name = "所在区域")]
        public string RegionName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        /// <value>
        ///     The grade identifier.
        /// </value>
        [Display(Name = "等级")]
        [HelpBlock("请设置合伙人等级")]
        [Field(ControlsType = ControlsType.RadioButton,
            DataSource = "Alabo.App.Core.UserType.Modules.ServiceCenter.ServiceCenterGradeConfig", GroupTabId = 1,
            Width = "150", ListShow = false, EditShow = true, SortOrder = 1)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        /// <value>
        ///     The name of the grade.
        /// </value>
        [Display(Name = "等级名称")]
        //[HelpBlock(helpText: "请设置合伙人等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Primary, GroupTabId = 1, Width = "180",
            ListShow = true, EditShow = false,
            SortOrder = 1)]
        public string GradeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        [Display(Name = "状态")]
        [HelpBlock("只有通过审核的情况下才能进行操作")]
        [Field(ControlsType = ControlsType.RadioButton, GroupTabId = 1,
            DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", Width = "150", ListShow = true,
            EditShow = true, SortOrder = 1005)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     类型的组织架构图
        ///     计算分润时，可加快速度
        /// </summary>
        /// <value>
        ///     The parent map.
        /// </value>
        [Display(Name = "类型的组织架构图")]
        public string ParentMap { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        /// <value>
        ///     The extensions.
        /// </value>
        [Display(Name = "扩展属性")]
        public string Extensions { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, EditShow = false,
            SortOrder = 1006)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     用户名
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
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
        /// <value>
        ///     The intro.
        /// </value>
        [Display(Name = "介绍")]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2500)]
        public string Intro { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        [Display(Name = "详细地址")]
        [HelpBlock("请输入详细的地址")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 601)]
        public string Address { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        /// <value>
        ///     The remark.
        /// </value>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2501)]
        public string Remark { get; set; }

        /// <summary>
        ///     是否为门店自提
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is pick up in store; otherwise, <c>false</c>.
        /// </value>
        public bool IsPickUpInStore { get; set; } = false;

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var userTypes = Ioc.Resolve<IAutoConfigService>().UserTypes();
            if (userTypes == null || userTypes.Count == 0) {
                return null;
            }
            var cityUserType = userTypes?.First(r => r.Id == UserTypeEnum.ServiceCenter.GetFieldId());
            var quickLinks = new List<ViewLink>
            {
                new ViewLink($"{cityUserType?.Name}添加", "/Admin/Basic/Edit?Service=IServiceCenterService&View=GetView",
                    Icons.Add, LinkType.TableQuickLink),
                new ViewLink("编辑", "/Admin/Basic/Edit?Service=IServiceCenterService&View=GetView&Id=[[Id]]", Icons.Edit,
                    LinkType.ColumnLink),
                new ViewLink("删除", "/Admin/Basic/Delete?Service=IServiceCenterService&Method=Delete&Id=[[Id]]",
                    Icons.Delete, LinkType.ColumnLinkDelete),
                new ViewLink($"{cityUserType?.Name}管理",
                    "/Admin/Basic/Delete?Service=IServiceCenterService&Method=Delete&Id=[[Id]]", Icons.List,
                    LinkType.FormQuickLink)
            };

            return quickLinks;
        }

        /// <summary>
        /// 通用分页查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<ServiceCenterView> PageTable(object query, AutoBaseModel autoModel) {
            var list = Resolve<IServiceCenterService>().GetPageList(query);
            return ToPageResult(list);
        }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("管理中心", "/ServiceCenterView/list")
            };
            return list;
        }
    }
}