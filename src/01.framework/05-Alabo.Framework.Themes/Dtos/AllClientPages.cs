using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Themes.Dtos {

    /// <summary>
    ///     所有客户端页面
    /// </summary>
    public class AllClientPages {

        /// <summary>
        ///     客户端类型
        /// </summary>
        [Display(Name = "客户端类型")]
        public ClientType ClientType { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        public double LastUpdate { get; set; }

        /// <summary>
        ///     页面所有信息
        /// </summary>
        public IList<ClientPage> PageList { get; set; }

        /// <summary>
        ///     站点信息
        /// </summary>
        public ClientSite Site { get; set; } = new ClientSite();

        /// <summary>
        ///     模板
        /// </summary>
        public ClientTheme Theme { get; set; } = new ClientTheme();
    }

    public class ClientTheme {

        /// <summary>
        ///     模板设置
        /// </summary>
        public object Setting { get; set; }

        /// <summary>
        ///     底部TarBar设置
        /// </summary>
        public object TabBarSetting { get; set; }

        /// <summary>
        ///     模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     模板菜单
        /// </summary>
        public ThemeMenu Menu { get; set; }

        /// <summary>
        ///     模板类型
        /// </summary>
        public ThemeType Type { get; set; } = ThemeType.Front;

        /// <summary>
        ///     模板Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}