using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.People.UserDigitals.Domain.Services {
	public interface IUserDigitalIndexService : IService<UserDigitalIndex, ObjectId>  {
	}
	}
