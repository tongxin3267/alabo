using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Validations;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Framework.Themes.Dtos.Service {

    public class ThemeDataInput {

        [Display(Name = "Id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ObjectId Id { get; set; }

        [Display(Name = "模块Id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ObjectId WidgetId { get; set; }

        public bool IsSystem { get; set; }

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId SiteId { get; set; }

        [Display(Name = "模板Id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ObjectId ThemeId { get; set; }

        [Display(Name = "页面Id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ObjectId PageId { get; set; }

        public string FullName { get; set; }

        /// <summary>
        ///     组件路径
        /// </summary>
        [Display(Name = "组件路径")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string ComponentPath { get; set; }

        public string Type { get; set; }

        /// <summary>
        ///     默认数据
        /// </summary>
        public string Value { get; set; }
    }
}