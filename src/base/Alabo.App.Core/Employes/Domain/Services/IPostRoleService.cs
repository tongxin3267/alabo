using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Employes.Domain.Services {

    public interface IPostRoleService : IService<PostRole, ObjectId> {

        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        List<PlatformRoleTreeOutput> RoleTrees();

        /// <summary>
        /// 获取后台管理模板菜单
        /// </summary>
        /// <returns></returns>
        List<ThemeOneMenu> GetAdminThemeMenus();

        /// <summary>
        /// 保存或修改权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Edit(PostRoleInput model);

        /// <summary>
        /// 初始化岗位权限数据，以及默认超级管理员
        /// </summary>
        void Init();
    }
}