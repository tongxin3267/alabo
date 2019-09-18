using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.Order.Domain.Dtos;

namespace Alabo.App.Shop.Order.Domain.Dtos {
	public interface IDeliverRepository : IRepository<Deliver, ObjectId>  {
	}
}
