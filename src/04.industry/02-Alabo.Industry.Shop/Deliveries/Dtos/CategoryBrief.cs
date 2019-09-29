using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Industry.Shop.Deliveries.Dtos
{
    public class CategoryBrief
    {
        /// <summary>
        ///     商品名称
        /// </summary>
        [Display(Name = "分类名称")]
        public string Name { get; set; }


        /// <summary>
        ///     Id
        /// </summary>
        [Display(Name = "Id")]
        public Guid Id { get; set; }
    }
}