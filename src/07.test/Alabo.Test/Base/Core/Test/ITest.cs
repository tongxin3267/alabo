using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Alabo.Dependency;

namespace Alabo.Test.Base.Core.Test
{
    public interface ITest : IScopeDependency
    {
    }

    public class ITestBase : ITest
    {
    }

    /// <summary>
    ///     服务扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddTestService<TService, TImplementation>(this IServiceCollection services)
            where TService : class, ITest
            where TImplementation : ITestBase, TService
        {
            services.TryAddScoped<TService>(t => t.GetService<TImplementation>());
            return services;
        }
    }
}