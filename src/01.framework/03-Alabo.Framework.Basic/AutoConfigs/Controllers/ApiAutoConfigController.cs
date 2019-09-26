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
        ///     获取AutoConfig,系统提供超过100以上的AutoConfig，可以在数据结构中查看：示范值：WebSiteConfig、MoneyTypeConfig、UserTypeConfig。访问/Admin/Table/List
        ///     查看所有自动配置
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取AutoConfig")]
        public ApiResult<List<JObject>> GetAutoConfigList([FromQuery] string parameter) {
            if (parameter.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(parameter);
            if (configType == null) {
                return ApiResult.Failure<List<JObject>>("输入参数不合法！");
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
            return ApiResult.Failure<List<JObject>>("输入参数不合法！或使用Api/GetAutoConfig接口");
        }

        /// <summary>
        ///     根据AutoConfig获取KeyValues，可以通过此来构建下拉菜单、picker.示范值：MoneyTypeConfig、UserTypeConfig
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "根据AutoConfig获取KeyValues")]
        public ApiResult<IList<KeyValue>> GetKeyValuesByAutoConfig([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<IList<KeyValue>>("类型不能为空");
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
        ///     获取AutoConfig,系统提供超过100以上的AutoConfig，可以在数据结构中查看：示范值：WebSiteConfig、MoneyTypeConfig、UserTypeConfig。访问/Admin/Table/List
        ///     查看所有自动配置
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取AutoConfig")]
        public ApiResult<object> GetAutoConfig([FromQuery] string parameter) {
            if (parameter.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(parameter);
            if (configType == null) {
                return ApiResult.Failure<object>("输入参数不合法！");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.Edit) {
                    var configValue = Resolve<IAutoConfigService>().GetValue(configType.FullName);
                    configValue = Resolve<IApiService>().InstanceToApiImageUrl(configValue);
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<object>("输入参数不合法！或使用Api/GetAutoConfigList接口");
        }

        /// <summary>
        ///     动态删除,删除单条记录，因为安全部分实体不支持，如需支持请在程序代码中配置
        /// </summary>
        [HttpDelete]
        [Display(Description = "删除单条记录")]
        [ApiAuth]
        public ApiResult Delete([FromBody]AutoConfigDelete entity) {
            if (entity == null) {
                return ApiResult.Failure("参数不能为空");
            }

            if (entity.Type.IsNullOrEmpty()) {
                return ApiResult.Failure("类型不能为空");
            }

            var type = entity.Type.GetTypeByName();
            if (type == null) {
                return ApiResult.Failure("类型不存在");
            }
            var attr = type.GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
            var configDescription = new ClassDescription(type.GetTypeInfo());

            if (attr != null && !string.IsNullOrEmpty(attr.Validator) && FilterSQLScript(attr.Validator) == 0) {
                var script = string.Format(attr.Validator, entity.Id);
                var isValidated = Resolve<IAutoConfigService>().Check(script);
                if (isValidated) {
                    return ApiResult.Failure("删除失败" + attr.ValidateMessage);
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
            Resolve<IAutoConfigService>().Log($"成功删除配置,IAutoConfig配置类型为:{type.GetTypeInfo().Name},配置ID:{entity.Id}");

            return ApiResult.Success("删除成功");
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
        /// 获取单条记录
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ApiResult<List<Link>> GetLinks([FromQuery] string type) {
            var result = Resolve<IAutoConfigService>().GetAllLinks();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取AutoConfig")]
        public ApiResult<object> Get([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(type);
            if (configType == null) {
                return ApiResult.Failure<object>("输入参数不合法！");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.Edit) {
                    var configValue = Resolve<IAutoConfigService>().GetValue(configType.FullName);
                    configValue = Resolve<IApiService>().InstanceToApiImageUrl(configValue);
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<object>("输入参数不合法！或使用Api/GetAutoConfigList接口");
        }

        /// <summary>
        ///     获取AutoConfig,系统提供超过100以上的AutoConfig，可以在数据结构中查看：示范值：WebSiteConfig、MoneyTypeConfig、UserTypeConfig。访问/Admin/Table/List
        ///     查看所有自动配置
        /// </summary>
        /// <param name="type">参数</param>
        [HttpGet]
        [Display(Description = "获取AutoConfig")]
        public ApiResult<List<JObject>> List([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }
            var configType = Resolve<IAutoConfigService>().GetTypeByName(type);
            if (configType == null) {
                return ApiResult.Failure<List<JObject>>("输入参数不合法！");
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
            return ApiResult.Failure<List<JObject>>("输入参数不合法！或使用Api/GetAutoConfig接口");
        }
    }
}