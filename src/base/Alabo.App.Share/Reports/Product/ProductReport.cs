using Quartz;
using System.Threading.Tasks;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Dependency;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Reports.Product {

    [ClassProperty(Name = "商品统计数据", Icon = "fa fa-building", Description = "商品统计数据")]
    public class ProductReport : IReportModel {

        /// <summary>
        /// 收藏量
        /// </summary>
        public long FavoriteCount { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public long TotalQuantity { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public long NoStock { get; set; }
    }

    public class ProductReportSchedule : JobBase {

        /// <summary>
        /// 数据统计服务
        /// </summary>
        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //获取底层访问容器对象

            //获取数据库操作对象
            var repositoryContext = scope.Resolve<IUserRepository>().RepositoryContext;
            if (repositoryContext == null) {
                return;
            }
            var productReport = new ProductReport();

            var sql = $"select SUM(FavoriteCount) from ZKShop_Product ";
            productReport.FavoriteCount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);
            sql = $"select sum(1) from ZKShop_Product ";
            productReport.TotalQuantity = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            sql = $"select sum(1) from ZKShop_Product where Stock<=0  ";
            productReport.NoStock = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            //添加统计数据到数据库
            scope.Resolve<IReportService>().AddOrUpdate<ProductReport>(productReport);
        }
    }
}