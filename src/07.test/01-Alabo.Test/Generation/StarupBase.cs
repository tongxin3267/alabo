using System;
using Xunit;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Core.Reflections.Services;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Generation
{
    public class StarupBase : CoreTest
    {
        [Fact]
        public void Start()
        {
            Console.WriteLine(@"开始生成代码");
            var allServiceType = Resolve<ITypeService>().GetAllEntityService(); //.Where(o=>!o.IsAbstract);

            foreach (var type in allServiceType)
            {
                try
                {
                    var startupTest = new StartupTest();
                }
                catch (Exception ex)
                {
                }
            }
        } /*end*/

        [Fact]
        public void StartSingle()
        {
            //var type = typeof(IKpiService);
            //var startupTest = new StartupTest();
            //startupTest.StartSingleServieCode(type, true);
        }
    }
}