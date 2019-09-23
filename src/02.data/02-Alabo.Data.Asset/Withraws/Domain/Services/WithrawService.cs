using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Withraws.Domain.Entities;

namespace Alabo.App.Asset.Withraws.Domain.Services {
	public class WithrawService : ServiceBase<Withraw, ObjectId>,IWithrawService  {
	public  WithrawService(IUnitOfWork unitOfWork, IRepository<Withraw, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
