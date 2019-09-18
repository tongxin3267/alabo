using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Admin.Domain.Enums {

    [ClassProperty(Name = "菜单类型")]
    public enum MenuType {

        /// <summary>
        ///     显示在头部
        /// </summary>
        Top = 1,

        /// <summary>
        ///     显示在左边IsShowLeftMenu
        /// </summary>
        Left = 2,

        /// <summary>
        ///     二级菜单
        /// </summary>
        TwoMenu = 3,

        /// <summary>
        ///     三级菜单
        /// </summary>
        ThreeMenu = 4,

        /// <summary>
        ///     不是菜单 IsShowMenu=false
        /// </summary>
        NoMenu = 5,

        /// <summary>
        /// 应用市场菜单
        /// </summary>
        MarketMenu = 6,

        /// <summary>
        /// 应用市场菜单,同时也是左边菜单
        /// </summary>
        TwoMenuAndMarket = 7,
    }
}