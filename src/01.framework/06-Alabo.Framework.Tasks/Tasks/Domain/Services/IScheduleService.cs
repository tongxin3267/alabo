using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Tasks.Domain.Services {

    public interface IScheduleService : IService<Schedule, ObjectId> {

        /// <summary>
        ///     获取所有的后台作业任务类型
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        void Init();
    }
}