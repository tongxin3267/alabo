using System;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.Industry.Shop.Categories.Domain.Repositories {

    public class CategoryRepository : RepositoryEfCore<Entities.Category, Guid>, ICategoryRepository {

        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}