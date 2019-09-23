using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Dependency;
using Alabo.Reflections;
using Alabo.Schedules.Job;
using Alabo.Web.CodeGeneration.EntityCode.Templates;
using System;
using System.Threading.Tasks;
using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Quartz;

namespace Alabo.Web.CodeGeneration.EntityCode {

    /// <summary>
    /// 实体单元测试生成
    /// </summary>
    public class EntityCodeGenerationJob : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //手动修改,实体路径，可右键实体手动复制路径
            var entityPath = @"C:\github\alabo\src\02.data\03-Alabo.Data.Things\Brands\Domain\Entities";
            CreateCode(typeof(Brand), entityPath);

            Console.WriteLine(@"所有代码生成完成");
        }

        public void CreateCode(Type type, string entityPath) {
            var keyIdType = Reflection.GetMemberType(type, "Id");
            if (keyIdType == null) {
                Console.WriteLine($@"{type.Name}非实体，请重新输入");
                return;
            }

            var idType = "long";
            if (keyIdType.Name.Contains("ObjectId")) {
                idType = "ObjectId";
            }
            ServcieTemplate.Create(type, idType, entityPath);
            //Console.WriteLine(@"服务代码已生成");
            RepositroyTemplate.Create(type, idType, entityPath);
            //Console.WriteLine(@"仓储代码已生成");
            ApiControllerTemplate.CreateApiController(type, idType, entityPath);
            //Console.WriteLine(@"Api接口代码已生成");
        }
    }
}