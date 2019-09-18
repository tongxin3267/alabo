using System.Collections.Generic;
using Alabo.Domains.Entities;

namespace Alabo.UI.AutoTables
{
    /// <summary>
    ///     自动表格
    /// </summary>
    public class AutoTable
    {
        /// <summary>
        ///     ApiUrl
        /// </summary>
        public string ApiUrl { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        /// <summary>
        ///     构建表格多标签搜索
        /// </summary>
        public List<KeyValue> Tabs { get; set; }

        /// <summary>
        ///     列
        /// </summary>
        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();

        /// <summary>
        ///     搜索相关
        /// </summary>
        public SearchOptions SearchOptions { get; set; } = new SearchOptions();

        /// <summary>
        ///     分页数据
        ///     如果有数据则Result中获取，否则Api接中获取
        /// </summary>

        public object Result { get; set; }

        /// <summary>
        ///     表格操作方法
        /// </summary>
        public List<TableAction> TableActions { get; set; }
    }

    public class TableColumn
    {
        /// <summary>
        ///     属性值
        /// </summary>
        public string Prop { get; set; }

        /// <summary>
        ///     标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///     显示类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public string Width { get; set; } = "120";

        /// <summary>
        ///     链接处理
        /// </summary>
        public string Options { get; set; }
    }


    public class SearchOptions
    {
        /// <summary>
        ///     基本搜索表单
        /// </summary>
        public List<SearchOptionForm> Forms { get; set; }

        /// <summary>
        ///     高级搜索表单
        /// </summary>
        public List<SearchOptionForm> AdvancedForms { get; set; }
    }

    public class SearchOptionForm
    {
        /// <summary>
        ///     属性值
        /// </summary>
        public string Prop { get; set; }

        /// <summary>
        ///     标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///     显示类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public string ModelValue { get; set; }

        /// <summary>
        ///     index
        /// </summary>
        public long SortOrder { get; set; }
    }
}