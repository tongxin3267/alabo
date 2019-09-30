using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Reflections;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Services;
using ZKCloud.Open.Share.Models;
using ZKCloud.Open.Share.Services;
using ITaskModule = Alabo.App.Share.TaskExecutes.ResultModel.ITaskModule;

namespace Alabo.App.Share.TaskExecutes.Domain.Services
{
    public class TaskModuleConfigService : ServiceBase, ITaskModuleConfigService
    {
        private static readonly string _shareModuleCacheKey = "ShareModuleCacheKeyList";
        private RestClientConfiguration _restClientConfiugration;
        private IServerAuthenticationManager _serverAuthenticationManager;
        private IShareApiClient _shareApiClient;

        public TaskModuleConfigService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<ShareModule> GetList(HttpContext context)
        {
            ObjectCache.Remove(_shareModuleCacheKey);
            TaskModuleConfigBaseService(context);
            if (!ObjectCache.TryGet(_shareModuleCacheKey, out IList<ShareModule> shareModuleList)
                ) //添加统计 TODO 重构注释 分润维度获取
                //var result = _openApiClient.GetShareList(_serverAuthenticationManager.Token.Token);
                // shareModuleList = result.Result;
                if (shareModuleList != null)
                    ObjectCache.Set(_shareModuleCacheKey, shareModuleList);

            //var shareModuleReports = new ShareModuleReports {
            //    ConfigCount = shareModuleList.Count,
            //    EnableCount = shareModuleList.Count(r => r.IsEnable),
            //    ShareModuleList = shareModuleList.ToJson()
            //};
            //Resolve<IReportService>().AddOrUpdate(shareModuleReports);

            return shareModuleList;
        }

        public async Task<ServiceResult> AddOrUpdate(HttpContext context, ShareModule shareModule, Type moduleType,
            IModuleConfig config)
        {
            TaskModuleConfigBaseService(context);
            var result = ServiceResult.Success;
            if (config == null) return ServiceResult.FailedWithMessage("配置信息不存在");

            if (moduleType == null) return ServiceResult.FailedWithMessage("类型不能为空");

            if (string.IsNullOrWhiteSpace(shareModule.Name)) return ServiceResult.FailedWithMessage("请输入配置名称");

            var attribute = GetModuleAttribute(moduleType);
            CheckTaskModuleAttribute(attribute, config);
            //检查是否符合单一原则
            if (!attribute.IsSupportMultipleConfiguration)
            {
                var moduleList = GetList(context);
                var find = moduleList?.FirstOrDefault(e => e.ModuleId == attribute.Id);
                if (find != null && find.Id != shareModule.Id) return ServiceResult.FailedWithMessage("该维度已配置，不支持重复配置");
            }

            try
            {
                shareModule.ConfigValue = config.ToRepositoryString();

                //添加
                if (shareModule.Id <= 0)
                {
                    var resultToken =
                        await _shareApiClient.AddShareModule(_serverAuthenticationManager.Token.Token, shareModule);
                    if (!resultToken) return ServiceResult.FailedWithMessage("添加失败");

                    DeleteCache();
                }
                else
                {
                    var find = GetSingle(context, shareModule.Id);
                    var resultToken =
                        await _shareApiClient.UpdateShareModule(_serverAuthenticationManager.Token.Token, shareModule);
                    if (!resultToken) return ServiceResult.FailedWithMessage("更新失败");

                    DeleteCache();
                }
            }
            catch
            {
                return ServiceResult.FailedWithMessage("服务异常，请稍后再试");
            }

            return result;
        }

        public ShareModule GetSingle(HttpContext context, long id)
        {
            TaskModuleConfigBaseService(context);
            return GetList(context)?.FirstOrDefault(r => r.Id == id);
        }

        public bool Delete(HttpContext context, long id)
        {
            TaskModuleConfigBaseService(context);
            var resultToken = _shareApiClient.DeleteShareModule(_serverAuthenticationManager.Token.Token, id);
            return resultToken.Result.Result;
        }

