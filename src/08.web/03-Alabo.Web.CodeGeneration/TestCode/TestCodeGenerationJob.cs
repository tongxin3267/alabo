using Alabo.Dependency;
using Alabo.Schedules.Job;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Alabo.Web.CodeGeneration.TestCode
{
    /// <summary>
    ///     单元测试方法生成
    /// </summary>
    public class TestCodeGenerationJob : JobBase
    {
        protected override Task Execute(IJobExecutionContext context, IScope scope)
        {
            throw new NotImplementedException();
        }
    }
}