using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.ViewModels;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Reports.User.Model {

    /// <summary>
    /// 会员基础数据统计
    /// </summary>
    [ClassProperty(Name = "会员基础数据", Icon = "fa fa-building", Description = "会员基础数据，包括会员总数，激活数，等级分布等信息")]
    public class UserBaseReport : IReportModel {

        /// <summary>
        /// 会员总数
        /// </summary>
        [Display(Name = "会员总数")]
        public long UserTotalNumber { get; set; }

        /// <summary>
        /// 激活会员数,正常的会员数量
        /// </summary>
        [Display(Name = "激活会员数")]
        public long UserNormalNumber { get; set; }

        /// <summary>
        /// 冻结的会员数
        /// </summary>
        [Display(Name = "冻结会员数")]
        public long UserFreezeNumber { get; set; }

        /// <summary>
        /// 已删除会员数
        /// </summary>
        [Display(Name = "已删除会员数")]
        public long UserDeleteNumber { get; set; }

        /// <summary>
        /// 会员等级分布，统计每个等级多少个会员
        /// </summary>
        public IList<ViewReport<long>> GradeNumber { get; set; }
    }
}