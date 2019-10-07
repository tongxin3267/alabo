using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.Industry.Cms.LightApps.Domain
{
    /// <summary>
    ///     表明统一以AutoData_开头
    /// </summary>
    public class AutoDataBaseEntity
    {
        /// <summary>
        ///     标识
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        [Key]
        [Display(Name = "ID", Order = 1)]
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, SortOrder = 1, Width = "50")]
        [JsonConverter(typeof(ObjectIdConverter))]
        [JsonIgnore]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        //  public string Tenant { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.TimePicker, ListShow = true, EditShow = false, SortOrder = 10001)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     数据分类ID保存到Commone_Relation中
        /// </summary>
        public long ClassId { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        [Field(ControlsType = ControlsType.TimePicker, ListShow = true, EditShow = false, SortOrder = 10001)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}