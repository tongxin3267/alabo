using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alabo.App.Share.TaskExecutes.Domain.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using ZKCloud.Open.Share.Models;

namespace Alabo.App.Share.TaskExecutes.Extensions {

    public class TaskModuleConfigrationAccessor {
        private readonly TaskManager _taskManager;

        private readonly ITaskModuleConfigService _taskModuleConfigService;

        public TaskModuleConfigrationAccessor(TaskManager taskManager, ITaskModuleConfigService taskModuleConfigService) {
            _taskManager = taskManager;
            _taskModuleConfigService = Ioc.Resolve<ITaskModuleConfigService>();
        }

        public IEnumerable<object> GetConfigurations(Type moduleType) {
            IList<object> list = new List<object>();
            if (!_taskManager.TryGetModuleAttribute(moduleType, out var moduleAttribute)) {
                return list;
            }

            list.AddRange(DynamicGetModuleConfigList(moduleType, moduleAttribute.ConfigurationType));
            return list;
        }

        public Tuple<Type, object> GetConfiguration(long id) {
            var find = new ShareModule();
            // var find = _taskModuleConfigService.GetSingle(id);
            if (find == null) {
                return null;
            }

            if (!_taskManager.TryGetModuleAttribute(find.ModuleId, out var moduleAttribute)) {
                return null;
            }

            if (!_taskManager.TryGetModuleType(find.ModuleId, out var moduleType)) {
                return null;
            }

            var configuration = DynamicGetModuleConfig(moduleType, moduleAttribute.ConfigurationType, id);
            return Tuple.Create(moduleType, configuration);
        }

        private object DynamicGetModuleConfig(Type moduleType, Type configurationType) {
            if (moduleType == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            if (configurationType == null) {
                throw new ArgumentNullException(nameof(configurationType));
            }

            var method = _taskModuleConfigService.GetType().GetMethods()
                .FirstOrDefault(e => e.Name == "Get" && e.GetParameters().Length == 0);
            if (method == null) {
                throw new MissingMethodException("not found method Get<TModule, TConfiguration>()");
            }

            method = method.MakeGenericMethod(moduleType, configurationType);
            var instanseExpression = Expression.Constant(_taskModuleConfigService);
            var callExpression = Expression.Call(instanseExpression, method);
            var lambdaExpression = Expression.Lambda<Func<object>>(callExpression);
            return lambdaExpression.Compile()();
        }

        private object DynamicGetModuleConfig(Type moduleType, Type configurationType, long id) {
            if (moduleType == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            if (configurationType == null) {
                throw new ArgumentNullException(nameof(configurationType));
            }

            var method = _taskModuleConfigService.GetType().GetMethods()
                .FirstOrDefault(e => e.Name == "Get" && e.GetParameters().Length == 1);
            if (method == null) {
                throw new MissingMethodException("not found method Get<TModule, TConfiguration>(int id)");
            }

            method = method.MakeGenericMethod(moduleType, configurationType);
            var instanseExpression = Expression.Constant(_taskModuleConfigService);
            var idExpression = Expression.Constant(id);
            var callExpression = Expression.Call(instanseExpression, method, idExpression);
            var lambdaExpression = Expression.Lambda<Func<object>>(callExpression);
            return lambdaExpression.Compile()();
        }

        /// <summary>
        ///     获取正常状态下的分润配置
        ///     ITaskModuleConfigService.GetList 方法
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="configurationType"></param>
        private IEnumerable<object> DynamicGetModuleConfigList(Type moduleType, Type configurationType) {
            if (moduleType == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            if (configurationType == null) {
                throw new ArgumentNullException(nameof(configurationType));
            }

            // var resultList = _taskModuleConfigService.GetList();
            var method = _taskModuleConfigService.GetType().GetMethods()
                .FirstOrDefault(e => e.Name == "GetList" && e.GetGenericArguments().Length == 2);

            if (method == null) {
                throw new MissingMethodException("not found method GetList<TModule, TConfiguration>()");
            }

            method = method.MakeGenericMethod(moduleType, configurationType);
            var instanseExpression = Expression.Constant(_taskModuleConfigService);
            var callExpression = Expression.Call(instanseExpression, method);
            var lambdaExpression = Expression.Lambda<Func<IEnumerable<object>>>(callExpression);
            var resultList = lambdaExpression.Compile()();
            return resultList;
        }
    }
}