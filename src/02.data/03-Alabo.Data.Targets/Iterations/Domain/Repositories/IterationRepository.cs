using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Cloud.Targets.Iterations.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Cloud.Targets.Iterations.Domain.Repositories;

namespace Alabo.Cloud.Targets.Iterations.Domain.Repositories {
	public class IterationRepository : RepositoryMongo<Iteration, ObjectId>,IIterationRepository  {
	public  IterationRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
