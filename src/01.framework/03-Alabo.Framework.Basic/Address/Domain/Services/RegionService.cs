using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alabo.App.Core.ApiStore.AMap.District;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Dtos;
using Alabo.Framework.Core.Enums.Enum;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Services {

    public class RegionService : ServiceBase<Region, ObjectId>, IRegionService {

        public RegionService(IUnitOfWork unitOfWork, IRepository<Region, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        public PagedList<Region> GetProvincePagedList(object query) {
            return GetPagedList(query, r => r.Level == RegionLevel.Province);
        }

        #region 数据初始

        /// <summary>
        ///     按照国际区域编码标准导入中国城市与区域
        /// </summary>
        public void Init() {
            if (!Exists()) {
                var regionIdList = new List<long>();
                var crileList = new List<string>();
                var client = new RegionMapClient();
                var mapDistrict = client.GetMapDistrict();
                if (mapDistrict != null && mapDistrict.Status == 1) {

                    #region // 添加国家

                    var country = mapDistrict.Districts[0];
                    var region = new Region(100000, 0) {
                        Country = Country.China,
                        Name = country.Name,
                        Center = country.Center,
                        Level = RegionLevel.Country,
                        FullName = country.Name
                    };
                    //  Add(region);

                    #endregion // 添加国家

                    foreach (var provinceItem in country.Districts) {
                        var list = new List<Region>();
                        if (provinceItem.AdCode.IsNullOrEmpty()) {
                            throw new ValidException("省份Id获取失败");
                        }

                        if (provinceItem.Level == "province") {
                            var provinceRegion = new Region(provinceItem.AdCode.ConvertToLong(0), 0) {
                                Country = Country.China,
                                Name = FormatRegionName(provinceItem.Name),
                                Center = provinceItem.Center,
                                Level = RegionLevel.Province,
                                FullName = provinceItem.Name
                            };
                            provinceRegion.ProvinceId = provinceRegion.RegionId;
                            list.Add(provinceRegion); // 添加省份

                            // 添加城市
                            foreach (var cityItem in provinceItem.Districts) {
                                if (cityItem.AdCode.IsNullOrEmpty()) {
                                    throw new ValidException("城市Id获取失败");
                                }

                                if (cityItem.Level == "city") {
                                    var cityRegion = new Region(cityItem.AdCode.ConvertToLong(0),
                                        provinceRegion.RegionId) {
                                        Country = Country.China,
                                        Name = FormatRegionName(cityItem.Name),
                                        Center = cityItem.Center,
                                        Level = RegionLevel.City,
                                        FullName = provinceItem.Name + " " + cityItem.Name,
                                        ProvinceId = provinceRegion.RegionId,
                                        ParentId = provinceRegion.RegionId
                                    };
                                    cityRegion.CityId = cityRegion.RegionId;
                                    list.Add(cityRegion);
                                    regionIdList.Add(cityRegion.RegionId);

                                    // 添加区域 区县
                                    int i = 60;
                                    foreach (var countyItem in cityItem.Districts) {
                                        if (countyItem.AdCode.IsNullOrEmpty()) {
                                            throw new ValidException("区域Id获取失败");
                                        }

                                        var countryId = countyItem.AdCode.ConvertToLong(0);
                                        if (regionIdList.Contains(countryId)) {
                                            countryId = (countryId.ToString() + i.ToString()).ConvertToLong(0);
                                            i++;
                                        }
                                        regionIdList.Add(countryId);
                                        var countyRegion = new Region(countryId,
                                            cityRegion.RegionId) {
                                            Country = Country.China,
                                            Name = FormatRegionName(countyItem.Name),
                                            Center = countyItem.Center,
                                            Level = RegionLevel.County,
                                            FullName = provinceItem.Name + " " + cityItem.Name + " " + countyItem.Name,
                                            ProvinceId = provinceRegion.RegionId,
                                            CityId = cityRegion.RegionId,
                                            ParentId = cityRegion.RegionId
                                        };
                                        list.Add(countyRegion);
                                    }
                                }
                            }
                        }

                        //批量添加
                        AddMany(list);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化区域ID表
        /// 执行时需Mogodb中region表为空
        /// </summary>

        private string FormatRegionName(string name) {
            if (name.IsNullOrEmpty()) {
                return string.Empty;
            }

            name = name.Replace("自治区", "")
                .Replace("特别行政区", "")
                .Replace("行政区", "")
                .Replace("自治县", "")
                .Replace("自治州", "")
                .Replace("直辖", "")
                .Replace("地区", "");
            return name;
        }

        #endregion 数据初始

        #region 初始化region表

        public void InitRegion() {
            var check = Resolve<IRegionService>().GetList();
            if (check.Count > 0) {
                return;
            }

            string fileDir = Environment.CurrentDirectory;
            var province = File.ReadAllText(fileDir + @"\wwwroot\static\js\region\province.js");
            var city = File.ReadAllText(fileDir + @"\wwwroot\static\js\region\city.js");
            var area = File.ReadAllText(fileDir + @"\wwwroot\static\js\region\area.js");

            var provinceList = province.Deserialize<RegionProvince>();
            var cityList = city.Deserialize<List<RegionProvince>>();
            var areaList = area.Deserialize<List<List<RegionProvince>>>();
            List<Region> regionList = new List<Region>();
            foreach (var item in provinceList) {
                var region = new Region {
                    RegionId = item.Value.ToInt64(),
                    Name = item.Label,
                    ProvinceId = item.Value.ToInt64(),
                    Level = RegionLevel.Province,
                };
                regionList.Add(region);
            }

            foreach (var temp in cityList) {
                foreach (var item in temp) {
                    var region = new Region {
                        RegionId = item.Value.ToInt64(),
                        Name = item.Label,
                        ProvinceId = item.Value.ToInt64(),
                        Level = RegionLevel.City,
                        ParentId = item.Value.Substring(0, 2).ToInt64()
                    };
                    regionList.Add(region);
                }
            }

            foreach (var item in areaList) {
                foreach (var temp in item) {
                    foreach (var tent in temp) {
                        var region = new Region {
                            RegionId = tent.Value.ToInt64(),

                            Name = tent.Label,
                            ProvinceId = tent.Value.ToInt64(),
                            Level = RegionLevel.County,
                            ParentId = tent.Value.Substring(0, 4).ToInt64()
                        };
                        regionList.Add(region);
                    }
                }
            }

            AddMany(regionList);
        }

        #endregion 初始化region表

        /// <summary>
        /// 根据枚举返回区域数据
        /// 返回JSON
        /// </summary>
        public string GetRegionData(RegionLevel level) {
            string result = string.Empty;
            string fileDir = Environment.CurrentDirectory;
            if (level == RegionLevel.Province) {
                var cacheName = "region_province";
                //判断缓存是否有值 ,如果有就直接读缓存
                if (ObjectCache.TryGet(cacheName, out string val)) {
                    result = val;
                } else {
                    result = File.ReadAllText(fileDir + @"\wwwroot\static\js\region\province.js");
                    //无缓存,写入缓存
                    ObjectCache.Set(cacheName, result);
                }
            }

            if (level == RegionLevel.City) {
                var cacheName = "region_city";
                //判断缓存是否有值 ,如果有就直接读缓存

                if (ObjectCache.TryGet(cacheName, out string val)) {
                    result = val;
                } else {
                    //判断缓存是否有值 ,如果有就直接读缓存
                    result = File.ReadAllText(fileDir + @"\wwwroot\static\js\region\city.js");
                    //无缓存,写入缓存
                    ObjectCache.Set(cacheName, result);
                }
            }

            if (level == RegionLevel.County) {
                var cacheName = "region_area";
                //判断缓存是否有值 ,如果有就直接读缓存
                if (ObjectCache.TryGet(cacheName, out string val)) {
                    result = val;
                } else {
                    //判断缓存是否有值 ,如果有就直接读缓存
                    result = File.ReadAllText(fileDir + @"\wwwroot\static\js\region\area.js");
                    //无缓存,写入缓存
                    ObjectCache.Set(cacheName, result);
                }
            }

            return result;
        }

        public string GetFullName(long areaId) {
            var region = GetSingle(r => r.RegionId == areaId);
            var res = string.Empty;
            if (region?.FullName != null) {
                return region?.FullName;
            } else {
                res = region?.Name;
                if (areaId.ToString().Length - 2 >= 2) {
                    areaId = Convert.ToInt64(areaId.ToString().Substring(0, areaId.ToString().Length - 2));
                    res = GetFullName(areaId) + res;
                } else {
                    return res;
                }
                return res;
            }
        }

        public IEnumerable<RegionTree> RegionTrees() {
            // 可是使用断点获取json数据
            var regionList = Resolve<IRegionService>().GetList();
            var regionJson = new List<RegionTree>();
            foreach (var item in regionList) {
                var region = new RegionTree {
                    Name = item.Name,
                    Id = item.RegionId,
                    ParentId = item.ParentId
                };
                if (regionJson.Where(x => x.Id == item.RegionId).Count() == 0) {
                    regionJson.Add(region);
                }
            }

            return regionJson;
        }

        #region 获取省份、城市、区域Id

        public long GetCountyId(long id) {
            var region = GetSingle(m => m.RegionId == id);
            if (region == null) {
                return 0;
            }

            if (region.Level == RegionLevel.County) {
                return region.RegionId;
            }

            return 0;
        }

        public long GetCityId(long id) {
            var region = GetSingle(m => m.RegionId == id);
            if (region == null) {
                return 0;
            }

            if (region.Level == RegionLevel.County) {
                if (region.CityId == 0L) {
                    var regStr = region.RegionId.ToString();
                    if (regStr.Length >= 4) {
                        return regStr.Substring(0, 4).ToInt64();
                    }
                }

                return region.CityId;
            }

            if (region.Level == RegionLevel.City) {
                return region.RegionId;
            }

            return 0;
        }

        public long GetProvinceId(long id) {
            var region = GetSingle(m => m.RegionId == id);
            if (region == null) {
                return 0;
            }

            if (region.Level == RegionLevel.County) {
                return region.ProvinceId;
            }

            if (region.Level == RegionLevel.City) {
                return region.ProvinceId;
            }

            if (region.Level == RegionLevel.Province) {
                return region.RegionId;
            }

            return 0;
        }

        #endregion 获取省份、城市、区域Id

        public string GetRegionNameById(long id) {
            var model = Resolve<IRegionService>().GetSingle(u => u.RegionId == id);
            if (model == null) {
                return string.Empty;
            }
            var regionName = model.Name;

            if (model.ParentId != 0) {
                var tempId = model.RegionId.ToString();
                //临时解决地址二级地址显示  待重构地址初始化
                if (tempId.Count() == 6) {
                    var regionParent = "";
                    for (int i = 0; i <= 3; i++) {
                        regionParent += tempId[i].ToString();
                    }
                    model.ParentId = regionParent.ToInt64();
                }

                var temp = Resolve<IRegionService>().GetSingle(u => u.RegionId == model.ParentId);
                if (temp != null) {
                    regionName = temp.Name + model.Name;
                    if (temp.ParentId != 0) {
                        var item = Resolve<IRegionService>().GetSingle(u => u.RegionId == temp.ParentId);
                        regionName = item.Name + temp.Name + model.Name;
                    }
                }
            }

            return regionName;
        }
    }
}