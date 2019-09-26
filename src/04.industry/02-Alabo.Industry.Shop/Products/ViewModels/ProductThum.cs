using Alabo.App.Core.Common.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Product.ViewModels {

    /// <summary>
    ///     商品缩略图
    /// </summary>
    public class ProductThum : BaseViewModel {

        /// <summary>
        ///     原始图地址，保留绝对地址，或者原始URL,当商城的图片尺寸修改时候，可以重新生成图片,商品放大镜查看地址
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        ///     缩略图地址,根据后台设置规则生成，商城通用访问或显示地址,通用用于列表页或首页显示用格式：“OriginalUrl_宽X高.文件后缀”,参考淘宝
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        ///     商品详情页橱窗图，根据后台设置规则生成，购买详情页显示格式“OriginalUrl_宽X高.文件后缀”参考淘宝
        /// </summary>
        public string ShowCaseUrl { get; set; }

        public string GetPicUrl(string ImageUrl) {
            if (ImageUrl != null) {
                return ImageUrl;
            }

            return Resolve<IAutoConfigService>().GetValue<WebSiteConfig>().NoPic;
        }
    }
}