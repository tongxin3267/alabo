using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.Targets.Targets.Domain.Entities;

namespace Alabo.Data.Targets.Targets.Domain.Services {
	public class TargetHistoryService : ServiceBase<TargetHistory, ObjectId>,ITargetHistoryService  {
	public  TargetHistoryService(IUnitOfWork unitOfWork, IRepository<TargetHistory, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
