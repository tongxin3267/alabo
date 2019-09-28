using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using System;

namespace Alabo.Framework.Core.WebUis.Services
{
    /// <summary>
    ///     通用表单服务
    /// </summary>
    public interface IAdminTableService : IService
    {
        /// <summary>
        ///     导出表格
        /// </summary>
        /// <param name="key">导出表格key，通过key从缓存中读取用户的设置</param>
        /// <param name="service">获取数据的服务</param>
        /// <param name="method">获取数据的方法</param>
        /// <param name="query">Url参数，用户通过界面选择传递</param>
        Tuple<ServiceResult, string, string> ToExcel(string key, string service, string method, object query);

        Tuple<ServiceResult, string, string> ToExcel(string type, object query);

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <returns></returns>
        Tuple<ServiceResult, string, string> ToExcel(Type type, dynamic data);
    }
}