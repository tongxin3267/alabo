using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Themes.Domain.Enums;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Themes.Domain.Entities {

    /// <summary>
    ///     主题模板配置
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Themes_Theme")]
    [ClassProperty(Name = "主题模板配置")]
    public class Theme : AggregateMongodbRoot<Theme> {

        /// <summary>
        ///     所属站点
        ///     站点为空的时候，表示系统模板
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        [Display(Name = "所属站点")]
        public ObjectId SiteId { get; set; } = ObjectId.Empty;

        /// <summary>
        ///     客户端类型
        /// </summary>
        [Display(Name = "客户端类型")]
        public ClientType ClientType { get; set; }

        /// <summary>
        ///     模板类型
        /// </summary>
        [Display(Name = "模板类型")]
        public ThemeType Type { get; set; }

        /// <summary>
        ///     模板名称
        /// </summary>
        [Display(Name = "模板名称")]
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Display(Name = "简介")]
        public string Intro { get; set; } = "心意甄选 新品新意 ,一站式解决跨境电商难题 ";

        /// <summary>
        ///     预览图片
        /// </summary>
        [Display(Name = "预览图片")]
        public string Image { get; set; } = "http://ui.5ug.com/images/theme/mb01.jpg";

        /// <summary>
        /// 模板设置
        /// </summary>
        public string Setting { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Display(Name = "是否默认")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 底部TarBar设置
        /// </summary>
        public string TabBarSetting { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 模板菜单
        /// </summary>
        public ThemeMenu Menu { get; set; }
    }

    /// <summary>
    /// 模板菜单
    /// </summary>
    public class ThemeMenu {

        /// <summary>
        /// 样式风格
        /// </summary>
        public string StyleType { get; set; }

        /// <summary>
        /// 菜单 数据
        /// </summary>
        public List<ThemeOneMenu> Menus { get; set; }
    }

    /// <summary>
    /// 模板操作菜单
    /// </summary>
    public class ThemeOneMenu {

        /// <summary>
        /// 权限唯一ID
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 二级菜单
        /// </summary>
        public List<ThemeTwoMenu> Menus { get; set; }
    }

    public class ThemeTwoMenu {

        /// <summary>
        /// 权限唯一ID
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 三级菜单
        /// </summary>
        public List<ThemeThreeMenu> Menus { get; set; }
    }

    /// <summary>
    /// 三级菜单
    /// </summary>

    public class ThemeThreeMenu {

        /// <summary>
        /// 权限唯一ID
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
    }
}