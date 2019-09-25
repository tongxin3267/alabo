using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Validations;

namespace Alabo.App.Core.Employes.Domain.Dtos {

    /// <summary>
    /// 岗位编辑
    /// </summary>
    public class PostRoleInput {

        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "Id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     岗位名称
        /// </summary>
        [Display(Name = "岗位名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "岗位说明")]
        public string Summary { get; set; }

        /// <summary>
        /// 岗位权限IDs
        /// </summary>
        public List<string> RoleIds { get; set; } = new List<string>();
    }
}