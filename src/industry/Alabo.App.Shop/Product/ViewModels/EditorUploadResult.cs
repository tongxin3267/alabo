using Newtonsoft.Json;

namespace Alabo.App.Shop.Product.ViewModels {

    public class EditorUploadResult {
        public const string STATE_SUCESS = "SUCESS";

        /// <summary>
        ///     文件名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     完整文件名
        /// </summary>
        [JsonProperty("originalName")]
        public string OriginalName { get; set; }

        /// <summary>
        ///     访问url
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        ///     文件类型，扩展名
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}