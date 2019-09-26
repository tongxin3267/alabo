using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Employes.Controllers {

    [ApiExceptionFilter]
    [Route("Api/PostRole/[action]")]
    public class ApiPostRoleController : ApiBaseController<PostRole, ObjectId> {

        public ApiPostRoleController() : base() {
            BaseService = Resolve<IPostRoleService>();
        }

        /// <summary>
        /// 权限编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public ApiResult Edit([FromBody] PostRoleInput model) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            var result = Resolve<IPostRoleService>().Edit(model);
            return ToResult(result);
        }

        /// <summary>
        /// 岗位树形菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Tree() {
            var result = Resolve<IPostRoleService>().RoleTrees();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet]
        //public new ApiResult<PostRole> ViewById(string id)
        //{
        //    return ApiResult.Failure<PostRole>("呵呵哒");
        //}

        /// <summary>
        ///     根据ID获取视图
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet]
        [Display(Description = "根据Id获取单个实例")]
        public override ApiResult<PostRole> ViewById(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<PostRole>("请在控制器中定义BaseService");
            }
            var result = Resolve<IPostRoleService>().GetViewById(id);
            var tree = Resolve<IPostRoleService>().RoleTrees();
            //判断第一级 是否勾选,如果勾选 判断第二季是否全选
            //取出所有一级有勾选的菜单
            var roleIds = result.RoleIds;
            result.RoleIds = new List<ObjectId>();
            var one = tree.Select(s => s.Id).Where(s => roleIds.Contains(s));
            result.RoleIds = GetPostRoleId(tree, roleIds);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取选中的id ,如果下级未全选则上级不返回
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        private List<ObjectId> GetPostRoleId(IList<PlatformRoleTreeOutput> input, IList<ObjectId> ids) {
            var result = new List<ObjectId>();
            if (ids == null || input == null) {
                return result;
            }

            foreach (var item in input) {
                foreach (var items in item.AppItems) {
                    foreach (var itemNode in items.AppItems) {
                        var id = ids.SingleOrDefault(s => s == itemNode.Id);
                        if (id != null && ObjectId.Empty != id) {
                            result.Add(itemNode.Id);//第三级全部加入
                        }
                    }
                    //如果全选则把上级写入
                    if (items.AppItems.Count == items.AppItems.Count(s => ids.Contains(s.Id))) {
                        result.Add(items.Id);
                    }
                }
                if (item.AppItems.Count == item.AppItems.Count(s => ids.Contains(s.Id))) {
                    result.Add(item.Id);
                }
            }
            return result;
        }
    }
}