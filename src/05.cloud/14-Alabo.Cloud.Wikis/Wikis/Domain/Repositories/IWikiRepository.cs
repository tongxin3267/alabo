using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Wikis.Wikis.Domain.Entities;

namespace Alabo.Cloud.Wikis.Wikis.Domain.Repositories {
	public interface IWikiRepository : IRepository<Wiki, ObjectId>  {
	}
}
