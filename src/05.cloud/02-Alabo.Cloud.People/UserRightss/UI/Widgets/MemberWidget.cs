using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI.Widgets;
using System;
using System.Linq;
using Alabo.Users.Entities;

namespace Alabo.App.Market.UI.Widgets {

    public class MemberWidget : IWidget {

        public object Get(string json) {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var userMap = json.ToObject<UserMap>();
            if (userMap == null) {
                throw new ValidException("会员Id没有传入进来");
            }
            var userId = userMap.UserId;
            if (userId <= 0) {
                throw new ValidException("会员Id没有传入进来");
            }

            var user = Ioc.Resolve<IUserService>().GetSingle(r => r.Id == userId);
            if (user == null) {
                throw new ValidException("对应ID会员不存在");
            }

            var sqlNewOrder = $@" SELECT COUNT(*) FROM ZKShop_Order WHERE CreateTime > '{DateTime.Now.Date.ToString("yyyyMMdd")}' AND OrderType = 1 AND UserId = {userId} ";
            var dayOrderCount = dbContext.ExecuteScalar(sqlNewOrder).ToString().ToLong();
            var storeRevenue = $@" SELECT Amount FROM Finance_Account WHERE MoneyTypeId = 'E97CCD1E-1478-49BD-BFC7-E73A5D699000' AND UserId = {userId} ";
            var revenudList = dbContext.ExecuteScalar(storeRevenue);
            var sqlFansCount = $@" SELECT COUNT(*) FROM User_User WHERE ParentId = {userId} ";
            var fansCount = dbContext.ExecuteScalar(sqlFansCount);

            var collectionSql = $@" SELECT COUNT(*) FROM User_User WHERE ParentId = {userId} ";

            var collectionCount = dbContext.ExecuteScalar(sqlFansCount);
            var gradeList = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var currentGrade = gradeList != null ? gradeList.FirstOrDefault(x => x.Id == user.GradeId) : new UserGradeConfig { };
            var nextGrade = currentGrade != null ? gradeList.FirstOrDefault(x => x.SortOrder == currentGrade.SortOrder + 1 && !x.Name.Contains("营销中心")) : new UserGradeConfig { };
            var userDetail = Ioc.Resolve<IUserDetailService>().GetSingle(userId);
            //    var favorite = Ioc.Resolve<IFavoriteService>().GetFavoriteCountByUserId(userId);
            var isNotAdmin = !Ioc.Resolve<IUserService>().IsAdmin(userId);
            if (userDetail.Avator.IsNullOrEmpty()) {
                userDetail.Avator = @"/wwwroot/static/images/avator/man_64.png";
            }

            var rs = new MemberView {
                Avator = Ioc.Resolve<IApiService>().ApiImageUrl(userDetail?.Avator),
                UserName = user?.UserName,
                VersionName = currentGrade?.Name,
                StoreRevenue = revenudList.ToString().ToDecimal(),
                FansCount = fansCount.ToString().ToLong(),
                QrCode = Ioc.Resolve<IUserQrCodeService>().QrCore(userId),
                TodayOrderCount = dayOrderCount,
                // 不是Admin才有升级
                UpgradeButton = isNotAdmin && nextGrade != null && !string.IsNullOrEmpty(nextGrade?.Name) ? $"升级{nextGrade?.Name}" : "",
                UpgradeGradeId = isNotAdmin && nextGrade != null && (nextGrade?.Id ?? Guid.Empty) != Guid.Empty ? (nextGrade?.Id ?? Guid.Empty) : Guid.Empty,
                // Favorite = favorite
            };

            return rs;
        }
    }

    public class MemberView {
        public string Avator { get; set; }
        public string UpgradeButton { get; set; }
        public string UserName { get; set; }
        public string VersionName { get; set; }
        public decimal StoreRevenue { get; set; }
        public long TodayOrderCount { get; set; }
        public long FansCount { get; set; }
        public string QrCode { get; set; }
        public Guid UpgradeGradeId { get; internal set; }
        public long Favorite { get; set; }
    }
}