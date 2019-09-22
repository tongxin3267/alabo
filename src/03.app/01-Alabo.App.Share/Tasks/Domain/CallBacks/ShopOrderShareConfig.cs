using Alabo.Domains.Entities;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Tasks.Domain.CallBacks {

    /// <summary>
    /// 订单分润控制
    /// </summary>
    public class ShopOrderShareConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        /// true:  订单完成才产生分润
        /// false：订单支付成功以后产生分润
        /// </summary>
        public bool OrderSuccess { get; set; } = false;

        public void SetDefault() {
        }
    }
}