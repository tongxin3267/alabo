using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Asset.Withraws.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Asset.Withraws.Domain.Services {
	public interface IWithrawService : IService<Withraw, ObjectId>  {
	}
	}
