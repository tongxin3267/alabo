using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Order.Domain.Dtos {
	public interface IDeliverService : IService<Deliver, ObjectId>  {
	}
	}
