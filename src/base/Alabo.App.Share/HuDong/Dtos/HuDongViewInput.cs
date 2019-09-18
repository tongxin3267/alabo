using MongoDB.Bson;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.Mongo.Extension;

namespace Alabo.App.Share.HuDong.Dtos {

    public class HuDongViewInput {

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "类型不能为空")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Key { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        public string userId { get; set; }
    }
}