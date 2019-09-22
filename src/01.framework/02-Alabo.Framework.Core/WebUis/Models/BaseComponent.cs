﻿using MongoDB.Bson;
using Newtonsoft.Json;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Themes.DiyModels.Base {

    /// <summary>
    ///     前端组件基类
    /// </summary>
    public abstract class BaseComponent : BaseViewModel {

        /// <summary>
        ///     Id,保存到mondb中
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.Hidden,
            SortOrder = 1)]
        //[JsonIgnore]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}