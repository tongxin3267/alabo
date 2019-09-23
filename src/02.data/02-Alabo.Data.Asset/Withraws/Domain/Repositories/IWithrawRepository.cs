using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Withraws.Domain.Entities;

namespace Alabo.App.Asset.Withraws.Domain.Repositories {

    public interface IWithrawRepository : IRepository<Withraw, long> {
    }
}