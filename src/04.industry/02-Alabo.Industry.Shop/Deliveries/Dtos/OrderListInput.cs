using Alabo.Domains.Query.Dto;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Dtos
{
    /// <summary>
    ///     订单
    /// </summary>
    public class ProductListInput : ApiInputDto
    {
        public ObjectId StoreId { get; set; }
    }
}