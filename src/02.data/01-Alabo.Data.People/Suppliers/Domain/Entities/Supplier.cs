using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Suppliers.Domain.Entities
{
    [ClassProperty(Name = "供应商")]
    [BsonIgnoreExtraElements]
    [Table("People_Supplier")]
    [AutoDelete(IsAuto = true)]
    public class Supplier : AggregateMongodbRoot<Supplier>
    {
        /// <summary>
        ///     供应商名称
        /// </summary>
        [Display(Name = "供应商名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.TextBox, IsShowBaseSerach = true)]
        public string Name { get; set; }

        [Display(Name = "所属区域")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.CityDropList)]
        public long RegionId { get; set; }

        /// <summary>
        ///     所属区域
        /// </summary>
        [Display(Name = "地址")]
        [Field(ListShow = true, EditShow = false, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.CityDropList)]
        public string Address { get; set; }
    }
}