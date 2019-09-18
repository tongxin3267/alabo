using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Dependency;
using Alabo.Schedules;
using Alabo.Schedules.Enum;
using Alabo.Schedules.Job;

namespace Alabo.App.Shop.Product.Schedules {

    /// <summary>
    /// 商品库存更改
    /// </summary>
    public class ProductStockSchedule : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var orderAdminService = scope.Resolve<IOrderAdminService>();

            // 更新库存，只有在更新货币类型和商城类型的时候，才更新数据库
            orderAdminService.ProductStockUpdate();
        }
    }
}