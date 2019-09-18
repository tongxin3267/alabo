using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Tasks.Domain.Services {

    public interface IScheduleService : IService<Schedule, ObjectId> {

        /// <summary>
        ///     ��ȡ���еĺ�̨��ҵ��������
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        void Init();
    }
}