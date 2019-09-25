using Alabo.App.Core.Common.ViewModels;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.App.Core.User.Domain.Services {

    public interface IUserQrCodeService : IService<UserDetail, long> {

        /// <summary>
        /// 二维码列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<ViewImagePage> QrCodePageList(object query);

        /// <summary>
        /// 二维码列表
        /// </summary>
        /// <returns></returns>
        PagedList<ViewImagePage> GetQrCodeList(PagedInputDto parameter);

        /// <summary>
        ///     生成会员二维码
        /// </summary>
        /// <param name="user">The 会员.</param>
        void CreateCode(Users.Entities.User user);

        /// <summary>
        /// 生成二维码任务
        /// </summary>
        void CreateCodeTask();

        /// <summary>
        /// 更新所有会员的用户等级
        /// </summary>
        void CreateAllUserQrCode();

        /// <summary>
        ///     生成二维码
        /// </summary>
        /// <param name="userId">会员Id</param>
        string QrCore(long userId);
    }
}