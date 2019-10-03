using Alabo.App.Share.TaskExecutes.Domain.Services;
using Alabo.App.Share.TaskExecutes.Extensions;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace Alabo.App.Share.TaskExecutes
{
    /// <summary>
    ///     Class TaskExtensions.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     The serializer settings
        /// </summary>
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

        /// <summary>
        ///     To the repository string.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static string ToRepositoryString(this IModuleConfig config)
        {
            if (config == null) {
                throw new ArgumentNullException(nameof(config));
            }

            return JsonConvert.SerializeObject(config, _serializerSettings);
        }

        /// <summary>
        ///     Converts to 模块 configuration.
        /// </summary>
        /// <param name="value">The value.</param>
        public static T ConvertToModuleConfig<T>(this string value) where T : IModuleConfig
        {
            if (string.IsNullOrWhiteSpace(value)) {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value, _serializerSettings);
        }

        /// <summary>
        ///     Converts to 模块 configuration.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="id">Id标识</param>
        /// <param name="name">The name.</param>
        public static T ConvertToModuleConfig<T>(this string value, int id, string name) where T : IModuleConfig
        {
            if (string.IsNullOrWhiteSpace(value)) {
                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(value, _serializerSettings);
            result.Id = id;
            result.Name = name;
            return result;
        }

        /// <summary>
        ///     Converts to 模块 configuration.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The 类型.</param>
        public static object ConvertToModuleConfig(this string value, Type type)
        {
            if (string.IsNullOrWhiteSpace(value)) {
                return null;
            }

            return JsonConvert.DeserializeObject(value, type, _serializerSettings);
        }

        /// <summary>
        ///     添加s the tasks.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection AddTasks(this IServiceCollection services)
        {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }

            //TaskManager taskManager = new TaskManager();
            services.AddSingleton<TaskManager>(); //注册taskManager
            services.AddScoped<TaskContext>(); //注册TaskContext
            services.AddScoped<ITaskModuleConfigService, TaskModuleConfigService>(); //注册TaskConfigService
            services.AddScoped<ITaskQueueService, TaskQueueService>(); //注册TaskQueueService
            //注册TaskModuleConfigrationAccessor
            services.AddScoped<TaskModuleConfigrationAccessor>();
            //注册TaskModuleFactory
            services.AddScoped<TaskModuleFactory>();
            //注册默认TaskActuator
            services.AddTransient<ITaskActuator, TaskActuator>();
            return services;
        }
    }
}