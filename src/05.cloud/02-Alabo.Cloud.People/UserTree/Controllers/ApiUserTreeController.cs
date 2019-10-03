using Alabo.Cloud.People.UserTree.Domain.Service;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.UserTree.Controllers
{
    /// <summary>
    ///     用户相关Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/UserTree/[action]")]
    public class ApiUserTreeController : ApiBaseController
    {
        /// <summary>
        ///     组织架构图函数
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "组织架构图函数")]
        [ApiAuth]
        public ApiResult Tree(long loginUserId)
        {
            //var ll = Resolve<IUserService>().GetList(e => e.ParentId == loginUserId);
            var bRoot = loginUserId == 0;
            if (bRoot) {
                loginUserId = AutoModel.BasicUser.Id;
            }

            var userTreeList = Resolve<IUserTreeService>().GetUserTree(loginUserId);
            var kk = userTreeList.ToJson();
            return ApiResult.Success(userTreeList);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUserTree(int id, string isFirst = "")
        {
            if (isFirst.ToLower().Equal("yes") && id == 1) {
                id = 0;
            }

            var uMsgs = Resolve<IUserTreeService>().GetUserTree(id);

            return ApiResult.Result(uMsgs);
        }
    }
}