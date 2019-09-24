using System;
using System.Collections.Generic;
using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Market.SecondBuy.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.SecondBuy.Domain.Repositories {
	public interface ISecondBuyOrderRepository : IRepository<SecondBuyOrder, ObjectId>
    {

 

    }
}
