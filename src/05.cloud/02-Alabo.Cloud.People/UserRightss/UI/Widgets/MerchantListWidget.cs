using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.Framework.Core.WebUis.Design.Widgets;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Mapping;

namespace Alabo.App.Market.UI.Widgets {

    public class MerchantListWidget : IWidget {

        public object Get(string json) {
            var dic = json.ToObject<Dictionary<string, string>>();

            //前端传值需注意大小写 userId为必传项
            dic.TryGetValue("userId", out string userId);
            if (userId.IsNullOrEmpty()) {
                return null;
            }

            var userGradeList = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var resultList = new List<AutoMerchantItem>();
            foreach (var item in userGradeList) {
                //不显示18将 暂时写死 回头修改
                if (item.SortOrder > 1005) {
                    continue;
                }
                var userRight = Ioc.Resolve<IUserRightsService>().GetSingle(u => u.UserId == userId.ToInt64() && u.GradeId == item.Id);
                if (userRight == null) {
                    userRight = new UserRights();
                }
                var merchant = AutoMapping.SetValue<AutoMerchantItem>(userRight);
                merchant.GradeName = item.Name;
                merchant.UsableCount = userRight.TotalCount - userRight.TotalUseCount;
                resultList.Add(merchant);
            }

            return resultList;
        }

        public class AutoMerchantItem {
            public string GradeName { get; set; }

            /// <summary>
            ///     可用数量
            /// </summary>

            public long UsableCount { get; set; }

            /// <summary>
            ///     单个用户的总数量
            ///     原则上与等级权益配置数量相同
            ///     考虑到可以单独为用户设置的情况
            /// </summary>
            public long TotalCount { get; set; }
        }
    }
}