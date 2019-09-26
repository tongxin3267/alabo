using Alabo.Cloud.Cms.Votes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.Votes.Domain.Services {
	public class VoteService : ServiceBase<Vote, ObjectId>,IVoteService  {
	public  VoteService(IUnitOfWork unitOfWork, IRepository<Vote, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
