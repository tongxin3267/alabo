using System;
using System.Collections.Generic;
using System.Text;
using ZKCloud.App.Core.Themes;
using ZKCloud.App.Core.UserType.Modules.Merchants;
using ZKCloud.App.Offline.Product.Domain.CallBacks;
using ZKCloud.App.Offline.Product.Domain.Entities;

namespace ZKCloud.App.Offline.Product.AppUrl
{
    public class MerchantProductAppUrl : BaseAppUrl, IAppUrl
    {
        public IList<AppUrlItem> AdminUrls()
        {

            var list = new List<AppUrlItem>
            {
                new AppUrlItem("商品管理", "List",new List<AppWidget>(){ new AppWidget(typeof(MerchantProductList), AppWidgetType.AutoTable) }),
                new AppUrlItem("商品添加", "Edit",new List<AppWidget>(){ new AppWidget(typeof(MerchantProduct), "admin-product-edit") }),
                //ClassUrl(typeof(MerchantProductClassRelation)),
                //TagUrl(typeof(ProductTagRelation)),
                // new AppUrlItem("类目管理", "/Category/Index"),
                //AutoConfigUrl(typeof(MerchantsConfig)),
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
