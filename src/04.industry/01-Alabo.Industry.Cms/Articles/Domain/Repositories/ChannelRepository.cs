using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Repositories
{
    public class ChannelRepository : RepositoryMongo<Channel, ObjectId>, IChannelRepository
    {
        public ChannelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}