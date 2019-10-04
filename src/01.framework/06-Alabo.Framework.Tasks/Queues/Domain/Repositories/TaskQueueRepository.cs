using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Alabo.Framework.Tasks.Queues.Domain.Repositories {

    internal class TaskQueueRepository : RepositoryEfCore<TaskQueue, long>, ITaskQueueRepository {

        public TaskQueueRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public IEnumerable<TaskQueue> GetUnhandledList() {
            var sql =
                @"SELECT Id, CreateTime ,ExecutionTime ,ExecutionTimes ,HandleTime ,IsHandled ,MaxExecutionTimes ,ModuleId ,
          Parameter ,Type ,UserId
		  FROM dbo.Task_TaskQueue WHERE IsHandled=0";
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                var list = new List<TaskQueue>();
                while (reader.Read()) {
                    list.Add(ReaderSingle(reader));
                }

                return list;
            }
        }

        private TaskQueue ReaderSingle(DbDataReader reader) {
            return new TaskQueue {
                Id = reader.Read<int>("Id"),
                CreateTime = reader.Read<DateTime>("CreateTime"),
                ExecutionTime = reader.Read<DateTime>("ExecutionTime"),
                ExecutionTimes = reader.Read<int>("ExecutionTimes"),
                HandleTime = reader.Read<DateTime>("HandleTime"),
                Status = (QueueStatus)reader.Read<int>("IsHandled"),
                Message = reader.Read<string>("Message"),
                MaxExecutionTimes = reader.Read<int>("MaxExecutionTimes"),
                ModuleId = reader.Read<Guid>("ModuleId"),
                Parameter = reader.Read<string>("Parameter"),
                Type = (TaskQueueType)reader.Read<int>("Type"),
                UserId = reader.Read<long>("UserId")
            };
        }
    }
}