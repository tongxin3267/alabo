namespace Alabo.Domains.Query.Dto
{
    /// <summary>
    ///     Api接口
    /// </summary>
    public class ApiInputDto
    {
        /// <summary>
        ///     客户端当前登录用户的Id
        /// </summary>
        public long LoginUserId { get; set; }

        /// <summary>
        ///     页面大小
        /// </summary>
        public long PageSize { get; set; } = 20;

        /// <summary>
        ///     当前页
        /// </summary>
        public long PageIndex { get; set; } = 1;

        /// <summary>
        ///     主键ID
        /// </summary>
        public string EntityId { get; set; }
    }
}