﻿using Alabo.Cloud.Wikis.Settings.Domain.Entities;
using Alabo.Cloud.Wikis.Wikis.Domain.Entities;
using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Dependency;
using Alabo.Reflections;
using Alabo.Schedules.Job;
using Alabo.Web.CodeGeneration.EntityCode.Templates;
using Quartz;
using System;
using System.Threading.Tasks;
using Alabo.Data.Targets.Reports.Domain.Entities;

namespace Alabo.Web.CodeGeneration.EntityCode
{
    /// <summary>
    ///     实体单元测试生成
    /// </summary>
    public class EntityCodeGenerationJob : JobBase
    {
        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //手动修改,实体路径，可右键实体手动复制路径
            var entityPath = @"C:\alaboshu.com\alabo\src\05.cloud\14-Alabo.Cloud.Wikis\Settings\Domain\Entities\";
            CreateCode(typeof(WikiProject), entityPath);

            entityPath = @"C:\alaboshu.com\alabo\src\05.cloud\14-Alabo.Cloud.Wikis\Wikis\Domain\Entities\";
            CreateCode(typeof(WikiHistory), entityPath);

            entityPath = @"C:\alaboshu.com\alabo\src\02.data\03-Alabo.Data.Targets\Reports\Domain\Entities\";
            CreateCode(typeof(TargetReport), entityPath);

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