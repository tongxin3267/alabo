using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.App.Agent.ShareHolders.CallBacks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Agent.ShareHolders.Domain.Entities {

    [ClassProperty(Name = "股东")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_ShareHolder")]
    public class ShareHolder : AggregateMongodbRoot<ShareHolder> {

        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.Hidden, SortOrder = 700)]
        public long UserId { get; set; }

        /// <summary>
        ///     股东名称
        /// </summary>
        [Display(Name = "股东名称")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string Name { get; set; }

        /// <summary>
        ///     所属用户名
        /// </summary>
        [Display(Name = "所属用户名")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string UserName { get; set; }

        /// <summary>
        ///     股东等级
        /// </summary>
        [Display(Name = "股东等级")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.DropdownList, DataSourceType = typeof(ShareHoldersGradeConfig), SortOrder = 700)]
        public Guid GradeId { get; set; }
    }
}