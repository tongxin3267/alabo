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
        /// ��ȡ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Relation> GetView([FromQuery] long id) {
            var result = Resolve<IRelationService>().GetSingle(id);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ���еķ�������
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<Link>> GetClassLinks() {
            var result = Resolve<IRelationService>().GetClassLinks();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ���еı�ǩ����
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<Link>> GetTagLinks() {
            var result = Resolve<IRelationService>().GetTagLinks();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ������¼
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ApiResult<List<Link>> GetLinks([FromQuery] string type) {
            //doto 2019��9��22��
            //var result = Resolve<IAutoConfigService>().GetAllLinks();
            //return ApiResult.Success(result);
            return null;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Save([FromBody] Relation model) {
            //check
            if (model.Name.IsNullOrEmpty()) {
                return ApiResult.Failure("����ʧ�ܣ����Ʋ���Ϊ��");
            }
            if (model.Type.IsNullOrEmpty()) {
                return ApiResult.Failure("����ʧ�ܣ�type�����쳣");
            }
            var type = model.Type.GetTypeByName();
            if (type == null) {
                return ApiResult.Failure("����ʧ�ܣ�type�����쳣");
            }
            //add
            model.Name = model.Name.Trim();
            model.Type = type.FullName;

            var result = Resolve<IRelationService>().AddOrUpdate(model, model.Id > 0);

            return ApiResult.Success(result);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveOrUpdateList([FromBody] List<Relation> list) {
            try {
                var confirm = list.GroupBy(s => s.Name).Select(s => s.First());
                if (confirm.Count() < list.Count()) {
                    return ApiResult.Failure("����ʧ�ܣ������ظ���ǩ");
                }

                var i = 0;
                foreach (var model in list) {
                    if (model.Name.IsNullOrEmpty()) {
                        return ApiResult.Failure("����ʧ�ܣ����Ʋ���Ϊ��");
                    }
                    if (model.Type.IsNullOrEmpty()) {
                        return ApiResult.Failure("����ʧ�ܣ�type�����쳣");
                    }
                    var type = model.Type.GetTypeByName();
                    if (type == null) {
                        return ApiResult.Failure("����ʧ�ܣ�type�����쳣");
                    }
                    //add
                    model.Name = model.Name.Trim();
                    model.Type = type.FullName;
                    model.SortOrder = i;
                    i++;
                    var result = Resolve<IRelationService>().AddOrUpdate(model, model.Id > 0);
                }

                return ApiResult.Success("����ɹ�!");
            } catch (System.Exception exc) {
                return ApiResult.Failure($"����ʧ��: {exc.Message}");
            }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult Delete([FromQuery] long id) {
            //get
            var relation = Resolve<IRelationService>().GetSingle(a => a.Id == id);
            if (relation == null) {
                return ApiResult.Failure("ɾ��ʧ�ܣ��ύ�����쳣");
            }
            //relation count
            var relationCount = Resolve<IRelationService>()
                .Count(a => a.FatherId == id && a.Status == Status.Normal);
            if (relationCount > 0) {
                return ApiResult.Failure("ɾ��ʧ�ܣ������ӷ��಻��ɾ��");
            }
            //relation index count
            var relationIndexCount = Resolve<IRelationIndexService>()
                .Count(e => e.RelationId == id);
            if (relationIndexCount > 0) {
                return ApiResult.Failure("ɾ��ʧ�ܣ�������������,����ɾ��");
            }
            //delete
            var result = Resolve<IRelationService>().Delete(o => o.Id == relation.Id);
            if (!result) {
                ApiResult.Failure("ɾ��ʧ��");
            }

            return ApiResult.Success();
        }

        /// <summary>
        ///     ��ȡ���еļ����������ǩ
        /// </summary>
        [HttpGet]
        public ApiResult GetAllType() {
            var result = Resolve<IRelationService>().GetAllTypes();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     �������ͻ�ȡ���еķ���
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "����")]
        public ApiResult GetClass(string type, long userId) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<object>("�������Ʋ���Ϊ��");
            }
            var attrNameForTitle = GetAttrValueByName(type, "Name");
            var result = Resolve<IRelationService>().GetClass(type, userId).OrderBy(s => s.SortOrder).ToList();

            var rsObj = new { Title = attrNameForTitle, Datas = result };
            return ApiResult.Success(rsObj);
        }

        /// <summary>
        ///     �������ͻ�ȡ���еĸ������࣬��ǩ�������ݽṹ(/Admin/Table/List)�Ҳ���
        /// </summary>
        /// <param name="type"></param>
        [Display(Description = "��������")]
        [HttpGet]
        public ApiResult<List<RelationApiOutput>> GetFatherClass(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<RelationApiOutput>>("�������Ʋ���Ϊ��");
            }

            var result = Resolve<IRelationService>().GetClass(type).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     �������ͻ�ȡ���е��ֵ����ͣ�������ǩ����࣬��ǩ�������ݽṹ(/Admin/Table/List)�Ҳ���
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetFatherKeyValues(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("�������Ʋ���Ϊ��");
            }

            var result = Resolve<IRelationService>().GetFatherKeyValues(type).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     �������ͻ�ȡ���еı�ǩ,��ǩ�������ݽṹ(/Admin/Table/List)�Ҳ���
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult GetTag(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure("�������Ʋ���Ϊ��");
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
        ///     �������ͻ�ȡ���е��ֵ����ͣ�������ǩ�����,��ǩ�ͷ������Ϳ������ݽṹ(/Admin/Table/List)�Ҳ���
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetKeyValues(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("�������Ʋ���Ϊ��");
            }

            var result = Resolve<IRelationService>().GetKeyValues(type).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     �������ͻ�ȡ���е��ֵ����ͣ�������ǩ�����,��ǩ�ͷ������Ϳ������ݽṹ(/Admin/Table/List)�Ҳ���
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetClassTree(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("�������Ʋ���Ϊ��");
            }

            var result = Resolve<IRelationService>().GetKeyValues(type).ToList();
            result.ForEach(u => { u.Key = u.Name; });
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     �������ͻ�ȡ���е��ֵ����ͣ�������ǩ�����,��ǩ�ͷ������Ϳ������ݽṹ(/Admin/Table/List)�Ҳ���
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        public ApiResult<List<KeyValue>> GetTags(string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<List<KeyValue>>("�������Ʋ���Ϊ��");
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