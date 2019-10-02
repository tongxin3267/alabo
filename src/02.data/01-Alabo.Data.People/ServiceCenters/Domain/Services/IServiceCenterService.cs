using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.People.ServiceCenters.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.People.ServiceCenters.Domain.Services {
	public interface IServiceCenterService : IService<ServiceCenter, ObjectId>  {
	}
	}
