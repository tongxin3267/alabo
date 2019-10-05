using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Targets.Iterations.Domain.Entities;

namespace Alabo.Cloud.Targets.Iterations.Domain.Services {
	public class IterationService : ServiceBase<Iteration, ObjectId>,IIterationService  {
	public  IterationService(IUnitOfWork unitOfWork, IRepository<Iteration, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
