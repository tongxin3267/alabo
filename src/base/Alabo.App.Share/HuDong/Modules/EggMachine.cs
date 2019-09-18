using System.Collections.Generic;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.HuDong.Modules {

    [ClassProperty(Name = "超级扭蛋机", Description = "超级扭蛋机", PageType = ViewPageType.List)]
    public class EggMachine : IHuDong {

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