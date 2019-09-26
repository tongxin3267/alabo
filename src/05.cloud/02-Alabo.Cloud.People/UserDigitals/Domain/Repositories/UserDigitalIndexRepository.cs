using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Cloud.People.UserDigitals.Domain.Repositories;

namespace Alabo.Cloud.People.UserDigitals.Domain.Repositories {
	public class UserDigitalIndexRepository : RepositoryMongo<UserDigitalIndex, ObjectId>,IUserDigitalIndexRepository  {
	public  UserDigitalIndexRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
