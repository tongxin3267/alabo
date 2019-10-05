using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Cloud.Targets.Targets.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Cloud.Targets.Targets.Domain.Repositories;

namespace Alabo.Cloud.Targets.Targets.Domain.Repositories {
	public class TargetRepository : RepositoryMongo<Target, ObjectId>,ITargetRepository  {
	public  TargetRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
