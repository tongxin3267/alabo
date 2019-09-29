using Alabo.Framework.Basic.Address.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    public class AddressEditViewIn : UserAddress
    {
        public long OrderId { get; set; }
    }
}