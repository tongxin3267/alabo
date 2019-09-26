using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.ViewModels;
using Alabo.App.Core.User.ViewModels.Admin;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;

namespace Alabo.App.Core.User.Domain.Services {

    public class UserMapService : ServiceBase<UserMap, long>, IUserMapService {
        private readonly IDictionary<long, UserMap> _parentMapCache = new Dictionary<long, UserMap>();

        public UserMapService(IUnitOfWork unitOfWork, IRepository<UserMap, long> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        ///     从缓存中读取架构图
        /// </summary>
        /// <param name="userId">用户Id</param>
        public UserMap GetParentMapFromCache(long userId) {
            if (!_parentMapCache.TryGetValue(userId, out var result)) {
                result = Repository<IUserMapRepository>().GetParentMap(userId);
                if (result != null) {
                    result.ParentMapList = result.ParentMap.DeserializeJson<List<ParentMap>>();
                    if (result.ParentMapList == null) {
                        result.ParentMapList = new List<ParentMap>();
                    }

                    if (!_parentMapCache.ContainsKey(userId)) {
                        _parentMapCache.Add(userId, result);
                    }
                }
            }

            return result;
        }

        public UserMap GetSingle(long userId) {
            //var userMap= Repository<IUserMapRepository>().GetSingle(userId);
            var userMap = GetSingle(r => r.UserId == userId);
            if (userMap != null) {
                //userMap.GradeInfoExtension = userMap.GradeInfo.DeserializeJson<GradeInfoExtension>();
                //if (userMap.GradeInfoExtension == null) {
                //    userMap.GradeInfoExtension = new GradeInfoExtension();
                //}

                userMap.ParentMapList = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
                if (userMap.ParentMapList == null) {
                    userMap.ParentMapList = new List<ParentMap>();
                }
            }

            return userMap;
        }

        public string GetParentMap(long parentId) {
            if (parentId == 0) {
                return new List<ParentMap>().ToJson();
            }

            var userMap = GetSingle(parentId);

            var parentMapList = new List<ParentMap>();
            // 如果为空，则给默认值
            if (userMap == null || parentId <= 0) {
                return new List<ParentMap>().ToJson();
            }

            // 如果推荐的关系图为空个，则初始化推荐人关系图
            // userMap.ParentMap.ToStr().Length<10 字符串格式不正确
            if (userMap.ParentMap.IsNullOrEmpty() || userMap.ParentMap.ToStr().Length < 10) {
                userMap.ParentMap = new List<ParentMap>().ToJson(); // 默认值
                Repository<IUserMapRepository>().UpdateMap(userMap.UserId, userMap.ParentMap);
                userMap = GetSingle(parentId);
            } else {
                parentMapList = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            }

            // 新关系图
            var newParentMapList = new List<ParentMap>
            {
                //新会员添加上去
                new ParentMap
                {
                    UserId = parentId,
                    ParentLevel = 1
                }
            };
            // 将老会员赋值上去
            foreach (var item in parentMapList) {
                newParentMapList.Add(new ParentMap {
                    UserId = item.UserId,
                    ParentLevel = item.ParentLevel + 1
                });
            }

            return newParentMapList.ToJson();
        }

        public void UpdateMap(long userId, long parentId) {
            var map = GetParentMap(parentId);
            if (!map.IsNullOrEmpty()) {
                Repository<IUserMapRepository>().UpdateMap(userId, map);
            }
        }

        public ServiceResult UpdateParentUserAfterUserDelete(long userId, long parentId) {
            //TODO 9月重构注释
            //var userTreeConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();
            //// 不修改
            //if (!userTreeConfig.AfterDeleteUserUpdateParentMap) {
            //    return ServiceResult.Success;
            //}

            ////var user = Resolve<IUserService>().GetSingle(r => r.Id == userId);
            ////if (user != null) {
            ////    return ServiceResult.FailedWithMessage("该用户没有物理删除会员");
            ////}
            //var parentUser = Resolve<IUserService>().GetSingle(r => r.Id == parentId);
            //if (parentUser == null || parentUser.Status != Status.Normal) {
            //    return ServiceResult.FailedWithMessage("用户已删除,其推荐人不存在或状态不正常");
            //}

            //var childUsers = Resolve<IUserService>().GetList(r => r.ParentId == userId);
            //var childUserIds = childUsers.Select(r => r.Id).ToList();
            //var result = Repository<IUserRepository>().UpdateRecommend(childUserIds, parentId);

            //if (result) {
            //    // 修改直推会员数量
            //    var recomonedUserCount = Resolve<IUserService>().Count(r => r.ParentId == parentUser.Id);
            //    var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == parentUser.Id);
            //    userMap.LevelNumber = recomonedUserCount;
            //    Resolve<IUserMapService>().Update(userMap);
            //    ParentMapTaskQueue();
            //    return ServiceResult.Success;
            //} else {
            //    return ServiceResult.FailedWithMessage("修改推荐人失败");
            //}
            return null;
        }

        public void ParentMapTaskQueue() {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.UserParentUpdate,
                CheckLastOne = true,
                ServiceName = typeof(IUserMapService).Name,
                Method = "UpdateAllUserParentMap"
            };
            Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
        }
    }
}