using Alabo.Industry.Shop.Products.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.PcDtos
{
    public class PickUpProductView : Product
    {
        public long OrderId { get; set; }
    }
}