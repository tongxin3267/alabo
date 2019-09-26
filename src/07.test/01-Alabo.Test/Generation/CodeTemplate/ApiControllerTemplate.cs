using System;
using System.IO;
using System.Text;
using Alabo.Exceptions;
using Alabo.Files;

namespace Alabo.Test.Generation.CodeTemplate {

    public static class ApiControllerTemplate {

        #region 生成API接口

        /// <summary>
        ///     生成服务方法
        ///     生成服务接口，与服务方法
        /// </summary>
        /// <param name="type"></param>
        public static void CreateApiController(Type type, string idType, string projectName = "zkcloudv11s") {
            //  type = typeof(WidgetHistory);
            //if (type.BaseType.FullName != typeof(MongoEntity).FullName) throw new ValidException("非Mongodb实体方法，不支持服务方法生成");

            if (!type.BaseType.FullName.Contains("Entities")) {
                throw new ValidException("命名空间必须包含Entities");
            }

            var testBuilder = new StringBuilder();
            var filePath = BaseTemplate.GetFilePath(type, "Controllers").Replace("Domain", "").Replace("Domains", "")
                .Replace(projectName, "");
            DirectoryHelper.CreateIfNotExists(filePath);
            var fileName = filePath + $"\\Api{type.Name}Controller.cs";
            if (!File.Exists(fileName)) {
                testBuilder.AppendLine(
                    "using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;");
                testBuilder.AppendLine("using System.Linq;");

                testBuilder.AppendLine("using Alabo.Domains.Entities;");
                testBuilder.AppendLine("using Microsoft.AspNetCore.Mvc;");
                testBuilder.AppendLine("using Alabo.Framework.Core.WebApis.Filter;");
                testBuilder.AppendLine("");
                testBuilder.AppendLine("using MongoDB.Bson;");
                testBuilder.AppendLine("using Alabo.App.Core.User;");
                testBuilder.AppendLine("using Alabo.RestfulApi;using ZKCloud.Open.ApiBase.Configuration;");
                testBuilder.AppendLine("using Alabo.Domains.Services;");
                testBuilder.AppendLine("using Alabo.Web.Mvc.Attributes;");
                testBuilder.AppendLine("using Alabo.Web.Mvc.Controllers;");
                testBuilder.AppendLine($"using {type.Namespace};");

                if (testBuilder.ToString().IndexOf(type.Namespace, StringComparison.OrdinalIgnoreCase) == -1) {
                    testBuilder.AppendLine($"using {type.Namespace};");
                }

                testBuilder.AppendLine();
                testBuilder.AppendLine(
                    $"namespace {type.Namespace.Replace("Entities", "Controllers").Replace("Domain.", "")} {{");

                testBuilder.AppendLine("\t\t[ApiExceptionFilter]");
                testBuilder.AppendLine($"\t\t[Route(\"Api/{type.Name}/[action]\")]");
                testBuilder.AppendLine(
                    $"\t\tpublic class Api{type.Name}Controller : ApiBaseController<{type.Name},{idType}>  {{");

                testBuilder.AppendLine(
                    $" public Api{type.Name}Controller() : base() ");
                testBuilder.AppendLine("\t{ ");
                testBuilder.AppendLine($"\t\tBaseService = Resolve<I{type.Name}Service>();");
                testBuilder.AppendLine("\t}");
                testBuilder.AppendLine();

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

        #endregion 生成API接口
    }
}