﻿using Alabo.Reflections;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.Autofac;
using Autofac;

namespace Alabo.Dependency {

    /// <summary>
    ///     AspectCore扩展
    /// </summary>
    public static partial class Extensions {

        /// <summary>
        ///     启用Aop
        /// </summary>
        public static void EnableAop(this ContainerBuilder builder) {
            builder.RegisterDynamicProxy(config => {
                config.NonAspectPredicates.Add(t =>
                    Alabo.Extensions.Extensions.SafeString(Reflection.GetTopBaseType(t.DeclaringType)) ==
                    "Microsoft.EntityFrameworkCore.DbContext");
                config.EnableParameterAspect();
            });
            builder.EnableAspectScoped();
        }

        /// <summary>
        ///     启用Aop作用域
        /// </summary>
        public static void EnableAspectScoped(this ContainerBuilder builder) {
            builder.AddScoped<IAspectScheduler, ScopeAspectScheduler>();
            builder.AddScoped<IAspectBuilderFactory, ScopeAspectBuilderFactory>();
            builder.AddScoped<IAspectContextFactory, ScopeAspectContextFactory>();
        }
    }
}