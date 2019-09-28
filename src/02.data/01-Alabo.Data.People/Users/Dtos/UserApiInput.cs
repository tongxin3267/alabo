using Alabo.Domains.Query.Dto;
using System;

namespace Alabo.Data.People.Users.Dtos
{
    public class UserApiInput : ApiInputDto
    {
        public Guid GradeId { get; set; } = Guid.Empty;

        public long ServiceCenterId { get; set; } = 0;

        public string UserName { get; set; }
    }
}