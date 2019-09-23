using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.Things.Goodss.Domain.Repositories;

namespace Alabo.Data.Things.Goodss.Domain.Repositories {
	public class GoodsLineRepository : RepositoryMongo<GoodsLine, ObjectId>,IGoodsLineRepository  {
	public  GoodsLineRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
