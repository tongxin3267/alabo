using System;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Categories.Domain.Entities;

namespace Alabo.Industry.Shop.Categories.Domain.Repositories
{
    public class CategoryPropertyValueRepository : RepositoryEfCore<CategoryPropertyValue, Guid>,
        ICategoryPropertyValueRepository
    {
        public CategoryPropertyValueRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}