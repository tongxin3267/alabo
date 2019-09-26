﻿using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Framework.Tasks.Queues.Domain.Entities;

namespace Alabo.Framework.Tasks.Queues.Domain.Repositories {

    public interface ITaskQueueRepository : IRepository<TaskQueue, long> {

        /// <summary>
        ///     获取未处理的队列
        /// </summary>
        IEnumerable<TaskQueue> GetUnhandledList();
    }
}