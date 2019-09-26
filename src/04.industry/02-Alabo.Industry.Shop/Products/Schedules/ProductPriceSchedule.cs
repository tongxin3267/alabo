using System.Threading.Tasks;
using Alabo.Cache;
using Alabo.Dependency;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Schedules.Job;
using Quartz;

namespace Alabo.Industry.Shop.Products.Schedules {

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