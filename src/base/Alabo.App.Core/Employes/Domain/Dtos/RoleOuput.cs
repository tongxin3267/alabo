using System.Collections.Generic;
using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.UI;

namespace Alabo.App.Core.Employes.Domain.Dtos {

    /// <summary>
    /// 权限输入模型
    /// </summary>
    public class RoleOuput {

        /// <summary>
        /// 管理员后台菜单
        /// </summary>
        public List<ThemeOneMenu> Menus { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public FilterType FilterType { get; set; }

        /// <summary>
        /// 网址前缀
        /// 比如后台管理员为： admin/
        /// 会员中心为 user/
        /// 城市管理后台admin-city/
        /// </summary>
        public string Prefix { get; set; }

        public IList<ObjectId> AllRoleIds { get; set; } = new List<ObjectId>();
    }
}