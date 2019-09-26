using System.ComponentModel.DataAnnotations.Schema;
using _01_Alabo.Cloud.Core.SendSms.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Entities {

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