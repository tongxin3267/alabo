using System;
using System.IO;
using System.Text;
using Alabo.Exceptions;

namespace Alabo.Web.CodeGeneration.EntityCode.Templates
{
    public static class RepositroyTemplate
    {
        #region 生成Repositories接口，与Repositories方法

        /// <summary>
        ///     生成服务接口，与服务方法
        /// </summary>
        /// <param name="type"></param>
        public static void Create(Type type, string idType, string entityPath)
        {
            //type = typeof(Theme);

            if (!type.BaseType.FullName.Contains("Entities")) throw new ValidException("命名空间必须包含Entities");

            var testBuilder = new StringBuilder();

            var filePath = BaseTemplate.GetFilePath(type, "Repositories", entityPath);
            var fileName = $"{filePath}\\I{type.Name}Repository.cs".Replace("test\\", "app\\");
            if (!File.Exists(fileName))
            {
                testBuilder.AppendLine(
                    "using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;");
                testBuilder.AppendLine("using System.Linq;");
                testBuilder.AppendLine("using MongoDB.Bson;");
                testBuilder.AppendLine("using Alabo.Domains.Repositories;");
                testBuilder.AppendLine($"using {type.Namespace};");

                if (testBuilder.ToString().IndexOf(type.Namespace, StringComparison.OrdinalIgnoreCase) == -1)
                    testBuilder.AppendLine($"using {type.Namespace};");

                testBuilder.AppendLine();
                testBuilder.AppendLine($"namespace {type.Namespace.Replace("Entities", "Repositories")} {{");
                ;

                testBuilder.AppendLine(
                    $"\tpublic interface I{type.Name}Repository : IRepository<{type.Name}, {idType}>  {{");

                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine("}");
                //创建文件

                using (var stream = File.Create(fileName))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(testBuilder);
                    }
                }
            }

            fileName = $"{filePath}\\{type.Name}Repository.cs".Replace("test\\", "app\\")
                .Replace("Entities", "Services");
            if (!File.Exists(fileName))
            {
                testBuilder = new StringBuilder();
                testBuilder.AppendLine(
                    "using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;");
                testBuilder.AppendLine("using System.Linq;");
                testBuilder.AppendLine("using MongoDB.Bson;");
                testBuilder.AppendLine($"using {type.Namespace};");
                testBuilder.AppendLine("using Alabo.Domains.Repositories;");
                testBuilder.AppendLine("using Alabo.Datas.UnitOfWorks;");
                testBuilder.AppendLine($"using  {type.Namespace.Replace("Entities", "Repositories")};");

                if (testBuilder.ToString().IndexOf(type.Namespace, StringComparison.OrdinalIgnoreCase) == -1)
                    testBuilder.AppendLine($"using {type.Namespace};");

                testBuilder.AppendLine();
                testBuilder.AppendLine($"namespace {type.Namespace.Replace("Entities", "Repositories")} {{");
                ;

                testBuilder.AppendLine(
                    $"\tpublic class {type.Name}Repository : RepositoryMongo<{type.Name}, {idType}>,I{type.Name}Repository  {{");

                testBuilder.AppendLine($"\tpublic  {type.Name}Repository(IUnitOfWork unitOfWork) : base(unitOfWork){{");
                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine("}");
                //创建文件

                using (var stream = File.Create(fileName))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(testBuilder);
                    }
                }
            }
        }

        #endregion 生成Repositories接口，与Repositories方法
    }
}