using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Shop.Store.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Shop.Store.Domain.Repositories;

namespace Alabo.App.Shop.Store.Domain.Repositories {
	public class DeliveryTemplateRepository : RepositoryMongo<DeliveryTemplate, ObjectId>,IDeliveryTemplateRepository  {
	public  DeliveryTemplateRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
