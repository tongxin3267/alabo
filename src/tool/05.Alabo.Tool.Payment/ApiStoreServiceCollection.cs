using Microsoft.Extensions.DependencyInjection;
using System;
using ZKCloud.Open.ApiStore.Payment.Modules.JdPay;
using ZKCloud.Open.ApiStore.Payment.Modules.QPay;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay;

namespace Alabo.App.Core.ApiStore {

    public static class ApiStoreServiceCollection {

        public static IServiceCollection AddApiStoreService(this IServiceCollection services) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAlipay();
            services.AddWeChatPay();
            services.AddQPay();
            services.AddJdPay();
            return services;
        }
    }
}