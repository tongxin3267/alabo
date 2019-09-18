using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Market.Votes.Domain.Entities;

namespace Alabo.App.Market.Votes.Domain.Services {
	public class VoteService : ServiceBase<Vote, ObjectId>,IVoteService  {
	public  VoteService(IUnitOfWork unitOfWork, IRepository<Vote, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
