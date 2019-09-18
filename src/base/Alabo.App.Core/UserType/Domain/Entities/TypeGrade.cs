using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Entities {

    /// <summary>
    ///     用户类型用户等级
    /// </summary>
    [ClassProperty(Name = "用户类型用户等级", Icon = "fa fa-building", Description = "用户类型用户等级",
        PageType = ViewPageType.List, SortOrder = 20, SideBarType = SideBarType.CustomerServiceSideBar
    )]
    [BsonIgnoreExtraElements]
    [Table("User_TypeGrade")]
    public class TypeGrade : AggregateMongodbRoot<TypeGrade> {

        /// <summary>
        ///     用户类型数据表Id
        ///     与UserType的Id对应
        /// </summary>
        [Display(Name = "用户类型数据表Id")]
        public long TypeId { get; set; }

        /// <summary>
        ///     用户类型ID
        /// </summary>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     用户类型对应的用户Id
        /// </summary>
        [Display(Name = "用户类型对应的用户Id")]
        public long TypeUserId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        /// <value>The name.</value>
        [Display(Name = "等级名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        public string Name { get; set; }
    }
}