using System.Collections.Generic;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.Account {

    /// <summary>
    ///     Class ViewUserAccounts.
    /// </summary>
    public class ViewUserAccounts : BaseViewModel {

        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public User.Domain.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets the accounts.
        /// </summary>
        public IList<Domain.Entities.Account> Accounts { get; set; }
    }
}