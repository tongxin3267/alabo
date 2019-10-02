using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.People.ServiceCenters.Domain.Entities;

namespace Alabo.Data.People.ServiceCenters.Domain.Repositories {
	public interface IServiceCenterRepository : IRepository<ServiceCenter, ObjectId>  {
	}
}
