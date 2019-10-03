using Alabo.Data.People.Circles.Domain.Entities;
using Alabo.Extensions;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Helpers;
using Alabo.Tool.Payment;
using Alabo.Tool.Payment.MiniProgram.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;

namespace Alabo.Data.People.Circles.Client
{
    /// <summary>
    ///     商圈数据导入
    ///     https://github.com/kzgame/china_regions/blob/master/region_dumps.json
    /// </summary>
    public class CircleMapClient : ApiStoreClient
    {
        private static readonly Func<IConnector> ConnectorCreator = () => new HttpClientConnector();

        private static readonly Func<IDataFormatter> FormmaterCreator = () => new JsonFormatter();

        private static readonly Uri BaseUri = new Uri("http://ui.5ug.com");

        /// <summary>
        ///     秘钥
        /// </summary>
        private string _key = "7a0bfcba0cbd3490c8cbf1331a5e68c2";

        /// <summary>
        ///     Initializes a new instance of the <see cref="MiniProgramClient" /> class.
        /// </summary>
        public CircleMapClient()
            : base(BaseUri, ConnectorCreator(), FormmaterCreator())
        {
        }

        public IList<CircleMapItem> GetCircleMap()
        {
            var baseUrl = "/static/json/circlemap.json";
            var url = BuildQueryUri(baseUrl);
            var result = Connector.Get(url);
            var aMapDistrict = result.DeserializeJson<List<CircleMapItem>>();
            return aMapDistrict;
        }

        /// <summary>
        ///     获取商圈数据
        /// </summary>
        public IList<Circle> GetCircleList()
        {
            try
            {
                var baseUrl = "/static/json/circle.json";
                var url = BuildQueryUri(baseUrl);
                var result = Connector.Get(url);
                var aMapDistrict = result.DeserializeJson<List<Circle>>();
                return aMapDistrict;
            }
            catch
            {
                return new List<Circle>();
            }
        }

        /// <summary>
        ///     转换成商圈格式
        ///     通过单元测试，保存json到服务上
        /// </summary>
        public IList<Circle> MapToCircle()
        {
            var provices = Ioc.Resolve<IRegionService>().GetList(r => r.Level == RegionLevel.Province);
            var list = new List<Circle>();
            var circelMaps = GetCircleMap();
            foreach (var proviceItem in provices)
            {
                var proviceMap = circelMaps.FirstOrDefault(r =>
                    r.Name.Contains(proviceItem.Name.Replace("省", "").Replace("市", "").Replace("区", "")));
                if (proviceMap == null) {
                    throw new SystemException();
                }

                // 城市
                var cities = Ioc.Resolve<IRegionService>()
                    .GetList(r => r.Level == RegionLevel.City && r.ParentId == proviceItem.RegionId);
                foreach (var cityItem in cities)
                {
                    var cityMap = proviceMap.Cities.FirstOrDefault(r =>
                        r.Name.Contains(cityItem.Name.Replace("省", "").Replace("市", "").Replace("区", "")));
                    if (cityMap == null) {
                        continue;
                    }

                    var coutries = Ioc.Resolve<IRegionService>().GetList(r =>
                        r.Level == RegionLevel.County && r.ParentId == cityItem.RegionId);
                    foreach (var countyItem in coutries)
                    {
                        var coutryItem = cityMap.Counties.FirstOrDefault(r =>
                            r.Name.Contains(countyItem.Name.Replace("省", "").Replace("市", "").Replace("区", "")
                                .Replace("县", "")));
                        if (coutryItem != null) {
                            foreach (var circleItem in coutryItem.Circles) {
                                if (circleItem.Name != "其他")
                                {
                                    var circle = new Circle
                                    {
                                        CityId = cityItem.RegionId,
                                        Name = circleItem.Name,
                                        CountyId = countyItem.RegionId,
                                        ProvinceId = proviceItem.RegionId,
                                        FullName = countyItem.FullName + circleItem.Name
                                    };
                                    list.Add(circle);
                                }
                            }
                        }
                    }
                }
            }

            // 通过单元测试，保存json到服务上
            var json = list.ToJson();
            return list;
        }
    }
}