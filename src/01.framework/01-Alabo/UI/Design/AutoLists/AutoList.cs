using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.UI.Design.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.UI.Design.AutoLists
{
    public class AutoList
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        /// <summary>
        ///     构建表格多标签搜索
        /// </summary>
        public List<KeyValue> Tabs { get; set; }

        /// <summary>
        ///     搜索相关
        /// </summary>
        public SearchOptions SearchOptions { get; set; } = new SearchOptions();


        /// <summary>
        ///     分页数据
        ///     如果有数据则Result中获取，否则Api接中获取
        /// </summary>

        public object Result { get; set; }
    }

    /// <summary>
    ///     自动列表
    ///     对应移动端zk-list功能
    /// </summary>
    [ClassProperty(Name = "zk-list对象")]
    public class AutoListItem
    {
        /// <summary>
        ///     主键ID
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        ///     网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     图标或图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     标题额外数据
        /// </summary>

        public object Value { get; set; }

        /// <summary>
        ///     描述，可以通过拼接完成
        /// </summary>
        public string Intro { get; set; }
    }
}