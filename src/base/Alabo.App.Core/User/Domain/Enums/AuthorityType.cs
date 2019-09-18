using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Enums {

    [ClassProperty(Name = "数据类型")]
    public enum AuthorityType : byte {

        /// <summary>
        ///     Boolean型
        /// </summary>
        Boolean = 1,

        /// <summary>
        ///     Int32型
        /// </summary>
        Int32 = 2,

        /// <summary>
        ///     Int64型
        /// </summary>
        Int64 = 3,

        /// <summary>
        ///     Double型
        /// </summary>
        Double = 4,

        /// <summary>
        ///     DateTime类型
        /// </summary>
        DateTime = 5,

        /// <summary>
        ///     DateTime范围类型
        /// </summary>
        DateTimeRange = 6
    }
}