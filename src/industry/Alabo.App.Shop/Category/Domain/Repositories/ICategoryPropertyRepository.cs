using System;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Category.Domain.Repositories {

    public interface ICategoryPropertyRepository : IRepository<CategoryProperty, Guid> {
    }
}