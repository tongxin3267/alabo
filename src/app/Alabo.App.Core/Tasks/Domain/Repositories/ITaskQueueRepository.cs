using System.Collections.Generic;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public interface ITaskQueueRepository : IRepository<TaskQueue, long> {

        /// <summary>
        ///     获取未处理的队列
        /// </summary>
        IEnumerable<TaskQueue> GetUnhandledList();
    }
}