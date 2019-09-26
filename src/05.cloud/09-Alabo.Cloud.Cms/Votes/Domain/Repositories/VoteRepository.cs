using Alabo.Cloud.Cms.Votes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.Votes.Domain.Repositories {
	public class VoteRepository : RepositoryMongo<Vote, ObjectId>,IVoteRepository  {
	public  VoteRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
