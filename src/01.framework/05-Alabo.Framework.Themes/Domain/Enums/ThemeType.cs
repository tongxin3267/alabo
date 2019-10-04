using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Themes.Domain.Enums {

    /// <summary>
    ///     模板类型
    /// </summary>
    [ClassProperty(Name = "模板类型")]
    public enum ThemeType {

        /// <summary>
        ///     前端
        /// </summary>
        Front = 1,

        /// <summary>
        ///     会员中心
        /// </summary>
        User = 2,

        /// <summary>
        ///     管理员
        /// </summary>
        Admin = 3,

        /// <summary>
        ///     城市代理商
        /// </summary>
        City = 5,

        /// <summary>
        ///     营销中
        /// </summary>
        Market = 10,

        /// <summary>
        ///     店铺
        /// </summary>
        Store = 100,

        /// <summary>
        ///     省代理管理后台
        ///     前缀：/Admin-Province/
        /// </summary>
        [Display(Name = "省代理后台")] Province = 11,

        /// <summary>
        ///     区县中心管理后台
        ///     前缀：/Admin-County/
        /// </summary>
        [Display(Name = "区县中心后台")] County = 13,

        /// <summary>
        ///     商圈管理后台
        ///     前缀：/Admin-Circle/
        /// </summary>
        [Display(Name = "商圈后台")] Circle = 14,

        /// <summary>
        ///     股东管理后台
        ///     前缀：/Admin-ShareHolder/
        /// </summary>
        [Display(Name = "股东后台")] ShareHolder = 20,

        /// <summary>
        ///     内部合伙人管理后台
        ///     前缀：/Admin-Partner/
        /// </summary>
        [Display(Name = "内部合伙人后台")] Partner = 21
    }
}