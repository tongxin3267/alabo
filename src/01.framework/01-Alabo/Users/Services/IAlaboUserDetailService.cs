using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.Users.Domain.Services {

    public interface IAlaboUserDetailService : IService<UserDetail, long> {
    }
}