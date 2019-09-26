using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Alabo.App.Share.TaskExecutes.ResultModel;
using Alabo.Data.Things.Orders.Extensions;
using Microsoft.AspNetCore.Http;

namespace Alabo.App.Share.TaskExecutes.Extensions {

    /// <summary>
    ///     Class TaskModuleFactory.
    /// </summary>
    public class TaskModuleFactory {

        /// <summary>
        ///     The HTTP context
        /// </summary>
        private readonly HttpContext _httpContext;

        /// <summary>
        ///     The task context
        /// </summary>
        private readonly TaskContext _taskContext;

        /// <summary>
        ///     The task manager
        /// </summary>
        private readonly TaskManager _taskManager;

        /// <summary>
        ///     The task 模块 configration accessor
        /// </summary>
        private readonly TaskModuleConfigrationAccessor _taskModuleConfigrationAccessor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskModuleFactory" /> class.
        /// </summary>
        /// <param name="taskContext">The task context.</param>
        /// <param name="taskManager">The task manager.</param>
        /// <param name="taskModuleConfigrationAccessor">The task 模块 configration accessor.</param>
        public TaskModuleFactory(TaskContext taskContext, TaskManager taskManager,
            TaskModuleConfigrationAccessor taskModuleConfigrationAccessor) {
            _taskContext = taskContext;
            _taskManager = taskManager;
            _taskModuleConfigrationAccessor = taskModuleConfigrationAccessor;
        }

        /// <summary>
        ///     Creates the modules.
        /// </summary>
        /// <param name="moduleType">The 模块 类型.</param>
        public IEnumerable<ITaskModule> CreateModules(Type moduleType) {
            IList<ITaskModule> list = new List<ITaskModule>();

            //var attribute = moduleType.GetTypeInfo().GetAttribute<TaskModuleAttribute>();
            //var shareModuleReports =Alabo.Helpers.Ioc.Resolve<IReportService>().GetValue<ShareModuleReports>();
            //var shareModuleList = shareModuleReports.ShareModuleList.DeserializeJson<List<ShareModule>>().ToList();
            //shareModuleList = shareModuleList.Where(e => e.ModuleId == attribute.Id && e.IsEnable == true).ToList();
            ////return list.Select(e => e.ConfigurationValue.ConvertToModuleConfig<TConfiguration>()).ToList();
            ////var resutList = shareModuleList.Select(e => e.ConfigValue.ConvertToModuleConfig<TConfiguration>((int)e.Id, e.Name)).ToList();
            //if (!attribute.IsSupportMultipleConfiguration) {
            //    //var singleList = new List<TConfiguration> {
            //    //    resutList.FirstOrDefault()
            //    //};
            //    //resutList = singleList;
            //}

            var configurations = _taskModuleConfigrationAccessor.GetConfigurations(moduleType);
            foreach (var item in configurations) {
                if (item == null) {
                    break;
                }

                var module = CreateModule(moduleType, item);
                if (module != null) {
                    list.Add(module);
                }
            }

            return list;
        }

        /// <summary>
        ///     Creates the 模块.
        /// </summary>
        /// <param name="id">Id标识</param>
        public ITaskModule CreateModule(long id) {
            var configuration = _taskModuleConfigrationAccessor.GetConfiguration(id);
            if (configuration == null) {
                return null;
            }

            return CreateModule(configuration.Item1, configuration.Item2);
        }

        /// <summary>
        ///     Creates the 模块.
        /// </summary>
        /// <param name="moduleType">The 模块 类型.</param>
        /// <param name="taskConfiguration">The task configuration.</param>
        private ITaskModule CreateModule(Type moduleType, object taskConfiguration) {
            if (!_taskManager.TryGetModuleAttribute(moduleType, out var moduleAttribute)) {
                return null;
            }

            if (moduleAttribute.ConfigurationType != taskConfiguration.GetType()) {
                return null;
            }

            ITaskModule result = null;
            var constructors = moduleType.GetConstructors();
            foreach (var item in constructors) {
                if (TryCreateTaskModule(item, taskConfiguration, out result)) {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        ///     Tries the create task 模块.
        /// </summary>
        /// <param name="moduleConstructor">The 模块 constructor.</param>
        /// <param name="taskConfiguration">The task configuration.</param>
        /// <param name="taskModule">The task 模块.</param>
        private bool TryCreateTaskModule(ConstructorInfo moduleConstructor, object taskConfiguration,
            out ITaskModule taskModule) {
            IList<Expression> taskModuleParameters = new List<Expression>();
            var parameters = moduleConstructor.GetParameters();
            foreach (var item in parameters) {
                if (item.ParameterType == taskConfiguration.GetType()) {
                    var taskConfigurationExpression = Expression.Constant(taskConfiguration);
                    var taskConfigurationConvertExpression =
                        Expression.Convert(taskConfigurationExpression, taskConfiguration.GetType());
                    taskModuleParameters.Add(taskConfigurationConvertExpression);
                } else {
                    var parameter =
                        _taskContext.HttpContextAccessor.HttpContext.RequestServices.GetService(item.ParameterType);
                    if (parameter == null) {
                        taskModule = null;
                        return false;
                    }

                    var parameterExpression = Expression.Constant(parameter);
                    var parameterConvertExpression = Expression.Convert(parameterExpression, item.ParameterType);
                    taskModuleParameters.Add(parameterConvertExpression);
                }
            }

            var newExpression = Expression.New(moduleConstructor, taskModuleParameters.ToArray());
            var lambdaExpression = Expression.Lambda<Func<ITaskModule>>(newExpression);
            taskModule = lambdaExpression.Compile()();
            return true;
        }
    }
}