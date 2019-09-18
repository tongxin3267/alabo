using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Admin.Domain.Dtos {

    /// <summary>
    /// 应用信息
    /// </summary>
    public class ClientApp {

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标志
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 唯一Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public long SortOrder { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 应用链接，权限，页面
        /// </summary>
        public List<ClientAppItem> AppItems { get; set; }
    }

    public class ClientAppItem {

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     链接名称
        /// </summary>
        [Display(Name = "名称")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 2)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string Name { get; set; }

        /// <summary>
        /// 循环类型
        /// 为空的时候不循环
        /// 比如可以循环AutoConfig,
        /// 也可以循环
        /// </summary>
     //   public AppUrlCycle Cycle { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标")]
        [Field(ListShow = true, EditShow = true, IsImagePreview = true, ControlsType = ControlsType.AlbumUploder,
            SortOrder = 1)]
        public string Icon { get; set; }

        /// <summary>
        ///     格式/user/index
        /// 不能包含下划线
        /// 在装换成前端
        /// </summary>
        [Display(Name = "Path")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Path { get; set; }
    }
}