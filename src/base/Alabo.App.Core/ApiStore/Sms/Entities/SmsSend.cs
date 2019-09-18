using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.ApiStore.Sms.Enums;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.ApiStore.Sms.Entities {

    [BsonIgnoreExtraElements]
    [Table("SMS_SmsSend")]
    [ClassProperty(Name = "短信发送", Icon = "fa fa-puzzle-piece")]
    public class SmsSend : AggregateMongodbUserRoot<SmsSend> {

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public SendState State { get; set; } = SendState.Root;
    }
}