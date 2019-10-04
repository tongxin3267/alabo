using Alabo.Cache;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Dynamics;

namespace Alabo.Web.Mvc.Controllers {

    /// <summary>
    ///     Class BaseController.
    /// </summary>
    public abstract class BaseController : Controller {

        /// <summary>
        ///     The default message 视图 name
        /// </summary>
        private static readonly string DefaultMessageViewName = "/Admin/Shared/Message.cshtml";

        //如果共用，可以放这里

        /// <summary>
        ///     Gets the name of the message 视图.
        /// </summary>
        protected virtual string MessageViewName => DefaultMessageViewName;

        /// <summary>
        ///     查询参数，可以通过动态 查询
        ///     包括分页等参数
        ///     PageIndex=5
        /// </summary>
        protected object Query => QueryDictionary().ToJson();

        /// <summary>
        ///     缓存
        /// </summary>
        public IObjectCache ObjectCache => Ioc.Resolve<IObjectCache>();

        /// <summary>
        ///     通用表单验证
        /// </summary>
        /// <param name="view">The 视图.</param>
        /// <param name="errorMessage">The error message.</param>
        protected static bool IsFormValid(ref dynamic view, out string errorMessage) {
            Type inputType = view.GetType();
            var propertyInfos = inputType.GetPropertiesFromCache();
            foreach (var item in propertyInfos) {
                try {
                    var property =
                        propertyInfos.FirstOrDefault(r => r.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                    // 所有的特性
                    var attributes = property.GetAttributes();
                    var value = property?.GetValue(view);
                    var stringValue = string.Empty;
                    try {
                        stringValue = value.ToString();
                    } catch (System.Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                    var displayAttribute = attributes.FirstOrDefault(r => r.GetType() == typeof(DisplayAttribute));
                    var displayName = property.Name;
                    if (displayAttribute != null) {
                        displayName = ((DisplayAttribute)displayAttribute).Name;
                    }

                    foreach (var attribute in attributes) {
                        // 验证必填
                        if (attribute.GetType() == typeof(RequiredAttribute)) {
                            if (stringValue.IsNullOrEmpty()) {
                                errorMessage = displayName + "不能为空";
                                return false;
                            }
                        }

                        // 验证长度大小
                        if (attribute.GetType() == typeof(StringLengthAttribute)) {
                            if (stringValue.Length > ((StringLengthAttribute)attribute).MaximumLength) {
                                errorMessage = displayName +
                                               $"长度不能超过{((StringLengthAttribute)attribute).MaximumLength}字符";
                                return false;
                            }
                        }

                        // 验证远程信息
                        if (attribute.GetType() == typeof(FieldAttribute)) {
                            var fieldAttribute = (FieldAttribute)attribute;
                            if (fieldAttribute.ValidType == ValidType.UserName ||
                                fieldAttribute.ValidType == ValidType.ParentUserName) {
                                // 推荐人为空的时候，不验证
                                if (fieldAttribute.ValidType == ValidType.ParentUserName &&
                                    stringValue.IsNullOrEmpty()) {
                                    break;
                                }

                                // 用户名为空的时候
                                if (fieldAttribute.ValidType == ValidType.UserName) {
                                    if (stringValue.IsNullOrEmpty()) {
                                        errorMessage = "用户名不能为空";
                                        return false;
                                    }
                                }

                                var user = EntityDynamicService.GetSingleUser(value.ToString());
                                var dynamicUser = user;
                                if (dynamicUser == null) {
                                    errorMessage = $"用户名{value}不存在，请重新输入";
                                    return false;
                                }

                                if (dynamicUser.Status != Status.Normal) {
                                    errorMessage = $"用户{value}，状态不正常，已被冻结或删除";
                                    return false;
                                }

                                // 推荐人为空的时候，不验证
                                if (fieldAttribute.ValidType == ValidType.ParentUserName && stringValue.IsNullOrEmpty()) {
                                    try {
                                        view.ParentUserId = dynamicUser.Id;
                                    } catch {
                                    }

                                    try {
                                        view.ParentId = dynamicUser.Id;
                                    } catch {
                                    }
                                }

                                // 用户Id动态转换
                                if (fieldAttribute.ValidType == ValidType.UserName) {
                                    try {
                                        view.UserId = dynamicUser.Id;
                                    } catch {
                                    }
                                }
                            }
                        }
                    }
                } catch (AggregateException filedException) {
                    Console.WriteLine(filedException);
                }
            }

            var error = string.Empty;
            errorMessage = error;
            return true;
        }

        /// <summary>
        ///     Dynamics the service.
        ///     获取动态服务
        /// </summary>
        /// <param name="serviceString">The service string.服务名称:比如IServicer</param>
        /// <param name="methodString">The method string. 方法名称：比如GetSingle</param>
        /// <param name="paramaters">The paramaters.</param>
        [NonAction]
        protected static Tuple<ServiceResult, object> DynamicService(string serviceString, string methodString,
            params object[] paramaters) {
            return Dynamics.DynamicService.ResolveMethod(serviceString, methodString, paramaters);
        }

        /// <summary>
        ///     Dynamics the 服务.
        /// </summary>
        /// <param name="serviceString">The 服务 string.</param>
        /// <param name="methodString">The method string.</param>
        /// <param name="runInUrl">if set to <c>true</c> [run in URL].</param>
        /// <param name="paramaters">The paramaters.</param>
        [NonAction]
        protected static Tuple<ServiceResult, object> DynamicService(string serviceString, string methodString,
            bool runInUrl, params object[] paramaters) {
            return Dynamics.DynamicService.ResolveMethod(serviceString, methodString, runInUrl, paramaters);
        }

        /// <summary>
        ///     Resolves this instance.
        /// </summary>
        [NonAction]
        protected T Resolve<T>()
            where T : IService {
            return Ioc.Resolve<T>();
        }

        /// <summary>
        ///     获取Url中的值
        ///     把空值过滤掉
        /// </summary>
        [NonAction]
        protected Dictionary<string, string> QueryDictionary() {
            //移除type 不然很容出现 类型为enum
            return HttpContext.ToDictionary().RemoveKey("type");
        }
    }
}