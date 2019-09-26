using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Entities.Extension;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Mapping.Dynamic;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Activitys.ViewModels {

    /// <summary>
    ///     Class ViewActivityModel.
    /// </summary>
    public class ViewActivityModel : BaseViewModel {

        /// <summary>
        ///     Gets or sets the 活动.
        /// </summary>
        public Activity Activity { get; set; } = new Activity();

        /// <summary>
        ///     Gets or sets the 活动 extension.
        /// </summary>
        public ActivityExtension ActivityExtension { get; set; } = new ActivityExtension();

        /// <summary>
        ///     Gets or sets the data time rang.
        /// </summary>
        public string DateTimeRang { get; set; }

        /// <summary>
        ///     Gets or sets the 会员 range.
        /// </summary>
        [Display(Name = "用户范围")]
        public UserRange UserRange { get; set; } = UserRange.ByUserGrade;

        /// <summary>
        ///     Gets or sets the configuration.
        ///     活动自定义配置
        /// </summary>
        public object Configuration { get; set; }

        public long ProductId { get; set; }

        /// <summary>
        ///     表单HttpContext对象
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        [BsonIgnore]
        [DynamicIgnore]
        public HttpContext HttpContext { get; set; }
    }
}