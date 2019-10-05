using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.Targets.Targets.Domain.Repositories;

namespace Alabo.Data.Targets.Targets.Domain.Repositories {
	public class TargetHistoryRepository : RepositoryMongo<TargetHistory, ObjectId>,ITargetHistoryRepository  {
	public  TargetHistoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
