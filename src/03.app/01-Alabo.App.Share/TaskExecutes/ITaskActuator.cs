using System;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Entities;

namespace Alabo.App.Share.TaskExecutes {

    /// <summary>
    ///     task执行器
    /// </summary>
    public interface ITaskActuator : IDisposable {

        /// <summary>
        ///     开始执行task任务，，分润等
        ///     Executes the task.
        /// </summary>
        /// <typeparam name="TParameter">The type of the t parameter.</typeparam>
        /// <param name="shareOrder"></param>
        /// <param name="type">The type.</param>
        /// <param name="parameter">参数</param>
        void ExecuteTask<TParameter>(Type type, ShareOrder shareOrder, TParameter parameter) where TParameter : class;

        /// <summary>
        ///     /// 执行队列
        ///     执行TaskQueue中的队列
        ///     Executes the task.
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="type"></param>
        /// <param name="taskQueue"></param>
        /// <param name="parameter"></param>
        void ExecuteQueue<TParameter>(Type type, TaskQueue taskQueue, TParameter parameter) where TParameter : class;
    }
}