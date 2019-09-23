using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Cache;
using Alabo.Datas.UnitOfWorks;
using Alabo.Dependency;
using Alabo.Schedules;
using Alabo.Schedules.Enum;
using Alabo.Schedules.Job;

namespace Alabo.App.Shop.Product.Schedules {

    /// <summary>
    ///     自动更新商品Sku价格
    /// </summary>
    /// <seealso cref="Alabo.Schedules.TaskScheduleBase" />
    public class ProductPriceSchedule : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var productSkuService = scope.Resolve<IProductSkuService>();

            // 更新Sku价格，只有在更新货币类型和商城类型的时候，才更新数据库
            var productPriceScheduleKey = "ProductPriceSchedule";
            if (scope.Resolve<IObjectCache>().TryGet(productPriceScheduleKey, out bool result)) {
                if (result) {
                    productSkuService.AutoUpdateSkuPrice();
                    scope.Resolve<IObjectCache>().Set(productPriceScheduleKey, false);
                }
            }
        }
    }
}