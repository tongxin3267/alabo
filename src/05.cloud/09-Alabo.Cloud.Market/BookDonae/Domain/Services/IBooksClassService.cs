using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Market.BookDonae.Domain.Dtos;
using Alabo.Domains.Services;
using Alabo.App.Market.BookDonae.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.BookDonae.Domain.Services {

    public interface IBooksClassService : IService<BooksClass, ObjectId> {

        void Init(BookPathHost pathHost);
    }
}