using System;
using System.Collections.Generic;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Services;
using Alabo.Schedules;

namespace Alabo.App.Core.Tasks.Domain.Services {

    /// <summary>
    ///     Interface ITaskQueueService
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.IService" />
    public interface ITaskQueueService : IService<TaskQueue, long> {

        /// <summary>
        /// 添加后台队列任务，比如二维码更新、推荐下修改、奖金池统计等
        /// </summary>
        /// <param name="backJobParameter"></param>
        /// <returns></returns>
        ServiceResult AddBackJob(BackJobParameter backJobParameter);

        /// <summary>
        /// 获取为处理的后台作业订单
        /// </summary>
        /// <returns></returns>
        IList<TaskQueue> GetBackJobPendingList();

        /// <summary>
        /// 获取后台升级相关，未处理的订单
        /// </summary>
        /// <returns></returns>
        IList<TaskQueue> GetUpgradePendingList();

        /// <summary>
        ///     Adds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="parameter">参数</param>
        void Add(long userId, Guid moduleId, object parameter);

        /// <summary>
        ///     Adds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="executionTime">The execution time.</param>
        /// <param name="parameter">参数</param>
        void Add(long userId, Guid moduleId, DateTime executionTime, object parameter);

        /// <summary>
        ///     Adds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="executionTime">The execution time.</param>
        /// <param name="executionTimes">The execution times.</param>
        /// <param name="parameter">参数</param>
        void Add(long userId, Guid moduleId, TaskQueueType type, DateTime executionTime, int executionTimes,
            object parameter);

        /// <summary>
        ///     Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        void Delete(long userId, Guid moduleId);

        /// <summary>
        ///     Handles the specified identifier.
        /// </summary>
        /// <param name="id">Id标识</param>
        void Handle(long id);

        /// <summary>
        ///     Counts the specified module identifier.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>System.Int32.</returns>
        int Count(Guid moduleId);

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <returns>TaskQueue.</returns>
        TaskQueue GetSingle(long id);

        /// <summary>
        ///     Gets all unhandled list.
        /// </summary>
        /// <returns>IEnumerable&lt;TaskQueue&gt;.</returns>
        IEnumerable<TaskQueue> GetAllUnhandledList();

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="IsHandled">if set to <c>true</c> [is handled].</param>
        /// <returns>IEnumerable&lt;TaskQueue&gt;.</returns>
        IEnumerable<TaskQueue> GetList(Guid ModuleId, bool IsHandled);

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="query">查询</param>
        /// <returns>IEnumerable&lt;TaskQueue&gt;.</returns>
        IEnumerable<TaskQueue> GetList(IPredicateQuery<TaskQueue> query);

        /// <summary>
        ///     Gets the paged list.
        /// </summary>
        /// <param name="query">查询</param>
        /// <returns>PagedList&lt;TaskQueue&gt;.</returns>
        PagedList<TaskQueue> GetPagedList(IPageQuery<TaskQueue> query);

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>TaskQueue.</returns>
        TaskQueue GetSingle(Guid moduleId);

        /// <summary>
        ///     Gets the task module attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>TaskModuleAttribute.</returns>
        TaskModuleAttribute GetTaskModuleAttribute<T>() where T : ITaskModule;

        /// <summary>
        ///     Gets the task module attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>TaskModuleAttribute.</returns>
        TaskModuleAttribute GetTaskModuleAttribute(Guid moduleId);

        /// <summary>
        ///     Gets all share module.
        ///     获取所有的分润维度
        /// </summary>
        /// <returns>Type.</returns>
        IDictionary<Type, TaskModuleAttribute> GetAllTaskModuleAttribute();

        /// <summary>
        ///     获取实体
        /// </summary>
        /// <param name="parparameter"></param>
        PagedList<TaskQueue> GetPageList(object parparameter);

        /// <summary>
        ///     获取实体
        /// </summary>
        /// <param name="moduleId"></param>
        List<TaskQueue> GetQueuePageList(Guid moduleId);
    }
}