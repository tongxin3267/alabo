using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis;
using Alabo.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Maps;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Category.Domain.Dtos
{
    /// <summary>
    /// 前台编辑视图
    /// </summary>
    public class CategoryView : UIBase, IAutoTable<CategoryView>
    {

        /// <summary>
        ///     父类目Id
        /// </summary>
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 是否是顶级
        /// </summary>
        public bool IsPartent { get; set; } = true;
        /// <summary>
        ///     类目名称
        /// </summary>
        [Display(Name = "类目名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        ///     父类目Id
        /// </summary>
        [Display(Name = "父类目Id")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public Guid PartentId { get; set; }



        /// <summary>
        ///     类目规格（购买用,IsSale=true)
        /// </summary>
        [Display(Name = "类目规格")]
        public List<ViewSaleProperty> SalePropertys { get; set; } = new List<ViewSaleProperty>();

        /// <summary>
        ///     类目属性（展示用,IsSale=false)
        ///     商品参数等
        /// </summary>
        [Display(Name = "类目属性")]
        public List<ViewDisplayProperty> DisplayPropertys { get; set; } = new List<ViewDisplayProperty>();

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 10000, Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     通用状态 状态：0正常,1冻结,2删除
        ///     实体的软删除通过此字段来实现
        ///     软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 10003, Width = "110", DataSourceType = typeof(Alabo.Domains.Enums.Status))]
        public Status Status { get; set; } = Status.Normal;




        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("编辑", "Edit",TableActionType.ColumnAction),//管理员编辑
                ToLinkAction("删除", "/Api/Coupon/DeleteCoupon",ActionLinkType.Delete,TableActionType.ColumnAction)//管理员删除
            };
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public PageResult<CategoryView> PageTable(object query, AutoBaseModel autoModel)
        {
            var cateList = Ioc.Resolve<ICategoryService>().GetPagedList(query);
            var plist = cateList.MapTo<PagedList<CategoryView>>();

            return ToPageResult(plist);
        }
    }

    public class ViewDisplayProperty
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     属性名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ViewPropertyControlsType ControlsType { get; set; }
        /// <summary>
        /// 简介，有属性值拼接起来
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long SortOrder { get; set; } = 1000;
        /// <summary>
        /// 属性值
        /// </summary>
        public List<ViewPropertyValue> Values { get; set; } = new List<ViewPropertyValue>();

    }


    public class ViewSaleProperty
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     属性名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        /// 简介，有属性值拼接起来
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long SortOrder { get; set; } = 1000;
        /// <summary>
        /// 属性值
        /// </summary>
        public List<ViewPropertyValue> Values { get; set; } = new List<ViewPropertyValue>();

    }

    public class ViewPropertyValue
    {
        public Guid Id { get; set; }
        /// <summary>
        ///     属性名称
        /// </summary>
        [Display(Name = "属性名称")]
        public string ValueName { get; set; }
    }

    [ClassProperty(Name = "视图控件显示类型")]
    public enum ViewPropertyControlsType
    {

        /// <summary>
        ///     文本框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "文本框")]
        TextBox = 2,

        /// <summary>
        ///     多选框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "多选框")]
        CheckBox = 4,

        /// <summary>
        ///     单选框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "单选框")]
        RadioButton = 5,

        /// <summary>
        ///     下拉列表
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "下拉列表")]
        DropdownList = 6,


        /// <summary>
        ///     样式参考
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "Switch切换")]
        Switch = 15,

    }
}