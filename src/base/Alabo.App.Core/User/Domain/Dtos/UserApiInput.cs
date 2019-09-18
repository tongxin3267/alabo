using System;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Core.User.Domain.Dtos {

    public class UserApiInput : ApiInputDto {
        public Guid GradeId { get; set; } = Guid.Empty;

        public long ServiceCenterId { get; set; } = 0;

        public string UserName { get; set; }
    }
}