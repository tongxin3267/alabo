using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Tasks.Schedules.Domain.Entities;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Schedules.Job;
using Alabo.Web.Mvc.Attributes;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Alabo.Framework.Tasks.Schedules.Domain.Services
{
    public class ScheduleService : ServiceBase<Schedule, ObjectId>, IScheduleService
    {
        public ScheduleService(IUnitOfWork unitOfWork, IRepository<Schedule, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        public IEnumerable<Type> GetAllTypes()
        {
            var cacheKey = "Schedule_alltypes";
            return ObjectCache.GetOrSetPublic(() =>
            {
                var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => !t.IsAbstract && t.BaseType == typeof(JobBase) ||
                                t.BaseType?.BaseType == typeof(JobBase))).ToList();

                return types;
            }, cacheKey).Value;
        }

        public void Init()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();

            var list = GetList();
            var addList = new List<Schedule>();
            foreach (var type in GetAllTypes()) {
                try
                {
                    long delay = 0;
                    var config = Activator.CreateInstance(type, loggerFactory);
                    var dynamicConfig = (dynamic) config;
                    var timeSpan = (TimeSpan) dynamicConfig.DelayInterval;
                    delay = timeSpan.TotalSeconds.ConvertToLong();

                    var find = list.FirstOrDefault(r => r.Type == type.FullName);
                    if (find == null)
                    {
                        find = new Schedule
                        {
                            Type = type.FullName,
                            Name = type.Name
                        };
                        var classPropertyAttribute = type.GetAttribute<ClassPropertyAttribute>();
                        if (classPropertyAttribute != null) {
                            find.Name = classPropertyAttribute.Name;
                        }

                        addList.Add(find);
                    }
                    else
                    {
                        var isUpdate = false;
                        if (find.Delay != delay)
                        {
                            find.Delay = delay;
                            isUpdate = true;
                        }

                        if (find.Name == type.Name)
                        {
                            find.Delay = delay;
                            var classPropertyAttribute = type.GetAttribute<ClassPropertyAttribute>();
                            if (classPropertyAttribute != null)
                            {
                                find.Name = classPropertyAttribute.Name;
                                isUpdate = true;
                            }
                        }

                        if (isUpdate) {
                            Update(find);
                        }
                    }
                }
                catch
                {
                }
            }

            AddMany(addList);
        }
    }
}