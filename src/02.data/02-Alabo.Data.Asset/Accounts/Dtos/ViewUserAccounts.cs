using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.Users.Entities;
using Alabo.Web.Mvc.ViewModel;
using System.Collections.Generic;

namespace Alabo.App.Asset.Accounts.Dtos
{
    /// <summary>
    ///     Class ViewUserAccounts.
    /// </summary>
    public class ViewUserAccounts : BaseViewModel
    {
        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Gets or sets the accounts.
        /// </summary>
        public IList<Account> Accounts { get; set; }
    }
}