using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Framework.Tasks.Queues.Domain.Repositories;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Schedules;
using Newtonsoft.Json;

namespace Alabo.Framework.Tasks.Queues.Domain.Servcies
{
    /// <summary>
    ///     Class TaskQueueService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="ITaskQueueService" />
    public class TaskQueueService : ServiceBase<TaskQueue, long>, ITaskQueueService
    {
        public TaskQueueService(IUnitOfWork unitOfWork, IRepository<TaskQueue, long> repository) : base(unitOfWork,
            repository)
        {
        }

        public ServiceResult AddBackJob(BackJobParameter backJobParameter)
        {
            if (backJobParameter == null || backJobParameter.ModuleId.IsGuidNullOrEmpty())
                throw new ArgumentNullException(nameof(backJobParameter));

            // 是否检查上一个队列的执行情况
            if (backJobParameter.CheckLastOne)
            {
                var find = GetSingle(r =>
                    r.ModuleId == backJobParameter.ModuleId && r.Status == QueueStatus.Pending &&
                    r.Type == TaskQueueType.Once);
                if (find != null) return ServiceResult.FailedWithMessage("上一次任务未完成，请稍后重试");
            }

            var model = new TaskQueue
            {
                ModuleId = backJobParameter.ModuleId,
                UserId = backJobParameter.UserId,
                Parameter = backJobParameter.ToJson(),
                Status = QueueStatus.Pending,
                Type = TaskQueueType.Once,
                CreateTime = DateTime.Now
            };
            if (model.UserId == 0) model.UserId = HttpWeb.UserId;
            var result = Add(model);
            if (result)
            {
                return ServiceResult.Success;
                ;
            }

            return ServiceResult.FailedWithMessage("后台队列任务添加失败");
        }

        /// <summary>
        ///     Adds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="parameter">参数</param>
        /// <exception cref="ArgumentNullException">parameter</exception>
        public void Add(long userId, Guid moduleId, object parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            var parameterString = JsonConvert.SerializeObject(parameter);
            var model = new TaskQueue
            {
                ModuleId = moduleId,
                UserId = userId,
                Parameter = parameterString,
                Status = QueueStatus.Pending,
                CreateTime = DateTime.Now,
                HandleTime = DateTime.Now
            };
            Repository<ITaskQueueRepository>().AddSingle(model);
        }

        /// <summary>
        ///     Adds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="executionTime">The execution time.</param>
        /// <param name="parameter">参数</param>
        /// <exception cref="ArgumentNullException">parameter</exception>
        public void Add(long userId, Guid moduleId, DateTime executionTime, object parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            var parameterString = JsonConvert.SerializeObject(parameter);
            var model = new TaskQueue
            {
                ModuleId = moduleId,
                UserId = userId,
                Parameter = parameterString,
                Status = QueueStatus.Pending,
                CreateTime = DateTime.Now,
                HandleTime = DateTime.Now,
                ExecutionTime = executionTime,
                Type = TaskQueueType.Once
            };
            Repository<ITaskQueueRepository>().AddSingle(model);
        }

        /// <summary>
        ///     Adds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="executionTime">The execution time.</param>
        /// <param name="executionTimes">The execution times.</param>
        /// <param name="parameter">参数</param>
        /// <exception cref="ArgumentNullException">parameter</exception>
        public void Add(long userId, Guid moduleId, TaskQueueType type, DateTime executionTime, int executionTimes,
            object parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            var parameterString = JsonConvert.SerializeObject(parameter);
            var model = new TaskQueue
            {
                ModuleId = moduleId,
                UserId = userId,
                Parameter = parameterString,
                Status = QueueStatus.Pending,
                CreateTime = DateTime.Now,
                HandleTime = DateTime.Now,
                ExecutionTime = executionTime,
                Type = type,
                MaxExecutionTimes = executionTimes
            };
            Repository<ITaskQueueRepository>().AddSingle(model);
        }

        /// <summary>
        ///     Handles the specified identifier.
        /// </summary>
        /// <param name="id">Id标识</param>
        public void Handle(long id)
        {
            var find = Repository<ITaskQueueRepository>().GetSingle(e => e.Id == id);
            if (find != null)
            {
                find.HandleTime = DateTime.Now;
                find.ExecutionTimes++;
                //单次执行任务直接终结
                if (find.Type == TaskQueueType.Once)
                    find.Status = QueueStatus.Handled;
                //非单次执行任务根据执行次数终结
                else if (find.MaxExecutionTimes > 0 && find.ExecutionTimes >= find.MaxExecutionTimes)
                    find.Status = QueueStatus.Handled;

                Repository<ITaskQueueRepository>().UpdateSingle(find);
            }
        }

        /// <summary>
        ///     Counts the specified module identifier.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>System.Int32.</returns>
        public int Count(Guid moduleId)
        {
            return (int) Repository<ITaskQueueRepository>().Count(e => e.ModuleId == moduleId);
        }

