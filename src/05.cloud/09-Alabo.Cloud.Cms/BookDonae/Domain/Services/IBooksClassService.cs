using Alabo.Cloud.Cms.BookDonae.Domain.Dtos;
using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Services {

    public interface IBooksClassService : IService<BooksClass, ObjectId> {

        void Init(BookPathHost pathHost);
    }
}