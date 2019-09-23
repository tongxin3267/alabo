using Alabo.Dependency;
using Alabo.Domains.Entities.Core;
using Alabo.Schedules.Job;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Reflections;
using Alabo.Web.CodeGeneration.EntityCode.Templates;

namespace Alabo.Web.CodeGeneration.EntityCode {

    /// <summary>
    /// 实体单元测试生成
    /// </summary>
    public class EntityCodeGenerationJob : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //手动修改,实体路径，可右键实体手动复制路径
            var entityPath = @"C:\github\alabo\src\04.industry\01-Alabo.Industry.Cms\Articles\Domain\Entities\";
            CreateCode(typeof(Article), entityPath);
            Console.WriteLine(@"所有代码生成完成");
        }

        public void CreateCode(Type type, string entityPath) {
            var keyIdType = Reflection.GetMemberType(type, "Id");
            if (keyIdType == null) {
                Console.WriteLine($@"{type.Name}非实体，请重新输入");
                return;
            }

            ServcieTemplate.Create(type, entityPath);
            Console.WriteLine(@"服务代码已生成");
            RepositroyTemplate.Create(type, entityPath);
            Console.WriteLine(@"仓储代码已生成");
            ApiControllerTemplate.CreateApiController(type, keyIdType.Name, entityPath);
            Console.WriteLine(@"Api接口代码已生成");
        }
    }
}