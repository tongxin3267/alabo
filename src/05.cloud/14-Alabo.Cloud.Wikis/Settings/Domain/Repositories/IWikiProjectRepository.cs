using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Wikis.Settings.Domain.Entities;

namespace Alabo.Cloud.Wikis.Settings.Domain.Repositories {
	public interface IWikiProjectRepository : IRepository<WikiProject, ObjectId>  {
	}
}
