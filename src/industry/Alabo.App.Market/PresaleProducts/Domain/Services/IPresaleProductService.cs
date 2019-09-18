using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Market.PresaleProducts.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.App.Market.PresaleProducts.Domain.Dtos;
using System.Collections.Generic;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.Domain.Dtos;

namespace Alabo.App.Market.PresaleProducts.Domain.Services {

    public interface IPresaleProductService : IService<PresaleProduct, ObjectId> {

        ProductItemApiOutput GetProducts(ProductApiInput productApiInput);

        PresaleProductItemApiOutput GetPresaleProducts(PresaleProductApiInput input);

        ServiceResult AddPresaleProducts(IList<PresaleProductEdit> viewProduct);

        ServiceResult UpdatePresaleProduct(PresaleProductEdit presaleProduct);

        ServiceResult UpdateStatus(long id, ProductStatus status);
    }
}