        /// <summary>
        ///     Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        public void Delete(long userId, Guid moduleId)
        {
            Delete(e => e.UserId == userId && e.ModuleId == moduleId);
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <returns>TaskQueue.</returns>
        public TaskQueue GetSingle(long id)
        {
            var find = Repository<ITaskQueueRepository>().GetSingle(e => e.Id == id);
            return find;
        }

        /// <summary>
        ///     Gets all unhandled list.
        /// </summary>
        /// <returns>IEnumerable&lt;TaskQueue&gt;.</returns>
        public IEnumerable<TaskQueue> GetAllUnhandledList()
        {
            //var list = Repository<ITaskQueueRepository>().GetList(e => e.IsHandled == false);
            //return list;
            return Repository<ITaskQueueRepository>().GetUnhandledList();
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="IsHandled">if set to <c>true</c> [is handled].</param>
        /// <returns>IEnumerable&lt;TaskQueue&gt;.</returns>
        public IEnumerable<TaskQueue> GetList(Guid ModuleId, bool IsHandled = false)
        {
            return Repository<ITaskQueueRepository>()
                .GetList(e => e.ModuleId == ModuleId && e.Status == QueueStatus.Handled);
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="query">查询</param>
        /// <returns>IEnumerable&lt;TaskQueue&gt;.</returns>
        /// <exception cref="ArgumentNullException">query</exception>
        public IEnumerable<TaskQueue> GetList(IPredicateQuery<TaskQueue> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return null;
            //   return Repository<ITaskQueueRepository>().GetList(query);
        }

        /// <summary>
        ///     Gets the paged list.
        /// </summary>
        /// <param name="query">查询</param>
        /// <returns>PagedList&lt;TaskQueue&gt;.</returns>
        /// <exception cref="ArgumentNullException">query</exception>
        public PagedList<TaskQueue> GetPagedList(IPageQuery<TaskQueue> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return null;
            // return Repository<ITaskQueueRepository>().GetPage(query);
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>TaskQueue.</returns>
        /// <exception cref="ArgumentNullException">moduleId</exception>
        public TaskQueue GetSingle(Guid moduleId)
        {
            if (moduleId == Guid.Empty) throw new ArgumentNullException(nameof(moduleId));

            return Repository<ITaskQueueRepository>()
                .GetSingle(q => q.ModuleId == moduleId && q.Status != QueueStatus.Pending);
        }

        /// <summary>
        ///     Gets the task module attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>TaskModuleAttribute.</returns>
        public TaskModuleAttribute GetTaskModuleAttribute<T>() where T : ITaskModule
        {
            var attribute = typeof(T).GetTypeInfo().GetAttribute<TaskModuleAttribute>();
            return attribute;
        }

        /// <summary>
        ///     Gets the task module attribute.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>TaskModuleAttribute.</returns>
        public TaskModuleAttribute GetTaskModuleAttribute(Guid moduleId)
        {
            var allTaskModuleAttribute = GetAllTaskModuleAttribute();
            var allAttributes = allTaskModuleAttribute.Select(r => r.Value);
            var attribute = allAttributes.FirstOrDefault(r => r.Id == moduleId);
            return attribute;
        }

        /// <summary>
        ///     Gets all share module.
        ///     获取所有的分润维度
        /// </summary>
        /// <returns>Type.</returns>
        public IDictionary<Type, TaskModuleAttribute> GetAllTaskModuleAttribute()
        {
            var cacheKey = "_TaskModuleAttribute";
            if (!ObjectCache.TryGetPublic(cacheKey, out IDictionary<Type, TaskModuleAttribute> taskModuleAttributes))
            {
                taskModuleAttributes = new Dictionary<Type, TaskModuleAttribute>();
                var assemblies = RuntimeContext.Current.GetPlatformRuntimeAssemblies();
                var moduleTypes = assemblies.SelectMany(e => e.GetTypes())
                    .Where(e => e.GetInterfaces().Contains(typeof(ITaskModule))).ToArray();
                foreach (var type in moduleTypes)
                {
                    var attribute = type.GetTypeInfo().GetAttribute<TaskModuleAttribute>();
                    if (attribute != null) taskModuleAttributes.Add(type, attribute);
                }

                ObjectCache.Set(cacheKey, taskModuleAttributes);
            }

            return taskModuleAttributes;
        }

        /// <summary>
        /// </summary>
        /// <param name="parparameter"></param>
        public PagedList<TaskQueue> GetPageList(object parparameter)
        {
            var pageList = GetPagedList(parparameter);
            var taskQueueModuleIds = TaskQueueModule.GetTaskQueueModuleIds();
            pageList.ForEach(r =>
            {
                var taskModuleAttribute = GetTaskModuleAttribute(r.ModuleId);
                if (taskQueueModuleIds.TryGetValue(r.ModuleId, out var name)) r.ModuleName = name;
            });
            return pageList;
        }

        public List<TaskQueue> GetQueuePageList(Guid moduleId)
        {
            var pageList = (IEnumerable<TaskQueue>) GetList(r => r.ModuleId == moduleId);
            var taskQueueModuleIds = TaskQueueModule.GetTaskQueueModuleIds();
            pageList.Foreach(r =>
            {
                var taskModuleAttribute = GetTaskModuleAttribute(r.ModuleId);
                if (taskQueueModuleIds.TryGetValue(r.ModuleId, out var name)) r.ModuleName = name;
            });
            return pageList.ToList();
        }

        public IList<TaskQueue> GetBackJobPendingList()
        {
            var list = GetListNoTracking(r =>
                r.Status == QueueStatus.Pending
                && r.ModuleId != TaskQueueModuleId.UserUpgradeByUpgradePoints);
            return list;
        }

        public IList<TaskQueue> GetUpgradePendingList()
        {
            var list = GetListNoTracking(r =>
                r.Status == QueueStatus.Pending
                && r.ModuleId == TaskQueueModuleId.UserUpgradeByUpgradePoints);
            return list.OrderBy(r => r.Id).ToList();
        }
    }
}