using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ZKCloud.Web.Apps.Demo02.Controllers {
    [App("demo02")]
    public class TestController : BaseController {
        // GET: /<controller>/
        public IActionResult Index() {
            return Content("demo02::index");
        }
    }
}
