using System;
using System.IO;
using System.Text;

namespace Alabo.Web.CodeGeneration.EntityCode.Templates {

    public static class ServcieTemplate {
        /// <summary>
        ///     生成服务接口，与服务方法
        /// </summary>

        public static void Create(Type type, string idType, string entityPath) {
            if (!type.BaseType.FullName.Contains("Entities")) {
                Console.WriteLine(@"命名空间必须包含Entities");
                return;
            }
            var testBuilder = new StringBuilder();
            var filePath = BaseTemplate.GetFilePath(type, "Services", entityPath);
            var fileName = $"{filePath}\\I{type.Name}Service.cs".Replace("test\\", "app\\")
                .Replace("Entities", "Services");
            if (!File.Exists(fileName)) {
                testBuilder.AppendLine(
                    "using System;");
                testBuilder.AppendLine("using System.Linq;");
                testBuilder.AppendLine("using MongoDB.Bson;");
                testBuilder.AppendLine("using Alabo.Domains.Services;");
                testBuilder.AppendLine($"using {type.Namespace};");
                testBuilder.AppendLine("using Alabo.Domains.Entities;");

                if (testBuilder.ToString().IndexOf(type.Namespace, StringComparison.OrdinalIgnoreCase) == -1) {
                    testBuilder.AppendLine($"using {type.Namespace};");
                }

                testBuilder.AppendLine();
                testBuilder.AppendLine($"namespace {type.Namespace.Replace("Entities", "Services")} {{");
                ;

                testBuilder.AppendLine($"\tpublic interface I{type.Name}Service : IService<{type.Name}, {idType}>  {{");

                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine("\t}");

                //创建文件

                using (var stream = File.Create(fileName)) {
                    using (var writer = new StreamWriter(stream)) {
                        writer.Write(testBuilder);
                    }
                }
            }

            fileName = $"{filePath}\\{type.Name}Service.cs".Replace("test\\", "app\\").Replace("Entities", "Services");
            if (!File.Exists(fileName)) {
                testBuilder = new StringBuilder();
                testBuilder.AppendLine(
                    "using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;");
                testBuilder.AppendLine("using System.Linq;");
                testBuilder.AppendLine("using MongoDB.Bson;");
                testBuilder.AppendLine("using Alabo.Domains.Services;");
                testBuilder.AppendLine("using Alabo.Datas.UnitOfWorks;");
                testBuilder.AppendLine("using Alabo.Domains.Repositories;");
                testBuilder.AppendLine($"using {type.Namespace};");

                if (testBuilder.ToString().IndexOf(type.Namespace, StringComparison.OrdinalIgnoreCase) == -1) {
                    testBuilder.AppendLine($"using {type.Namespace};");
                }

                testBuilder.AppendLine();
                testBuilder.AppendLine($"namespace {type.Namespace.Replace("Entities", "Services")} {{");
                ;

                testBuilder.AppendLine(
                    $"\tpublic class {type.Name}Service : ServiceBase<{type.Name}, {idType}>,I{type.Name}Service  {{");
                testBuilder.AppendLine(
                    $"\tpublic  {type.Name}Service(IUnitOfWork unitOfWork, IRepository<{type.Name}, {idType}> repository) : base(unitOfWork, repository){{");
                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine("}");
                //创建文件

                using (var stream = File.Create(fileName)) {
                    using (var writer = new StreamWriter(stream)) {
                        writer.Write(testBuilder);
                    }
                }
            }
        }
    }
}