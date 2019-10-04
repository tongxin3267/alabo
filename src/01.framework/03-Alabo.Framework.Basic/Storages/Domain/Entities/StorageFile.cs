using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Framework.Basic.Storages.Domain.Entities {

    /// <summary>
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Basic_StorageFile")]
    [ClassProperty(Name = "�洢�ļ�")]
    [AutoDelete(IsAuto = true)]
    public class StorageFile : AggregateMongodbRoot<StorageFile> {

        /// <summary>
        ///     �ļ���
        /// </summary>
        [Field(Operator = Operator.Contains)]
        public string Name { get; set; }

        /// <summary>
        ///     ��С
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        ///     ��չ��
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     ·��
        /// </summary>
        public string Path { get; set; }
    }
}