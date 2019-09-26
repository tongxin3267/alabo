using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.AutoConfigs.Domain.Dtos;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Themes.DiyModels.Links;
using Alabo.AutoConfigs.Entities;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Alabo.Core.Reflections.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Common.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/AutoConfig/[action]")]
    public class ApiAutoConfigController : ApiBaseController<AutoConfig, long> {

        public ApiAutoConfigController() : base() {
            BaseService = Resolve<IAutoConfigService>();
        }

        /// <summary>
        ///     ��ȡAutoConfig,ϵͳ�ṩ����100���ϵ�AutoConfig�����������ݽṹ�в鿴��ʾ��ֵ��WebSiteConfig��MoneyTypeConfig��UserTypeConfig������/Admin/Table/List
        ///     �鿴�����Զ�����
        /// </summary>
        /// <param name="parameter">����</param>
        [HttpGet]
        [Display(Description = "��ȡAutoConfig")]
        public ApiResult<List<JObject>> GetAutoConfigList([FromQuery] string parameter) {
            if (parameter.IsNullOrEmpty()) {
                ApiResult.Failure("���Ͳ���Ϊ��");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(parameter);
            if (configType == null) {
                return ApiResult.Failure<List<JObject>>("����������Ϸ���");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.List) {
                    var configValue = Resolve<IAutoConfigService>().GetList(configType.FullName);
                    //var configJson = Resolve<IApiService>().InstanceToApiImageUrl(configValue.ToJson());
                    //configValue = configJson.ToObject<List<JObject>>();
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<List<JObject>>("����������Ϸ�����ʹ��Api/GetAutoConfig�ӿ�");
        }

        /// <summary>
        ///     ����AutoConfig��ȡKeyValues������ͨ���������������˵���picker.ʾ��ֵ��MoneyTypeConfig��UserTypeConfig
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "����AutoConfig��ȡKeyValues")]
        public ApiResult<IList<KeyValue>> GetKeyValuesByAutoConfig([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<IList<KeyValue>>("���Ͳ���Ϊ��");
            }

            var configValue = Resolve<ITypeService>().GetAutoConfigDictionary(type);
            IList<KeyValue> keyValues = new List<KeyValue>();
            foreach (var item in configValue) {
                var keyValue = new KeyValue {
                    Name = item.Value.ToStr(),
                    Key = item.Key,
                    Value = item.Value.ToStr()
                };
                keyValues.Add(keyValue);
            }

            return ApiResult.Success(keyValues);
        }

        /// <summary>
        ///     ��ȡAutoConfig,ϵͳ�ṩ����100���ϵ�AutoConfig�����������ݽṹ�в鿴��ʾ��ֵ��WebSiteConfig��MoneyTypeConfig��UserTypeConfig������/Admin/Table/List
        ///     �鿴�����Զ�����
        /// </summary>
        /// <param name="parameter">����</param>
        [HttpGet]
        [Display(Description = "��ȡAutoConfig")]
        public ApiResult<object> GetAutoConfig([FromQuery] string parameter) {
            if (parameter.IsNullOrEmpty()) {
                ApiResult.Failure("���Ͳ���Ϊ��");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(parameter);
            if (configType == null) {
                return ApiResult.Failure<object>("����������Ϸ���");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.Edit) {
                    var configValue = Resolve<IAutoConfigService>().GetValue(configType.FullName);
                    configValue = Resolve<IApiService>().InstanceToApiImageUrl(configValue);
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<object>("����������Ϸ�����ʹ��Api/GetAutoConfigList�ӿ�");
        }

        /// <summary>
        ///     ��̬ɾ��,ɾ��������¼����Ϊ��ȫ����ʵ�岻֧�֣�����֧�����ڳ������������
        /// </summary>
        [HttpDelete]
        [Display(Description = "ɾ��������¼")]
        [ApiAuth]
        public ApiResult Delete([FromBody]AutoConfigDelete entity) {
            if (entity == null) {
                return ApiResult.Failure("��������Ϊ��");
            }

            if (entity.Type.IsNullOrEmpty()) {
                return ApiResult.Failure("���Ͳ���Ϊ��");
            }

            var type = entity.Type.GetTypeByName();
            if (type == null) {
                return ApiResult.Failure("���Ͳ�����");
            }
            var attr = type.GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
            var configDescription = new ClassDescription(type.GetTypeInfo());

            if (attr != null && !string.IsNullOrEmpty(attr.Validator) && FilterSQLScript(attr.Validator) == 0) {
                var script = string.Format(attr.Validator, entity.Id);
                var isValidated = Resolve<IAutoConfigService>().Check(script);
                if (isValidated) {
                    return ApiResult.Failure("ɾ��ʧ��" + attr.ValidateMessage);
                }
            }

            var list = Resolve<IAutoConfigService>().GetObjectList(type);
            object deleteItem = null;
            foreach (var item in list) {
                if (item.GetType().GetProperty("Id").GetValue(item).ToGuid() == entity.Id.ToGuid()) {
                    if (type.GetTypeInfo().BaseType.Name == "BaseGradeConfig") {
                        if (item.GetType().GetProperty("IsDefault").GetValue(item).ToBoolean()) {
                            continue;
                        }
                    }

                    deleteItem = item;
                    break;
                }
            }

            if (deleteItem != null) {
                list.Remove(deleteItem);
            }

            var config = Resolve<IAutoConfigService>().GetConfig(type.FullName);
            config.Value = JsonConvert.SerializeObject(list);
            Resolve<IAutoConfigService>().AddOrUpdate(config);
            Resolve<IAutoConfigService>().Log($"�ɹ�ɾ������,IAutoConfig��������Ϊ:{type.GetTypeInfo().Name},����ID:{entity.Id}");

            return ApiResult.Success("ɾ���ɹ�");
        }

        /// <summary>
        ///     Filters the SQL script.
        /// </summary>
        /// <param name="sSql">The s SQL.</param>
        [NonAction]
        private int FilterSQLScript(string sSql) {
            int srcLen, decLen = 0;
            sSql = sSql.ToLower().Trim();
            srcLen = sSql.Length;
            sSql = sSql.Replace("exec", "");
            sSql = sSql.Replace("delete", "");
            sSql = sSql.Replace("master", "");
            sSql = sSql.Replace("truncate", "");
            sSql = sSql.Replace("declare", "");
            sSql = sSql.Replace("create", "");
            sSql = sSql.Replace("xp_", "no");
            decLen = sSql.Length;
            if (srcLen == decLen) {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ������¼
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ApiResult<List<Link>> GetLinks([FromQuery] string type) {
            var result = Resolve<IAutoConfigService>().GetAllLinks();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ������¼
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ȡAutoConfig")]
        public ApiResult<object> Get([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                ApiResult.Failure("���Ͳ���Ϊ��");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(type);
            if (configType == null) {
                return ApiResult.Failure<object>("����������Ϸ���");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.Edit) {
                    var configValue = Resolve<IAutoConfigService>().GetValue(configType.FullName);
                    configValue = Resolve<IApiService>().InstanceToApiImageUrl(configValue);
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<object>("����������Ϸ�����ʹ��Api/GetAutoConfigList�ӿ�");
        }

        /// <summary>
        ///     ��ȡAutoConfig,ϵͳ�ṩ����100���ϵ�AutoConfig�����������ݽṹ�в鿴��ʾ��ֵ��WebSiteConfig��MoneyTypeConfig��UserTypeConfig������/Admin/Table/List
        ///     �鿴�����Զ�����
        /// </summary>
        /// <param name="type">����</param>
        [HttpGet]
        [Display(Description = "��ȡAutoConfig")]
        public ApiResult<List<JObject>> List([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                ApiResult.Failure("���Ͳ���Ϊ��");
            }
            var configType = Resolve<IAutoConfigService>().GetTypeByName(type);
            if (configType == null) {
                return ApiResult.Failure<List<JObject>>("����������Ϸ���");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.List) {
                    var configValue = Resolve<IAutoConfigService>().GetList(configType.FullName);
                    //var configJson = Resolve<IApiService>().InstanceToApiImageUrl(configValue.ToJson());
                    //configValue = configJson.ToObject<List<JObject>>();
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<List<JObject>>("����������Ϸ�����ʹ��Api/GetAutoConfig�ӿ�");
        }
    }
}