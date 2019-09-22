﻿using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.ViewModels;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Convert = System.Convert;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class UserRepository : RepositoryEfCore<Entities.User, long>, IUserRepository {

        private const string userDetailSql =
            @"SELECT dbo.User_User.Id, dbo.User_User.UserName, dbo.User_User.Name, dbo.User_User.Mobile, dbo.User_User.Email, dbo.User_User.ParentId,
                 dbo.User_User.Status, dbo.User_User.GradeId, dbo.User_UserDetail.Remark,
                 dbo.User_UserDetail.OpenId,
                 dbo.User_UserDetail.LastLoginTime, dbo.User_UserDetail.ModifiedTime, dbo.User_UserDetail.LoginNum,
                 dbo.User_UserDetail.LastLoginIp, dbo.User_UserDetail.RegisterIp, dbo.User_UserDetail.CreateTime,
                 dbo.User_UserDetail.Birthday, dbo.User_UserDetail.Avator, dbo.User_UserDetail.Sex, dbo.User_UserDetail.AddressId,
                 dbo.User_UserDetail.RegionId,dbo.User_UserDetail.ServiceCenterUserId, dbo.User_UserDetail.IsServiceCenter,
                  dbo.User_UserDetail.PayPassword, dbo.User_UserDetail.Password,
                 dbo.User_UserMap.LevelNumber, dbo.User_UserMap.TeamNumber,
                 dbo.User_UserMap.TeamSales, dbo.User_UserMap.ChildNode, dbo.User_UserMap.ParentMap,
                 dbo.User_UserDetail.Id AS DetailId, dbo.User_UserMap.Id AS MapId
 FROM      dbo.User_User INNER JOIN
                 dbo.User_UserDetail ON dbo.User_User.Id = dbo.User_UserDetail.UserId INNER JOIN
                 dbo.User_UserMap ON dbo.User_User.Id = dbo.User_UserMap.UserId ";

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public Entities.User GetSingleByMail(string mail) {
            var sql = @"SELECT * FROM User_User WHERE Email = @Email";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Email", mail))) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public Entities.User GetSingleByMobile(string mobile) {
            var sql = @"SELECT * FROM User_User WHERE Mobile = @Mobile";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Mobile", mobile))) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public Entities.User GetSingle(string UserName) {
            var sql = @"SELECT * FROM User_User WHERE UserName = @UserName";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@UserName", UserName))) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public Entities.User GetSingle(long userId) {
            var sql = @"SELECT * FROM User_User WHERE Id = @Id";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Id", userId))) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public Entities.User UserTeam(long userId) {
            var sql =
                @"SELECT u.* ,vi.ServiceCenterUserId from User_User u  ,(SELECT ud.userId ,ud.ServiceCenterUserId FROM user_userDetail ud  )
                         vi  WHERE vi.userId=u.id and u.Id=@Id";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Id", userId))) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public Entities.User GetUserDetail(long userId) {
            var sql = $"{userDetailSql}  where User_User.Id={userId}";
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                    user.Detail = ReadUserDetail(reader);
                    user.Map = ReadUserMap(reader);
                }

                return user;
            }
        }

        public Entities.User GetUserDetail(string UserName) {
            var sql = $"{userDetailSql} where User_User.UserName='{UserName}'";
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                Entities.User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                    user.Detail = ReadUserDetail(reader);
                    user.Map = ReadUserMap(reader);
                }

                return user;
            }
        }

        public bool CheckUserExists(string UserName, string password, out long userId) {
            var sql = "SELECT TOP 1 Id FROM dbo.User_User WHERE Name=@Name AND Password=@Password";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Name", UserName),
                RepositoryContext.CreateParameter("@Password", password)
            };
            userId = RepositoryContext.ExecuteScalar(sql, parameters).ToInt64();
            return userId > 0;
        }

        public bool ExistsName(string name) {
            var sql = "select count(Id) from User_User where UserName=@UserName";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@UserName", name));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        public bool ExistsMail(string mail) {
            var sql = "select count(Id) from User_User where EMail=@EMail";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@EMail", mail));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        public bool ExistsMobile(string mobile) {
            var sql = "select count(Id) from User_User where Mobile=@mobile";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@mobile", mobile));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        [Obsolete("the Transation has bug")]
        public Entities.User Add(Entities.User user, List<MoneyTypeConfig> moneyTypes) {
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            var sql =
                "INSERT INTO dbo.User_User ([Name], [Email], [Mobile], [UserName],[Status],[GradeId],[ParentId] ) VALUES  (@Name,@Email,@Mobile,@UserName ,@Status,@GradeId,@ParentId); select @@identity;";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Name", user.Name),
                RepositoryContext.CreateParameter("@Email", user.Email),
                RepositoryContext.CreateParameter("@Status", user.Status),
                RepositoryContext.CreateParameter("@GradeId", user.GradeId),
                RepositoryContext.CreateParameter("@Mobile", user.Mobile),
                RepositoryContext.CreateParameter("@ParentId", user.ParentId),
                RepositoryContext.CreateParameter("@UserName", user.UserName)
            };

            using (var transaction = RepositoryContext.BeginNativeDbTransaction()) {
                try {
                    var result = Convert.ToInt64(RepositoryContext.ExecuteScalar(transaction, sql, parameters));
                    if (result != 0) {
                        user.Id = Convert.ToInt64(result);
                    }

                    user.Map.UserId = user.Id;
                    user.Map = AddUserMap(transaction, user.Map);
                    user.Detail.UserId = user.Id;
                    //添加用户详情
                    user.Detail = AddUserDetail(transaction, user.Detail);
                    //添加用户资产
                    AddAccount(transaction, user.Id, moneyTypes);
                    //添加分润订单
                    //TODO 9月重构注释
                    AddShareOrder(transaction, user.Id);
                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw ex;
                }

                return user;
            }
        }

        public bool UpdateSingle(Entities.User model) {
            if (model == null) {
                throw new ArgumentNullException("model");
            }

            var sql =
                "UPDATE dbo.User_User SET Name=@Name, Email=@Email, Mobile=@Mobile, Status=@Status,GradeId=@GradeId WHERE Id=@Id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Name", model.Name),
                RepositoryContext.CreateParameter("@Email", model.Email),
                RepositoryContext.CreateParameter("@Mobile", model.Mobile),
                RepositoryContext.CreateParameter("@Status", model.Status),
                RepositoryContext.CreateParameter("@GradeId", model.GradeId),
                RepositoryContext.CreateParameter("@Id", model.Id)
            };
            var count = RepositoryContext.ExecuteNonQuery(sql, parameters);
            if (count > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     真正删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        [Obsolete("This function is not debug 17/12/21", true)]
        public bool Delete(long userId) {
            var sqlList = new List<string>
            {
                $"delete from User_User where Id={userId};",
                $"delete from User_UserType where UserId={userId};",
                $"delete from [Finance_Account] where UserId={userId};",
                $"delete from [User_UserMap] where UserId={userId};",
                $"delete from [User_UserDetail] where UserId={userId};"
            };
            var count = RepositoryContext.ExecuteSqlList(sqlList);
            if (count > 0) {
                return true;
            }

            return false;
        }

        public IList<Entities.User> GetViewUserList(UserInput userInput, out long count) {
            if (userInput.PageIndex < 0) {
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");
            }

            if (userInput.PageSize > 100) {
                userInput.PageSize = 100;
            }

            var sqlWhere = string.Empty;
            if (Convert.ToInt16(userInput.Status) > 0) {
                sqlWhere = $"{sqlWhere} AND Status={(int)userInput.Status}";
            }

            if (userInput.ParentId > 0) {
                sqlWhere = $"{sqlWhere} AND ParentId={userInput.ParentId}";
            }

            if (!userInput.Email.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND Email= '{userInput.Email}' ";
            }

            if (!userInput.Mobile.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND Mobile= '{userInput.Mobile}' ";
            }

            if (!userInput.UserName.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND UserName='{userInput.UserName}' ";
            }

            if (!userInput.Name.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND Name='{userInput.Name}'";
            }

            if (!userInput.GradeId.IsGuidNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND GradeId='{userInput.GradeId}'";
            }

            if (userInput.ServiceCenterId > 0) {
                sqlWhere = $"{sqlWhere} AND ServiceCenterUserId={userInput.ServiceCenterId}";
            }

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM User_User where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            var result = new List<Entities.User>();
            var sql = $@"SELECT TOP {userInput.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,* FROM User_User where 1=1 {sqlWhere}
                               ) as A
                        WHERE RowNumber > {userInput.PageSize}*({userInput.PageIndex}-1)  ";
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadUser(dr));
                }
            }

            return result;
        }

        public UserStatisticOutput GetUserStatistic(long userId) {
            var statistic = new UserStatisticOutput();

            var sqlWhere = $"ParentId={userId}";
            var sqlUnativated = $"{sqlWhere} AND Mobile = UserName and Mobile like 'WX%'";
            var sqlToday = $"{sqlWhere} AND DateDiff(dd,CreateTime,getdate())=0";
            var sqlThisMonth = $"{sqlWhere} AND DateDiff(mm,CreateTime,getdate())=0";

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM User_User where {sqlWhere}";
            statistic.Total = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            sqlCount = $"SELECT COUNT(Id) [Count] FROM User_User where {sqlUnativated}";
            statistic.UnActivated = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            sqlCount = $"SELECT COUNT(Id) [Count] FROM User_User where {sqlToday}";
            statistic.Today = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            sqlCount = $"SELECT COUNT(Id) [Count] FROM User_User where {sqlThisMonth}";
            statistic.ThisMonth = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            return statistic;
        }

        public EarnStatisticOutput GetEarnStatistic(long userId) {
            var statistic = new EarnStatisticOutput();

            var sqlWhere = $"UserId={userId}";
            var sqlToday = $"{sqlWhere} AND DateDiff(dd,CreateTime,getdate())=0";

            var sqlCount = $"SELECT SUM(Amount) FROM [Share_Reward] where {sqlWhere}";
            statistic.Total = RepositoryContext.ExecuteScalar(sqlCount).ConvertToDecimal(0);

            sqlCount = $"SELECT SUM(Amount) FROM [Share_Reward] where {sqlToday}";
            statistic.Today = RepositoryContext.ExecuteScalar(sqlCount).ConvertToDecimal(0);

            statistic.Balance = 123;

            return statistic;
        }

        public IList<Entities.User> GetList(IList<long> userIds) {
            var sql = $@"SELECT * FROM User_User WHERE Id in ({userIds.ToSqlString()})";
            var result = new List<Entities.User>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    result.Add(ReadUser(reader));
                }

                return result;
            }
        }

        public long MaxUserId() {
            var sql = "select MAX(Id) from  User_User ";
            var result = RepositoryContext.ExecuteScalar(sql);
            return result.ConvertToLong();
        }

        public bool UpdateRecommend(List<long> userIds, long parentId) {
            var sql = $" update  user_user set parentId={parentId} where Id in ({userIds.ToSqlString()})";
            var result = RepositoryContext.ExecuteNonQuery(sql);
            if (result > 0) {
                return true;
            }

            return false;
        }

        private void AddAccount(DbTransaction transaction, long userId, List<MoneyTypeConfig> moneyTypes) {
            foreach (var item in moneyTypes) {
                var sql = @"INSERT INTO [dbo].[Finance_Account]
                            ([UserId],[MoneyTypeId],[Amount],[FreezeAmount],[HistoryAmount],[Token]) VALUES
                             (@UserId,@MoneyTypeId,0,0,0,@Token)";
                //TODO 9月重构注释
                // var token = Ioc.Resolve<IAccountService>().GetToken(userId, item);
                var token = string.Empty;
                var parameters = new[]
                {
                    RepositoryContext.CreateParameter("@UserId", userId),
                    RepositoryContext.CreateParameter("@MoneyTypeId", item.Id),
                    RepositoryContext.CreateParameter("@Token", token)
                };
                RepositoryContext.ExecuteScalar(transaction, sql, parameters);
            }
        }

        /// <summary>
        ///     添加一条分润订单记录
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="userId">用户Id</param>
        private void AddShareOrder(DbTransaction transaction, long userId) {
            var sql =
                @"INSERT INTO [dbo].[Task_ShareOrder] ([UserId] ,[Amount]   ,[EntityId] ,[Parameters] ,[Status],[SystemStatus]  ,[TriggerType] ,[Summary],[CreateTime] ,[UpdateTime],[Extension],[ExecuteCount])
                            VALUES
                         (@UserId ,@Amount   ,@EntityId ,@Parameters ,@Status,@SystemStatus  ,@TriggerType ,@Summary,@CreateTime ,@UpdateTime,@Extension,@ExecuteCount)
            ";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@UserId", userId),
                RepositoryContext.CreateParameter("@Amount", 1),
                RepositoryContext.CreateParameter("@EntityId", userId),
                RepositoryContext.CreateParameter("@Parameters", string.Empty),
                RepositoryContext.CreateParameter("@Status", Convert.ToInt16(ShareOrderStatus.Pending)),
                RepositoryContext.CreateParameter("@SystemStatus", Convert.ToInt16(ShareOrderSystemStatus.Pending)),
                RepositoryContext.CreateParameter("@TriggerType", Convert.ToInt16(TriggerType.UserReg)),
                RepositoryContext.CreateParameter("@Summary", string.Empty),
                RepositoryContext.CreateParameter("@CreateTime", DateTime.Now),
                RepositoryContext.CreateParameter("@Extension", string.Empty),
                RepositoryContext.CreateParameter("@ExecuteCount", 0),
                RepositoryContext.CreateParameter("@UpdateTime", DateTime.Now)
            };
            RepositoryContext.ExecuteScalar(transaction, sql, parameters);
        }

        private UserDetail AddUserDetail(DbTransaction transaction, UserDetail userDetail) {
            if (userDetail == null) {
                throw new ArgumentNullException("userDetail");
            }

            var sql = @"INSERT INTO [dbo].[User_UserDetail]
           ([UserId],[Password],[PayPassword] ,
            [RegionId],[AddressId],[Sex],[Birthday]
           ,[CreateTime],[RegisterIp],[LoginNum],[LastLoginIp],[LastLoginTime],
           [ModifiedTime],[OpenId],[Avator])
             VALUES
           (@UserId,@Password,@PayPassword ,
            @RegionId,@AddressId,@Sex,@Birthday
           ,@CreateTime,@RegisterIp,@LoginNum,@LastLoginIp,@LastLoginTime,
           @ModifiedTime,@OpenId,@Avator);
            select @@identity;";

            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@UserId", userDetail.UserId),
                RepositoryContext.CreateParameter("@Password", userDetail.Password),
                RepositoryContext.CreateParameter("@PayPassword", userDetail.PayPassword),

                RepositoryContext.CreateParameter("@RegionId", userDetail.RegionId),
                RepositoryContext.CreateParameter("@AddressId", userDetail.AddressId),
                RepositoryContext.CreateParameter("@Sex", userDetail.Sex),
                RepositoryContext.CreateParameter("@Birthday", userDetail.Birthday),

                RepositoryContext.CreateParameter("@CreateTime", userDetail.CreateTime),
                RepositoryContext.CreateParameter("@RegisterIp", userDetail.RegisterIp),
                RepositoryContext.CreateParameter("@LoginNum", userDetail.LoginNum),
                RepositoryContext.CreateParameter("@LastLoginIp", userDetail.LastLoginIp),
                RepositoryContext.CreateParameter("@LastLoginTime", userDetail.LastLoginTime),

                RepositoryContext.CreateParameter("@OpenId", userDetail.OpenId ?? "default"),
                RepositoryContext.CreateParameter("@Avator", userDetail.Avator ?? ""),

                RepositoryContext.CreateParameter("@ModifiedTime", userDetail.ModifiedTime)

                // RepositoryContext.CreateParameter("@Remark",userDetail.Remark)
            };

            var result = RepositoryContext.ExecuteScalar(transaction, sql, parameters);
            if (result != null && result != DBNull.Value) {
                userDetail.Id = Convert.ToInt64(result);
            }

            return userDetail;
        }

        private UserMap AddUserMap(DbTransaction transaction, UserMap userMap) {
            if (userMap == null) {
                throw new ArgumentNullException("userMap");
            }

            var sql = @"INSERT INTO [dbo].[User_UserMap]
               ([UserId] ,[LevelNumber],[TeamNumber] ,[TeamSales]
               ,[ChildNode] ,[ParentMap])
                 VALUES
             (@UserId,@LevelNumber,@TeamNumber ,@TeamSales
              ,@ChildNode ,@ParentMap);
                select @@identity;";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@UserId", userMap.UserId),
                RepositoryContext.CreateParameter("@LevelNumber", userMap.LevelNumber),
                RepositoryContext.CreateParameter("@TeamSales", userMap.TeamSales),
                RepositoryContext.CreateParameter("@TeamNumber", userMap.TeamNumber),
                RepositoryContext.CreateParameter("@ChildNode", userMap.ChildNode),
                RepositoryContext.CreateParameter("@ParentMap", userMap.ParentMap)
            };

            var result = RepositoryContext.ExecuteScalar(transaction, sql, parameters);
            if (result != null && result != DBNull.Value) {
                userMap.Id = Convert.ToInt64(result);
            }

            return userMap;
        }

        private Entities.User ReadUser(IDataReader reader) {
            var user = new Entities.User {
                Id = reader["Id"].ConvertToLong(0),
                ParentId = reader["ParentId"].ConvertToLong(0),
                UserName = reader["UserName"].ToString(),
                Email = reader["Email"].ToString(),
                Mobile = reader["Mobile"].ToString(),
                Name = reader["Name"].ToString(),
                GradeId = reader["GradeId"].ToGuid(),
                Status = (Status)reader["Status"].ConvertToInt(0)
            };
            return user;
        }

        private UserDetail ReadUserDetail(IDataReader reader) {
            var userDetail = new UserDetail {
                Id = reader["DetailId"].ConvertToLong(0),
                UserId = reader["Id"].ConvertToLong(),
                Password = reader["Password"].ToString(),
                PayPassword = reader["PayPassword"].ToString(),

                RegionId = reader["RegionId"].ConvertToLong(),
                AddressId = reader["AddressId"].ToString(),
                Sex = (Sex)reader["Sex"].ConvertToInt(),
                Birthday = reader["Birthday"].ToDateTime(),

                CreateTime = reader["CreateTime"].ToDateTime(),
                RegisterIp = reader["RegisterIp"].ToString(),
                LoginNum = reader["LoginNum"].ConvertToLong(),
                LastLoginIp = reader["LastLoginIp"].ToString(),
                LastLoginTime = reader["LastLoginTime"].ToDateTime(),

                Avator = reader["Avator"].ToString(),
                OpenId = reader["OpenId"].ToString(),
                ModifiedTime = reader["ModifiedTime"].ToDateTime(),
                Remark = reader["Remark"].ToString()
            };
            return userDetail;
        }

        private UserMap ReadUserMap(IDataReader reader) {
            var userMap = new UserMap {
                Id = reader["DetailId"].ConvertToLong(0),
                UserId = reader["Id"].ConvertToLong(),
                LevelNumber = reader["LevelNumber"].ConvertToLong(),
                TeamSales = reader["TeamSales"].ConvertToDecimal(),
                TeamNumber = reader["TeamNumber"].ConvertToLong(),
                ChildNode = reader["ChildNode"].ToString(),
                ParentMap = reader["ParentMap"].ToString()
            };
            return userMap;
        }

        private ViewUser ReadViewUser(IDataReader reader) {
            var user = new ViewUser {
                Id = reader["Id"].ConvertToLong(0),
                UserName = reader["UserName"].ToString(),
                Email = reader["Email"].ToString(),
                Mobile = reader["Mobile"].ToString(),
                Name = reader["Name"].ToString(),
                GradeId = reader["GradeId"].ToGuid(),
                Status = (Status)reader["Status"].ConvertToInt(0),

                ParentId = reader["ParentId"].ConvertToLong(0),
                IdentityStatus = (IdentityStatus)reader["IdentityStatus"],
                IsServiceCenter = (bool)reader["IsServiceCenter"],
                ServiceCenterUserId = reader["ServiceCenterUserId"].ConvertToLong(0),
                Sex = (Sex)reader["Sex"].ConvertToLong(0),
                Avator = reader["Avator"].ToString(),
                CreateTime = reader["CreateTime"].ConvertToDateTime()
            };
            return user;
        }
    }
}