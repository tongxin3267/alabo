using System;
using System.Collections.Generic;
using System.Data;
using Alabo.App.Core.UserType.Domain.Dtos;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;

namespace Alabo.App.Core.UserType.Domain.Repositories {

    internal class UserTypeRepository : RepositoryEfCore<Entities.UserType, long>, IUserTypeRepository {

        public UserTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public Entities.UserType GetSingle(long userId, Guid userTypeId) {
            var sql = @"SELECT * FROM User_UserType WHERE UserId = @userId and UserTypeId=@userTypeId";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@UserId", userId),
                RepositoryContext.CreateParameter("@UserTypeId", userTypeId)
            };
            using (var reader = RepositoryContext.ExecuteDataReader(sql, parameters)) {
                Entities.UserType userType = null;
                if (reader.Read()) {
                    userType = ReadUserType(reader);
                }

                return userType;
            }
        }

        public Entities.UserType GetSingle(long id) {
            var sql = @"SELECT * FROM User_UserType WHERE Id = @Id";
            using (var reader = RepositoryContext.ExecuteDataReader(sql, RepositoryContext.CreateParameter("@Id", id))) {
                Entities.UserType userType = null;
                if (reader.Read()) {
                    userType = ReadUserType(reader);
                }

                return userType;
            }
        }

        public Entities.UserType GetSingle(Guid userTypeId, long entityId) {
            var sql = @"SELECT * FROM User_UserType WHERE UserTypeId = @UserTypeId and EntityId=@EntityId";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@UserTypeId", userTypeId),
                RepositoryContext.CreateParameter("@EntityId", entityId)
            };
            using (var reader = RepositoryContext.ExecuteDataReader(sql, parameters)) {
                Entities.UserType userType = null;
                if (reader.Read()) {
                    userType = ReadUserType(reader);
                }

                return userType;
            }
        }

        public IList<Entities.UserType> GetViewUserTypeList(UserTypeInput userTypeInput, out long count) {
            if (userTypeInput.PageIndex < 0) {
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");
            }

            if (userTypeInput.PageSize > 100) {
                userTypeInput.PageSize = 100;
            }

            var sqlWhere = string.Empty;
            if (userTypeInput.Status.HasValue) {
                sqlWhere = $"{sqlWhere} AND Status={(int)userTypeInput.Status}";
            }

            if (userTypeInput.ParentUserId > 0) {
                sqlWhere = $"{sqlWhere} AND ParentId={userTypeInput.ParentUserId}";
            }

            if (userTypeInput.UserId != 0) {
                sqlWhere = $"{sqlWhere} AND UserId='{userTypeInput.UserId}' ";
            }

            if (!userTypeInput.Name.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND Name='{userTypeInput.Name}'";
            }

            if (!userTypeInput.GradeId.IsGuidNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND GradeId='{userTypeInput.GradeId}'";
            }

            if (!userTypeInput.UserTypeId.IsGuidNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND UserTypeId='{userTypeInput.UserTypeId}'";
            }

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM User_UserType where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            var result = new List<Entities.UserType>();
            var sql = $@"SELECT TOP {userTypeInput.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,* FROM User_UserType where 1=1 {
                    sqlWhere
                }
                               ) as A
                        WHERE RowNumber > {userTypeInput.PageSize}*({userTypeInput.PageIndex}-1)  ";
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadUserType(dr));
                }
            }

            return result;
        }

        public IList<Guid> UserAllGradeId(long userId) {
            var sql = $@"select GradeId from [User_UserType] where userId={userId} ";
            IList<Guid> result = new List<Guid>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    var gradeId = reader["parentId"].ToGuid();
                    result.Add(gradeId);
                }
            }

            return result;
        }

        private Entities.UserType ReadUserType(IDataReader reader) {
            var userType = new Entities.UserType {
                Id = reader["Id"].ConvertToLong(0),
                ParentUserId = reader["ParentUserId"].ConvertToLong(0),
                UserTypeId = reader["UserTypeId"].ToGuid(),
                ParentMap = reader["ParentMap"].ToString(),
                Extensions = reader["Extensions"].ToString(),
                CreateTime = reader["CreateTime"].ConvertToDateTime(),
                EntityId = reader["EntityId"].ConvertToLong(),
                Name = reader["Name"].ToString(),
                GradeId = reader["GradeId"].ToGuid(),
                Status = (UserTypeStatus)reader["Status"].ConvertToInt(0),
                UserId = reader["UserId"].ConvertToLong(0)
            };
            return userType;
        }
    }
}