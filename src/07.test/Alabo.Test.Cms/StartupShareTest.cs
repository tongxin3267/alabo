using System;
using System.IO;
using System.Text;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Exceptions;
using Alabo.Files;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Generation {

    public class StartupShareTest : CoreTest {

        //[Theory]
        //[InlineData(typeof(NLevelDistributionModule))]
        //[InlineData(typeof(RebateModule))]
        //[InlineData(typeof(ProvinceShareModule))]
        //[InlineData(typeof(CountryModule))]
        //[InlineData(typeof(NLevelDistributionCultivateModule))]
        //[InlineData(typeof(PartnerShareModule))]
        //[InlineData(typeof(FiexUserShareConfigModule))]
        //[InlineData(typeof(HighLevelPerformanceModule))]
        //[InlineData(typeof(TeamRangPerformanceModule))]
        public void Start(Type type) {
            CreateTest(type);
        }

        public void CreateTest(Type type) {
            var moduleAttribute = Resolve<ITaskModuleConfigService>().GetModuleAttribute(type);
            if (moduleAttribute == null) {
                throw new ValidException("请定义模块特性");
            }

            var host = new TestHostingEnvironment();
            var testBuilder = new StringBuilder();
            var proj = string.Empty;
            if (type.Module.Name == "ZKCloud") {
                proj = type.Assembly.GetName().Name + ".Test";
            } else {
                proj = type.Assembly.GetName().Name.Replace(".App.", ".Test.");
            }

            var paths = type.Namespace.Replace(type.Assembly.GetName().Name, "")
                .Split(".", StringSplitOptions.RemoveEmptyEntries);
            var filePath = $"{host.TestRootPath}\\{proj}";
            foreach (var item in paths) {
                filePath += "\\" + item;
                if (!Directory.Exists(filePath)) {
                    Directory.CreateDirectory(filePath);
                }
            }

            var fileName = $"{filePath}\\{type.Name}Tests.cs";

            if (!File.Exists(fileName)) {
                var templatePath = host.TestRootPath + "/Alabo.Test/Generation/Template/ShareTestTemplate.txt";
                var template = FileHelper.Read(templatePath);

                var testFullName = type.FullName;
                template = template.Replace("[[TestFullName]]",
                    type.FullName.Replace("App.", "Test.").Replace($".{type.Name}", ""));
                template = template.Replace("[[ClassName]]", $"{type.Name}Tests");
                template = template.Replace("[[ModuleFullName]]", type.FullName);

                FileHelper.Write(fileName, template);
            }
        } /*end*/
    }
}