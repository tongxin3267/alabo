using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Entities.Extension;
using Alabo.Industry.Shop.Activitys.Domain.Enum;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Industry.Shop.Activitys.Domain.Entities {

    /// <summary>
    ///     活动记录表
    ///     表结构尚未考虑完整，程序完成时补充
    /// </summary>
    [ClassProperty(Name = "活动记录表")]
    public class ActivityRecord : AggregateUserRoot<ActivityRecord, long> {

        public ActivityRecord() : this(0) {
        }

        public ActivityRecord(long id) : base(id) {
        }

        /// <summary>
        ///     Gets or sets the parent identifier.
        ///     上一个活动记录的Id，比如拼团
        /// </summary>
        [Display(Name = "上一个活动记录的Id")]
        public long ParentId { get; set; }

        /// <summary>
        ///     活动ID
        /// </summary>
        [Display(Name = "活动ID")]
        public long ActivityId { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        [Display(Name = "所属店铺")]
        public long StoreId { get; set; }

        /// <summary>
        ///     关联订单
        /// </summary>
        [Display(Name = "关联订单")]
        public long OrderId { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        [Display(Name = "获取或设置状态")]
        public ActivityRecordStatus Status { get; set; }

        /// <summary>
        ///     活动记录扩展
        /// </summary>
        [Field(ExtensionJson = "ActivityRecordExtension")]
        [Display(Name = "扩展")]
        public string Extension { get; set; }

        /// <summary>
        ///     活动记录扩展
        /// </summary>
        [Display(Name = "活动记录扩展")]

        public ActivityRecordExtension ActivityRecordExtension { get; set; } = new ActivityRecordExtension(); public class ActivityRecoredRecordTableMap : MsSqlAggregateRootMap<ActivityRecord> {

            protected override void MapTable(EntityTypeBuilder<ActivityRecord> builder) {
                builder.ToTable("Shop_ActivityRecord");
            }

            protected override void MapProperties(EntityTypeBuilder<ActivityRecord> builder) {
                //应用程序编号
                builder.HasKey(e => e.Id);
                builder.Ignore(e => e.ActivityRecordExtension);
                builder.Ignore(e => e.Version);
                if (TenantContext.IsTenant) {
                    // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
                }
            }
        }
    }
}