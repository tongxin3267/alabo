using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.People.ServiceCenters.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.People.ServiceCenters.Domain.Repositories;

namespace Alabo.Data.People.ServiceCenters.Domain.Repositories {
	public class ServiceCenterRepository : RepositoryMongo<ServiceCenter, ObjectId>,IServiceCenterRepository  {
	public  ServiceCenterRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
