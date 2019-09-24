using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Market.SecondBuy.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Market.SecondBuy.Domain.Repositories;
using System.Collections.Generic;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.SecondBuy.Domain.Repositories
{
    public class SecondBuyOrderRepository : RepositoryMongo<SecondBuyOrder, ObjectId>, ISecondBuyOrderRepository
    {
        public SecondBuyOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
