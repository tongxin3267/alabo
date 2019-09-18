using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.App.Shop.Order.Domain.PcDtos
{
    public class PickUpProductView : Product.Domain.Entities.Product
    {
        public long OrderId { get; set; }
    }
}
