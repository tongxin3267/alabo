using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Common.Domain.Enum {

    [ClassProperty(Name = "处理状态")]
    public enum MessageStatus {

        /// <summary>
        ///     待处理
        /// </summary>
        Pending = 1,

        /// <summary>
        ///     处理中，暂未使用
        /// </summary>
        Processing,

        /// <summary>
        ///     处理完毕
        /// </summary>
        Handled,

        /// <summary>
        ///     发生错误
        /// </summary>
        Error,

        /// <summary>
        ///     已取消
        /// </summary>
        Canceld
    }
}