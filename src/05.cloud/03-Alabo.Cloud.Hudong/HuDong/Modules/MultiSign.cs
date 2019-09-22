using System.Collections.Generic;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.HuDong.Modules {

    [ClassProperty(Name = "多功能签到", Description = "多功能签到", PageType = ViewPageType.List)]
    public class MultiSign : IHuDong {

        public List<HudongAward> DefaultAwards() {
            var list = new List<HudongAward>();
            var item = new HudongAward {
                Grade = "一等奖",
                Type = HudongAwardType.None
            };
            list.Add(item);
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public HudongSetting Setting() {
            var setting = new HudongSetting();
            setting.RewardCount = 0;
            return setting;
        }
    }
}