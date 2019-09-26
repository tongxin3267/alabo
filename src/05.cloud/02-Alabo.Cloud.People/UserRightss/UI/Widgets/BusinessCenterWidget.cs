using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Dtos;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.App.Market.UserRightss.Domain.CallBack;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis.Design.Widgets;
using Alabo.Exceptions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Users.Entities;

namespace Alabo.App.Market.UI.Widgets {

    /// <summary>
    /// 营销中心Widget
    /// </summary>
    public class BusinessCenterWidget : IWidget {

        public object Get(string json) {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var valDic = json.ToObject<Dictionary<string, long>>();
            var userId = valDic.GetValue("loginUserId");
            if (userId < 1) {
                throw new ValidException("会员Id没有传入进来");
            }
            var user = Ioc.Resolve<IUserService>().GetSingle(r => r.Id == userId);
            if (user == null) {
                throw new ValidException("对应ID会员不存在");
            }
            var rights = Ioc.Resolve<IUserRightsService>().GetView(true);//用管理员权限去获取
            var items = Ioc.Resolve<IUserRightsService>().GetList(x => x.UserId == userId).ToList();
            var sqlStoreRevenue = $@" SELECT Amount FROM Asset_Account WHERE MoneyTypeId = 'E97CCD1E-1478-49BD-BFC7-E73A5D699000' AND UserId = {userId} ";
            var storeRevenue = dbContext.ExecuteScalar(sqlStoreRevenue);
            var userGrades = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var sqlWeekCount = $@" SELECT COUNT(*) FROM User_User WHERE ParentId = {userId} AND CreateTime > '{DateTime.Now.Date.AddDays(-7).ToString("yyyyMMdd")}' ";
            var weekNewCount = dbContext.ExecuteScalar(sqlWeekCount).ToString().ToLong();
            //var rightList = Ioc.Resolve<IUserRightsService>().GetView(userId);

            var gradeList = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var currentGrade = gradeList != null ? gradeList.FirstOrDefault(x => x.Id == user.GradeId) : new UserGradeConfig { };

            var rs = new BusinessCenterView {
                IsBusinessCenter = currentGrade.Name.Contains("营销"),
                InviteCard = "8UHDFIE8W",
                CashAmount = storeRevenue.ToString().ToDecimal(),
                ServiceCount = items.Sum(x => x.TotalUseCount),
                WeekNewCount = weekNewCount,
                UserInfo = user,
                RightsInfo = rights != null && rights.Count > 0 ? rights.FirstOrDefault(x => x.GradeId == user.GradeId) : new UserRightsOutput { },
                Items = items.Select(x => new RightsItem {
                    RightInfo = x,
                    GradeName = rights.FirstOrDefault(y => y.GradeId == x.GradeId).Name,
                    ButtonText = $"开通{rights.FirstOrDefault(y => y.GradeId == x.GradeId).Intro}",
                    Icon = Ioc.Resolve<IApiService>().ApiImageUrl(userGrades.FirstOrDefault(s => s.Id == x.GradeId)?.Icon)
                }),
            };

            return rs;
        }
    }

    public class BusinessCenterView {

        /// <summary>
        /// 是否(准)营销中心
        /// </summary>
        public bool IsBusinessCenter { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCard { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头衔
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal CashAmount { get; set; }

        /// <summary>
        /// 已服务多少家
        /// </summary>
        public long ServiceCount { get; set; }

        /// <summary>
        /// 本周新加多少家
        /// </summary>
        public long WeekNewCount { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public User UserInfo { get; set; }

        /// <summary>
        /// 权益信息
        /// </summary>
        public UserRightsOutput RightsInfo { get; set; }

        /// <summary>
        /// 权益列表
        /// </summary>
        public IEnumerable<RightsItem> Items { get; set; }
    }

    public class RightsItem {
        public UserRights RightInfo { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// 按键文字
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }
    }
}