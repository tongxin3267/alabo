using Alabo.App.Asset.Accounts.Domain.Entities;

namespace Alabo.App.Asset.Accounts.Dtos {

    public class AccountApiView : Account {
        public string MoneyName { get; set; }
    }
}