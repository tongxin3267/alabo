using Alabo.Domains.Query.Dto;

namespace Alabo.App.Core.Finance.Domain.Dtos.Account {

    public class UserAccountInput : PagedInputDto {
        public string UserName { get; set; }
    }
}