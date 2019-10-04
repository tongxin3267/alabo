using Alabo.Cache;
using Alabo.Dependency;
using Alabo.Helpers;
using Alabo.Tenants;
using Alabo.Tenants.Domain.Entities;
using Alabo.Web.Mvc.Exception;
using System;
using System.Threading.Tasks;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.ApiBase.Services;
using Qz = Quartz;
using Thread = System.Threading.Thread;

namespace Alabo.Schedules.Job {

    /// <summary>
    ///     Quartz作业
    /// </summary>
    public abstract class JobBase : IJob, Qz.IJob {

        /// <summary>
        ///     组名称
        /// </summary>
        private readonly string _groupName;

        /// <summary>
        ///     作业名称
        /// </summary>
        private readonly string _jobName;

        /// <summary>
        ///     触发器名称
        /// </summary>
        private readonly string _triggerName;

        private SecretKeyAuthentication _secretKeyAuthentication;

        /// <summary>
        ///     初始化
        /// </summary>
        protected JobBase() {
            _jobName = Id.Guid();
            _triggerName = Id.Guid();
            _groupName = Id.Guid();
        }

        public SecretKeyAuthentication Token {
            get {
                if (_secretKeyAuthentication == null) {
                    var serverAuthenticationManager = Ioc.Resolve<IServerAuthenticationManager>();
                    var result = serverAuthenticationManager.UpdateTokenAsync().GetAwaiter().GetResult();
                    return result.Result;
                }

                return _secretKeyAuthentication;
            }
        }

        /// <summary>
        ///     执行
        /// </summary>
        /// <param name="context">执行上下文</param>
        public async Task Execute(Qz.IJobExecutionContext context) {
            using (var scope = Ioc.BeginScope()) {
                try {
                    //get tenant and switch
                    var jobDataMap = context.JobDetail.JobDataMap;
                    var tenantName = jobDataMap.GetString(nameof(Tenant));
                    if (!string.IsNullOrWhiteSpace(tenantName)) {
                        TenantContext.SwitchDatabase(scope, tenantName);
                    }
                    //execute
                    await Execute(context, scope);
                } catch (Exception ex) {
                    var type = context.JobInstance;
                    ExceptionLogs.Write(ex, type.GetType().Name);
                }
            }
        }

        /// <summary>
        ///     获取作业名称
        /// </summary>
        public virtual string GetJobName() {
            return _jobName;
        }

        /// <summary>
        ///     获取触发器名称
        /// </summary>
        public virtual string GetTriggerName() {
            return _triggerName;
        }

        /// <summary>
        ///     获取组名称
        /// </summary>
        public virtual string GetGroupName() {
            return _groupName;
        }

        /// <summary>
        ///     获取Cron表达式
        /// </summary>
        public virtual string GetCron() {
            return null;
        }

        /// <summary>
        ///     获取重复执行次数，默认返回null，表示持续重复执行
        /// </summary>
        public virtual int? GetRepeatCount() {
            return null;
        }

        /// <summary>
        ///     获取开始执行时间
        /// </summary>
        public virtual DateTimeOffset? GetStartTime() {
            return null;
        }

        /// <summary>
        ///     获取结束执行时间
        /// </summary>
        public virtual DateTimeOffset? GetEndTime() {
            return null;
        }

        /// <summary>
        ///     获取重复执行间隔时间
        /// </summary>
        public virtual TimeSpan? GetInterval() {
            return null;
        }

        /// <summary>
        ///     获取重复执行间隔时间，单位：小时
        /// </summary>
        public virtual int? GetIntervalInHours() {
            return null;
        }

        /// <summary>
        ///     获取重复执行间隔时间，单位：分
        /// </summary>
        public virtual int? GetIntervalInMinutes() {
            return null;
        }

        /// <summary>
        ///     获取重复执行间隔时间，单位：秒
        /// </summary>
        public virtual int? GetIntervalInSeconds() {
            return null;
        }

        /// <summary>
        ///     第一次启动时，暂停时间
        /// </summary>
        /// <param name="context">执行上下文</param>
        /// <param name="scope"></param>
        /// <param name="tiemSpan">时间间隔</param>
        /// <returns></returns>
        protected void FirstWaiter(Qz.IJobExecutionContext context, IScope scope, TimeSpan tiemSpan) {
            var objectCache = scope.Resolve<IObjectCache>();
            var type = context.JobInstance;
            var cacheKey = $"Job_{type.GetType().Name}_FirstWaiter";
            objectCache.TryGet(cacheKey, out bool sendState);
            if (sendState == false) {
                Thread.Sleep(tiemSpan);
                scope.Resolve<IObjectCache>().Set(cacheKey, true);
            }
        }

        /// <summary>
        ///     执行
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="scope">作用域</param>
        protected abstract Task Execute(Qz.IJobExecutionContext context, IScope scope);
    }
}