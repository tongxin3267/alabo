using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Kpis.GradeKpis.Domain.Enum {

    [ClassProperty(Name = "升降类型")]
    public enum GradeKpiType {

        /// <summary>
        /// 晋升
        /// </summary>
        PromotedKpi = 1,

        /// <summary>
        /// 降职
        /// </summary>
        DemotionKpi = 2,
    }
}