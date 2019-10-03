using Alabo.Data.People.Users.Dtos;
using Alabo.Data.People.Users.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Users.Entities;
using Alabo.Users.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Alabo.Data.People.Users.Domain.Repositories
{
    public class UserRepository : RepositoryEfCore<User, long>, IUserRepository
    {
        private const string UserDetailSql =
            @"SELECT dbo.User_User.Id, dbo.User_User.UserName, dbo.User_User.Name, dbo.User_User.Mobile, dbo.User_User.Email, dbo.User_User.ParentId,
                 dbo.User_User.Status, dbo.User_User.GradeId, dbo.User_UserDetail.Remark,
                 dbo.User_UserDetail.OpenId,
                 dbo.User_UserDetail.LastLoginTime, dbo.User_UserDetail.ModifiedTime, dbo.User_UserDetail.LoginNum,
                 dbo.User_UserDetail.LastLoginIp, dbo.User_UserDetail.RegisterIp, dbo.User_UserDetail.CreateTime,
                 dbo.User_UserDetail.Birthday, dbo.User_UserDetail.Avator, dbo.User_UserDetail.Sex, dbo.User_UserDetail.AddressId,
                 dbo.User_UserDetail.RegionId, dbo.User_UserDetail.IdentityStatus,
                  dbo.User_UserDetail.PayPassword, dbo.User_UserDetail.Password,
                 dbo.User_UserMap.LevelNumber, dbo.User_UserMap.TeamNumber,
                 dbo.User_UserMap.ChildNode, dbo.User_UserMap.ParentMap,
                 dbo.User_UserDetail.Id AS DetailId, dbo.User_UserMap.Id AS MapId
 FROM      dbo.User_User INNER JOIN
                 dbo.User_UserDetail ON dbo.User_User.Id = dbo.User_UserDetail.UserId INNER JOIN
                 dbo.User_UserMap ON dbo.User_User.Id = dbo.User_UserMap.UserId ";

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetSingleByMail(string mail)
        {
            var sql = @"SELECT * FROM User_User WHERE Email = @Email";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Email", mail)))
            {
                User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public User GetSingleByMobile(string mobile)
        {
            var sql = @"SELECT * FROM User_User WHERE Mobile = @Mobile";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Mobile", mobile)))
            {
                User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public User GetSingle(string userName)
        {
            var sql = @"SELECT * FROM User_User WHERE UserName = @UserName";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@UserName", userName)))
            {
                User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public User GetSingle(long userId)
        {
            var sql = @"SELECT * FROM User_User WHERE Id = @Id";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Id", userId)))
            {
                User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public User UserTeam(long userId)
        {
            var sql =
                @"SELECT u.* ,vi.ServiceCenterUserId from User_User u  ,(SELECT ud.userId ,ud.ServiceCenterUserId FROM user_userDetail ud  )
                         vi  WHERE vi.userId=u.id and u.Id=@Id";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Id", userId)))
            {
                User user = null;
                if (reader.Read()) {
                    user = ReadUser(reader);
                }

                return user;
            }
        }

        public User GetUserDetail(long userId)
        {
            var sql = $"{UserDetailSql}  where User_User.Id={userId}";
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                User user = null;
                if (reader.Read())
                {
                    user = ReadUser(reader);
                    user.Detail = ReadUserDetail(reader);
                    user.Map = ReadUserMap(reader);
                }

                return user;
            }
        }

        public User GetUserDetail(string userName)
        {
            var sql = $"{UserDetailSql} where User_User.UserName='{userName}'";
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                User user = null;
                if (reader.Read())
                {
                    user = ReadUser(reader);
                    user.Detail = ReadUserDetail(reader);
                    user.Map = ReadUserMap(reader);
                }

                return user;
            }
        }

        public bool CheckUserExists(string userName, string password, out long userId)
        {
            var sql = "SELECT TOP 1 Id FROM dbo.User_User WHERE Name=@Name AND Password=@Password";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Name", userName),
                RepositoryContext.CreateParameter("@Password", password)
            };
            userId = RepositoryContext.ExecuteScalar(sql, parameters).ToInt64();
            return userId > 0;
        }

        public bool ExistsName(string name)
        {
            var sql = "select count(Id) from User_User where UserName=@UserName";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@UserName", name));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        public bool ExistsMail(string mail)
        {
            var sql = "select count(Id) from User_User where EMail=@EMail";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@EMail", mail));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        public bool ExistsMobile(string mobile)
        {
            var sql = "select count(Id) from User_User where Mobile=@mobile";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@mobile", mobile));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        [Obsolete("the Transation has bug")]
        public User Add(User user, List<MoneyTypeConfig> moneyTypes)
        {
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

            using (var transaction = RepositoryContext.BeginNativeDbTransaction())
            {
                try
                {
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
                    AddShareOrder(transaction, user.Id);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

                return user;
            }
        }

        public bool UpdateSingle(User model)
        {
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
        public bool Delete(long userId)
        {
            var sqlList = new List<string>
            {
                $"delete from User_User where Id={userId};",
                $"delete from User_UserType where UserId={userId};",
                $"delete from [Asset_Account] where UserId={userId};",
                $"delete from [User_UserMap] where UserId={userId};",
                $"delete from [User_UserDetail] where UserId={userId};"
            };
            var count = RepositoryContext.ExecuteSqlList(sqlList);
            if (count > 0) {
                return true;
            }

            return false;
        }

        public IList<User> GetViewUserList(UserInput userInput, out long count)
        {
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

            var result = new List<User>();
            var sql = $@"SELECT TOP {userInput.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,* FROM User_User where 1=1 {sqlWhere}
                               ) as A
                        WHERE RowNumber > {userInput.PageSize}*({userInput.PageIndex}-1)  ";
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                while (dr.Read()) {
                    result.Add(ReadUser(dr));
                }
            }

            return result;
        }

        public IList<User> GetList(IList<long> userIds)
        {
            var sql = $@"SELECT * FROM User_User WHERE Id in ({userIds.ToSqlString()})";
            var result = new List<User>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                while (reader.Read()) {
                    result.Add(ReadUser(reader));
                }

                return result;
            }
        }

        public long MaxUserId()
        {
            var sql = "select MAX(Id) from  User_User ";
            var result = RepositoryContext.ExecuteScalar(sql);
            return result.ConvertToLong();
        }

        public bool UpdateRecommend(List<long> userIds, long parentId)
        {
            var sql = $" update  user_user set parentId={parentId} where Id in ({userIds.ToSqlString()})";
            var result = RepositoryContext.ExecuteNonQuery(sql);
            if (result > 0) {
                return true;
            }

            return false;
        }

        private void AddAccount(DbTransaction transaction, long userId, List<MoneyTypeConfig> moneyTypes)
        {
            foreach (var item in moneyTypes)
            {
                var sql = @"INSERT INTO [dbo].[Asset_Account]
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
        private void AddShareOrder(DbTransaction transaction, long userId)
        {
            var sql =
                @"INSERT INTO [dbo].[Things_ShareOrder] ([UserId] ,[Amount]   ,[EntityId] ,[Parameters] ,[Status],[SystemStatus]  ,[TriggerType] ,[Summary],[CreateTime] ,[UpdateTime],[Extension],[ExecuteCount])
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

        private UserDetail AddUserDetail(DbTransaction transaction, UserDetail userDetail)
        {
            if (userDetail == null) {
                throw new ArgumentNullException("userDetail");
            }

            var sql = @"INSERT INTO [dbo].[User_UserDetail]
           ([UserId],[Password],[PayPassword] ,
            [RegionId],[AddressId],[Sex],[Birthday]
           ,[CreateTime],[RegisterIp],[LoginNum],[LastLoginIp],[LastLoginTime],
           [ModifiedTime],[OpenId],[Avator],[IdentityStatus])
             VALUES
           (@UserId,@Password,@PayPassword ,
            @RegionId,@AddressId,@Sex,@Birthday
           ,@CreateTime,@RegisterIp,@LoginNum,@LastLoginIp,@LastLoginTime,
           @ModifiedTime,@OpenId,@Avator,@Identity);
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

                RepositoryContext.CreateParameter("@ModifiedTime", userDetail.ModifiedTime),
                RepositoryContext.CreateParameter("@Identity", userDetail.IdentityStatus)

                // RepositoryContext.CreateParameter("@Remark",userDetail.Remark)
            };

            var result = RepositoryContext.ExecuteScalar(transaction, sql, parameters);
            if (result != null && result != DBNull.Value) {
                userDetail.Id = Convert.ToInt64(result);
            }

            return userDetail;
        }

        private UserMap AddUserMap(DbTransaction transaction, UserMap userMap)
        {
            if (userMap == null) {
                throw new ArgumentNullException("userMap");
            }

            var sql = @"INSERT INTO [dbo].[User_UserMap]
               ([UserId] ,[LevelNumber],[TeamNumber],[ChildNode],[ParentMap])
                 VALUES(@UserId,@LevelNumber,@TeamNumber,@ChildNode ,@ParentMap);select @@identity;";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@UserId", userMap.UserId),
                RepositoryContext.CreateParameter("@LevelNumber", userMap.LevelNumber),
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

        private User ReadUser(IDataReader reader)
        {
            var user = new User
            {
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

        private UserDetail ReadUserDetail(IDataReader reader)
        {
            var userDetail = new UserDetail
            {
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
                Remark = reader["Remark"].ToString(),
                IdentityStatus = (IdentityStatus)reader["IdentityStatus"]
            };
            return userDetail;
        }

        private UserMap ReadUserMap(IDataReader reader)
        {
            var userMap = new UserMap
            {
                Id = reader["DetailId"].ConvertToLong(0),
                UserId = reader["Id"].ConvertToLong(),
                LevelNumber = reader["LevelNumber"].ConvertToLong(),
                TeamNumber = reader["TeamNumber"].ConvertToLong(),
                ChildNode = reader["ChildNode"].ToString(),
                ParentMap = reader["ParentMap"].ToString()
            };
            return userMap;
        }

        private ViewUser ReadViewUser(IDataReader reader)
        {
            var user = new ViewUser
            {
                Id = reader["Id"].ConvertToLong(0),
                UserName = reader["UserName"].ToString(),
                Email = reader["Email"].ToString(),
                Mobile = reader["Mobile"].ToString(),
                Name = reader["Name"].ToString(),
                GradeId = reader["GradeId"].ToGuid(),
                Status = (Status)reader["Status"].ConvertToInt(0),

                ParentId = reader["ParentId"].ConvertToLong(0),
                IdentityStatus = (IdentityStatus)reader["IdentityStatus"],
                Sex = (Sex)reader["Sex"].ConvertToLong(0),
                Avator = reader["Avator"].ToString(),
                CreateTime = reader["CreateTime"].ConvertToDateTime()
            };
            return user;
        }
    }
}