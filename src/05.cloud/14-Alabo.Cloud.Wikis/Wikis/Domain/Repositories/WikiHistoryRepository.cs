using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Cloud.Wikis.Wikis.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Cloud.Wikis.Wikis.Domain.Repositories;

namespace Alabo.Cloud.Wikis.Wikis.Domain.Repositories {
	public class WikiHistoryRepository : RepositoryMongo<WikiHistory, ObjectId>,IWikiHistoryRepository  {
	public  WikiHistoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
