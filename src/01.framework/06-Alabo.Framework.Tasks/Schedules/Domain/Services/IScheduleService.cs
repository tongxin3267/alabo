using Alabo.Domains.Services;
using Alabo.Framework.Tasks.Schedules.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Alabo.Framework.Tasks.Schedules.Domain.Services {

    public interface IScheduleService : IService<Schedule, ObjectId> {

        /// <summary>
        ///     ��ȡ���еĺ�̨��ҵ��������
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        void Init();
    }
}