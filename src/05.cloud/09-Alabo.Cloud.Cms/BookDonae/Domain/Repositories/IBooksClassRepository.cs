using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Repositories
{
    public interface IBooksClassRepository : IRepository<BooksClass, ObjectId>
    {
    }
}