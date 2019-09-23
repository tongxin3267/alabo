using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.Things.Goodss.Domain.Entities;

namespace Alabo.Data.Things.Goodss.Domain.Repositories {
	public interface IGoodsLineRepository : IRepository<GoodsLine, ObjectId>  {
	}
}
