using System.Collections.Generic;

namespace Alabo.App.Core.ApiStore.AMap.District {

    /// <summary>
    ///     高德地图说明
    ///     https://lbs.amap.com/api/webservice/guide/api/district
    /// </summary>
    public class AMapDistrict {

        /// <summary>
        ///     返回结果状态值
        ///     值为0或1，0表示失败；1表示成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     返回状态说明，status为0时，info返回错误原因，否则返回“OK”。
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        ///     返回状态说明，10000代表正确，详情参阅info状态表
        /// </summary>
        public string InfoCode { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public IList<CountryDistrict> Districts { get; set; }
    }

    /// <summary>
    ///     国家
    /// </summary>
    public class CountryDistrict {

        /// <summary>
        ///     行政区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     行政区边界坐标点
        ///     在区县级别，有28个区县不能返回中心点
        ///     在乡镇/街道界别，有9262个乡镇/街道不能返回中心点
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        // province:省份（直辖市会在province和city显示）
        // city:市（直辖市会在province和city显示）
        // district:区县
        //street:街道
        /// </summary>
        public string Level { get; set; }

        public IList<ProvinceDistrict> Districts { get; set; }
    }

    /// <summary>
    ///     省份
    /// </summary>
    public class ProvinceDistrict {

        /// <summary>
        ///     区域编码
        /// </summary>
        public string AdCode { get; set; }

        /// <summary>
        ///     行政区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     行政区边界坐标点
        ///     在区县级别，有28个区县不能返回中心点
        ///     在乡镇/街道界别，有9262个乡镇/街道不能返回中心点
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        // province:省份（直辖市会在province和city显示）
        // city:市（直辖市会在province和city显示）
        // district:区县
        //street:街道
        /// </summary>
        public string Level { get; set; }

        public IList<CityDistrict> Districts { get; set; }
    }

    /// <summary>
    ///     城市
    /// </summary>
    public class CityDistrict {
        ///// <summary>
        ///// 城市编码
        ///// </summary>
        //public string citycode { get; set; }

        /// <summary>
        ///     区域编码
        /// </summary>
        public string AdCode { get; set; }

        /// <summary>
        ///     行政区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     行政区边界坐标点
        ///     在区县级别，有28个区县不能返回中心点
        ///     在乡镇/街道界别，有9262个乡镇/街道不能返回中心点
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        // province:省份（直辖市会在province和city显示）
        // city:市（直辖市会在province和city显示）
        // district:区县
        //street:街道
        /// </summary>
        public string Level { get; set; }

        public IList<DistrictDistrict> Districts { get; set; }
    }

    /// <summary>
    ///     区县
    /// </summary>
    public class DistrictDistrict {
        ///// <summary>
        ///// 城市编码
        ///// </summary>
        //public string citycode { get; set; }

        /// <summary>
        ///     区域编码
        /// </summary>
        public string AdCode { get; set; }

        /// <summary>
        ///     行政区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     行政区边界坐标点
        ///     在区县级别，有28个区县不能返回中心点
        ///     在乡镇/街道界别，有9262个乡镇/街道不能返回中心点
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        // province:省份（直辖市会在province和city显示）
        // city:市（直辖市会在province和city显示）
        // district:区县
        //street:街道
        /// </summary>
        public string Level { get; set; }

        public IList<CircleDistrict> Districts { get; set; }
    }

    /// <summary>
    ///     商圈
    /// </summary>
    public class CircleDistrict {
        ///// <summary>
        ///// 城市编码
        ///// </summary>
        //public string citycode { get; set; }

        /// <summary>
        ///     区域编码
        /// </summary>
        public string AdCode { get; set; }

        /// <summary>
        ///     行政区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     行政区边界坐标点
        ///     在区县级别，有28个区县不能返回中心点
        ///     在乡镇/街道界别，有9262个乡镇/街道不能返回中心点
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        // province:省份（直辖市会在province和city显示）
        // city:市（直辖市会在province和city显示）
        // district:区县
        //street:街道
        /// </summary>
        public string Level { get; set; }

        public IList<MapDistrict> Districts { get; set; }
    }

    public class MapDistrict {
        ///// <summary>
        ///// 城市编码
        ///// </summary>
        //public string citycode { get; set; }

        /// <summary>
        ///     区域编码
        /// </summary>
        public string AdCode { get; set; }

        /// <summary>
        ///     行政区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     行政区边界坐标点
        ///     在区县级别，有28个区县不能返回中心点
        ///     在乡镇/街道界别，有9262个乡镇/街道不能返回中心点
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        // province:省份（直辖市会在province和city显示）
        // city:市（直辖市会在province和city显示）
        // district:区县
        //street:街道
        /// </summary>
        public string Level { get; set; }
    }
}