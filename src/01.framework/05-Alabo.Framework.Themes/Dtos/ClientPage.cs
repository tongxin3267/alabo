using Alabo.Framework.Themes.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Themes.Dtos {

    /// <summary>
    ///     客户端页面设置
    /// </summary>
    public class ClientPage {

        /// <summary>
        ///     页面Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     是否为母版页
        ///     如果是PC的时候，可以是有母版页
        ///     移动端暂不支持母版页布局
        /// </summary>
        public bool IsLayoutPage { get; set; } = false;

        /// <summary>
        ///     页面标题，通过DIY来设置的
        /// </summary>
        [Display(Name = "页面名称")]
        public string Name { get; set; }

        /// <summary>
        ///     访问网址
        /// </summary>
        [Display(Name = "网址")]
        public string Url { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        [Display(Name = "路径")]
        public string Path { get; set; }

        /// <summary>
        ///     Diy的布局方式
        /// </summary>
        [Display(Name = "Diy的布局方式")]
        public IList<PageLayout> Layouts { get; set; } = new List<PageLayout>();

        /// <summary>
        ///     模块
        /// </summary>
        public IList<PageWidget> Widgets { get; set; } = new List<PageWidget>();

        /// <summary>
        ///     页面设置
        /// </summary>
        public PageSetting Setting { get; set; } = new PageSetting();
    }
}