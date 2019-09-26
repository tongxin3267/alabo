using System;
using Alabo.Files;

namespace Alabo.Web.CodeGeneration.EntityCode.Templates
{
    public static class BaseTemplate
    {
        public static string GetFilePath(Type type, string flag, string entityPath)
        {
            if (flag == "Controllers") entityPath = entityPath.Replace("Domain", "");
            var filePath = entityPath.Replace("Entities", flag);
            DirectoryHelper.CreateIfNotExists(filePath);

            return filePath;
        }
    }
}