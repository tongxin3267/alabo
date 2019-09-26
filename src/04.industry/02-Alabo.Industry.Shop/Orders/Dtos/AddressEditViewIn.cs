using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Framework.Basic.Address.Domain.Entities;

namespace Alabo.App.Shop.Order.Domain.Dtos
{
    public class AddressEditViewIn : UserAddress
    {
        public long OrderId { get; set; }
    }
}
