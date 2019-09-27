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
    public class Circle : UserTypeAggregateRoot<Circle, UserGradeConfig>
    {
        /// <summary>
        ///     所属区域
        /// </summary>
        [Display(Name = "所属区域")]
        [Field(ListShow = true, EditShow = false, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.CityDropList)]
        public string RegionName { get; set; }

        /// <summary>
        ///     商圈所属省份
        /// </summary>
        [Display(Name = "省份编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long ProvinceId { get; set; }

        /// <summary>
        ///     商圈所属城市，可以等于null
        /// </summary>
        [Display(Name = "城市编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long CityId { get; set; }

        /// <summary>
        ///     商圈所属区域
        /// </summary>
        [Display(Name = "区县编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long CountyId { get; set; }

        /// <summary>
        ///     全称
        /// </summary>
        [Display(Name = "全称")]
        [Field(ListShow = true, SortOrder = 5, Width = "350", ControlsType = ControlsType.TextBox,
            IsShowBaseSerach = true)]
        public string FullName { get; set; }
    }
}