using Alabo.Cloud.Cms.Votes.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.Votes.Domain.Services {
	public interface IVoteService : IService<Vote, ObjectId>  {
	}
	}
