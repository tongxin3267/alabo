using Alabo.Domains.Services;
using Alabo.Framework.Tasks.Schedules.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Alabo.Framework.Tasks.Schedules.Domain.Services {

    public interface IScheduleService : IService<Schedule, ObjectId> {

        /// <summary>
        ///     获取所有的后台作业任务类型
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        void Init();
    }
}