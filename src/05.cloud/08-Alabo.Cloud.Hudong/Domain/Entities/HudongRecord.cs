using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Share.HuDong.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.App.Share.HuDong.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [Table("HudongRecord")]
    [ClassProperty(Name = "互动记录", Icon = "fa fa-cog", SortOrder = 1, SideBarType = SideBarType.LogSideBar)]
    public class HudongRecord : AggregateMongodbUserRoot<HudongRecord>
    {
        /// <summary>
        ///     获奖描述
        /// </summary>
        [BsonRequired]
        public string Intro { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [BsonRequired]
        public AwardStatus HuDongStatus { get; set; }

        /// <summary>
        ///     互动类型
        /// </summary>
        public HuDongEnums HuDongType { get; set; }

        /// <summary>
        ///     等级
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        ///     奖品类型
        /// </summary>
        public HudongAwardType HuDongActivityType { get; set; }
    }
}