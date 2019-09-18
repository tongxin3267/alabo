using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.AfterSale.Domain.Entities;

namespace Alabo.App.Shop.AfterSale.Domain.Repositories {
	public interface IEvaluateRepository : IRepository<Evaluate, ObjectId>  {
	}
}
