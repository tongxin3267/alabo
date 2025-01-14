﻿using Alabo.Cloud.People.UserRightss.Domain.Entities;
using Alabo.Cloud.People.UserRightss.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Helpers;
using Alabo.UI.Design.Widgets;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Cloud.People.UserRightss.UI.Widgets
{
    /// <summary>
    /// </summary>
    public class MerchantGridWidget : IWidget
    {
        /// <summary>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public object Get(string json)
        {
            var dic = json.ToObject<Dictionary<string, string>>();

            //前端传值需注意大小写 userId为必传项
            dic.TryGetValue("userId", out var userId);
            if (userId.IsNullOrEmpty()) {
                return null;
            }

            var userGradeList = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userRights = Ioc.Resolve<IUserRightsService>().GetList(u => u.UserId == u.UserId.ToInt64());
            var merchantList = new List<MerchantItem>();
            foreach (var item in userGradeList)
            {
                var userRight = userRights.FirstOrDefault(u => u.GradeId == item.Id);
                if (userRight == null)
                {
                    userRight = new UserRights();
                    userRight.TotalUseCount = 0;
                }

                var temp = new MerchantItem
                {
                    Count = userRight.TotalUseCount,
                    GradeName = item.Name
                };
                merchantList.Add(temp);
            }

            return merchantList;
        }

        /// <summary>
        /// </summary>
        public class MerchantItem
        {
            /// <summary>
            ///     开通商家等级名称
            /// </summary>
            public string GradeName { get; set; }

            /// <summary>
            ///     开通数量
            /// </summary>
            public long Count { get; set; }
        }
    }
}