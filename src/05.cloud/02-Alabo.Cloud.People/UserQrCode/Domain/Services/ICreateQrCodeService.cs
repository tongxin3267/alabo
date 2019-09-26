using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.Cloud.People.UserQrCode.Domain.Services {

    public interface ICreateQrCodeService : IService {

        /// <summary>
        /// 生成二维码任务
        /// </summary>
        void CreateCodeTask();

        /// <summary>
        /// 更新所有会员的二维码
        /// </summary>
        void CreateAllUserQrCode();

        string QrCore(long userId);

        void CreateCode(User user);
    }
}