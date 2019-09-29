using System;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Categories.Domain.Entities;

namespace Alabo.Industry.Shop.Categories.Domain.Repositories
{
    public interface ICategoryPropertyValueRepository : IRepository<CategoryPropertyValue, Guid>
    {
    }
}