using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Cloud.Wikis.Settings.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.Wikis.Settings.Domain.Services {
	public interface IWikiClassService : IService<WikiClass, ObjectId>  {
	}
	}
