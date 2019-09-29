namespace Alabo.App.Asset.Accounts.Dtos
{
    public class AccountViewIn : ViewAccount
    {
        public long ChargeUserId { get; set; }

        public string AdminPayPassword { get; set; }
    }
}