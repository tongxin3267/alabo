using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.People.UserDigitals.Domain.Entities;

namespace Alabo.Cloud.People.UserDigitals.Domain.Services {
	public class UserDigitalIndexService : ServiceBase<UserDigitalIndex, ObjectId>,IUserDigitalIndexService  {
	public  UserDigitalIndexService(IUnitOfWork unitOfWork, IRepository<UserDigitalIndex, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
