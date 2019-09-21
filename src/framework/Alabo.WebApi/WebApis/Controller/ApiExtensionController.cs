using System;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Maps;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Security;
using Alabo.UI;
using Alabo.Web.Mvc.Controllers;
using ApiResult = ZKCloud.Open.ApiBase.Models.ApiResult;

namespace Alabo.App.Core.Api.Controller {

    /// <summary>
    /// 扩展
    /// </summary>
    public abstract class ApiExtensionController : BaseController {

        /// <summary>
        /// 根据ServiceResult类型返回错误信息
        /// </summary>
        /// <param name="serviceResult"></param>
        /// <returns></returns>
        public ApiResult ToResult(ServiceResult serviceResult) {
            if (serviceResult.Succeeded) {
                return ApiResult.Success();
            } else {
                return ApiResult.Failure(serviceResult.ToString());
            }
        }

        /// <summary>
        /// 根据ServiceResult类型返回错误信息
        /// 返回对象通过serviceResult.ReturnObject过来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceResult"></param>
        /// <returns></returns>
        public ApiResult<T> ToResult<T>(ServiceResult serviceResult) {
            if (serviceResult.Succeeded) {
                var obj = (T)serviceResult.ReturnObject;
                return ApiResult.Success(obj);
            } else {
                return ApiResult.Failure<T>(serviceResult.ToString());
            }
        }

        /// <summary>
        /// 根据ServiceResult类型返回错误信息
        /// 返回对象通过serviceResult.ReturnObject过来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceResult"></param>
        /// <returns></returns>
        public ApiResult<T> ToResult<T>(Tuple<ServiceResult, T> serviceResult) {
            if (serviceResult.Item1.Succeeded) {
                return ApiResult.Success(serviceResult.Item2);
            } else {
                return ApiResult.Failure<T>(serviceResult.Item1.ToString());
            }
        }

        private User.Domain.Entities.User _user = null;

        /// <summary>
        /// 当前Api接口访问的登录用户，由前台传UserId过来判断
        /// </summary>
        public User.Domain.Entities.User User {
            get {
                if (_user == null && AutoModel.BasicUser != null) {
                    _user = Resolve<IAlaboUserService>().GetSingle(AutoModel.BasicUser.Id);
                }

                return _user;
            }
        }

        private AutoBaseModel _autoModel = null;

        /// <summary>
        /// 基础信息
        /// </summary>
        public AutoBaseModel AutoModel {
            get {
                if (_autoModel == null) {
                    _autoModel = new AutoBaseModel();
                    var filter = HttpContext.Request.Query["filter"].ConvertToInt(-1);
                    if (filter <= 0) {
                        filter = HttpContext.Request.Headers["zk-filter"].ToString().ConvertToInt(-1);
                    }
                    if (filter > 0) {
                        filter.IntToEnum(out FilterType filterType);
                        _autoModel.Filter = filterType;
                    }

                    var userId = HttpContext.Request.Headers["zk-user-id"].ToString().ConvertToLong(-1);
                    if (userId > 0) {
                        var user = Resolve<IAlaboUserService>().GetSingle(userId);
                        _autoModel.BasicUser = new BasicUser { Id = userId };
                        if (user != null) {
                            _autoModel.BasicUser = user.MapTo<BasicUser>();
                        }
                    }
                }

                return _autoModel;
            }
        }
    }
}