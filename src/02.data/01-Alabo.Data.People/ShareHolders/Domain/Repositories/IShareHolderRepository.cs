using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.ShareHolders.Domain.Entities;

namespace Alabo.App.Agent.ShareHolders.Domain.Repositories {
	public interface IShareHolderRepository : IRepository<ShareHolder, ObjectId>  {
	}
}
