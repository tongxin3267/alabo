using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Products.Dtos;
using Alabo.Industry.Shop.Products.ViewModels;

namespace Alabo.Industry.Shop.Products.Domain.Repositories {

    public interface IProductRepository : IRepository<Entities.Product, long> {

        List<ProductItem> GetProductItems(ProductApiInput input, out long count);
    }
}