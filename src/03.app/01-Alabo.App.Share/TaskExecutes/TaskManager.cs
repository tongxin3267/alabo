using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Framework.Tasks.Schedules.Domain.Enums;
using Alabo.Reflections;
using Alabo.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ITaskModule = Alabo.App.Share.TaskExecutes.ResultModel.ITaskModule;

namespace Alabo.App.Share.TaskExecutes
{
    /// <summary>
    ///     模块管理器
    /// </summary>
    public class TaskManager
    {
        private readonly HashSet<Guid> _taskIdCache = new HashSet<Guid>();

        private readonly IDictionary<Guid, TaskModuleAttribute> _taskModuleAttributeCache =
            new Dictionary<Guid, TaskModuleAttribute>();

        private readonly IDictionary<Type, Guid> _taskModuleIdCache = new Dictionary<Type, Guid>();

        private readonly IDictionary<Type, TaskModuleAttribute> _taskModuleTypeAttributeCache =
            new Dictionary<Type, TaskModuleAttribute>();

        private readonly IDictionary<Guid, Type> _taskModuleTypeCache = new Dictionary<Guid, Type>();

        private readonly HashSet<Type> _taskTypeCache = new HashSet<Type>();

        private readonly HashSet<Type> _taskTypePriceCache = new HashSet<Type>(); // 价格类型模式

        private readonly HashSet<Type> _taskTypeUpgradeCache = new HashSet<Type>(); // 升级类型模式

        public TaskManager()
        {
            BuildTaskModuleCache();
        }

        /// <summary>
        ///     自动搜索程序集，构建taskmodule缓存
        /// </summary>
        private void BuildTaskModuleCache()
        {
            var assemblies = RuntimeContext.Current.GetPlatformRuntimeAssemblies();
            var moduleTypes = assemblies.SelectMany(e => e.GetTypes())
                .Where(e => e.GetInterfaces().Contains(typeof(ITaskModule))).ToArray();

            foreach (var type in moduleTypes)
            {
                var attributes = type.GetTypeInfo().GetAttributes<TaskModuleAttribute>();
                if (attributes == null || attributes.Count() <= 0) {
                    continue;
                }

                var attribute = attributes.First();
                if (_taskIdCache.Contains(attribute.Id) || _taskTypeCache.Contains(type)) {
                    continue;
                }

                //add to cache with id key
                _taskIdCache.Add(attribute.Id);
                _taskModuleAttributeCache.Add(attribute.Id, attribute);
                _taskModuleTypeCache.Add(attribute.Id, type);
                //add to cache with type key
                _taskTypeCache.Add(type);
                _taskModuleTypeAttributeCache.Add(type, attribute);
                _taskModuleIdCache.Add(type, attribute.Id);

                if (attribute.FenRunResultType == FenRunResultType.Price) {
                    _taskTypePriceCache.Add(type); // 价格类型模块
                }

                if (attribute.FenRunResultType == FenRunResultType.Queue) {
                    _taskTypeUpgradeCache.Add(type); // 升级类型
                }
            }
        }

        /// <summary>
        ///     根据模块Id获取模块类型
        /// </summary>
        /// <param name="id">模块id</param>
        /// <param name="type">模块类型</param>
        /// <returns>获取是否成功</returns>
        public bool TryGetModuleType(Guid id, out Type type)
        {
            return _taskModuleTypeCache.TryGetValue(id, out type);
        }

        /// <summary>
        ///     根据模块Id获取模块描述类型
        /// </summary>
        /// <param name="id">模块id</param>
        /// <param name="attribute">模块描述类型</param>
        /// <returns>获取是否成功</returns>
        public bool TryGetModuleAttribute(Guid id, out TaskModuleAttribute attribute)
        {
            return _taskModuleAttributeCache.TryGetValue(id, out attribute);
        }

        /// <summary>
        ///     模块Id是否存在
        /// </summary>
        /// <param name="id">模块id</param>
        /// <returns>是否存在的结果</returns>
        public bool ContainsModuleId(Guid id)
        {
            return _taskIdCache.Contains(id);
        }

        /// <summary>
        ///     获取系统已加载的所有模块id
        /// </summary>
        /// <returns>模块id集合</returns>
        public Guid[] GetModuleIdArray()
        {
            return _taskIdCache.ToArray();
        }

        /// <summary>
        ///     根据模块类型获取模块Id
        /// </summary>
        /// <param name="type">模块类型</param>
        /// <param name="id">模块id</param>
        /// <returns>获取是否成功</returns>
        public bool TryGetModuleId(Type type, out Guid id)
        {
            return _taskModuleIdCache.TryGetValue(type, out id);
        }

        /// <summary>
        ///     根据模块类型获取模块描述信息
        /// </summary>
        /// <param name="type">模块类型</param>
        /// <param name="attribute">模块描述类</param>
        /// <returns>获取是否成功</returns>
        public bool TryGetModuleAttribute(Type type, out TaskModuleAttribute attribute)
        {
            return _taskModuleTypeAttributeCache.TryGetValue(type, out attribute);
        }

        /// <summary>
        ///     加载的模块类型是否存在
        /// </summary>
        /// <param name="type">模块类型</param>
        /// <returns>是否存在</returns>
        public bool ContainsType(Type type)
        {
            return _taskTypeCache.Contains(type);
        }

        /// <summary>
        ///     获取所有模块类型
        /// </summary>
        /// <returns>所有模块类型数组</returns>
        public Type[] GetModuleTypeArray()
        {
            return _taskTypeCache.ToArray();
        }

        public TaskModuleAttribute[] GetModuleAttributeArray()
        {
            return _taskModuleAttributeCache.Select(e => e.Value).OrderBy(r => r.SortOrder).ToArray();
        }

        /// <summary>
        ///     价格类型模块
        /// </summary>
        public Type[] GetModulePriceArray()
        {
            return _taskTypePriceCache.ToArray();
        }

        /// <summary>
        ///     升级类型模块
        /// </summary>
        public Type[] GetModuleUpgradeArray()
        {
            return _taskTypeUpgradeCache.ToArray();
        }
    }
}