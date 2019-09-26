using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.User.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Schedules;
using _01_Alabo.Cloud.Core.UserTree.Domain.UI;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Users.Dtos;

namespace _01_Alabo.Cloud.Core.UserTree.Domain.Service {

    public class TreeUpdateService : ServiceBase, ITreeUpdateService {

        public TreeUpdateService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        /// 检查所有的会员关系图是否正确
        /// </summary>

        public void ParentMapTaskQueue() {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.UserParentUpdate,
                CheckLastOne = true,
                ServiceName = typeof(IUserMapService).Name,
                Method = "UpdateAllUserParentMap"
            };
            Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
        }

        public ServiceResult CheckOutUserMap() {
            //  UpdateMap(113);
            var pageCount = 30; // 每次处理30个
            var totalCount = Repository<IUserTreeRepository>().RepositoryContext
                .ExecuteScalar("select count(id) from User_User").ConvertToLong();
            var totalPage = totalCount / 30 + 1;
            for (var i = 1; i <= totalPage; i++) {
                var userIds = new List<long>();
                var sql =
                    $"SELECT TOP 30 Id FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id  ) AS RowNumber,Id FROM User_User  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                using (var reader = Repository<IUserTreeRepository>().RepositoryContext.ExecuteDataReader(sql)) {
                    while (reader.Read()) {
                        userIds.Add(reader["Id"].ConvertToLong());
                    }
                }

                foreach (var userId in userIds) {
                    var user = Resolve<IUserService>().GetSingle(userId);
                    var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == userId);
                    var parentMap = userMap.ParentMap.ToObject<List<ParentMap>>();
                    if (user.ParentId == 0) {
                        if (parentMap.Count != 0) {
                            return ServiceResult.FailedWithMessage($"会员{user.UserName},Id{user.Id}家谱图错误");
                        }
                    } else {
                        var fristMap = parentMap.FirstOrDefault();
                        if (fristMap == null || fristMap.UserId != user.ParentId) {
                            return ServiceResult.FailedWithMessage($"会员{user.UserName},Id{user.Id}家谱图错误");
                        }
                    }
                }
            }
            return ServiceResult.Success;
        }

        /// <summary>
        /// 更新所有会员的架构
        /// </summary>
        public void UpdateAllUserMap() {
            // 运营之前先更新这个update User_UserMap set ParentMap=''
            var pageCount = 30; // 每次处理30个
            var totalCount = Repository<IUserTreeRepository>().RepositoryContext
                .ExecuteScalar("select count(id) from User_User").ConvertToLong();
            var totalPage = totalCount / 30 + 1;
            for (var i = 1; i <= totalPage; i++) {
                var userIds = new List<long>();
                var sql =
                    $"SELECT TOP 30 Id FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id  ) AS RowNumber,Id FROM User_User  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                using (var reader = Repository<IUserTreeRepository>().RepositoryContext.ExecuteDataReader(sql)) {
                    while (reader.Read()) {
                        userIds.Add(reader["Id"].ConvertToLong());
                    }
                }
                foreach (var userId in userIds) {
                    UpdateMap(userId);
                }
            }
        }

        private void UpdateMap(long userId) {
            var user = Resolve<IUserService>().GetSingle(userId);
            if (user.ParentId > 0) {
                var parentUserMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == user.ParentId);
                // 父级关系图未生成
                if (parentUserMap.ParentMap.IsNullOrEmpty()) {
                    UpdateMap(parentUserMap.UserId);
                } else {
                    var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == userId);
                    userMap.ParentMap = Resolve<IUserMapService>().GetParentMap(user.ParentId);
                    Resolve<IUserMapService>().Update(userMap);
                }
            } else {
                var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == userId);
                if (userMap != null) {
                    userMap.ParentMap = Resolve<IUserMapService>().GetParentMap(user.ParentId);
                    Resolve<IUserMapService>().Update(userMap);
                }
            }
        }
    }
}