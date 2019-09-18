using System.Collections.Generic;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Domain.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.HuDong.Modules {

    [ClassProperty(Name = "红包雨")]
    public class RedPack : IHuDong {

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