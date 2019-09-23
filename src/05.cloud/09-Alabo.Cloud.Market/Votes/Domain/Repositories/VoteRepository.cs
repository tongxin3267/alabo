using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Market.Votes.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Market.Votes.Domain.Repositories;

namespace Alabo.App.Market.Votes.Domain.Repositories {
	public class VoteRepository : RepositoryMongo<Vote, ObjectId>,IVoteRepository  {
	public  VoteRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
