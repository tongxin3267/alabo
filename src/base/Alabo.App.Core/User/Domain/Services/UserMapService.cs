using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.ViewModels;
using Alabo.App.Core.User.ViewModels.Admin;
using Alabo.Core.Extensions;
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

        public List<Entities.User> GetList(long parendId) {
            throw new NotImplementedException();
        }

        public List<long> GetChildUserIds(long parentId) {
            throw new NotImplementedException();
        }

        public void UpdateMap(long userId, long parentId) {
            var map = GetParentMap(parentId);
            if (!map.IsNullOrEmpty()) {
                Repository<IUserMapRepository>().UpdateMap(userId, map);
            }
        }

        public void UpdateTeamInfo(long childuUserId = 0) {
            Repository<IUserMapRepository>().UpdateTeamInfo(childuUserId);
        }

        /// <summary>
        ///     获取团队用户
        ///     根据UserMap.childNode字段获取
        /// </summary>
        /// <param name="userMap"></param>
        public IEnumerable<Entities.User> GetTeamUser(UserMap userMap) {
            if (userMap != null) {
                var userIdStrings = userMap.ChildNode.ToSplitList();
                var userIds = new List<long>();
                userIdStrings.ForEach(e => {
                    var _userId = e.Trim().ConvertToLong(0);
                    if (_userId > 0) {
                        userIds.Add(_userId);
                    }
                });
                var users = Resolve<IUserService>().GetList(userIds);
                return users;
            }

            return null;
        }

        /// <summary>
        ///     更新所有用户的组织架构图
        /// </summary>
        public void UpdateAllUserParentMap() {
            var pageCount = 30; // 每次处理30个
            var totalCount = Repository<IUserMapRepository>().RepositoryContext
                .ExecuteScalar("select count(id) from User_User").ConvertToLong();
            var totalPage = totalCount / 30 + 1;
            for (var i = 1; i < totalPage + 1; i++) {
                var userIds = new List<long>();
                var sql =
                    $"SELECT TOP 30 Id FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id ) AS RowNumber,Id FROM User_User  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                using (var reader = Repository<IUserMapRepository>().RepositoryContext.ExecuteDataReader(sql)) {
                    while (reader.Read()) {
                        userIds.Add(reader["Id"].ConvertToLong());
                    }
                }

                foreach (var userId in userIds) {
                    var user = Resolve<IUserService>().GetSingle(userId);
                    try {
                        UpdateMap(user.Id, user.ParentId);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        ///     获取直推会员、间推、团队的等级分布
        /// </summary>
        /// <param name="query"></param>
        public PagedList<UserGradeInfoView> GetUserGradeInfoPageList(object query) {
            var pageList = Resolve<IGradeInfoService>().GetPagedList(query);
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            dictionary.TryGetValue("type", out var type);
            var gradeInfList = new List<UserGradeInfoView>();
            var userIds = pageList.Select(r => r.UserId).Distinct().ToList();
            var users = Resolve<IUserService>().GetList(userIds);
            foreach (var gradeInfo in pageList) {
                var user = users.FirstOrDefault(r => r.Id == gradeInfo.UserId);
                if (user == null) {
                    continue;
                }

                var view = new UserGradeInfoView();
                //  AutoMapping.SetValue(gradeInfo, view);
                view.Id = gradeInfo.UserId; //Id=用户Id
                view.GradeName = Resolve<IGradeService>().GetGrade(user.GradeId)?.Name;
                view.UserName = gradeInfo.UserName;

                if (type == "recomend") {
                    var result = GetDictionary(gradeInfo.RecomendGradeInfo);
                    view.GradeInfo = result.Item1;
                    view.GradeInfoString = result.Item2;
                    view.TotalCountString = $"直推总数{gradeInfo.RecomendCount}";
                }

                if (type == "second") {
                    var result = GetDictionary(gradeInfo.SecondGradeInfo);
                    view.GradeInfo = result.Item1;
                    view.GradeInfoString = result.Item2;
                    view.TotalCountString = $"间推总数{gradeInfo.SecondCount}";
                }

                if (type == "team") {
                    var result = GetDictionary(gradeInfo.TeamGradeInfo);
                    view.GradeInfo = result.Item1;
                    view.GradeInfoString = result.Item2;
                    view.TotalCountString = $"团队总数{gradeInfo.TeamCount}";
                }

                view.DisplayName = $"直推会员{gradeInfo.RecomendCount}个";
                view.ModifiedTime = gradeInfo.ModifiedTime;
                gradeInfList.Add(view);
            }

            return PagedList<UserGradeInfoView>.Create(gradeInfList, pageList.RecordCount, pageList.PageSize,
                pageList.PageIndex);
        }

        private Tuple<Dictionary<Guid, long>, string> GetDictionary(IEnumerable<GradeInfoItem> gradeInfo) {
            var dictionary = new Dictionary<Guid, long>();
            var str = string.Empty;
            if (gradeInfo != null) {
                gradeInfo.Foreach(r => {
                    var grade = Resolve<IGradeService>().GetGrade(r.GradeId);
                    dictionary.Add(r.GradeId, r.Count);
                    str += $"{grade.Name}({r.Count})  ";
                });
            }

            str = $"<code>{str}</code>";
            return Tuple.Create(dictionary, str);
        }

        #region 修改推荐人

        public ViewRelationUpdate GetUpdateParentUserView(object id) {
            return new ViewRelationUpdate();
        }

        public ServiceResult UpdateParentUser(ViewRelationUpdate view) {
            var user = Resolve<IUserService>().GetSingle(r => r.UserName == view.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户名不存在");
            }

            var parentUser = Resolve<IUserService>().GetSingle(view.ParentUserName);
            if (parentUser == null) {
                return ServiceResult.FailedWithMessage("推荐人不存在");
            }

            if (parentUser.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("推荐人状态不正常");
            }

            var currentUserPay = Resolve<IUserDetailService>().GetSingle(r => r.Id == view.UserId);
            if (view.PayPassword.ToMd5HashString() != currentUserPay.PayPassword) {
                return ServiceResult.FailedWithMessage("支付密码错误！");
            }

            var result = ServiceResult.Success;
            var context = Repository<IUserRepository>().RepositoryContext;
            try {
                context.BeginTransaction();
                user.ParentId = parentUser.Id;
                Resolve<IUserService>().Update(user);

                ParentMapTaskQueue();
                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                result = ServiceResult.FailedWithMessage(ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            if (!result.Succeeded) {
                return ServiceResult.FailedWithMessage("推荐人修改失败");
            }

            return ServiceResult.Success;
        }

        #endregion 修改推荐人

        public ServiceResult UpdateParentUserAfterUserDelete(long userId, long parentId) {
            var userTreeConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();
            // 不修改
            if (!userTreeConfig.AfterDeleteUserUpdateParentMap) {
                return ServiceResult.Success;
            }

            //var user = Resolve<IUserService>().GetSingle(r => r.Id == userId);
            //if (user != null) {
            //    return ServiceResult.FailedWithMessage("该用户没有物理删除会员");
            //}
            var parentUser = Resolve<IUserService>().GetSingle(r => r.Id == parentId);
            if (parentUser == null || parentUser.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户已删除,其推荐人不存在或状态不正常");
            }

            var childUsers = Resolve<IUserService>().GetList(r => r.ParentId == userId);
            var childUserIds = childUsers.Select(r => r.Id).ToList();
            var result = Repository<IUserRepository>().UpdateRecommend(childUserIds, parentId);

            if (result) {
                // 修改直推会员数量
                var recomonedUserCount = Resolve<IUserService>().Count(r => r.ParentId == parentUser.Id);
                var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == parentUser.Id);
                userMap.LevelNumber = recomonedUserCount;
                Resolve<IUserMapService>().Update(userMap);
                ParentMapTaskQueue();
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedWithMessage("修改推荐人失败");
            }
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

        #region 转移团队

        public ViewTransferRelationship GetTransferRelationship(object id) {
            return new ViewTransferRelationship();
        }

        public ServiceResult TransferRelationship(ViewTransferRelationship view) {
            var user = Resolve<IUserService>().GetSingle(r => r.UserName == view.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户名不存在");
            }

            var parentUser = Resolve<IUserService>().GetSingle(view.ParentUserName);
            if (parentUser == null) {
                return ServiceResult.FailedWithMessage("推荐人不存在");
            }

            if (parentUser.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("推荐人状态不正常");
            }

            var currentUserPay = Resolve<IUserDetailService>().GetSingle(r => r.Id == view.UserId);
            if (view.PayPassword.ToMd5HashString() != currentUserPay.PayPassword) {
                return ServiceResult.FailedWithMessage("支付密码错误！");
            }

            var result = UpdateParentUserAfterUserDelete(user.Id, parentUser.Id);

            if (!result.Succeeded) {
                return ServiceResult.FailedWithMessage("推荐人修改失败");
            }

            return ServiceResult.Success;
        }

        #endregion 转移团队
    }
}