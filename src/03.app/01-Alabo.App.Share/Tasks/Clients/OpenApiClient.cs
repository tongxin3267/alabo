using System;
using System.Collections.Generic;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.DiyClient;
using ZKCloud.Open.Share.Models;

namespace Alabo.App.Core.Tasks.Clients {

    public class OpenApiClient : RestClientBase, IOpenApiClient {
        private static readonly Func<IConnector> _connectorCreator = () => new HttpClientConnector();

        private static readonly Func<IDataFormatter> _formmaterCreator = () => new JsonFormatter();

        private static readonly string _getShareModuleList = "/api/share/getsharemodulelist";

        public OpenApiClient(Uri baseUri)
            : base(baseUri, _connectorCreator(), _formmaterCreator()) {
        }

        public OpenApiClient(string baseUrl)
            : this(new Uri(baseUrl)) {
        }

        public ApiResult<IList<ShareModule>> GetShareList(string token) {
            var uri = BuildQueryUri(_getShareModuleList);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token}
            };
            var data = DataFormatter.FromObject(new {
                token
            });
            var result = Connector.Post(uri, parameters, data);
            return DataFormatter.ToObject<ApiResult<IList<ShareModule>>>(result);
        }
    }
}