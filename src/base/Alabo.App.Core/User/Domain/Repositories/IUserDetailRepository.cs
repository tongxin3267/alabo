﻿using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IUserDetailRepository : IRepository<UserDetail, long> {

        UserDetail Add(UserDetail userDetail);

        /// <summary>
        ///     修改登录密码
        /// </summary>
        /// <param name="userId">  </param>
        /// <param name="password">传入明文即可</param>
        bool ChangePassword(long userId, string password);

        /// <summary>
        ///     修改支付密码
        /// </summary>
        /// <param name="userId">     </param>
        /// <param name="paypassword">传入明文即可</param>
        bool ChangePayPassword(long userId, string paypassword);

        /// <summary>
        ///     Existses the open identifier.
        /// </summary>
        /// <param name="opneId">The opne identifier.</param>
        bool ExistsOpenId(string opneId);

        /// <summary>
        /// </summary>
        /// <param name="userDetail"></param>
        /// <param name="count"></param>
        List<UserDetail> GetList(UserDetailInpt userDetail, out long count);

        /// <summary>
        ///     获取运营中心下的所有会员
        /// </summary>
        /// <param name="userId">用户Id</param>
        IList<long> GetAllServiceCenterUserIds(long userId);
    }
}