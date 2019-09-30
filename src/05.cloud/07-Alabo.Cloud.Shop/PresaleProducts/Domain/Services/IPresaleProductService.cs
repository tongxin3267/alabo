using Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Dtos;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.Cloud.Shop.PresaleProducts.Domain.Services
{
    public interface IPresaleProductService : IService<PresaleProduct, ObjectId>
    {
        ProductItemApiOutput GetProducts(ProductApiInput productApiInput);

        PresaleProductItemApiOutput GetPresaleProducts(PresaleProductApiInput input);

        ServiceResult AddPresaleProducts(IList<PresaleProductEdit> viewProduct);

        ServiceResult UpdatePresaleProduct(PresaleProductEdit presaleProduct);

        ServiceResult UpdateStatus(long id, ProductStatus status);
    }
}