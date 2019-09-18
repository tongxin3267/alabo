using System;

namespace Alabo.App.Core.UserType.Domain.Dtos {

    /// <summary>
    ///     用户类型关系图
    /// </summary>
    public class UserTypeParentMap {

        /// <summary>
        ///     层次
        /// </summary>
        public long ParentLevel { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     推荐人用户类型
        ///     前期功能未实现
        /// </summary>
        public Guid UserTypeId { get; set; }
    }
}