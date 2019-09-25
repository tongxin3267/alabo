namespace Alabo.Users.Dtos {

    /// <summary>
    ///     组织架构关系图
    /// </summary>
    public class ParentMap {

        /// <summary>
        ///     层次
        /// </summary>
        public long ParentLevel { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}