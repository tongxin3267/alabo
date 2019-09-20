using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Agent.ShareHolders.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Agent.ShareHolders.Domain.Repositories;

namespace Alabo.App.Agent.ShareHolders.Domain.Repositories {
	public class ShareHolderRepository : RepositoryMongo<ShareHolder, ObjectId>,IShareHolderRepository  {
	public  ShareHolderRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
