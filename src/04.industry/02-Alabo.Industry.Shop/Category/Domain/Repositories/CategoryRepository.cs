using System;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Category.Domain.Repositories {

    public class CategoryRepository : RepositoryEfCore<Entities.Category, Guid>, ICategoryRepository {

        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}