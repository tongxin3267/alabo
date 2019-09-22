using System.Collections.Generic;

namespace Alabo.App.Core.User.Domain.Dtos {

    public class VantAddress {

        /// <summary>
        /// 省份地址
        /// </summary>
        public Dictionary<string, string> province_list { get; set; }

        /// <summary>
        /// 城市地址
        /// </summary>
        public Dictionary<string, string> city_list { get; set; }

        /// <summary>
        /// 区县地址格式
        /// </summary>
        public Dictionary<string, string> county_list { get; set; }
    }
}