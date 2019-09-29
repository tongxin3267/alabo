using Alabo.Domains.Repositories.Mongo.Extension;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Domains.Query.Dto
{
    /// <summary>
    ///     Class PagedInputDto.
    /// </summary>
    public class PagedInputDto : EntityDto
    {
        /// <summary>
        ///     Gets or sets the size of the 分页.
        /// </summary>
        public long PageSize { get; set; } = 15;

        /// <summary>
        ///     Gets or sets the index of the 分页.
        /// </summary>
        public long PageIndex { get; set; } = 1;

        /// <summary>
        ///     搜索关键字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     货号
        /// </summary>
        public string Bn { get; set; }

        /// <summary>
        ///     供应商
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

        /// <summary>
        ///     会员ID
        /// </summary>
        public long UserId { get; set; }
    }

    /// <summary>
    ///     Class PagedInputDto.
    /// </summary>
    public class PagedInputDto<T> : EntityDto<T>
    {
        /// <summary>
        ///     Gets or sets the size of the 分页.
        /// </summary>
        public long PageSize { get; set; } = 15;

        /// <summary>
        ///     Gets or sets the index of the 分页.
        /// </summary>
        public long PageIndex { get; set; } = 1;
    }

    /// <summary>
    ///     基础表单数据服务
    /// </summary>
    public class FormInputDto : EntityDto
    {
        /// <summary>
        ///     Gets or sets the 服务.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        ///     Gets or sets the 添加 或 更新.
        /// </summary>
        public string AddOrUpdate { get; set; }

        /// <summary>
        ///     Gets or sets the 视图.
        /// </summary>
        public string View { get; set; }

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public string Id { get; set; }
    }
}