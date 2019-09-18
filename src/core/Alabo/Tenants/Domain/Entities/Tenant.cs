using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Tenants.Domain.Entities {

    /// <summary>
    ///     主机站点
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Base_Tenant")]
    [ClassProperty(Name = "租户", Icon = "fa fa-cog", SortOrder = 1, SideBarType = SideBarType.LogSideBar)]
    public class Tenant : AggregateMongodbUserRoot<Tenant> {

        /// <summary>
        ///     Name
        /// </summary>
        [BsonRequired]
        public string Name { get; set; }

        /// <summary>
        ///     Database Name
        /// </summary>
        [BsonRequired]
        public string DatabaseName { get; set; }

        /// <summary>
        ///     后台访问签名,访问唯一key
        ///     后台访问格式:http://www.5ug.com/Admin/Login?Key={sign}
        /// </summary>
        [BsonRequired]
        public string Sign { get; set; }

        /// <summary>
        ///     Api服务Url
        /// </summary>
        [BsonRequired]
        public string ServiceUrl { get; set; }

        /// <summary>
        ///     前端Url
        /// </summary>
        [BsonRequired]
        public string ClientUrl { get; set; }

        /// <summary>
        /// 获取站点Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId SiteId { get; set; }

        /// <summary>
        /// 站点信息
        /// </summary>
        public TenantSite Site { get; set; }
    }

    /// <summary>
    /// 租户站点信息
    /// </summary>
    public class TenantSite {

        /// <summary>
        /// 站点Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 站点Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId FatherId { get; set; }

        /// <summary>
        /// 站点编号
        /// </summary>
        [Display(Name = "项目编号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, SortOrder = 1)]
        public string ProjectNum { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// 用户ID,平台的站点用户
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     站点名称
        /// </summary>
        [Display(Name = "公司名称")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Link = "/Admin/Site/Info?id=[[Id]]", Width = "200", ListShow = true,
            SortOrder = 2)]
        public string CompanyName { get; set; }

        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "100", ListShow = true,
            SortOrder = 4)]
        [Display(Name = "手机号码")]
        public string Phone { get; set; }

        public string ClientHost { get; set; }

        /// <summary>
        ///     前台预览地址，移动端预览地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// PC端后台预览地址
        /// </summary>
        public string AdminUrl { get; set; }
    }
}