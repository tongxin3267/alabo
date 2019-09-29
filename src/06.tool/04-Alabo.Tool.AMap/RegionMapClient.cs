using System;
using Alabo.Extensions;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;

namespace Alabo.Tool.AMap
{
    /// <summary>
    ///     高德地图区域查询
    ///     https://lbs.amap.com/api/webservice/guide/api/district
    ///     http://restapi.amap.com/v3/config/district?key=7a0bfcba0cbd3490c8cbf1331a5e68c2&keywords=&subdistrict=3&extensions
    ///     =base
    /// </summary>
    public class RegionMapClient : ClientBase
    {
        private static readonly Func<IConnector> _connectorCreator = () => new HttpClientConnector();

        private static readonly Func<IDataFormatter> _formmaterCreator = () => new JsonFormatter();

        private static readonly Uri _baseUri = new Uri("http://restapi.amap.com");

        /// <summary>
        ///     秘钥
        /// </summary>
        private readonly string key = "7a0bfcba0cbd3490c8cbf1331a5e68c2";

        /// <summary>
        ///     Initializes a new instance of the <see cref="MiniProgramClient" /> class.
        /// </summary>
        public RegionMapClient()
            : base(_baseUri)
        {
        }

        /// <summary>
        ///     通过高德地图获取所有的地址
        /// </summary>
        public AMapDistrict GetMapDistrict()
        {
            var baseUrl = $"/v3/config/district?key={key}&keywords=&subdistrict=3&extensions=base";

            var url = BuildQueryUri(baseUrl);
            var result = Connector.Get(url);

            var aMapDistrict = result.DeserializeJson<AMapDistrict>();
            return aMapDistrict;
        }
    }
}