using Alabo.Data.People.Teams.Domain.Configs;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Convert = System.Convert;

namespace Alabo.Data.People.Users.Domain.Repositories
{
    public class UserMapRepository : RepositoryEfCore<UserMap, long>, IUserMapRepository
    {
        public UserMapRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserMap Add(UserMap userMap)
        {
            if (userMap == null) {
                throw new ArgumentNullException("userMap");
            }

            var sql = @"INSERT INTO [dbo].[User_UserMap]
               ([UserId] ,[LevelNumber],[TeamNumber]
               ,[ChildNode] ,[ParentMap])
                 VALUES
             (@UserId ,@LevelNumber,@TeamNumber
              ,@ChildNode ,@ParentMap)";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@LevelNumber", userMap.LevelNumber),
                RepositoryContext.CreateParameter("@TeamNumber", userMap.TeamNumber),
                RepositoryContext.CreateParameter("@ChildNode", userMap.ChildNode),
                RepositoryContext.CreateParameter("@ParentMap", userMap.ParentMap)
            };

            var result = RepositoryContext.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value) {
                userMap.Id = Convert.ToInt64(result);
            }

            return userMap;
        }

        public UserMap GetParentMap(long userId)
        {
            var sql = @"SELECT ParentMap FROM User_UserMap WHERE UserId = @UserId";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@userId", userId)))
            {
                var userMap = new UserMap();
                if (reader.Read()) {
                    userMap.ParentMap = reader["ParentMap"].ToString();
                }

                return userMap;
            }
        }

        public UserMap GetSingle(long userId)
        {
            var sql = @"SELECT * FROM User_UserMap WHERE UserId = @UserId";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@UserId", userId)))
            {
                UserMap userMap = null;
                if (reader.Read()) {
                    userMap = ReadUser(reader);
                }

                return userMap;
            }
        }

        public void UpdateMap(long userId, string map)
        {
            var updateMapSql = "UPDATE dbo.User_UserMap SET ParentMap=@ParentMap WHERE UserId=@UserId";
            var parameters2 = new[]
            {
                RepositoryContext.CreateParameter("@UserId", userId),
                RepositoryContext.CreateParameter("@ParentMap", map)
            };

            RepositoryContext.Transation(() =>
            {
                var count = RepositoryContext.ExecuteNonQuery(updateMapSql, parameters2);
            });
        }

        /// <summary>
        ///     根据下面的会员，更新团队信息
        /// </summary>
        /// <param name="childuUserId"></param>
        public void UpdateTeamInfo(long childuUserId = 0)
        {
            var userConfig = Ioc.Resolve<IAutoConfigService>().GetValue<TeamConfig>();
            var userIds = new List<long>
            {
                childuUserId
            };

            if (childuUserId == 0)
            {
                var pageCount = 30; // 每次处理30个
                // 总数
                var totalCount = RepositoryContext.ExecuteScalar("select count(id) from User_UserMap").ConvertToLong();
                var totalPage = totalCount / 30 + 1;
                for (var i = 1; i < totalPage + 1; i++)
                {
                    userIds = new List<long>();
                    var sql =
                        $"SELECT TOP 30 userId FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id ) AS RowNumber,userId FROM User_UserMap  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                    using (var reader = RepositoryContext.ExecuteDataReader(sql))
                    {
                        while (reader.Read()) {
                            userIds.Add(reader["UserId"].ConvertToLong());
                        }
                    }

                    UpdateTeamInfo(userIds, userConfig);
                }
            }
            else
            {
                UpdateTeamInfo(userIds, userConfig);
            }
        }

        private UserMap ReadUser(IDataReader reader)
        {
            var user = new UserMap
            {
                Id = reader["Id"].ConvertToLong(0),
                UserId = reader["UserId"].ConvertToLong(0),
                ParentMap = reader["ParentMap"].ToString(),
                LevelNumber = reader["LevelNumber"].ConvertToLong(0),
                TeamNumber = reader["TeamNumber"].ConvertToLong(0),
                ChildNode = reader["ChildNode"].ToString()
            };
            return user;
        }

        private void UpdateTeamInfo(List<long> userIds, TeamConfig userConfig)
        {
            var sqlList = new List<string>();

            IEnumerable<BatchParameter> batchParameters = new List<BatchParameter>();
            foreach (var userId in userIds)
            {
                var userMap = GetSingle(userId);
                if (userMap != null)
                {
                    // 设置当前会员的直推数量和子会员数据为空
                    var sql =
                        $"update User_UserMap set LevelNumber=0 , ChildNode='',TeamNumber=0 where UserId={userId}";
                    sqlList.Add(sql);
                    var parentMap = new List<ParentMap>();
                    try
                    {
                        parentMap = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
                    }
                    catch (Exception ex)
                    {
                        // 组织架构图数据出错处理
                        Console.WriteLine(ex.Message);
                        userMap.ParentMap = new List<ParentMap>().ToJson(); // 默认值
                        UpdateMap(userMap.UserId, userMap.ParentMap);
                        parentMap = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
                    }

                    // 更新直推会员数量
                    var parentUser = parentMap?.FirstOrDefault(r => r.ParentLevel == 1); // 直推用户Id
                    if (parentUser != null)
                    {
                        sql = $"update User_UserMap set LevelNumber=LevelNumber+1  where UserId={parentUser.UserId}";
                        sqlList.Add(sql);
                    }

                    // 更新团队有效人数的会员数
                    var teamUserIds = parentMap.Where(r => r.ParentLevel <= userConfig.TeamLevel).Select(r => r.UserId)
                        .ToList();
                    if (teamUserIds.Count > 0)
                    {
                        sql =
                            $"update User_UserMap set  TeamNumber=TeamNumber+1 ,ChildNode=ChildNode+',{userId}' where UserId in ({teamUserIds.ToSqlString()})";
                        sqlList.Add(sql);
                    }
                }
            }

            var count = RepositoryContext.ExecuteSqlList(sqlList);
        }
    }
}