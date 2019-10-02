using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.ServiceCenters.Domain.Entities;

namespace Alabo.Data.People.ServiceCenters.Domain.Services {
	public class ServiceCenterService : ServiceBase<ServiceCenter, ObjectId>,IServiceCenterService  {
	public  ServiceCenterService(IUnitOfWork unitOfWork, IRepository<ServiceCenter, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
