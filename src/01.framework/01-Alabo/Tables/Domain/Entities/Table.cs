using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Model;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Tables.Domain.Entities
{
    /// <summary>
    ///     表以及数据库结构说明书
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Core_Table")]
    [ClassProperty(Name = "数据库表", Icon = "fa fa-cog", SortOrder = 1,
        SideBarType = SideBarType.TableSideBar)]
    public class Table : AggregateMongodbRoot<Table>
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Field(IsShowBaseSerach = true, IsMain = true, IsShowAdvancedSerach = true,
            ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     实体名称
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox,
            LabelColor = LabelColor.Success,
            GroupTabId = 1, Width = "280", ListShow = true, SortOrder = 2)]
        [Display(Name = "标识")]
        public string Key { get; set; }

        /// <summary>
        ///     表名
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox,
            TableDispalyStyle = TableDispalyStyle.Code,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 2)]
        [Display(Name = "表名")]
        public string TableName { get; set; }

        /// <summary>
        ///     数据库类型
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, LabelColor = LabelColor.Brand,
            DataSource = "Alabo.Domains.Repositories.Model.TableStyle",
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 2)]
        [Display(Name = "数据库类型")]
        public TableType TableType { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            GroupTabId = 1, Width = "180", ListShow = false, SortOrder = 2)]
        [Display(Name = "类型")]
        public string Type { get; set; }

        /// <summary>
        ///     数据库结构说明
        /// </summary>
        [Field(ControlsType = ControlsType.ChildList,
            GroupTabId = 1, Width = "120", ListShow = true, SortOrder = 2)]
        [Display(Name = "列结构")]
        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();
    }

    /// <summary>
    ///     列结构
    /// </summary>
    [ClassProperty(Name = "列结构", Icon = "fa fa-cog", SortOrder = 1,
        SideBarType = SideBarType.TableSideBar)]
    public class TableColumn : BaseViewModel
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Field(IsShowBaseSerach = true, IsMain = true, IsShowAdvancedSerach = true,
            ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 20)]
        public string Name { get; set; }

        /// <summary>
        ///     字段
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 1)]
        [Display(Name = "字段")]
        public string Key { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox,
            TableDispalyStyle = TableDispalyStyle.Code,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 2)]
        [Display(Name = "类型")]
        public string Type { get; set; }
    }
}