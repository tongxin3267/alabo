using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Basic.Relations.Dtos {

    /// <summary>
    ///     级联数据类型：包含分类与标签
    /// </summary>
    public class RelationApiOutput {

        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     关系对应类名
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     是否选择, 默认false
        /// </summary>
        public bool Check { get; set; }

        /// <summary>
        ///     排序码
        /// </summary>
        public long SortOrder { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public long UserId { get; set; } = 0;

        /// <summary>
        ///     是否能添加子级类目
        /// </summary>
        public bool IsCanAddChild { get; set; } = true;

        /// <summary>
        ///     父级分类
        /// </summary>
        public IList<RelationApiOutput> ChildClass { get; set; } = new List<RelationApiOutput>();
    }
}