using System;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Category.Domain.Repositories {

    public class CategoryPropertyValueRepository : RepositoryEfCore<CategoryPropertyValue, Guid>,
        ICategoryPropertyValueRepository {

        public CategoryPropertyValueRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}