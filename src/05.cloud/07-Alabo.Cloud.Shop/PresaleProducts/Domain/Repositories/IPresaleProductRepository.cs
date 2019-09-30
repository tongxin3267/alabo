using Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Entities;
using Alabo.Cloud.Shop.PresaleProducts.Domain.ViewModels;
using Alabo.Domains.Repositories;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.Cloud.Shop.PresaleProducts.Domain.Repositories
{
    public interface IPresaleProductRepository : IRepository<PresaleProduct, ObjectId>
    {
        List<PresaleProductItem> GetPresaleProducts(PresaleProductApiInput input, out long count);
    }
}