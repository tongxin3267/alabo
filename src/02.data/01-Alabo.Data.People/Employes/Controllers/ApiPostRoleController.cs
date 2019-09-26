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
        /// Ȩ�ޱ༭
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
        /// ��λ���β˵�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Tree() {
            var result = Resolve<IPostRoleService>().RoleTrees();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ��λ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet]
        //public new ApiResult<PostRole> ViewById(string id)
        //{
        //    return ApiResult.Failure<PostRole>("�Ǻ���");
        //}

        /// <summary>
        ///     ����ID��ȡ��ͼ
        /// </summary>
        /// <param name="id">��ʶ</param>
        [HttpGet]
        [Display(Description = "����Id��ȡ����ʵ��")]
        public override ApiResult<PostRole> ViewById(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<PostRole>("���ڿ������ж���BaseService");
            }
            var result = Resolve<IPostRoleService>().GetViewById(id);
            var tree = Resolve<IPostRoleService>().RoleTrees();
            //�жϵ�һ�� �Ƿ�ѡ,�����ѡ �жϵڶ����Ƿ�ȫѡ
            //ȡ������һ���й�ѡ�Ĳ˵�
            var roleIds = result.RoleIds;
            result.RoleIds = new List<ObjectId>();
            var one = tree.Select(s => s.Id).Where(s => roleIds.Contains(s));
            result.RoleIds = GetPostRoleId(tree, roleIds);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡѡ�е�id ,����¼�δȫѡ���ϼ�������
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
                            result.Add(itemNode.Id);//������ȫ������
                        }
                    }
                    //���ȫѡ����ϼ�д��
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