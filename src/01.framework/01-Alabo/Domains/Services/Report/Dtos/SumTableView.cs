using Alabo.Domains.Query.Dto;
using System;
using System.Collections.Generic;

namespace Alabo.Domains.Services.Report.Dtos
{
    public class SumTableInput : PagedInputDto
    {
        /// <summary>
        ///     实体类型，通过Type可以获取表格
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     附件查询条件
        /// </summary>
        public string SqlWhere { get; set; }

        /// <summary>
        ///     其他查询参数
        /// </summary>
        public object Query { get; set; }

        /// <summary>
        ///     字段列表,只统计long和decamel类型
        /// </summary>
        public List<string> Fields { get; set; }

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
        public string SpecialField { get; set; }
    }

    /// <summary>
    ///     输出
    /// </summary>
    public class SumTableOutput
    {
        /// <summary>
        ///     日期格式
        ///     格式：2019-6-26
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///     数据项
        /// </summary>
        public List<SumTableOutputItems> Items { get; set; }
    }

    /// <summary>
    ///     数据统计项
    /// </summary>
    public class SumTableOutputItems
    {
        /// <summary>
        ///     数据项名称
        ///     比如订单数量，去字段特性Filed或DisplayName的值
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     值从数据库中获取
        /// </summary>
        public decimal Value { get; set; }
    }
}