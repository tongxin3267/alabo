using MongoDB.Bson;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    public interface IIdentityService : IService<Identity, ObjectId> {
        /// <summary>
        ///     Adds the or update.
        ///     添加实名认证
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>ServiceResult.</returns>
       // ServiceResult AddOrUpdate(IdentityView input);

        /// <summary>
        ///人脸认证
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult FaceIdentity(Identity view);

        /// <summary>
        ///     是否实名认证
        /// </summary>
        /// <param name="userId">用户Id</param>
        bool IsIdentity(long userId);

        IdentityView GetView(string id);

        ServiceResult Identity(Identity view);
    }
}