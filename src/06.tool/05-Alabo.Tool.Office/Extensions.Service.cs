﻿using Alabo.Tool.Office.Npoi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Alabo.Tool.Office
{
    /// <summary>
    ///     Office操作扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     注册Npoi Office操作服务
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddNpoi(this IServiceCollection services)
        {
            services.TryAddSingleton<IExportFactory, ExportFactory>();
            return services;
        }
    }
}