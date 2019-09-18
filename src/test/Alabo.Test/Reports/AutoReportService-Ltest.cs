using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZKCloud.App.Core.Reports.Domain.Dtos;
using ZKCloud.App.Core.Reports.Domain.Services;
using ZKCloud.Domains.Dtos;
using ZKCloud.Extensions;
using ZKCloud.Helpers;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Reports
{
    public class AutoReportService_Ltest : CoreTest
    {
        /// <summary>
        /// 指定同步网址，数据服务中心
        /// </summary>
        private readonly string _serviceHostUrl = "http://localhost:9018";

        /// <summary>
        /// 分布统计图,支持漏斗图、环图、饼状图
        /// </summary>
        [Fact]
        public void LT001()
        {
            //CountReportInput
            var sqlwhereWhere = new CountReportInput
            {
                EntityType="order",
            };
            var proList = Ioc.Resolve<IAutoReportService>().GetPieReport(sqlwhereWhere);// (x => x.Id > 700);

            var result = "";
            
        }

        [Fact]
        public void CreateRouterT000est() {
            var type = "Status".GetTypeByName();
            foreach (Enum item in Enum.GetValues(type)) {
                var value = item.GetDisplayName();
                var key = System.Convert.ToInt16(item);
            }
        }
    }
}
    