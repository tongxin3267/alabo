using Alabo.Linq;

namespace Alabo.UI.Design.AutoReports.Dtos {

    /// <summary>
    ///     Report统计参数类
    /// </summary>
    public class CountReportInput {

        /// <summary>
        ///     字段查询提交
        /// </summary>
        public EntityQueryCondition Condition { get; set; }

        /// <summary>
        ///     实体类型,支持SqlService，和Mongodb
        ///     User,Order,HudongRecord
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        ///     附加枚举字段，外键，AutoConfig等，必须为枚举类型，AutoConfig
        ///     比如Pay表,PayStatus字段
        ///     生成四条线：所有，等待付款，付款成功，处理失败
        ///     1.会员数量统计 type:User （一条线）
        ///     2.会员根据状态统计:type:User,SpecialField:Status
        ///     3.会员根据AutoConfig统计:type:User,SpecialField:gradeId
        ///     3.订单数量统计：type:Order
        ///     4.订单根据状态统计：type:Order,SpecialField:Status
        ///     5.支付根据状态统计：type:Pay,SpecialField:Status
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     分页信息，构建table
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        ///     分页信息，构建table
        /// </summary>
        public int PageIndex { get; set; } = 1;
    }
}