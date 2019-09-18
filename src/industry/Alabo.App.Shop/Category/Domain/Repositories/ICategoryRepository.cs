using System;
using Alabo.Domains.Repositories;
using CategoryModel = Alabo.App.Shop.Category.Domain.Entities.Category;

namespace Alabo.App.Shop.Category.Domain.Repositories {

    public interface ICategoryRepository : IRepository<CategoryModel, Guid> {
    }
}