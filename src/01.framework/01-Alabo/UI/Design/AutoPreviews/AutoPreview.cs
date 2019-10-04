using Alabo.Domains.Entities;
using System.Collections.Generic;

namespace Alabo.UI.Design.AutoPreviews {

    /// <summary>
    ///     自动预览对应zk-priview组件
    /// </summary>
    public class AutoPreview {

        /// <summary>
        ///     命名空间
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     键值对
        /// </summary>
        public IList<KeyValue> KeyValues { get; set; }
    }
}