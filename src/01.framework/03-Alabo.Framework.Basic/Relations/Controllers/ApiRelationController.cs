using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Common.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Relation/[action]")]
    public class ApiRelationController : ApiBaseController<Relation, long> {

        public ApiRelationController() : base() {
            BaseService = Resolve<IRelationService>();
        }

        /// <summary>
        /// 获取单个分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Relation> GetView([FromQuery] long id) {
            var result = Resolve<IRelationService>().GetSingle(id);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取所有的分类链接
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<Link>> GetClassLinks() {
            var result = Resolve<IRelationService>().GetClassLinks();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取所有的标签链接
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<Link>> GetTagLinks() {
            var result = Resolve<IRelationService>().GetTagLinks();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ApiResult<List<Link>> GetLinks([FromQuery] string type) {
            //doto 2019年9月22日
            //var result = Resolve<IAutoConfigService>().GetAllLinks();
            //return ApiResult.Success(result);
            return null;
        }

        /// <summary>
        /// 保存分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Save([FromBody] Relation model) {
            //check
            if (model.Name.IsNullOrEmpty()) {
                return ApiResult.Failure("保存失败：名称不能为空");
            }
            if (model.Type.IsNullOrEmpty()) {
                return ApiResult.Failure("保存失败：type参数异常");
            }
            var type = model.Type.GetTypeByName();
            if (type == null) {
                return ApiResult.Failure("保存失败：type参数异常");
            }
            //add
            model.Name = model.Name.Trim();
            model.Type = type.FullName;

            var result = Resolve<IRelationService>().AddOrUpdate(model, model.Id > 0);

            return ApiResult.Success(result);
        }

        /// <summary>
        /// 批量保存更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveOrUpdateList([FromBody] List<Relation> list) {
            try {
                var confirm = list.GroupBy(s => s.Name).Select(s => s.First());
                if (confirm.Count() < list.Count()) {
                    return ApiResult.Failure("保存失败：存在重复标签");
                }

                var i = 0;
                foreach (var model in list) {
                    if (model.Name.IsNullOrEmpty()) {
                        return ApiResult.Failure("保存失败：名称不能为空");
                    }
                    if (model.Type.IsNullOrEmpty()) {
                        return ApiResult.Failure("保存失败：type参数异常");
                    }
                    var type = model.Type.GetTypeByName();
                    if (type == null) {
                        return ApiResult.Failure("保存失败：type参数异常");
                    }
                    //add
                    model.Name = model.Name.Trim();
                    model.Type = type.FullName;
                    model.SortOrder = i;
                    i++;
                    var result = Resolve<IRelationService>().AddOrUpdate(model, model.Id > 0);
                }

                return ApiResult.Success("保存成功!");
            } catch (System.Exception exc) {
                return ApiResult.Failure($"保存失败: {exc.Message}");
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult Delete([FromQuery] long id) {
            //get
            var relation = Resolve<IRelationService>().GetSingle(a => a.Id == id);
            if (relation == null) {
                return ApiResult.Failure("删除失败：提交数据异常");
            }
            //relation count
            var relationCount = Resolve<IRelationService>()
                .Count(a => a.FatherId == id && a.Status == Status.Normal);
            if (relationCount > 0) {
                return ApiResult.Failure("删除失败：含有子分类不能删除");
            }
            //relation index count
            var relationIndexCount = Resolve<IRelationIndexService>()
                .Count(e => e.RelationId == id);
            if (relationIndexCount > 0) {
                return ApiResult.Failure("删除失败：分类下有数据,不能删除");
            }
            //delete
            var result = Resolve<IRelationService>().Delete(o => o.Id == relation.Id);
            if (!result) {
                ApiResult.Failure("删除失败");
            }

            return ApiResult.Success();
        }

        /// <summary>
        ///     获取所有的级联分类与标签
        /// </summary>
        [HttpGet]
        public ApiResult GetAllType() {
            var result = Resolve<IRelationService>().GetAllTypes();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     根据类型获取所有的分类
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "分类")]
        public ApiResult GetClass(string type, long userId) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<object>("类型名称不能为空");
            }
            var attrNameForTitle = GetAttrValueByName(type, "Name");
            var result = Resolve<IRelationService>().GetClass(type, userId).OrderBy(s => s.SortOrder).ToList();

            var rsObj = new { Title = attrNameForTitle, Datas = result };
            return ApiResult.Success(rsObj);
        }

        /// <summary>
        ///     根据类型获取所有的父级分类，标签可在数据结构(/Admin/Table/List)找查找
        /// </summary>
        /// <param name="type"></param>
        [Display(Description = "父级分类")]
        [HttpGet]
        public ApiResult<List<RelationApiOutput>> GetFatherClass(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<RelationApiOutput>>("类型名称不能为空");
            }

            var result = Resolve<IRelationService>().GetClass(type).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类，标签可在数据结构(/Admin/Table/List)找查找
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetFatherKeyValues(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("类型名称不能为空");
            }

            var result = Resolve<IRelationService>().GetFatherKeyValues(type).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     根据类型获取所有的标签,标签可在数据结构(/Admin/Table/List)找查找
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult GetTag(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure("类型名称不能为空");
            }
            var attrNameForTitle = GetAttrValueByName(type, "Name");
            var result = Resolve<IRelationService>().GetClass(type).ToList();

            var rsObj = new { Title = attrNameForTitle, Datas = result };
            return ApiResult.Success(rsObj);
        }

        private object GetAttrValueByName(string type, string name) {
            object rsObj = new object();
            try {
                var typeInstance = type.GetTypeByName();
                rsObj = typeInstance.CustomAttributes.FirstOrDefault().NamedArguments.Where(x => x.MemberName == name).FirstOrDefault().TypedValue.Value;
            } catch (System.Exception) {
                //
            }

            return rsObj;
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类,标签和分类类型可在数据结构(/Admin/Table/List)找查找
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetKeyValues(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("类型名称不能为空");
            }

            var result = Resolve<IRelationService>().GetKeyValues(type).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类,标签和分类类型可在数据结构(/Admin/Table/List)找查找
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetClassTree(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("类型名称不能为空");
            }

            var result = Resolve<IRelationService>().GetKeyValues(type).ToList();
            result.ForEach(u => { u.Key = u.Name; });
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类,标签和分类类型可在数据结构(/Admin/Table/List)找查找
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetTags(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("类型名称不能为空");
            }

            var result = Resolve<IRelationService>().GetKeyValues(type).ToList();
            result.ForEach(u => {
                u.Key = u.Value;
                u.Value = u.Name;
            });
            return ApiResult.Success(result);
        }
    }
}