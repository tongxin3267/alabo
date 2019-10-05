using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Targets.Targets.Domain.Entities;

namespace Alabo.Cloud.Targets.Targets.Domain.Services {
	public class TargetService : ServiceBase<Target, ObjectId>,ITargetService  {
	public  TargetService(IUnitOfWork unitOfWork, IRepository<Target, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
