using Alabo.Cloud.Cms.Votes.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.Votes.Domain.Repositories {
	public interface IVoteRepository : IRepository<Vote, ObjectId>  {
	}
}
