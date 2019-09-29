using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Stores.Domain.Entities
{
    /// <summary>
    ///     线上商城店铺
    /// </summary>
    [ClassProperty(Name = "供应商管理",
        GroupName = "基本信息,高级选项", Icon = "fa fa-puzzle-piece", SortOrder = 1, Description = "设置以及查看供应商的详细信息")]
    [AutoDelete(IsAuto = true)]
    [Table("People_Store")]
    public class Store : UserTypeAggregateRoot<Store>
    {
        /// <summary>
        ///     是否为平台,
        ///     平台只能有一个
        /// </summary>
        [Display(Name = "是否为平台")]
        public bool IsPlatform { get; set; } = false;

        /// <summary>
        ///     店铺评分相关数据
        /// </summary>
        [Display(Name = "店铺评分")]
        public StoreScore Score { get; set; }
    }

    /// <summary>
    ///     店铺评分
    /// </summary>
    public class StoreScore
    {
        /// <summary>
        ///     人气
        /// </summary>
        public string Popularity { get; set; }

        /// <summary>
        ///     综合评分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        ///     商品得分
        /// </summary>
        public decimal ProductScore { get; set; }

        /// <summary>
        ///     服务得分
        /// </summary>
        public decimal ServiceScore { get; set; }

        /// <summary>
        ///     物流得分
        /// </summary>
        public decimal LogisticsScore { get; set; }
    }
}