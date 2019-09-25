using System;
using System.Collections.Generic;
using System.Data;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Users.Entities;
using Alabo.Users.Enum;

namespace Alabo.App.Core.User.Domain.Repositories {

    internal class UserDetailRepository : RepositoryEfCore<UserDetail, long>, IUserDetailRepository {

        public UserDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public UserDetail Add(UserDetail userDetail) {
            if (userDetail == null) {
                throw new ArgumentNullException("userDetail");
            }

            var sql = @"INSERT INTO [dbo].[User_UserDetail]
           ([UserId],[Password],[PayPassword] ,
			[RegionId],[AddressId],[Sex],[Birthday]
           ,[CreateTime],[RegisterIp],[LoginNum],[LastLoginIp],[LastLoginTime]
           ,[OpenId] ,[ModifiedTime],[Remark])
     VALUES
           (@UserId,@Password,@PayPassword ,
			@RegionId,@AddressId,@Sex,@Birthday
           ,@CreateTime,@RegisterIp,@LoginNum,@LastLoginIp,@LastLoginTime
           ,@OpenId ,@ModifiedTime,@Remark)
        ";
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

                RepositoryContext.CreateParameter("@OpenId", userDetail.OpenId),
                RepositoryContext.CreateParameter("@ModifiedTime", userDetail.ModifiedTime),
                RepositoryContext.CreateParameter("@Remark", userDetail.Remark)
            };

            var result = RepositoryContext.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value) {
                userDetail.Id = Convert.ToInt64(result);
            }

            return userDetail;
        }

        public bool ChangePassword(long userId, string password) {
            var sql = @"update User_UserDetail set Password=@Password,ModifiedTime=GETDATE() where userId=@userid";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Password", password),
                RepositoryContext.CreateParameter("@userid", userId)
            };
            var count = RepositoryContext.ExecuteNonQuery(sql, parameters);
            if (count > 0) {
                return true;
            }

            return false;
        }

        public bool ChangePayPassword(long userId, string paypassword) {
            var sql =
                @"update User_UserDetail set PayPassword=@PayPassword,ModifiedTime=GETDATE() where userId=@userid";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@PayPassword", paypassword),
                RepositoryContext.CreateParameter("@userid", userId)
            };
            var count = RepositoryContext.ExecuteNonQuery(sql, parameters);
            if (count > 0) {
                return true;
            }

            return false;
        }

        public bool ExistsOpenId(string openId) {
            var sql = "select count(Id) from User_UserDetail where OpenId=@OpenId";
            var result = RepositoryContext.ExecuteScalar(sql, RepositoryContext.CreateParameter("@OpenId", openId));
            if (result == null || result == DBNull.Value) {
                return false;
            }

            return Convert.ToInt64(result) > 0;
        }

        public List<UserDetail> GetList(UserDetailInpt userDetail, out long count) {
            if (userDetail.PageIndex < 0) {
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");
            }

            if (userDetail.PageSize > 100) {
                userDetail.PageSize = 100;
            }

            var sqlWhere = string.Empty;

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM User_User where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            var sql = $@"SELECT TOP {userDetail.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,* FROM User_UserDetail where 1=1 {
                    sqlWhere
                }
                               ) as A
                        WHERE RowNumber > {userDetail.PageSize}*({userDetail.PageIndex}-1)";
            var result = new List<UserDetail>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadUser(dr));
                }
            }

            return result;
        }

        public IList<long> GetAllServiceCenterUserIds(long userId) {
            var sql = $"select UserId  from User_UserDetail where ServiceCenterUserId={userId}";
            IList<long> result = new List<long>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadUserId(dr));
                }
            }

            return result;
        }

        private UserDetail ReadUser(IDataReader reader) {
            var UserDetail = new UserDetail {
                Id = reader["Id"].ConvertToLong(0),
                UserId = reader["UserId"].ConvertToLong(0),
                RegionId = reader["RegionId"].ConvertToLong(),
                AddressId = reader["AddressId"].ToString(),
                Sex = (Sex)reader["Sex"].ConvertToInt(0),
                Birthday = reader["Birthday"].ConvertToDateTime(),
                CreateTime = reader["CreateTime"].ConvertToDateTime(),
                Avator = reader["Avator"].ToString(),
                OpenId = reader["OpenId"].ToString()
            };
            return UserDetail;
        }

        private long ReadUserId(IDataReader reader) {
            return reader["UserId"].ConvertToLong(0);
        }
    }
}