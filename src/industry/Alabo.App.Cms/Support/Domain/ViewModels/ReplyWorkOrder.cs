using MongoDB.Bson;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Cms.Support.Domain.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.Mongo.Extension;

namespace Alabo.App.Cms.Support.Domain.ViewModels {

    public class ReplyWorkOrder {

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     回复内容
        /// </summary>
        public string ReplyContent { get; set; }

        /// <summary>
        ///     受理人
        /// </summary>
        public long AcceptUserId { get; set; }

        /// <summary>
        ///     <summary>
        ///         工单状态
        ///     </summary>
        [Display(Name = "工单状态")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public WorkOrderState State { get; set; }

        /// <summary>
        ///     是否公开
        /// </summary>
        [Display(Name = "是否私有")]
        public PublishWay PublishWay { get; set; } = PublishWay.Pri;

        [Display(Name = "问题回复")] public string Reply { get; set; }
    }
}