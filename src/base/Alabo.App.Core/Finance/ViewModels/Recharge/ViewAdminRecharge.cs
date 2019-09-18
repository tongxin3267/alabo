using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.Recharge {

    /// <summary>
    ///     Class ViewAdminRecharge.
    /// </summary>
    [ClassProperty(Name = "充值管理", Icon = "fa fa-puzzle-piece")]
    public class ViewAdminRecharge : BaseViewModel {

        /// <summary>
        ///     交易用户
        /// </summary>
        public User.Domain.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets the 会员 grade.
        /// </summary>
        public UserGradeConfig UserGrade { get; set; }

        public Trade Trade { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 configuration.
        /// </summary>
        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     备注  包含失败原因
        /// </summary>
        public TradeRemark Remark { get; set; }

        /// <summary>
        ///     Gets or sets the prex recharge.
        /// </summary>
        public Trade PrexRecharge { get; set; }

        /// <summary>
        ///     Gets or sets the next recharge.
        /// </summary>
        public Trade NextRecharge { get; set; }
    }
}