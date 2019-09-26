using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Employes.Domain.Entities {

    /// <summary>
    /// 岗位，也可立即成部门表
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Admin_PostRole")]
    [ClassProperty(Name = "岗位权限")]
    [AutoDelete(IsAuto = true, EntityType = typeof(Employee), RelationId = "PostRoleId")]
    public class PostRole : AggregateMongodbRoot<PostRole> {

        /// <summary>
        ///     岗位名称
        /// </summary>
        [Display(Name = "岗位名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, Width = "150", IsMain = true, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "岗位说明")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, Width = "400", SortOrder = 100)]
        public string Summary { get; set; }

        /// <summary>
        /// 岗位权限IDs
        /// </summary>
        public List<ObjectId> RoleIds { get; set; }
    }
}