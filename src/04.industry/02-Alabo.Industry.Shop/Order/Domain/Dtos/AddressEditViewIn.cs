using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.User.Domain.Entities;

namespace Alabo.App.Shop.Order.Domain.Dtos
{
    public class AddressEditViewIn : UserAddress
    {
        public long OrderId { get; set; }
    }
}
