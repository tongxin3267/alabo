using System;

namespace Alabo.UI.AutoTasks
{
    public class AutoTask
    {
        public AutoTask()
        {
        }

        public AutoTask(string name, Guid moduleId, Type serviceType, string method)
        {
            Name = name;
            Method = method;
            ModuleId = moduleId;
            ServcieType = serviceType;
        }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     集成后台服务的Type
        /// </summary>
        public Type ServcieType { get; set; }

        /// <summary>
        ///     方法
        /// </summary>
        public string Method { get; set; }
    }
}