        /// <summary>
        public TaskModuleAttribute GetModuleAttribute(Type moduleType)
        {
            var attributes = moduleType.GetTypeInfo().GetAttributes<TaskModuleAttribute>();
            if (attributes == null || attributes.Count() <= 0)
                throw new ArgumentException($"type {moduleType.Name} do not have module attribute.");

            return attributes.First();
        }

        /// <summary>
        ///     锁定
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="id">主键ID</param>
        /// <param name="configValue"></param>
        public async Task<ServiceResult> LockShareModuleAsync(HttpContext context, long id, object configValue)
        {
            TaskModuleConfigBaseService(context);
            try
            {
                var shareModule = GetSingle(context, id);
                if (shareModule == null) return ServiceResult.FailedWithMessage("配置未找到");

                shareModule.ConfigValue = configValue.ToJson();
                var resultToken =
                    await _shareApiClient.UpdateShareModule(_serverAuthenticationManager.Token.Token, shareModule);
                if (!resultToken) return ServiceResult.FailedWithMessage("锁定/解锁失败");

                DeleteCache();
            }
            catch
            {
                return ServiceResult.FailedWithMessage("服务异常，请稍后再试");
            }

            return ServiceResult.Success;
        }

        public IEnumerable<TConfiguration> GetList<TModule, TConfiguration>()
            where TModule : ITaskModule
            where TConfiguration : IModuleConfig
        {
            var attribute = GetModuleAttribute<TModule>();
            CheckTaskModuleAttribute(attribute, typeof(TConfiguration));
            // TODO 2019年9月24日 重构 维度获取
            //var shareModuleList = Resolve<IReportService>().GetValue<ShareModuleReports>();
            //var list = shareModuleList.ShareModuleList.DeserializeJson<List<ShareModule>>().ToList();

            //list = list.Where(e => e.ModuleId == attribute.Id && e.IsEnable).ToList();
            ////return list.Select(e => e.ConfigurationValue.ConvertToModuleConfig<TConfiguration>()).ToList();
            //var resutList = list.Select(e => e.ConfigValue.ConvertToModuleConfig<TConfiguration>((int)e.Id, e.Name))
            //    .ToList();
            //if (!attribute.IsSupportMultipleConfiguration) {
            //    var singleList = new List<TConfiguration>
            //    {
            //        resutList.FirstOrDefault()
            //    };
            //    resutList = singleList;
            //}

            //return resutList;
            return null;
        }

        private TaskModuleAttribute GetModuleAttribute<T>()
            where T : ITaskModule
        {
            var attributes = typeof(T).GetTypeInfo().GetAttributes<TaskModuleAttribute>();
            if (attributes == null || attributes.Count() <= 0)
                throw new ArgumentException($"type {typeof(T).Name} do not have module attribute.");

            return attributes.First();
        }

        private void CheckTaskModuleAttribute(TaskModuleAttribute attribute, IModuleConfig config)
        {
            if (attribute.ConfigurationType != config.GetType())
                throw new TypeAccessException(
                    $"config type {config.GetType().Name} not equals attribtue config type {attribute.ConfigurationType}");
        }

        private void CheckTaskModuleAttribute(TaskModuleAttribute attribute, Type configType)
        {
            if (attribute.ConfigurationType != configType)
                throw new TypeAccessException(
                    $"config type {configType.Name} not equals attribtue config type {attribute.ConfigurationType}");
        }

        private void DeleteCache()
        {
            ObjectCache.Remove(_shareModuleCacheKey);
        }

        private void TaskModuleConfigBaseService(HttpContext context)
        {
            _serverAuthenticationManager = context.RequestServices.GetService<IServerAuthenticationManager>();
            _restClientConfiugration = context.RequestServices.GetService<RestClientConfiguration>();
            _shareApiClient = new ShareApiClient(_restClientConfiugration.OpenApiUrl);
        }
    }
}