using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alabo.Dependency;
using Alabo.Schedules.Job;
using Quartz;

namespace Alabo.Web.CodeGeneration.TestCode {

    /// <summary>
    /// 单元测试方法生成
    /// </summary>
    public class TestCodeGenerationJob : JobBase {

        protected override Task Execute(IJobExecutionContext context, IScope scope) {
            throw new NotImplementedException();
        }
    }
}