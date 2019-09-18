using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Offline.Order.Domain.Entities.Extensions;
using Alabo.App.Offline.Order.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Offline.Order.Domain.Entities
{
    /// <summary>
    /// 订单
    /// </summary>
    [ClassProperty(Name = "订单")]
    [Table("Offline_MerchantOrder")]
    public class MerchantOrder : AggregateDefaultUserRoot<MerchantOrder>
    {
        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "根据Id自动生成12位序列号")]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10)
                {
                    searSerial = $"{Id.ToString()}";
                }
                return searSerial;
            }
        }

        /// <summary>
        /// 店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string MerchantStoreId { get; set; }

        /// <summary>
        /// 支付方式Id
        /// </summary>
        [Display(Name = "支付方式Id")]
        public long PayId { get; set; } = 0;

        /// <summary>
        /// 订单交易状态
        /// </summary>
        [Display(Name = "订单状态")]
        public MerchantOrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public MerchantOrderType OrderType { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 4)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "订单总数量")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 5)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单实际支付的金额")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 6)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     使用账户支付的部分
        /// </summary>
        [Display(Name = "使用账户支付的部分")]
        public string AccountPay { get; set; }

        /// <summary>
        /// 扩展信息以json方式保存
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// extension
        /// </summary>
        public MerchantOrderExtension MerchantOrderExtension { get; set; }
    }

    public class OrderTableMap : MsSqlAggregateRootMap<MerchantOrder>
    {
        protected override void MapTable(EntityTypeBuilder<MerchantOrder> builder)
        {
            builder.ToTable("Offline_MerchantOrder");
        }

        protected override void MapProperties(EntityTypeBuilder<MerchantOrder> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Version);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.MerchantOrderExtension);
        }
    }
}