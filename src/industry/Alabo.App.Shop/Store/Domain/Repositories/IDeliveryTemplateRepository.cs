using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.Store.Domain.Entities;

namespace Alabo.App.Shop.Store.Domain.Repositories {
	public interface IDeliveryTemplateRepository : IRepository<DeliveryTemplate, ObjectId>  {
	}
}
