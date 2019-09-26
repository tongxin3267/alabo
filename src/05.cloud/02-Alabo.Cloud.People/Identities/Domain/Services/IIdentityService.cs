using Alabo.Cloud.People.Identities.Domain.Entities;
using Alabo.Data.People.Users.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Identities.Domain.Services
{
    public interface IIdentityService : IService<Identity, ObjectId>
    {
        /// <summary>
        ///     Adds the or update.
        ///     添加实名认证
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>ServiceResult.</returns>
        // ServiceResult AddOrUpdate(IdentityView input);

        /// <summary>
        ///     人脸认证
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