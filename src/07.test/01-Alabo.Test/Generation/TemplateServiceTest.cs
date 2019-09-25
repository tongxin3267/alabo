using System;
using System.Text;
using Alabo.Files;

namespace Alabo.Test.Generation
{
    public static class TemplateServiceTest
    {
        /// <summary>
        ///     生成而外的方法
        /// </summary>
        /// <param name="text"></param>
        public static string CreateCommonTest(string text, string serviceModel)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetFromCache(text, serviceModel));
            stringBuilder.Append(Count(text, serviceModel));

            return stringBuilder.ToString();
        }

        private static string GetFromCache(string text, string serviceModel)
        {
            if (text.IndexOf(@"[TestMethod(""GetSingleFromCache_Test"")]", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return string.Empty;
            }

            // 单元测试时模板路径
            var templatePath = FileHelper.RootPath + "/Generation/Template/ServcieCodeGetCache.txt";
            var template = FileHelper.Read(templatePath);
            template = template.Replace("[[IModelService]]", serviceModel);

            return template;
        }

        private static string Count(string text, string serviceModel)
        {
            if (text.IndexOf(@"[TestMethod(""Count_Expected_Test"")]", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return string.Empty;
            }

            var templatePath = FileHelper.RootPath + "/Generation/Template/CountTest.txt";
            var template = FileHelper.Read(templatePath);
            template = template.Replace("[[IModelService]]", serviceModel);

            return template;
        }
    }
}