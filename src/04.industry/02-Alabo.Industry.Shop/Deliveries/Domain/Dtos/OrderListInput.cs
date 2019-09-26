using Alabo.Domains.Query.Dto;

namespace Alabo.Industry.Shop.Deliveries.Domain.Dtos {

    /// <summary>
    ///     订单
    /// </summary>
    public class ProductListInput : ApiInputDto {
        public long StoreId { get; set; }
    }
}