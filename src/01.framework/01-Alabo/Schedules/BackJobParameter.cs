using Newtonsoft.Json;
using System;

namespace Alabo.Schedules
{
    public class BackJobParameter
    {
        /// <summary>
        ///     当前用户Id
        /// </summary>
        [JsonIgnore]
        public long UserId { get; set; }

        /// <summary>
        ///     模块Id
        /// </summary>
        [JsonIgnore]
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///     方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        ///     参数
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        ///     是否检查上一个任务是否执行
        /// </summary>
        [JsonIgnore]
        public bool CheckLastOne { get; set; } = false;
    }
}