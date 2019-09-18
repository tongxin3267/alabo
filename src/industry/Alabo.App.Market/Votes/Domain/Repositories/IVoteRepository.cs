using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Market.Votes.Domain.Entities;

namespace Alabo.App.Market.Votes.Domain.Repositories {
	public interface IVoteRepository : IRepository<Vote, ObjectId>  {
	}
}
