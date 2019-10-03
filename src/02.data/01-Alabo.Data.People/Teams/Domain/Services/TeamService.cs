using Alabo.Data.People.Teams.Domain.Configs;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Data.People.Teams.Domain.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        /// <summary>
        ///     The 会员 map repository
        /// </summary>
        private readonly IUserMapRepository _userMapRepository;

        /// <summary>
        ///     The 会员 repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        public TeamService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepository = Repository<IUserRepository>();
            _userMapRepository = Repository<IUserMapRepository>();
        }

        /// <summary>
        ///     获取团队用户，返回用户Id列表
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public IList<long> GetTeamUsers(long userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     一个高等级指保留一个
        ///     获取用户的上级用户，返回用户Id列表
        ///     根据组织架构图，获取等级比自己高的所有上级用户
        ///     比如业务员A，则获取经理B，总监C等，同一个高等级指获取一个
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="gradeId">当前用户的等级</param>
        /// <param name="equalUserGradeId"></param>
        public IList<User> GetParentTeamByGradeId(long userId, Guid gradeId, bool equalUserGradeId = false)
        {
            var grades = Resolve<IGradeService>().GetUserGradeList();
            var grade = grades.FirstOrDefault(r => r.Id == gradeId);
            if (grade == null) {
                throw new ValidException("等级Id传入出错");
            }

            // 所有上级用户
            var teamUser = GetParentUsers(userId);
            // 升级点高于当前等级的
            if (equalUserGradeId) {
                grades = grades.Where(r => r.Contribute >= grade.Contribute).ToList();
            } else {
                grades = grades.Where(r => r.Contribute > grade.Contribute).ToList();
            }

            var result = new List<User>();
            foreach (var item in teamUser)
            {
                var parentGrade = grades.FirstOrDefault(r => r.Id == item.GradeId);
                if (parentGrade != null)
                {
                    result.Add(item);
                    // 获取上一个更高等级的
                    grades = grades.Where(r => r.Contribute > grade.Contribute).ToList();
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the team by grade identifier.
        /// </summary>
        public IList<User> GetTeamByGradeId(long userId, Guid gradeId, bool equalUserGradeId = false)
        {
            var grades = Resolve<IGradeService>().GetUserGradeList();
            var grade = grades.FirstOrDefault(r => r.Id == gradeId);
            if (grade == null) {
                throw new ValidException("等级Id传入出错");
            }

            var user = Resolve<IUserService>().GetSingle(userId);
            // 所有上级用户
            var teamUser = GetParentUsers(userId);
            // 升级点高于当前等级的
            if (equalUserGradeId) {
                grades = grades.Where(r => r.Contribute >= grade.Contribute).ToList();
            } else {
                grades = grades.Where(r => r.Contribute > grade.Contribute).ToList();
            }

            long parentContribute = 0;
            var result = new List<User>();
            result.Add(user);
            var contributeMin = grades.Min(u => u.Contribute);
            foreach (var item in teamUser)
            {
                if (contributeMin >= 500000) {
                    return result;
                }

                var parentGrade = grades.FirstOrDefault(r => r.Id == item.GradeId);

                if (parentGrade != null)
                {
                    //主任
                    if (contributeMin < 10000)
                    {
                        //代码未整理
                        if (parentGrade.Contribute >= 10000 && parentContribute < parentGrade.Contribute)
                        {
                            result.Add(item);
                            parentContribute = parentGrade.Contribute;
                        }
                        else
                        {
                            if (parentGrade.Contribute < 10000 && parentContribute < 10000) {
                                result.Add(item);
                            }
                        }

                        //找到营业部停止
                        if (parentGrade.Contribute >= 500000) {
                            return result;
                        }
                    }
                    else
                    {
                        //主任以外判断
                        if (gradeId != parentGrade.Id)
                        {
                            if (parentGrade.Contribute >= 500000)
                            {
                                result.Add(item);
                                return result;
                            }

                            result.Add(item);
                            // 获取上一个更高等级的
                            grades = grades.Where(r => r.Contribute > grade.Contribute).ToList();
                        }
                    }
                }
            }

            return result;
        }

        public void UpdateTeamInfo(long childuUserId = 0)
        {
            Repository<IUserMapRepository>().UpdateTeamInfo(childuUserId);
        }

        /// <summary>
        ///     获取团队用户
        ///     根据UserMap.childNode字段获取
        /// </summary>
        /// <param name="userMap"></param>
        public IEnumerable<User> GetTeamUser(UserMap userMap)
        {
            if (userMap != null)
            {
                var userIdStrings = userMap.ChildNode.ToSplitList();
                var userIds = new List<long>();
                userIdStrings.ForEach(e =>
                {
                    var userId = e.Trim().ConvertToLong(0);
                    if (userId > 0) {
                        userIds.Add(userId);
                    }
                });
                var users = Resolve<IUserService>().GetList(userIds);
                return users;
            }

            return null;
        }

        /// <summary>
        ///     获取推荐关系图,根据团队等级定义
        ///     获取团队关系图
        /// </summary>
        /// <param name="userId">用户Id</param>
        public IEnumerable<ParentMap> GetTeamMap(long userId)
        {
            var userMap = Resolve<IUserMapService>().GetSingle(userId);

            if (userMap != null)
            {
                var maps = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
                if (maps != null)
                {
                    var userConfig = Resolve<IAutoConfigService>().GetValue<TeamConfig>();
                    return maps.Where(r => r.ParentLevel <= userConfig.TeamLevel);
                }
            }

            return null;
        }

        /// <summary>
        ///     获取上级团队用户，根团队配置
        /// </summary>
        /// <param name="userId">用户Id</param>
        public IList<User> GetParentUsers(long userId)
        {
            var parnetMap = GetTeamMap(userId);
            if (parnetMap != null)
            {
                var users = _userRepository.GetList(parnetMap.Select(r => r.UserId).ToList());
                var resultList = new List<User>();
                // 排序
                foreach (var item in parnetMap)
                {
                    var user = users.FirstOrDefault(r => r.Id == item.UserId);
                    if (user != null) {
                        resultList.Add(user);
                    }
                }

                return resultList;
            }

            return null;
        }

        /// <summary>
        ///     获取用户的团队用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<User> GetChildUsers(long userId)
        {
            var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == userId);
            if (userMap != null)
            {
                var userIdStrings = userMap.ChildNode.ToSplitList();
                var userIds = new List<long>();
                userIdStrings.ForEach(e =>
                {
                    var idItem = e.Trim().ConvertToLong(0);
                    if (idItem > 0) {
                        userIds.Add(idItem);
                    }
                });
                var users = Resolve<IUserService>().GetList(userIds);
                return users;
            }

            return null;
        }
    }
}