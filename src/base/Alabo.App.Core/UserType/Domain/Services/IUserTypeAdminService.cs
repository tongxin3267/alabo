using Microsoft.AspNetCore.Http;
using System;
using Alabo.App.Core.UserType.Domain.Dtos;
using Alabo.App.Core.UserType.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    public interface IUserTypeAdminService : IService {

        Tuple<ServiceResult, PagedList<ViewUserType>> GetViewUserTypePageList(UserTypeInput userTypeInput);

        ServiceResult AddOrUpdate(ViewUserTypeEdit userType, HttpRequest httpRequest);

        ViewUserTypeEdit GetUserTypeEdit(Guid userTypeId, long id);
    }
}