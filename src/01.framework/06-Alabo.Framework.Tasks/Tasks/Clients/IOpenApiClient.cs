using System.Collections.Generic;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.ApiBase.Services;
using ZKCloud.Open.Share.Models;

namespace Alabo.App.Core.Tasks.Clients {

    public interface IOpenApiClient : IRestClient {

        /// <summary>
        ///     获取所有的分润模块
        /// </summary>
        /// <param name="token"></param>
        ApiResult<IList<ShareModule>> GetShareList(string token);
    }
}