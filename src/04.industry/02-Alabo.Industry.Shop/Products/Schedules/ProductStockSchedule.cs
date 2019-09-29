using System.Threading.Tasks;
using Alabo.Dependency;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Schedules.Job;
using Quartz;

namespace Alabo.Industry.Shop.Products.Schedules
{
    /// <summary>
    ///     商品库存更改
    /// </summary>
    public class ProductStockSchedule : JobBase
    {
        protected override async Task Execute(IJobExecutionContext context, IScope scope)
        {
            var orderAdminService = scope.Resolve<IOrderAdminService>();

            // 更新库存，只有在更新货币类型和商城类型的时候，才更新数据库
            orderAdminService.ProductStockUpdate();
        }
    }
}