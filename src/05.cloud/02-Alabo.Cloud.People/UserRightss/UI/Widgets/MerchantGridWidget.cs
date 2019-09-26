using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.Core.WebUis.Design.Widgets;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Market.UI.Widgets {

    /// <summary>
    ///
    /// </summary>
    public class MerchantGridWidget : IWidget {

        /// <summary>
        ///
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public object Get(string json) {
            var dic = json.ToObject<Dictionary<string, string>>();

            //前端传值需注意大小写 userId为必传项
            dic.TryGetValue("userId", out string userId);
            if (userId.IsNullOrEmpty()) {
                return null;
            }

            var userGradeList = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userRights = Ioc.Resolve<IUserRightsService>().GetList(u => u.UserId == u.UserId.ToInt64());
            var merchantList = new List<MerchantItem>();
            foreach (var item in userGradeList) {
                var userRight = userRights.FirstOrDefault(u => u.GradeId == item.Id);
                if (userRight == null) {
                    userRight = new UserRights();
                    userRight.TotalUseCount = 0;
                }
                var temp = new MerchantItem {
                    Count = userRight.TotalUseCount,
                    GradeName = item.Name
                };
                merchantList.Add(temp);
            }

            return merchantList;
        }

        /// <summary>
        ///
        /// </summary>
        public class MerchantItem {

            /// <summary>
            /// 开通商家等级名称
            /// </summary>
            public string GradeName { get; set; }

            /// <summary>
            /// 开通数量
            /// </summary>
            public long Count { get; set; }
        }
    }
}