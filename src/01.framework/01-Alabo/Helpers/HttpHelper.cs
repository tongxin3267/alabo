using Alabo.Domains.Entities;
using Alabo.Extensions;
using System;
using System.Collections.Generic;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Helpers {

    /// <summary>
    ///     Api请求
    /// </summary>
    public static class HttpHelper {

        /// <summary>
        ///     Api  接口请求，只针对Zkcloud中的Api接口有效，返回类型为ApiResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static Tuple<ServiceResult, T> Get<T>(string url, string para = null) where T : new() {
            string response;
            try {
                response = url.HttpGet();
            } catch {
                return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage($"网络请求失败,apiUrl:{url}"), default);
            }

            if (response.IsNullOrEmpty()) {
                return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage($"网络请求为空,apiUrl:{url}"), default);
            }

            try {
                var result = response.ToObject<ApiResult<T>>();
                if (result.Status != ResultStatus.Success) {
                    return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage(result.Message), default);
                }

                return new Tuple<ServiceResult, T>(ServiceResult.Success, result.Result);
            } catch {
                return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage($"请求成功，序列化出错,apiUrl:{url}"),
                    default);
            }
        }

        /// <summary>
        ///     Api  接口请求，只针对Zkcloud中的Api接口有效，返回类型为ApiResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static Tuple<ServiceResult, T> Post<T>(string url, object para,
            Dictionary<string, string> dicHeader = null) where T : new() {
            string response;
            try {
                response = url.Post(para.ToJsons(), dicHeader);
            } catch {
                return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage($"网络请求失败,apiUrl:{url}"), default);
            }

            if (response.IsNullOrEmpty()) {
                return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage($"网络请求为空,apiUrl:{url}"), default);
            }

            try {
                var result = response.ToObject<ApiResult<T>>();
                if (result.Status != ResultStatus.Success) {
                    return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage(result.Message), default);
                }

                return new Tuple<ServiceResult, T>(ServiceResult.Success, result.Result);
            } catch {
                return new Tuple<ServiceResult, T>(ServiceResult.FailedWithMessage($"请求成功，序列化出错,apiUrl:{url}"),
                    default);
            }
        }
    }
}