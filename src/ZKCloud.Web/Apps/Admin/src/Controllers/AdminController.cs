using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Container;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.Mvc;
using MyUser = ZKCloud.Web.Apps.User.src.Entities.User;
using ZKCloud.Web.Apps.User.src.Repositories;

namespace ZKCloud.Web.Apps.Admin.src.Controllers {
	[App("Admin")]
	public class AdminController : BaseController {
		/// <summary>
		/// 登录页面
		/// 访问地址:/Admin/admin/login
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin/login")]
		public IActionResult Login() {
			return View();
		}
		/// <summary>
		/// 后台首页
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin/index")]
		public IActionResult Index() {		
			return View();
		}

		public IActionResult LoginSubmit(MyUser user) {
			var list = Resolve<UserRepository>().ReadMany(e => e.Username == user.Username);
			if (list.Count() == 0)
				return Content("用户不存在！");
			if (list.First().Password != user.Password)
				return Content("密码错误");
			return Content("demo01::test::testadd called.");
		}

		public IActionResult Regedit() {
			return View();
		}
		public IActionResult RegeditSubmit(MyUser user) {
			var list = Resolve<UserRepository>().ReadMany(e => e.Username == user.Username);
			if (list.Count() != 0)
				return Content("用户已存在！");
			Resolve<UserRepository>().AddSingle(user);
			return Content("注册成功！");
		}


        /// <summary>
        /// 登录页面
        /// 访问地址:/admin/roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("admin/article")]
        public IActionResult Article()
        {
            return View();
        }

        /// <summary>
        /// 登录页面
        /// 访问地址:/admin/roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("admin/roles")]
        public IActionResult Roles()
        {
            return View();
        }
    }
}

