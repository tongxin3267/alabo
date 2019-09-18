using System.Collections.Generic;
using Alabo.App.Open.HuDong.Domain.Entities;

namespace Alabo.App.Open.HuDong {

    /// <summary>
    /// 互动活动接口
    /// </summary>
    public interface IHuDong {

        /// <summary>
        /// 获取活动的默认奖励
        /// </summary>
        /// <returns></returns>
        List<HudongAward> DefaultAwards();

        /// <summary>
        /// 互动设置
        /// </summary>
        /// <returns></returns>
        HudongSetting Setting();
    }
}