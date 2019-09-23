using System;
using Alabo.Core.Files;

namespace Alabo.Web.CodeGeneration.EntityCode.Templates {

    public static class BaseTemplate {

        public static string GetFilePath(Type type, string flag, string entityPath) {
            var filePath = entityPath.Replace("Entities", flag);
            DirectoryHelper.CreateIfNotExists(filePath);

            return filePath;
        }
    }
}