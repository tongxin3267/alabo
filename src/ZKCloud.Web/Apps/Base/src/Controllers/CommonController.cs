using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Web.Mvc;
using Microsoft.AspNet.Http;
using ZKCloud.Web.Apps.Base.src.Domains.Services;

namespace ZKCloud.Web.Apps.Base.src.Domains {
	[App("Base")]
	public class CommonController : BaseController {
		// GET: /<controller>/

		public IActionResult Index() {
			new ZKCloud.Web.Apps.Perset.src.Entities.InitRegion().InitRegionData();

			var sessionId = HttpContext.Session.GetString("SessionId");
			if (sessionId == null)
			{
                //return Redirect("/user/login"); //暂时改成跳转到管理首页
                return Redirect("/admin/index");
            }
			return View();
		}
		/// <summary>
		/// 获取验证码
		/// </summary>
		/// <returns></returns> 
		[HttpGet("getCaptcha")]
		public IActionResult Captcha() {
			var query = HttpContext.Request.Query;
			var key = query["key"].ToString() ?? "CaptchaKey";
			var captchaServices = new CaptchaServices();
			var code = captchaServices.GetCaptcha(key, HttpContext.Session);
			return Content(code);
		}


		/// <summary>
		/// 动态视图返回
		/// </summary>
		/// <returns></returns>
		public IActionResult dynamic() {
			string checkType = HttpContext.Items.SingleOrDefault(c => c.Key.Equals("__check__")).Value.ToString();
			//if (checkType.Equals("admin"))
			//{
			//	//判断当前是否登陆且是管理员，否则提示没有权限,重新登陆
			//	return Redirect("/test/noadmin");
			//	//return Redirect("/admin/login");
			//}
			//if (checkType.Equals("user"))
			//{
			//	//判断当前是否登陆且是用户角色，否则提示没有权限,重新登陆
			//	return Redirect("/test/nouser");
			//	//return Redirect("/user/login");
			//}

			string viewName = HttpContext.Items.SingleOrDefault(c => c.Key.Equals("__dynamicviewname__")).Value.ToString();

            //这里将路由中的app改成实际访问的app名称，用于其他不是用相对路径查找视图的操作
            string actAppName= HttpContext.Items.SingleOrDefault(c => c.Key.Equals("__appname__")).Value.ToString();
            RouteData.Values["app"] = actAppName;
			return View(viewName);
		}

		[HttpGet("test/noadmin")]
		public IActionResult noadmin() {
			ViewBag.message = "该页面需要管理员权限,请用管理员身份登录";
			return View("~/apps/demo01/template/nopermission");
		}
		[HttpGet("test/nouser")]
		public IActionResult nouser() {
			ViewBag.message = "该页面需要会员权限,请用先登录";
			return View("~/apps/demo01/template/nopermission");
		}
	}
}
