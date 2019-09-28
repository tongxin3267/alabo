using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Domain.Enums;
using Alabo.Framework.Themes.Domain.Services;
using Alabo.Framework.Themes.Dtos;
using Alabo.Maps;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Data.People.Employes.Domain.Services
{
    public class PostRoleService : ServiceBase<PostRole, ObjectId>, IPostRoleService
    {
        public PostRoleService(IUnitOfWork unitOfWork, IRepository<PostRole, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     获取后台管理模板菜单
        /// </summary>
        /// <returns></returns>
        public List<ThemeOneMenu> GetAdminThemeMenus()
        {
            var clientPageInput = new ClientPageInput
            {
                ClientType = ClientType.PcWeb,
                Type = ThemeType.Admin
            };
            var defaultAdminTemplage = Resolve<IThemeService>().GetDefaultTheme(clientPageInput);
            var menus = defaultAdminTemplage?.Menu?.Menus;
            return menus;
        }

        /// <summary>
        ///     获取权限菜单树
        /// </summary>
        /// <returns></returns>
        public List<PlatformRoleTreeOutput> RoleTrees()
        {
            var adminThemeMenus = GetAdminThemeMenus();
            // 所有的岗位都包含首页权限，否则不能访问
            adminThemeMenus = adminThemeMenus
                .Where(r => !r.Url.Contains("admin/index", StringComparison.OrdinalIgnoreCase)).ToList();

            var result = adminThemeMenus?.Select(s =>
            {
                var res = new PlatformRoleTreeOutput
                {
                    Id = s.Id,
                    IsEnable = s.IsEnable,
                    Name = s.Name,
                    Url = s.Url,
                    AppItems = s.Menus?.Select(ss =>
                    {
                        var res1 = new PlatformRoleTreeOutput
                        {
                            Id = ss.Id,
                            IsEnable = ss.IsEnable,
                            Name = ss.Name,
                            Url = ss.Url,
                            AppItems = ss.Menus?.Select(sss =>
                            {
                                var res2 = new PlatformRoleTreeOutput
                                {
                                    Id = sss.Id,
                                    IsEnable = sss.IsEnable,
                                    Name = sss.Name,
                                    Url = sss.Url,
                                    AppItems = null
                                };
                                return res2;
                            }).ToList()
                        };
                        return res1;
                    }).ToList()
                };

                return res;
            }).ToList();

            return result;
        }

        /// <summary>
        ///     权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult Edit(PostRoleInput model)
        {
            var postRole = model.MapTo<PostRole>();
            if (postRole.RoleIds == null) postRole.RoleIds = new List<ObjectId>();

            // 添加首页权限Id
            var adminThemeMenus = GetAdminThemeMenus();
            var indexMenu =
                adminThemeMenus.FirstOrDefault(r => r.Url.Contains("admin/index", StringComparison.OrdinalIgnoreCase));
            if (indexMenu != null) postRole.RoleIds.Add(indexMenu.Id);
            // 消息页面
            indexMenu = adminThemeMenus.FirstOrDefault(r =>
                r.Url.Contains("admin/message", StringComparison.OrdinalIgnoreCase));
            if (indexMenu != null) postRole.RoleIds.Add(indexMenu.Id);

            if (model.RoleIds == null) model.RoleIds = new List<string>();
            foreach (var item in model.RoleIds)
            {
                var roleId = item.ToObjectId();
                if (roleId != ObjectId.Empty) postRole.RoleIds.Add(roleId);
            }

            postRole.RoleIds = postRole.RoleIds;

            if (!AddOrUpdate(postRole, s => s.Id == postRole.Id)) return ServiceResult.FailedMessage("更新失败");
            return ServiceResult.Success;
        }

        #region 初始化岗位权限

        /// <summary>
        ///     初始化岗位权限
        /// </summary>
        public void Init()
        {
            #region 添加岗位权限

            if (!Exists())
            {
                var list = new List<PostRole>();
                var postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803001"),
                    Name = "超级管理员",
                    Summary = "具备店铺所有管理的权限"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803002"),
                    Name = "客服",
                    Summary = "具备“进入在线客服”、“查看商品列表和分组”、“查看订单，修改订单备注，延长订单发货，取消订单”的店铺管理权限。"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803003"),
                    Name = "财务",
                    Summary = "具备“资产管理”等相关功能的管理权限"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803004"),
                    Name = "运营",
                    Summary = "具备“客户管理”,“营销管理”, “订单查询”,“门店”, “商品”等相关功能的管理权限 "
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803005"),
                    Name = "普通管理员",
                    Summary = "具备除了添加删除员工、修改角色、删除数据、资产概览等之外，其他所有店铺管理权限"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803006"),
                    Name = "仓管",
                    Summary = "具备商品管理,订单管理等相关功能的管理权限"
                };
                list.Add(postRole);

                AddMany(list);
            }

            #endregion 添加岗位权限

            if (!Resolve<IEmployeeService>().Exists())
            {
                var user = Resolve<IUserService>().GetSingle("admin");
                if (user != null)
                {
                    var employee = new Employee
                    {
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

        #endregion 初始化岗位权限
    }
}