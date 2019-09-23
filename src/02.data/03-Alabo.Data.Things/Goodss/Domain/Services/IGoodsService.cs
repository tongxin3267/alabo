using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.Things.Goodss.Domain.Services {
	public interface IGoodsService : IService<Goods, long>  {
	}
	}
