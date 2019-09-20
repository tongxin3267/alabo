using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alabo.App.Core.Admin.Domain.Dtos;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Domain.Services;
using Alabo.App.Core.Themes.Dtos;
using Alabo.App.Core.Themes.Dtos.Service;
using Alabo.Domains.Entities;
using Alabo.Helpers;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Core.Themes.Clients {

    public class ThemeClient : ClientBase, IThemeClient {
        private static readonly string _getInitOpenSite = "/Api/DiyService/InitOpenSiteAsync";

        private static readonly string _getApps = "/Api/DiyService/GetApps";

        public async Task<List<ClientApp>> GetApps(string token, Guid proejctId) {
            var uri = BuildQueryUri(_getApps);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"projectId", proejctId.ToString()}
            };
            var result = await Connector.GetAsync(uri, parameters);
            var apiResult = DataFormatter.ToObject<ApiResult<List<ClientApp>>>(result);
            if (apiResult.Status == ResultStatus.Success) {
                return apiResult.Result;
            }
            return null;
        }

        public ThemePublish GetThemeAndThemePages(string token, ObjectId themeId, Guid projectId) {
            var uri = BuildQueryUri("Api/DiyService/GetTheme");
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"themeId", themeId.ToString()},
                {"projectId", projectId.ToString()}
            };

            var result = Connector.Get(uri, parameters);
            var apiResult = DataFormatter.ToObject<ApiResult<ThemePublish>>(result);
            if (apiResult.Status == ResultStatus.Success) {
                return apiResult.Result;
            }

            return null;
        }

        public ThemePublish InitDefaultTheme(ClientPageInput clientPageInput) {
            throw new NotImplementedException();
        }

        public async Task<ThemePublish> GetThemeAndThemePagesAsync(string token, ObjectId themeId, Guid projectId) {
            var uri = BuildQueryUri("Api/DiyService/GetTheme");
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"themeId", themeId.ToString()},
                {"projectId", projectId.ToString()}
            };

            var result = await Connector.GetAsync(uri, parameters);
            var apiResult = DataFormatter.ToObject<ApiResult<ThemePublish>>(result);
            if (apiResult.Status == ResultStatus.Success) {
                return apiResult.Result;
            }

            return null;
        }

        public async Task<ServiceResult> InitOpenSite(string token, Guid proejctId) {
            var uri = BuildQueryUri(_getInitOpenSite);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"projectId", proejctId.ToString()}
            };
            var result = await Connector.GetAsync(uri, parameters);
            var apiResult = DataFormatter.ToObject<ApiResult<List<Theme>>>(result);
            if (apiResult.Status != ResultStatus.Success) {
                return ServiceResult.FailedWithMessage(apiResult.Message);
            }
            var themeList = Ioc.Resolve<IThemeService>().GetList();
            var addList = new List<Theme>();

            foreach (var item in apiResult.Result) {
                var find = themeList.FirstOrDefault(r => r.Id == item.Id);
                if (find == null) {
                    addList.Add(item);
                }
            }

            if (addList.Count > 0) {
                Ioc.Resolve<IThemeService>().AddMany(addList);
            }

            return ServiceResult.Success;
        }
    }
}