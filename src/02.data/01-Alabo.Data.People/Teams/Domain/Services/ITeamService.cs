using System;
using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Domains.Services;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;

namespace Alabo.App.Core.User.Domain.Services {

    public interface ITeamService : IService {

        /// <summary>
        ///     获取团队用户，返回用户Id列表
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        IList<long> GetTeamUsers(long userId);

        /// <summary>
        ///     创世纪
        /// </summary>
        IList<Users.Entities.User> GetTeamByGradeId(long userId, Guid gradeId, bool equalUserGradeId = false);

        /// <summary>
        ///     获取用户的上级用户，返回用户Id列表
        ///     根据组织架构图，获取等级比自己高的所有上级用户
        ///     比如业务员A，则获取经理B，总监C等，同一个高等级指获取一个
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="gradeId"></param>
        /// <param name="equalUserGradeId">用户的等级相同时，是否上级用户是否为团队：比如A->B，A,B都是业务员。如果false：则A不是B的团队</param>
        IList<Users.Entities.User> GetParentTeamByGradeId(long userId, Guid gradeId, bool equalUserGradeId = false);

        /// <summary>
        ///     获取推荐关系图
        ///     获取团队关系图
        ///     比如上级有100个，但是如果团队定义只有三层，则直接返回三个
        /// </summary>
        /// <param name="userId">用户Id</param>
        IEnumerable<ParentMap> GetTeamMap(long userId);

        /// <summary>
        ///     根据ParentId计算组织架构图
        /// 获取用户的上级用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        IList<Users.Entities.User> GetParentUsers(long userId);

        /// <summary>
        ///     获取团队用户
        ///     根据UserMap.childNode字段获取
        /// </summary>
        /// <param name="userId"></param>
        IEnumerable<Users.Entities.User> GetChildUsers(long userId);

        /// <summary>
        /// 根据下面的会员，更新团队信息
        /// </summary>
        /// <param name="childuUserId"></param>
        void UpdateTeamInfo(long childuUserId = 0);

        /// <summary>
        ///     获取团队用户
        ///     根据UserMap.childNode字段获取
        /// </summary>
        /// <param name="userMap"></param>
        IEnumerable<Users.Entities.User> GetTeamUser(UserMap userMap);
    }
}