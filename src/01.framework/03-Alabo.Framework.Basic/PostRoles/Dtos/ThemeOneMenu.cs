using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Domains.Repositories.Mongo.Extension;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Framework.Basic.PostRoles.Dtos
{
    /// <summary>
    ///     模板操作菜单
    /// </summary>
    public class ThemeOneMenu
    {
        /// <summary>
        ///     权限唯一ID
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     二级菜单
        /// </summary>
        public List<ThemeTwoMenu> Menus { get; set; }
    }

    public class ThemeTwoMenu
    {
        /// <summary>
        ///     权限唯一ID
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     三级菜单
        /// </summary>
        public List<ThemeThreeMenu> Menus { get; set; }
    }

    /// <summary>
    ///     三级菜单
    /// </summary>
    public class ThemeThreeMenu
    {
        /// <summary>
        ///     权限唯一ID
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     链接地址
        /// </summary>
        public string Url { get; set; }
    }
}