﻿using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Core.Enums.Enum;
using Alabo.Mapping.Dynamic;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Themes.Dtos.Service {

    /// <summary>
    ///     模板中心
    /// </summary>
    public class TemplateCenterInput : BaseViewModel {
        public ClientType ClientType { get; set; }

        /// <summary>
        ///     表单HttpContext对象
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        [BsonIgnore]
        [DynamicIgnore]
        public HttpContext HttpContext { get; set; }
    }
}