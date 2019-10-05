using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Cloud.Wikis.Settings.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Cloud.Wikis.Settings.Domain.Repositories;

namespace Alabo.Cloud.Wikis.Settings.Domain.Repositories {
	public class WikiProjectRepository : RepositoryMongo<WikiProject, ObjectId>,IWikiProjectRepository  {
	public  WikiProjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
