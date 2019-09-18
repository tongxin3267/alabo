using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Articles.Domain.Repositories {

    public class ChannelRepository : RepositoryMongo<Channel, ObjectId>, IChannelRepository {

        public ChannelRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}