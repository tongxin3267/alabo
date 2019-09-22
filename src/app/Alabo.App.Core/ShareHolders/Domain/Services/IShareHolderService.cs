using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Agent.ShareHolders.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Agent.ShareHolders.Domain.Services {
	public interface IShareHolderService : IService<ShareHolder, ObjectId>  {
	}
	}
