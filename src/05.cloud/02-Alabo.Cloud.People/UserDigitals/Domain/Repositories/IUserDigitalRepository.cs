using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Cloud.People.UserDigitals.Domain.Entities;

namespace Alabo.Cloud.People.UserDigitals.Domain.Repositories {
	public interface IUserDigitalRepository : IRepository<UserDigital, ObjectId>  {
	}
}
