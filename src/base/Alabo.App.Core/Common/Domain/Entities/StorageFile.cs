using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Common.Domain.Entities {

    /// <summary>
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Common_StorageFile")]
    [ClassProperty(Name = "存储文件")]
    [AutoDeleteAttribute(IsAuto = true)]
    public class StorageFile : AggregateMongodbRoot<StorageFile> {

        /// <summary>
        /// 文件名
        /// </summary>
        [Field(Operator = Operator.Contains)]
        public string Name { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
    }
}