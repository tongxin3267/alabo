using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Dtos;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.Framework.Themes.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [Table("Themes_ThemePage")]
    [ClassProperty(Name = "主题页面")]
    public class ThemePage : AggregateMongodbRoot<ThemePage>
    {
        /// <summary>
        ///     主题Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        [Display(Name = "主题Id")]
        public ObjectId ThemeId { get; set; }

        /// <summary>
        ///     页面标题，通过DIY来设置的
        /// </summary>
        [Display(Name = "页面名称")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, IsMain = true,
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "150", ListShow = true, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        ///     访问网址
        /// </summary>
        [Display(Name = "网址")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", TableDispalyStyle = TableDispalyStyle.Code,
            ListShow = true, SortOrder = 3)]
        public string Url { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        [Display(Name = "路径")]
        public string Path { get; set; }

        /// <summary>
        ///     客户端类型
        /// </summary>
        [Display(Name = "客户端类型")]
        public ClientType ClientType { get; set; }

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

        /// <summary>
        ///     是否为母版页
        ///     如果是PC的时候，可以是有母版页
        ///     移动端暂不支持母版页布局
        /// </summary>
        public bool IsLayoutPage { get; set; } = false;

        /// <summary>
        ///     转换成客户端页面
        /// </summary>
        /// <returns></returns>
        public ClientPage ToClientPage()
        {
            var clientPage = new ClientPage
            {
                Name = Name,
                Url = Url,
                Path = Path,
                Layouts = Layouts,
                Setting = Setting,
                Id = Id.ToString(),
                IsLayoutPage = IsLayoutPage,
                Widgets = Widgets
            };

            return clientPage;
        }
    }

    /// <summary>
    ///     一个页面多个布局
    ///     x: 0, y: 0, w: 12, h: 1, i: 0
    /// </summary>
    public class PageLayout
    {
        /// <summary>
        ///     X坐标
        /// </summary>
        public long X { get; set; }

        /// <summary>
        ///     Y坐标
        /// </summary>
        public long Y { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public long W { get; set; }

        /// <summary>
        ///     高度
        /// </summary>
        public long H { get; set; }

        /// <summary>
        ///     索引
        /// </summary>
        public long I { get; set; }
    }

    /// <summary>
    ///     一个布局多个模块
    /// </summary>
    [BsonIgnoreExtraElements]
    public class PageWidget
    {
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        public List<PageWidget> Columns { get; set; }

        /// <summary>
        ///     是否从Api接口请求
        /// </summary>
        public bool IsApiRequest { get; set; }

        /// <summary>
        ///     母版页索引标识，如果大于1的时候，为母版页,也可能是容器的索引
        /// </summary>
        public long MasterPageIndex { get; set; }

        /// <summary>
        ///     应用Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId WidgetId { get; set; }

        /// <summary>
        ///     数据Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId DataId { get; set; }

        /// <summary>
        ///     对应的组件的路径
        ///     Commponts的路径：示列/core/zk-image/
        /// </summary>
        public string ComponentPath { get; set; }

        /// <summary>
        ///     Api地址
        ///     示列/Api/Diy/GetLink
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        ///     应用数据
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        ///     布局方式
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        ///     组件标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     样式相关配置
        /// </summary>
        public WidgetStyle Style { get; set; }
    }

    public class WidgetStyle
    {
        /// <summary>
        ///     边框 使用对象
        /// </summary>
        public string Border { get; set; }

        /// <summary>
        ///     风格
        /// </summary>
        public string StyleId { get; set; }

        /// <summary>
        ///     风格CSS配置
        /// </summary>
        public string Css { get; set; }
    }

    public class WidgetLayout
    {
        /// <summary>
        ///     布局类型
        ///     tab-layer,grid-layer,float-layer
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        ///     模块
        /// </summary>
        public List<WidgetLayoutColumns> Columns { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     选项配置
        /// </summary>
        public string Options { get; set; }
    }

    public class WidgetLayoutColumns
    {
        /// <summary>
        ///     模块
        /// </summary>
        public List<PageWidget> Widgets { get; set; }

        /// <summary>
        ///     额外配置
        /// </summary>
        public string Option { get; set; }
    }

    /// <summary>
    ///     页面设置
    /// </summary>
    public class PageSetting
    {
        /// <summary>
        ///     底部TarBar设置
        /// </summary>
        public string TabBarSetting { get; set; }

        /// <summary>
        ///     是否登录
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        ///     是否是真实页面
        /// </summary>
        public bool IsRead { get; set; } = false;

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     关键字
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     显示头部
        /// </summary>
        public bool ShowHead { get; set; } = true;

        /// <summary>
        ///     显示底部
        /// </summary>
        public bool ShowFoot { get; set; } = true;

        /// <summary>
        ///     背景颜色
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        ///     弹出广告图
        /// </summary>
        public string PopImage { get; set; }
    }
}