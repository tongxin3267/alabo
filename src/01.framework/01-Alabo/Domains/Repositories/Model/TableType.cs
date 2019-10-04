using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Repositories.Model {

    [ClassProperty(Name = "表类型")]
    public enum TableType {

        /// <summary>
        ///     Sql Server数据库
        /// </summary>
        [Display(Name = "MsSql数据库")] SqlServer = 1,

        /// <summary>
        ///     Mongodb数据库
        /// </summary>
        [Display(Name = "Mongo数据库")] Mongodb = 2,

        /// <summary>
        ///     自动配置类型
        /// </summary>
        [Display(Name = "配置")] AutoConfig = 3,

        /// <summary>
        ///     枚举
        /// </summary>
        [Display(Name = "枚举")] Enum = 4,

        /// <summary>
        ///     级联分类
        /// </summary>
        [Display(Name = "级联分类")] ClassRelation = 5,

        /// <summary>
        ///     级联标签
        /// </summary>
        [Display(Name = "级联标签")] TagRelation = 6
    }
}