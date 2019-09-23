using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.Things.Brands.Domain.Services {

    public interface IBrandService : IService<Brand, ObjectId> {
    }
}