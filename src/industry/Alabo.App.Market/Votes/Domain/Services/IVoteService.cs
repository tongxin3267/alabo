using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Market.Votes.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.Votes.Domain.Services {
	public interface IVoteService : IService<Vote, ObjectId>  {
	}
	}
