using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IAlaboUserDetailRepository : IRepository<UserDetail, long> {
    }
}