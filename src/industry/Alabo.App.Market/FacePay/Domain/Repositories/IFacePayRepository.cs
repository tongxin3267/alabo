using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Market.FacePay.Domain.Entities;

namespace Alabo.App.Market.FacePay.Domain.Repositories {

    public interface IFacePayRepository : IRepository<Entities.FacePay, ObjectId> {
    }
}