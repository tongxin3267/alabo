using Alabo.Cloud.People.UserTree.Domain.Configs;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Users.Entities;
using System.Collections.Generic;

namespace Alabo.Cloud.People.UserTree.Domain.Repositories
{
    public class UserMapRepository : RepositoryEfCore<UserMap, long>, IUserTreeRepository
    {
        public UserMapRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region 组织架构图

        public List<Users.Entities.UserTree> GetTree(long userId, UserTreeConfig userTreeConfig,
            UserTypeConfig userType,
            List<UserGradeConfig> userGradeConfigList, UserTypeConfig userServiceConfig)
        {
            bool serviceFlag = true, levelFlag = true, directFlag = true;
            //long Level = 3;
            if (userTreeConfig != null)
            {
                serviceFlag = userTreeConfig.IsShowServiceCenter;
                levelFlag = userTreeConfig.IsShowUserGrade;
                directFlag = userTreeConfig.IsShowDirectMemberNum;
            }

            var str =
                $"select u.Id,u.Name,u.UserName,u.GradeId ,ISNULL( (SELECT count(1) from User_User uu WHERE uu.ParentId=u.Id) ,0) as ChildCount from User_User as u where u.ParentId = {userId}";

            //str.Append("select  u.Id,u.Name,u.UserName,u.GradeId ,tmp.ChildCount from User_User as u  LEFT JOIN ");
            //str.Append("(select ParentId ,COUNT(1) ChildCount from User_User ");
            //str.Append($" where ParentId in (select id from User_User ut  where ut.ParentId={userId} ");
            //if (userId == 0) {
            //    str.Append("or ut.ParentId=ut.Id");
            //}
            //str.Append(" ) group by ParentId )");
            //str.Append($"as tmp on tmp.ParentId=u.id where u.ParentId={userId}");
            //if (userId == 0) {
            //    str.Append(" or u.ParentId=u.Id");
            //}

            var list = new List<Users.Entities.UserTree>();
            using (var reader = RepositoryContext.ExecuteDataReader(str))
            {
                while (reader.Read())
                {
                    var UserName = reader["UserName"].ToString();
                    var id = reader["Id"].ToString();
                    var realName = reader["Name"].ToString();
                    //string realName = "*" + exName.Substring(exName.Length - 1, 1);
                    //bool isCenter = reader["IsCenter"].ToInt16() == 1;
                    var GradeId = reader["GradeId"].ToGuid();
                    var childCount = reader["ChildCount"].ToInt64();
                    //decimal teamSales = reader.Read<decimal>("teamSales");
                    //long teamNumber = reader.Read<long>("TeamNumber");
                    var userTree = new Users.Entities.UserTree
                    {
                        Id = reader["Id"].ToInt64(),
                        Name = UserName,
                        PId = userId
                    };
                    //显示直推人数
                    if (directFlag) userTree.Name = UserName + "  " + childCount + "人";

                    //显示门店
                    //if (serviceFlag) {
                    //    if (isCenter) {
                    //        userTree.Name += "：门店";
                    //    }
                    //}
                    //显示等级
                    if (levelFlag)
                        if (userType != null)
                        {
                            var userGrade = userGradeConfigList.Find(r => r.Id == GradeId);
                            if (userGrade != null)
                                userTree.Name = $@"{UserName}({realName})" + " " + userGrade.Name +
                                                $"（直推:{childCount}））";
                        }

                    userTree.Open = false;
                    userTree.IsParent = childCount > 0 ? true : false;
                    userTree.Icon = userTree.IsParent
                        ? @"../../../wwwroot/lib/zTree_v3-master/group.png"
                        : @"../../../wwwroot/lib/zTree_v3-master/user.png";
                    list.Add(userTree);
                }

                return list;
            }
        }

        #endregion 组织架构图
    }
}