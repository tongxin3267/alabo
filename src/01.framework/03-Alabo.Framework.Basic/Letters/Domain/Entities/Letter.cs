using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Framework.Basic.Letters.Domain.Enums;
using Alabo.Users.Entities;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Framework.Basic.Letters.Domain.Entities
{
    /// <summary>
    ///     站内信
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Attach_Letter")]
    [ClassProperty(Name = "站内信")]
    public class Letter : AggregateMongodbUserRoot<Letter>
    {
        /// <summary>
        ///     消息标题
        /// </summary>
        [Display(Name = "消息标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Title { get; set; }

        /// <summary>
        ///     消息内容
        /// </summary>
        [Display(Name = "消息内容")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Content { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        [Display(Name = "消息类型")]
        public LetterType Type { get; set; }

        /// <summary>
        ///     发送方式
        /// </summary>
        [Display(Name = "发送方式")]
        public SendType Send { get; set; }

        /// <summary>
        ///     发件人用户ID
        /// </summary>
        [Display(Name = "发件人用户ID")]
        public long SenderUserId { get; set; }

        /// <summary>
        ///     收件人用户ID
        /// </summary>
        [Display(Name = "收件人用户ID")]
        public long ReceiverUserId { get; set; }

        /// <summary>
        ///     是否可回复
        /// </summary>
        [Display(Name = "是否可回复")]
        public bool IsCanReply { get; set; } = false;

        /// <summary>
        ///     是否回复
        /// </summary>
        [Display(Name = "是否回复")]
        public bool IsReply { get; set; } = false;

        /// <summary>
        ///     是否已读
        /// </summary>
        [Display(Name = "是否已读")]
        public bool IsRead { get; set; } = false;

        /// <summary>
        ///     发件人
        /// </summary>
        [Display(Name = "发件人")]
        [BsonIgnore]
        public User SenderUser { get; set; }

        /// <summary>
        ///     收件人
        /// </summary>
        [Display(Name = "收件人")]
        [BsonIgnore]
        public User ReceiverUser { get; set; }
    }
}