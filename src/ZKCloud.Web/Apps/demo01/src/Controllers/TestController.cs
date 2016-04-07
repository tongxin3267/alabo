using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNet.Mvc;
using ZKCloud.Container;
using ZKCloud.Domain.Repositories;
using ZKCloud.Apps;
using ZKCloud.Web.Mvc;
using ZKCloud.Web.Apps.Demo01.Domain.Models;
using ZKCloud.Web.Apps.Demo01.Domain.Repositories;
using ZKCloud.Web.Apps.Demo01.Domain.Services;
using ZKCloud.Domain.Entities;
using ZKCloud.Core.AutoConfig;
using ZKCloud.Core.AutoConfig.Domain.Entities;
using ZKCloud.Core.AutoConfig.Domain.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ZKCloud.Web.Apps.Demo01.Controllers {
	[App("demo01")]
	public class TestController : BaseController {
		// GET: /<controller>/
		public IActionResult Index() {
			string result = "demo01::test::index called.\n";
			var list = Resolve<TestDataRepository>().ReadMany(e => e.Id < 1000);
			result += string.Join("", list.Select(e => $"id:{e.Id}, name:{e.Name}\n").ToArray());
			return View((object)result);
		}

		public IActionResult Add() {
			Resolve<TestDataRepository>().AddSingle(new TestData()
			{
				Name = "aaaaaaaaaaaa"
			});
			return Content("demo01::test::testadd called.");
		}

        public IActionResult Test() {
            IDynamicAppCompiler compiler = new RoslynAppCompiler("c:\\users\\leven\\desktop\\", "demo01");
            var result = compiler.AddDefaultUsing()
                .AddCoreReference()
                .AddCurrentReferences()
                .AddFile(System.IO.Path.Combine(ZKCloud.Runtime.RuntimeContext.Current.Path.BaseDirectory, "apps\\demo01\\src\\Controllers\\TestController.cs"), Encoding.UTF8)
                .Build();
            return Content($"result:{result.Success}\r\nmessage:{result.Message}.");
        }

		public IActionResult TestView() {
			return View();
		}

		public IActionResult Login() {
			return View();
		}

        public PagedList<TestData> list(int? page) {
            Console.WriteLine("TestController::List()");
            page = page ?? 1;
            if (page < 1)
                page = 1;
            int pageSize = 20;
            var model = Resolve<ITestDataService>().GetList(page.Value, pageSize);
            return model;
        }

        public IActionResult TestConfig() {
            try {
                AutoConfigDescription configDescription = AutoConfigDescription.Create(typeof(TestConfig));
                var configs = configDescription.CreateGenericConfigs(new TestConfig() {
                    Name = "demo 01 name",
                    Title = "demo 01 title",
                    IntValue = 100
                });
                Resolve<IGenericConfigService>().AddOrUpdate(configs);
                configs = Resolve<IGenericConfigService>().GetList("demo01");
                TestConfig config = configDescription.CreateAutoConfig<TestConfig>(configs);
                return Content($"config.name={config.Name}, config.Title={config.Title}, config.IntValue={config.IntValue}");
            } catch (Exception e) {
                return Content(e.ToString());
            }
        }
	}

    [Config("demo01")]
    public class TestConfig : IAutoConfig {
        [ConfigProperty("name", ConfigPropertyType.Text)]
        public string Name { get; set; }

        [ConfigProperty("title", ConfigPropertyType.Text)]
        public string Title { get; set; }

        public int IntValue { get; set; }
    }
}
