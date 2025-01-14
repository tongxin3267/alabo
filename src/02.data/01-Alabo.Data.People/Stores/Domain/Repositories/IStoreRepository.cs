﻿using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Repositories
{
    public interface IStoreRepository : IRepository<Store, ObjectId>
    {
    }
}