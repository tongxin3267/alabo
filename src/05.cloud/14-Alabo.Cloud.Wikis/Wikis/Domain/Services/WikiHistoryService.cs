using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Wikis.Wikis.Domain.Entities;

namespace Alabo.Cloud.Wikis.Wikis.Domain.Services {
	public class WikiHistoryService : ServiceBase<WikiHistory, ObjectId>,IWikiHistoryService  {
	public  WikiHistoryService(IUnitOfWork unitOfWork, IRepository<WikiHistory, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
