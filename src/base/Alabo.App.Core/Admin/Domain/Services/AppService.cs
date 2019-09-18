using Castle.Core.Internal;
using System.Collections.Specialized;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Extensions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Helpers;

namespace Alabo.App.Core.Admin.Domain.Services {

    public class AppService : ServiceBase, IAppService {

        public AppService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     App名称集合
        /// </summary>
        public NameValueCollection AppNameCollection() {
            var nameValueCollection = new NameValueCollection
            {
                // Alabo.App.Core
                {"Common", "常用"},
                {"Customer", "自定义"},
                {"User", "用户"},
                {"Finance", "财务"},
                {"UserType", "用户类型"},

                {"ApiStore", "第三方接口"},
                {"Api", "Api"},
                {"Admin", "后台管理"},

                // Alabo.App.Shop
                {"Product", "商品"},
                {"Activitys", "活动"},
                {"Store", "店铺"},
                {"Order", "订单"},
                {"Category", "类目"},
                {"AfterSale", "售后"},

                // Alabo.App.CMS
                {"Articles", "文章"},
                {"Support", "工单"},

                // Alabo.App.Share
                {"Messages", "短信"},
                {"Share", "分润"},

                // 公用
                {"Reports", "报表"},
                {"Themes", "模板"},
                {"Tasks", "任务"},
                {"Markets", "应用市场"},
                // Alabo.App.Erp
                {"Stock", "库存"}
            };
            return nameValueCollection;
        }

        public ServiceResult VerifySafePassword() {
            if (HttpWeb.HttpContext.Request.Form.ContainsKey("PayPassword")) {
                var safePassword = HttpWeb.HttpContext.Request.Form["PayPassword"].ToString();
                if (safePassword.IsNullOrEmpty()) {
                    return ServiceResult.FailedWithMessage("请输入支付密码");
                }
                var loginUser = Resolve<IUserService>().GetUserDetail(HttpWeb.UserId);
                if (safePassword.ToMd5HashString() != loginUser.Detail.PayPassword) {
                    return ServiceResult.FailedWithMessage("操作失败：支付密码不正确");
                }
            }

            return ServiceResult.Success;
        }
    }
}