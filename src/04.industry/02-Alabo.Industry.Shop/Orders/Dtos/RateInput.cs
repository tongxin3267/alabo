using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    public class RateInput : ApiInputDto {

        /// <summary>
        ///     评价方式，好评中评，差评
        /// </summary>
        [Display(Name = "评价方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ReviewType ReviewType { get; set; }

        /// <summary>
        ///     商品评分，描述相符
        ///     最终的商品评分需更新到Product表中
        /// </summary>
        [Display(Name = "商品评分")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 5, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long ProductScore { get; set; }

        /// <summary>
        ///     物流速度 物流评分
        /// </summary>
        [Display(Name = "物流评分")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 5, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long LogisticsScore { get; set; }

        /// <summary>
        ///     服务评分
        /// </summary>
        [Display(Name = "服务评分")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 5, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long ServiceScore { get; set; }

        /// <summary>
        ///     评论图片或视频
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        ///     评论详情
        /// </summary>
        [Display(Name = "服务评分")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [MinLength(5, ErrorMessage = "评论内容不能低于5个字")]
        public string Intro { get; set; }
    }
}