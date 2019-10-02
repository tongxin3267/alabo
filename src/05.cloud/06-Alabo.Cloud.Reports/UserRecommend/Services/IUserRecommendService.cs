using Alabo.Data.People.Users.ViewModels.Admin;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Cloud.Reports.UserRecommend.Services {

    public interface IUserRecommendService : IService {

        /// <summary>
        ///     获取直推会员、间推、团队的等级分布
        /// </summary>
        /// <param name="query"></param>
        PagedList<UserGradeInfoView> GetUserGradeInfoPageList(object query);
    }
}