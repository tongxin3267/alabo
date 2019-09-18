using System;
using Alabo.Domains.Repositories;
using Alabo.App.Market.PresaleProducts.Domain.Entities;
using Alabo.App.Market.PresaleProducts.Domain.Dtos;
using Alabo.App.Market.PresaleProducts.Domain.ViewModels;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Alabo.App.Market.PresaleProducts.Domain.Repositories {

    public interface IPresaleProductRepository : IRepository<PresaleProduct, ObjectId> {

        List<PresaleProductItem> GetPresaleProducts(PresaleProductApiInput input, out long count);
    }
}