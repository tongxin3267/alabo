using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Tenants;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Accounts.Domain.Entities
{
    /// <summary>
    ///     用户资产账户
    ///     资产账户保留用户所有的资产信息
    ///     一个会员可以有多个状态
    /// </summary>
    [ClassProperty(Name = "用户资产账户")]
    public class Account : AggregateDefaultUserRoot<Account>
    {
        /// <summary>
        ///     货币类型id
        /// </summary>
        [Display(Name = "货币类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币量(余额)
        /// </summary>
        [Display(Name = "金额")]
        [Required(ErrorMessage = "请填写操作金额")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     冻结的货币量
        /// </summary>
        [Display(Name = "冻结的货币量")]
        public decimal FreezeAmount { get; set; }

        /// <summary>
        ///     历史累计转入货币量(截至总收入)
        /// </summary>
        [Display(Name = "累计转入货币量")]
        public decimal HistoryAmount { get; set; }

        /// <summary>
        ///     钱包地址
        /// </summary>
        [Display(Name = "钱包地址")]
        public string Token { get; set; }

        /// <summary>
        ///     资产配置，货币类型，不插入数据库
        /// </summary>
        [Display(Name = "资产配置")]
        public MoneyTypeConfig MoneyTypeConfig { get; set; }
    }

    public class AccountTableMap : MsSqlAggregateRootMap<Account>
    {
        protected override void MapTable(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Asset_Account");
        }

        protected override void MapProperties(EntityTypeBuilder<Account> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.MoneyTypeConfig);
            builder.Ignore(e => e.UserName);

            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}