using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Wikis.Settings.Domain.Entities;

namespace Alabo.Cloud.Wikis.Settings.Domain.Services {
	public class WikiClassService : ServiceBase<WikiClass, ObjectId>,IWikiClassService  {
	public  WikiClassService(IUnitOfWork unitOfWork, IRepository<WikiClass, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
