using System;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Category.Domain.Repositories {

    public class CategoryPropertyRepository : RepositoryEfCore<CategoryProperty, Guid>, ICategoryPropertyRepository {

        public CategoryPropertyRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}