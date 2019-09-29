using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.UI;
using System;

namespace Alabo.Data.People.Users.Dtos
{
    public class UserInput : PagedInputDto
    {
        public string UserName { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid GradeId { get; set; } = Guid.Empty;

        public Status Status { get; set; }

        public long ServiceCenterId { get; set; } = 0;

        public long ParentId { get; set; } = 0;

        public FilterType FilterType { get; set; }
    }
}