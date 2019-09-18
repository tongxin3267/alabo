using System;
using System.Collections.Generic;
using System.Text;
using ZKCloud.App.Core.Themes;
using ZKCloud.App.Shop.Order.Domain.PcDtos;

namespace ZKCloud.App.Offline.Order.AppUrl
{
    public class MerchantOrderAppUrl : BaseAppUrl, IAppUrl
    {
        public IList<AppUrlItem> AdminUrls()
        {
            var list = new List<AppUrlItem>
            {
                new AppUrlItem("订单管理", "Index",new List<AppWidget>(){
                    new AppWidget(string.Empty)
                }),
                new AppUrlItem("订单列表", "List",new List<AppWidget>(){
                    new AppWidget(typeof(PlatformApiOrderList),AppWidgetType.AutoTable)
                }),
                new AppUrlItem("订单详情", "Show",new List<AppWidget>(){
                    new AppWidget("zk-order-show")
                })

            };
            return list;
        }

        public IList<AppUrlItem> FrontUrls()
        {
            throw new NotImplementedException();
        }

        public IList<AppUrlItem> UserUrls()
        {
            throw new NotImplementedException();
        }
    }
}
