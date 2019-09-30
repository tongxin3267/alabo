using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZKCloud.Open.Share.Models;
using ITaskModule = Alabo.App.Share.TaskExecutes.ResultModel.ITaskModule;

namespace Alabo.App.Share.TaskExecutes.Domain.Services
{
    /// <summary>
    ///     Task模块配置服务
    /// </summary>
    public interface ITaskModuleConfigService : IService
    {
        /// <summary>
        ///     添加或更新一个分润配置
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="shareModule"></param>
        /// <param name="moduleType"></param>
        /// <param name="config"></param>
        Task<ServiceResult> AddOrUpdate(HttpContext context, ShareModule shareModule, Type moduleType,
            IModuleConfig config);

        /// <summary>
        ///     删除一个配置
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="id">主键ID</param>
        bool Delete(HttpContext context, long id);

        /// <summary>
        ///     获取制定id的taskmoduleconfig对象，用于管理后台编辑单个对象页面
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="id">主键ID</param>
        ShareModule GetSingle(HttpContext context, long id);

        /// <summary>
        ///     锁定或解锁分润维度
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="id">主键ID</param>
        /// <param name="configValue"></param>
        Task<ServiceResult> LockShareModuleAsync(HttpContext context, long id, object configValue);

        /// <summary>
        ///     获取所有的taskmoduleconfig对象，用于管理后台列表页面
        /// </summary>
        IList<ShareModule> GetList(HttpContext context);

        /// <summary>
        ///     获取属性
        /// </summary>
        /// <param name="moduleType"></param>
        TaskModuleAttribute GetModuleAttribute(Type moduleType);

        /// <summary>
        ///     获取所有的分润模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <typeparam name="TConfiguration"></typeparam>
        IEnumerable<TConfiguration> GetList<TModule, TConfiguration>()
            where TModule : ITaskModule
            where TConfiguration : IModuleConfig;
    }
}