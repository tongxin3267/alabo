using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Shop.Store.Domain.Dtos {

    /// <summary>
    ///     订单
    /// </summary>
    public class ProductListInput : ApiInputDto {
        public long StoreId { get; set; }
    }
}