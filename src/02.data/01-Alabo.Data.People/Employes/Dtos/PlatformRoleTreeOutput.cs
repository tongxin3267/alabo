using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Repositories.Mongo.Extension;

namespace Alabo.App.Core.Employes.Domain.Dtos {

    public class PlatformRoleTreeOutput {

        /// <summary>
        /// id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 上一级权限
        /// </summary>
        [Display(Name = "链接名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     Url及权限
        /// </summary>
        [Display(Name = "地址")]
        public string Url { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public IList<PlatformRoleTreeOutput> AppItems { get; set; }
    }
}