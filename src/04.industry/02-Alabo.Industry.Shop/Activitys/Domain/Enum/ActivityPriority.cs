using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Activitys.Domain.Enum {

    [ClassProperty(Name = "活动优先级")]
    public enum ActivityPriority {

        /// <summary>
        ///     独占
        /// </summary>
        Exclusive = 1,

        /// <summary>
        ///     同优先级可重复
        /// </summary>
        SharedWithSamePriority,

        /// <summary>
        ///     可与更高优先级重复
        /// </summary>
        SharedWithHigherPriority,

        /// <summary>
        ///     可与相同或更高优先级重复
        /// </summary>
        SharedWithHigherOrSamePriority,

        /// <summary>
        ///     可与更低优先级重复
        /// </summary>
        SharedWithLowerPriority,

        /// <summary>
        ///     可与相同或更低优先级重复
        /// </summary>
        SharedWithLowerOrSamePriority,

        /// <summary>
        ///     所有可重复享受
        /// </summary>
        SharedAll
    }
}