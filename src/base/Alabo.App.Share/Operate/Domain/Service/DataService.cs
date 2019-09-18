using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Open.Operate.Domain.Service {

    public class DataService : ServiceBase, IDataService {
        /// <summary>
        /// 检查所有的会员关系图是否正确
        /// </summary>

        public ServiceResult CheckOutUserMap() {
            //  UpdateMap(113);
            var pageCount = 30; // 每次处理30个
            var totalCount = Repository<IUserMapRepository>().RepositoryContext
                .ExecuteScalar("select count(id) from User_User").ConvertToLong();
            var totalPage = totalCount / 30 + 1;
            for (var i = 1; i <= totalPage; i++) {
                var userIds = new List<long>();
                var sql =
                    $"SELECT TOP 30 Id FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id  ) AS RowNumber,Id FROM User_User  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                using (var reader = Repository<IUserMapRepository>().RepositoryContext.ExecuteDataReader(sql)) {
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
            var totalCount = Repository<IUserMapRepository>().RepositoryContext
                .ExecuteScalar("select count(id) from User_User").ConvertToLong();
            var totalPage = totalCount / 30 + 1;
            for (var i = 1; i <= totalPage; i++) {
                var userIds = new List<long>();
                var sql =
                    $"SELECT TOP 30 Id FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id  ) AS RowNumber,Id FROM User_User  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                using (var reader = Repository<IUserMapRepository>().RepositoryContext.ExecuteDataReader(sql)) {
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

        public DataService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}