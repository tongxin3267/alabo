using Alabo.Domains.Query.Dto;
using Alabo.Domains.Repositories.Mongo.Extension;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Deliveries.Dtos
{
    /// <summary>
    ///     订单
    /// </summary>
    public class ProductListInput : ApiInputDto
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId StoreId { get; set; }
    }
}