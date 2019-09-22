using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using System;
using System.Collections.Generic;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IGradeService : IService {
        UserGradeConfig DefaultUserGrade { get; }

        IList<UserGradeConfig> GetUserGradeList();

        UserGradeConfig GetGrade(Guid gradeId);

        /// <summary>
        ///     获取所有的用户类类型等级
        /// </summary>
        /// <param name="key"></param>
        List<BaseGradeConfig> GetGradeListByKey(string key);

        /// <summary>
        ///     根据等级Id获取用户等级
        /// </summary>
        /// <param name="guid"></param>
        List<BaseGradeConfig> GetGradeListByGuid(Guid guid);

        /// <summary>
        ///     根据用户类型Id和等级Id获取用户类型等级
        /// </summary>
        /// <param name="userTypeId">用户类型Id></param>
        /// <param name="gradeId">等级Id</param>
        BaseGradeConfig GetGradeByUserTypeIdAndGradeId(Guid userTypeId, Guid gradeId);

        /// <summary>
        ///     修改会员等级
        /// </summary>
        /// <param name="userGradeInput"></param>
        ServiceResult UpdateUserGrade(UserGradeInput userGradeInput);
    }
}