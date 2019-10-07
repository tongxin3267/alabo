using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Cloud.Wikis.Wikis.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.Wikis.Wikis.Domain.Services {
	public interface IWikiHistoryService : IService<WikiHistory, ObjectId>  {
	}
	}
