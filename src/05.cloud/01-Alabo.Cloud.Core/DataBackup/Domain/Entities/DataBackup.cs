using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Market.DataBackup.Domain.Enums;
using Alabo.Core.WebUis;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Model;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.DataBackup.Domain.Entities {

    /// <summary>
    ///     备份还原
    ///     后期可拓展各种用户类型关系网
    /// </summary>
    [BsonIgnoreExtraElements][Table("Market_DataBackup")]
    [ClassProperty(Name = "备份还原", Description = "备份还原", Icon = IconFlaticon.background,
        SideBarType = SideBarType.RelationshipIndexSideBar)]
    public class DataBackup : AggregateMongodbRoot<DataBackup> {

        /// <summary>
        ///     备份文件路劲
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     备份方式
        /// </summary>
        public BackupType Backup { get; set; }

        /// <summary>
        ///     数据库方式
        /// </summary>
        public TableType TableType { get; set; }
    }
}