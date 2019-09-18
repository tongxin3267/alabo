using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Base.Entities
{
    /// <summary>
    ///     日志
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Base_Logs")]
    [ClassProperty(Name = "日志", Icon = "fa fa-cog", SortOrder = 1,
        SideBarType = SideBarType.LogSideBar)]
    public class Logs : AggregateMongodbUserRoot<Logs>
    {
        /// <summary>
        ///     类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     级别
        /// </summary>
        [Field(IsShowBaseSerach = true, IsTabSearch = true, DataSource = "LogsLevel",
            IsShowAdvancedSerach = true,
            Width = "60", ListShow = true,
            SortOrder = 2)]
        [Display(Name = "级别")]
        public LogsLevel Level { get; set; } = LogsLevel.Information;

        /// <summary>
        ///     日志内容
        /// </summary>
        [Required]
        [Field(ListShow = true, IsMain = true, Width = "500")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        ///     IP地址
        /// </summary>
        [Field(ListShow = true, Width = "120")]
        [Display(Name = "IP地址")]
        public string IpAddress { get; set; }

        /// <summary>
        ///     浏览器
        /// </summary>
        [Field(ListShow = false, Width = "120")]
        public string Browser { get; set; }

        /// <summary>
        ///     请求地址
        /// </summary>
        [Display(Name = "请求网址")]
        [Field(ListShow = true, Width = "220", TableDispalyStyle = TableDispalyStyle.Code)]
        public string Url { get; set; }
    }
}