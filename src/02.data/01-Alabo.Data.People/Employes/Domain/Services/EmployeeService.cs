using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.PostRoles.Dtos;
using Alabo.Framework.Core.Admins.Services;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Helpers;
using Alabo.UI;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Data.People.Employes.Domain.Services
{
    public class EmployeeService : ServiceBase<Employee, ObjectId>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork, IRepository<Employee, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        public Tuple<ServiceResult, RoleOuput> Login(UserOutput userOutput) {
            var user = Resolve<IUserService>().GetSingle(userOutput.Id);
            if (user == null) {
                return new Tuple<ServiceResult, RoleOuput>(ServiceResult.FailedWithMessage("用户不存在"), null);
            }

            var token = Resolve<IUserService>().GetUserToken(user);
            if (userOutput.Token != token) {
                return new Tuple<ServiceResult, RoleOuput>(ServiceResult.FailedWithMessage("Token不正确,权限验证失败"), null);
            }

            var employee = Resolve<IEmployeeService>().GetSingle(r => r.UserId == user.Id);
            if (employee == null) {
                if (user.UserName == "admin") {
                    // 当admin不是管理员时，重新初始数据
                    Resolve<IAdminService>().DefaultInit();
                    employee = Resolve<IEmployeeService>().GetSingle(r => r.UserId == user.Id);
                }
            }

            if (employee == null) {
                return new Tuple<ServiceResult, RoleOuput>(ServiceResult.FailedWithMessage("当前用户非员工不能登录"), null);
            }

            var menus = Resolve<IPostRoleService>().GetAdminThemeMenus();
            if (menus == null) {
                return new Tuple<ServiceResult, RoleOuput>(ServiceResult.FailedWithMessage("管理员模板不存在菜单"), null);
            }

            var roleOuput = new RoleOuput {
                FilterType = FilterType.Admin,
                Prefix = "Admin/"
            };

            if (employee.IsSuperAdmin) {
                //超级管理员
                roleOuput.Menus = menus;
            } else {
                // 非超级管理员，根据岗位返回菜单
                var postRole = Resolve<IPostRoleService>().GetSingle(r => r.Id == employee.PostRoleId);
                if (postRole == null) {
                    return new Tuple<ServiceResult, RoleOuput>(ServiceResult.FailedWithMessage("岗位权限不存在"), null);
                }

                var list = new List<ThemeOneMenu>();
                var listCount = 0;

                for (var i = 0; i < menus.Count; i++) {
                    var item = menus[i];
                    var itemIds = postRole.RoleIds.Where(s => menus.Select(ss => ss.Id).Contains(s));
                    if (itemIds.Contains(item.Id)) {
                        list.Add(new ThemeOneMenu {
                            Id = item.Id,
                            Icon = item.Icon,
                            IsEnable = item.IsEnable,
                            Name = item.Name,
                            Url = item.Url,
                            Menus = new List<ThemeTwoMenu>()
                        });
                        listCount = list.Count - 1;
                        //list[listCount].Menus = new List<ThemeTwoMenu>();

                        var nodeListCount = 0;
                        //foreach (var itemNode in item.Menus)
                        for (var j = 0; j < item.Menus.Count; j++) {
                            var itemNode = item.Menus[j];
                            var itemNodeIds = postRole.RoleIds.Where(s => item.Menus.Select(ss => ss.Id).Contains(s));
                            if (itemNodeIds.Contains(itemNode.Id)) {
                                list[listCount].Menus.Add(new ThemeTwoMenu {
                                    Id = itemNode.Id,
                                    Icon = itemNode.Icon,
                                    IsEnable = itemNode.IsEnable,
                                    Name = itemNode.Name,
                                    Url = itemNode.Url,
                                    Menus = new List<ThemeThreeMenu>()
                                });
                                nodeListCount = list[listCount].Menus.Count - 1;
                                //list[listCount].Menus[nodeListCount].Menus = new List<ThemeThreeMenu>();

                                foreach (var itemNodes in itemNode.Menus) {
                                    var itemNodesIds = postRole.RoleIds.Where(s =>
                                        itemNode.Menus.Select(ss => ss.Id).Contains(s));
                                    if (itemNodesIds.Contains(itemNodes.Id)) {
                                        list[listCount].Menus[nodeListCount].Menus.Add(itemNodes);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // 权限Id处理
            roleOuput.Menus?.ForEach(one => {
                if (!roleOuput.AllRoleIds.Contains(one.Id)) {
                    roleOuput.AllRoleIds.Add(one.Id);
                }

                one.Menus?.ForEach(two => {
                    if (!roleOuput.AllRoleIds.Contains(two.Id)) {
                        roleOuput.AllRoleIds.Add(two.Id);
                    }

                    two.Menus?.ForEach(three => {
                        if (!roleOuput.AllRoleIds.Contains(three.Id)) {
                            roleOuput.AllRoleIds.Add(three.Id);
                        }
                    });
                });
            });
            return new Tuple<ServiceResult, RoleOuput>(ServiceResult.Success, roleOuput);
        }

        #region 根据Token自动登录

        public string GetLoginToken(GetLoginToken loginToken) {
            if (loginToken == null) {
                throw new ValidException("对象不能为空");
            }

            if (loginToken.SiteId == null) {
                throw new ValidException("SiteId不能为空");
            }

            if (loginToken.AdminUrl == null) {
                throw new ValidException("AdminUrl不能为空");
            }

            if (loginToken.UserId <= 0) {
                throw new ValidException("userId不正确");
            }

            var key = loginToken.UserId + 500;
            var key2 = loginToken.Timestamp + 9912;
            var key3 = loginToken.AdminUrl.Trim().ToLower() + "admin";
            var token = key.ToString().ToMd5HashString() + key3.ToMd5HashString().Substring(5, 15) +
                        key2.ToStr().ToMd5HashString().Substring(2, 18) + key3.ToMd5HashString();
            return token.ToLower().Trim();
        }

        /// <summary>
        ///     根据Token自动登录
        /// </summary>
        /// <param name="loginByToken"></param>
        /// <returns></returns>
        public LoginInput LoginByToken(GetLoginToken loginByToken) {
            var maxTimestamp = DateTime.Now.AddMinutes(5).ConvertDateTimeInt();
            var minTimestamp = DateTime.Now.AddMinutes(-5).ConvertDateTimeInt();
            if (loginByToken.Timestamp.ConvertToLong() < minTimestamp ||
                loginByToken.Timestamp.ConvertToLong() > maxTimestamp) {
                throw new ValidException($"时间错计算错误，服务器当前时间{DateTime.Now}");
            }

            if (HttpWeb.Ip != loginByToken.Ip) {
                throw new ValidException("IP不正确");
            }

            if (loginByToken.UserId <= 0) {
                throw new ValidException("用户Id不正确");
            }

            if (loginByToken.Token.IsNullOrEmpty() || loginByToken.Token.Length < 30) {
                throw new ValidException("token不正确");
            }

            if (loginByToken.SiteId.IsNullOrEmpty() || loginByToken.SiteId.Length < 20) {
                throw new ValidException("token不正确");
            }

            var token = GetLoginToken(loginByToken);
            if (token != loginByToken.Token) {
                throw new ValidException("Token验证失败");
            }

            //var theme = Resolve<IThemeService>().FirstOrDefault();
            //if (theme != null) {
            //    if (theme.SiteId.ToString() != loginByToken.SiteId) {
            //        throw new ValidException($"站点验证失败");
            //    }
            //}

            var user = Resolve<IUserService>().GetSingle("admin");
            if (user == null) {
                Resolve<IAdminService>().DefaultInit();
            }

            if (user != null) {
                var userDetail = Resolve<IUserDetailService>().GetByIdNoTracking(r => r.UserId == user.Id);
                var loginInput = new LoginInput {
                    UserName = user.UserName,
                    Password = userDetail.Password
                };
                return loginInput;
            }

            throw new ValidException("超级管理员不存在");
        }

        public void Init() {
            if (!Resolve<IEmployeeService>().Exists()) {
                var user = Resolve<IUserService>().GetSingle("admin");
                if (user != null) {
                    var employee = new Employee {
                        PostRoleId = ObjectId.Parse("a008029a8600000752803001"),
                        Name = "超级管理员",
                        Intro = "系统默认超级管理员",
                        IsEnable = true,
                        UserId = user.Id,
                        IsSuperAdmin = true
                    };
                    Resolve<IEmployeeService>().Add(employee);
                }
            }
        }

        #endregion 根据Token自动登录
    }
}