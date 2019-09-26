using Alabo.Domains.Query.Dto;

namespace Alabo.App.Asset.Accounts.Dtos
{
    public class UserAccountInput : PagedInputDto
    {
        public string UserName { get; set; }
    }
}