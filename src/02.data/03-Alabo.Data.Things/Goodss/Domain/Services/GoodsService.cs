using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.Things.Goodss.Domain.Entities;

namespace Alabo.Data.Things.Goodss.Domain.Services {
	public class GoodsService : ServiceBase<Goods, long>,IGoodsService  {
	public  GoodsService(IUnitOfWork unitOfWork, IRepository<Goods, long> repository) : base(unitOfWork, repository){
	}
	}
}
