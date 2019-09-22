using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Offline.Merchants.Domain.Entities
{
    /// <summary>
    /// 门店设置
    /// </summary>
    [Table("Offline_MerchantStore")]
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "门店设置")]
    public class MerchantStore : AggregateMongodbUserRoot<MerchantStore>
    {
        [BsonIgnore]
        public string StoreId { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("请您务必输入店铺名称")]
        public string Name { get; set; }

        /// <summary>
        /// 门店logo
        /// </summary>
        [Display(Name = "门店Logo")]
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = true)]
        [HelpBlock("请您务必上传门店 Logo")]
        public string Logo { get; set; }

        /// <summary>
        /// 门店描述
        /// </summary>
        [Display(Name = "门店描述")]
        [Field(ControlsType = ControlsType.TextArea, ListShow = true)]
        [HelpBlock("请您认真输入门店描述")]
        public string Description { get; set; }
    }
}