using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Enums;
using Alabo.Web.Mvc.Attributes;
using System.Collections.Generic;

namespace Alabo.App.Share.HuDong.Modules
{
    [ClassProperty(Name = "红包雨")]
    public class RedPack : IHuDong
    {
        public List<HudongAward> DefaultAwards()
        {
            var list = new List<HudongAward>();
            var item = new HudongAward
            {
                Grade = "一等奖",
                Type = HudongAwardType.None
            };
            list.Add(item);
            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public HudongSetting Setting()
        {
            var setting = new HudongSetting();
            setting.RewardCount = 0;
            return setting;
        }
    }
}