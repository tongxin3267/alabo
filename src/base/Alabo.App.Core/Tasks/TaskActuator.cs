using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Entities.Extensions;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Repositories;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Extensions;
using Alabo.Helpers;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Reflections;

namespace Alabo.App.Core.Tasks {

    public class TaskActuator : ITaskActuator {
        private static readonly string _moduleConfigrationIdKey = "ConfigurationId";

        private readonly ILogger<ITaskActuator> _logger;

        private readonly TaskContext _taskContext;

        private readonly TaskManager _taskManager;

        private readonly TaskModuleFactory _taskModuleFactory;

        private IList<ITaskResult> _resultList = new List<ITaskResult>();

        public TaskActuator(TaskManager taskManager, TaskModuleFactory taskModuleFactory, TaskContext taskContext,
            ILoggerFactory _loggerFactory) {
            _taskManager = taskManager;
            _taskModuleFactory = taskModuleFactory;
            _logger = _loggerFactory.CreateLogger<ITaskActuator>();
            _taskContext = taskContext;
        }

        public TaskActuator() {
        }

        #region 执行TaskQueue中的队列

        /// <summary>
        ///     /// 执行队列
        ///     执行TaskQueue中的队列
        ///     Executes the task.
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="moduleType"></param>
        /// <param name="taskQueue"></param>
        /// <param name="parameter"></param>
        public void ExecuteQueue<TParameter>(Type moduleType, TaskQueue taskQueue, TParameter parameter)
            where TParameter : class {
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (moduleType == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            var taskModuleAttribute = moduleType.GetTypeInfo().GetAttribute<TaskModuleAttribute>();
            if (taskModuleAttribute == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            // 如果模块Id不相同退出
            if (taskModuleAttribute.Id != taskQueue.ModuleId) {
                return;
            }

            //从数据库中获取所有分润模块
            var modules = _taskModuleFactory.CreateModules(moduleType);
            if (modules.Count() <= 0) {
                return;
            }

            // 参数处理
            var taskParameter = new TaskParameter();
            var propertyList = GetOrCreatePropertyCache<TParameter>();
            foreach (var item in propertyList) {
                taskParameter.AddValue(item.Key.Name, item.Value(parameter));
            }

            var taskMessage = new TaskMessage {
                Type = moduleType.FullName,
                ModuleId = taskModuleAttribute.Id,
                ModuleName = taskModuleAttribute.Name
            };
            IList<ITaskResult> resultList = new List<ITaskResult>();
            var success = false;
            foreach (var item in modules) {
                taskParameter.TryGetValue("QueueId", out long queueId);
                try {
                    var result = item.Execute(taskParameter);
                    if (result != null) {
                        if (result.Status == ResultStatus.Success && result.Result.Count() > 0) {
                            resultList.AddRange(result.Result);
                            success = true;
                        } else {
                            var taskQueueUpdate = Ioc.Resolve<ITaskQueueService>().GetSingle(taskQueue.Id);
                            // 将操作记录更新到数据
                            taskMessage.Message = result.Message;
                            taskQueueUpdate.Status = QueueStatus.Error;
                            taskQueueUpdate.Message = $"升级队列失败,.message{taskMessage.Message}";
                            taskQueueUpdate.HandleTime = DateTime.Now;
                            Ioc.Resolve<ITaskQueueService>().Update(taskQueueUpdate);
                            _logger.LogWarning(result.Message);
                        }
                    }
                } catch (Exception ex) {
                    //执行出错，将错误写入到数据库中
                    var taskQueueUpdate = Ioc.Resolve<ITaskQueueService>().GetSingle(taskQueue.Id);
                    taskMessage.Message = $"升级队列失败执行出错{moduleType}:{ex.Message}";
                    taskQueueUpdate.Status = QueueStatus.Error;
                    taskQueueUpdate.Message = $"{taskMessage.Message}";
                    taskQueueUpdate.HandleTime = DateTime.Now;
                    Ioc.Resolve<ITaskQueueService>().Update(taskQueueUpdate);
                }
            }

            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            IList<UserGradeChangeResult> gradeResultList = new List<UserGradeChangeResult>();
            foreach (var graderesult in resultList) {
                if (graderesult is UserGradeChangeResult) {
                    gradeResultList.Add((UserGradeChangeResult)graderesult);
                }
            }

            // 更新分润结果，财务结果到数据库
            Ioc.Resolve<IShareOrderRepository>().UpdateUpgradeTaskResult(gradeResultList);

            if (success) {
                //更新成功
                var taskQueueUpdate = Ioc.Resolve<ITaskQueueService>().GetSingle(taskQueue.Id);
                taskQueueUpdate.Status = QueueStatus.Handled;
                taskQueueUpdate.Message = "success";
                taskQueueUpdate.HandleTime = DateTime.Now;
                taskQueueUpdate.ExecutionTimes = taskQueue.ExecutionTimes + 1;
                Ioc.Resolve<ITaskQueueService>().Update(taskQueueUpdate);
            }
        }

        #endregion 执行TaskQueue中的队列

        #region 执行分润模块

        /// <summary>
        ///     开始执行task任务，包括会员升级，分润等
        ///     Executes the task.
        /// </summary>
        /// <typeparam name="TParameter">The type of the t parameter.</typeparam>
        /// <param name="moduleType">The type.</param>
        /// <param name="parameter">参数</param>
        /// <param name="shareOrder"></param>
        /// <returns>ServiceResult.</returns>
        public void ExecuteTask<TParameter>(Type moduleType, ShareOrder shareOrder, TParameter parameter)
            where TParameter : class {
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (moduleType == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            var taskModuleAttribute = moduleType.GetTypeInfo().GetAttribute<TaskModuleAttribute>();
            if (taskModuleAttribute == null) {
                throw new ArgumentNullException(nameof(moduleType));
            }

            //从数据库中获取所有分润模块
            var modules = _taskModuleFactory.CreateModules(moduleType);
            if (modules.Count() <= 0) {
                return;
            }

            // 参数处理
            var taskParameter = new TaskParameter();
            var propertyList = GetOrCreatePropertyCache<TParameter>();
            foreach (var item in propertyList) {
                taskParameter.AddValue(item.Key.Name, item.Value(parameter));
            }

            var taskMessage = new TaskMessage {
                Type = moduleType.FullName,
                ModuleId = taskModuleAttribute.Id,
                ModuleName = taskModuleAttribute.Name
            };
            IList<ITaskResult> resultList = new List<ITaskResult>();
            foreach (var item in modules) {
                // 通过动态类型获取配置属性
                var configuration = ((dynamic)item).Configuration;
                var triggerType = (TriggerType)configuration.TriggerType;
                if (triggerType != shareOrder.TriggerType) {
                    continue;
                }

                taskMessage.ConfigName = (string)configuration.Name;
                taskParameter.TryGetValue("ShareOrderId", out long ShareOrderId);
                try {
                    var result = item.Execute(taskParameter);
                    if (result != null) {
                        if (result.Status == ResultStatus.Success && result.Result.Count() > 0) {
                            resultList.AddRange(result.Result);
                        } else {
                            // 将操作记录更新到数据
                            taskMessage.Message = result.Message;
                            Ioc.Resolve<IShareOrderService>()
                                .AddTaskMessage(ShareOrderId, taskMessage);
                            _logger.LogWarning(result.Message);
                        }
                    }
                } catch (Exception ex) {
                    //执行出错，将错误写入到数据库中
                    taskMessage.Message = $"DefaultTaskActuator.Execute执行出错{moduleType}:{ex.Message}";
                    Ioc.Resolve<IShareOrderService>().AddTaskMessage(ShareOrderId, taskMessage);
                }
            }

            //更新执行次数
            var repositoryContext = Ioc.Resolve<IShareOrderRepository>();
            repositoryContext.UpdateExcuteCount(shareOrder.Id, modules.Count());

            if (resultList.Count > 0) {
                var shareOrderNative = Ioc.Resolve<IShareOrderService>()
                    .GetSingleNative(shareOrder.Id);
                if (shareOrderNative.Status == ShareOrderStatus.Pending) {
                    UpdateTaskPriceResult(resultList);
                }
            }
        }

        /// <summary>
        ///     执行分润结果，更新数据库
        /// </summary>
        /// <param name="resultList"></param>
        private void UpdateTaskPriceResult(IList<ITaskResult> resultList) {
            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;

            IList<long> shareUsreIds = new List<long>();
            foreach (var item in resultList) {
                var shareResult = ((TaskQueueResult<ITaskResult>)item).ShareResult;
                shareUsreIds.Add(shareResult.ShareUser.Id);
            }

            IList<ShareResult> shareResultList = new List<ShareResult>();
            resultList.Foreach(r => { shareResultList.Add(((TaskQueueResult<ITaskResult>)r).ShareResult); });
            // 更新分润结果，财务结果到数据库
            Ioc.Resolve<IShareOrderRepository>().UpdatePriceTaskResult(shareResultList);
        }

        #endregion 执行分润模块

        #region 底层缓存处理

        /// <summary>
        ///     Gets the or create property cache.
        ///     通过缓存动态获取属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IList&lt;KeyValuePair&lt;PropertyInfo, Func&lt;T, System.Object&gt;&gt;&gt;.</returns>
        private IList<KeyValuePair<PropertyInfo, Func<T, object>>> GetOrCreatePropertyCache<T>() {
            if (Cache<T>.PropertyCache == null) {
                IList<KeyValuePair<PropertyInfo, Func<T, object>>> list =
                    new List<KeyValuePair<PropertyInfo, Func<T, object>>>();
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var item in properties) {
                    var parameterExpression = Expression.Parameter(typeof(T));
                    var propertyExpression = Expression.Property(parameterExpression, item);
                    var convertExpression = Expression.Convert(propertyExpression, typeof(object));
                    var lambdaExpression = Expression.Lambda<Func<T, object>>(convertExpression, parameterExpression);
                    list.Add(new KeyValuePair<PropertyInfo, Func<T, object>>(item, lambdaExpression.Compile()));
                }

                Cache<T>.PropertyCache = list;
            }

            return Cache<T>.PropertyCache;
        }

        public void Dispose() {
            _resultList = new List<ITaskResult>();
        }

        private class Cache<T> {
            public static IList<KeyValuePair<PropertyInfo, Func<T, object>>> PropertyCache { get; set; }
        }

        #endregion 底层缓存处理
    }
}