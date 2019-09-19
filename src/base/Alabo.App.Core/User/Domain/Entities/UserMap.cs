using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities.Extensions;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Entities {

    [ClassProperty(Name = "用户组织架构图")]
    public class UserMap : AggregateDefaultUserRoot<UserMap> {

        /// <summary>
        ///     直推会员数量
        /// </summary>
        [Display(Name = "直推会员数量")]
        public long LevelNumber { get; set; } = 0;

        /// <summary>
        ///     团队数量
        ///     下级的团队数量，比如下限的团队数量
        /// </summary>
        [Display(Name = "团队数量")]
        public long TeamNumber { get; set; } = 0;

        /// <summary>
        ///     团队业绩
        /// </summary>
        [Display(Name = "团队业绩")]
        public decimal TeamSales { get; set; } = 0.0M;

        /// <summary>
        ///     下级会员ID
        /// </summary>
        [Display(Name = "下级会员")]
        public string ChildNode { get; set; } = string.Empty;

        /// <summary>
        ///     上级用户组织架构图
        /// </summary>
        [Display(Name = "上级用户组织架构图")]
        public string ParentMap { get; set; } = "";

        /// <summary>
        ///     商城销售统计
        /// </summary>
        [Display(Name = "商城销售统计")]
        public ShopSaleExtension ShopSaleExtension { get; set; } = new ShopSaleExtension();

        /// <summary>
        ///     组织架构图
        /// </summary>
        [Display(Name = "组织架构图列表")]
        public IList<ParentMap> ParentMapList { get; set; } = new List<ParentMap>();

        /// <summary>
        ///     商城销售信息
        /// </summary>
        [Display(Name = "商城销售信息")]
        public string ShopSaleInfo { get; set; }
    }

    public class UserMapTableMap : MsSqlAggregateRootMap<UserMap> {

        protected override void MapTable(EntityTypeBuilder<UserMap> builder) {
            builder.ToTable("User_UserMap");
        }

        protected override void MapProperties(EntityTypeBuilder<UserMap> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.ShopSaleExtension);
            builder.Ignore(e => e.ParentMapList);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
        }
    }

    /// <summary>
    ///     组织架构图
    /// </summary>
    public class UserTree {
        public long Id { get; set; }
        public long PId { get; set; }
        public string Name { get; set; }
        public bool Open { get; set; }
        public bool IsParent { get; set; }

        public string Icon { get; set; }
    }
}