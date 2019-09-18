using System.Collections.Generic;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Product.Domain.Repositories {

    public interface IProductRepository : IRepository<Entities.Product, long> {

        List<ProductItem> GetProductItems(ProductApiInput input, out long count);
    }
}