using System;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Categories.Domain.Entities;

namespace Alabo.Industry.Shop.Categories.Domain.Repositories
{
    public class CategoryPropertyRepository : RepositoryEfCore<CategoryProperty, Guid>, ICategoryPropertyRepository
    {
        public CategoryPropertyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}