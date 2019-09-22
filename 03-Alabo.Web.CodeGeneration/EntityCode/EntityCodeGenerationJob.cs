using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.Dependency;
using Alabo.Domains.Entities.Core;
using Alabo.Helpers;
using Alabo.Schedules.Job;
using Alabo.Test.Generation.CodeTemplate;
using Quartz;
using Senparc.NeuChar.Entities;

namespace Alabo.Web.CodeGeneration.EntityCode {

    /// <summary>
    /// 实体单元测试生成
    /// </summary>
    public class EntityCodeGenerationJob : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //手动修改
            CreateCode(typeof(Article));
            Console.WriteLine(@"所有代码生成完成");
        }

        public void CreateCode(Type type) {
            var keyType = "long"; // Sql实体主键类型为long
            if (type.GetInterfaces().Contains(typeof(IMongoEntity))) {
                keyType = "ObjectId"; // MongoDb实体 主键类型为ObjectId
            }
            ServcieTemplate.Create(type);
            Console.WriteLine(@"服务代码已生成");
            RepositroyTemplate.Create(type);
            Console.WriteLine(@"仓储代码已生成");
            ApiControllerTemplate.CreateApiController(type, keyType);
            Console.WriteLine(@"Api接口代码已生成");
        }
    }
}