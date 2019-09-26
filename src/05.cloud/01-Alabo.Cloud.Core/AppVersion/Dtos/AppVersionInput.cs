using _01_Alabo.Cloud.Core.AppVersion.Domain.Enums;

namespace _01_Alabo.Cloud.Core.AppVersion.Dtos
{
    /// <summary>
    ///     app版本检测输入字段
    /// </summary>
    public class AppVersionInput
    {
        /// <summary>
        ///     客户端
        /// </summary>
        public AppClient Client { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public string Version { get; set; }
    }
}