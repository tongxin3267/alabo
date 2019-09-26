using System;
using System.Threading.Tasks;
using Alabo.Dependency;
using Alabo.Schedules.Job;
using Quartz;

namespace Alabo.Tenants
{
    /// <summary>
    ///     Tenant job
    /// </summary>
    public class TenantJob : JobBase
    {
        protected override Task Execute(IJobExecutionContext context, IScope scope)
        {
            //TODO:新增租户时重新循环租户任务(zhang.zl)
            throw new NotImplementedException();
        }
    }
}