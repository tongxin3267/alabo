using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.People.UserDigitals.Domain.Entities;

namespace Alabo.Cloud.People.UserDigitals.Domain.Services {
	public class UserDigitalService : ServiceBase<UserDigital, ObjectId>,IUserDigitalService  {
	public  UserDigitalService(IUnitOfWork unitOfWork, IRepository<UserDigital, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
