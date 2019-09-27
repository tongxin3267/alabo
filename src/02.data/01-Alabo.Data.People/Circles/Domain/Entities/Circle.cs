using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Circles.Domain.Entities
{
    /// <summary>
    /// 商圈
    /// </summary>
    [ClassProperty(Name = "商圈")]
    [BsonIgnoreExtraElements]
    [Table("People_Circle")]
    [AutoDelete(IsAuto = true)]
    public class Circle : UserTypeAggregateRoot<Circle>
    {
        /// <summary>
        ///     所属区域
        /// </summary>
        [Display(Name = "所属区域")]
        [Field(ListShow = true, EditShow = false, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.CityDropList)]
        public string RegionName { get; set; }
    }
}