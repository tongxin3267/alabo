using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Contracts.Domain.Entities {

    [BsonIgnoreExtraElements]
    [Table("Contract_Contract")]
    [ClassProperty(Name = "电子合同", Icon = "fa fa-cog", SortOrder = 1)]
    public class Contract : AggregateMongodbUserRoot<Contract> {

        /// <summary>
        /// 合同内容
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, EditShow = true, GroupTabId = 1)]
        public string Content { get; set; }

        /// <summary>
        /// 电子合同类型
        /// </summary>
        public UserTypeEnum Type { get; set; }
    }
}