using MongoDB.Bson;
using Newtonsoft.Json;
using Alabo.Domains.Repositories.Mongo.Extension;

namespace Alabo.App.Core.Themes.Dtos.Service {

    public class SystemWidgetDataInput {

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId WidgetId { get; set; }

        public bool IsDefault { get; set; }

        public string FullName { get; set; }

        public string Type { get; set; }

        /// <summary>
        ///     组件路径
        /// </summary>
        public string ComponentPath { get; set; }

        /// <summary>
        ///     默认数据
        /// </summary>
        public string Value { get; set; }
    }
}