using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZKCloud.Open;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.Message.Models;

namespace Alabo.Tables.Client
{
    public sealed class OpenMessageClient : RestClientBase, IOpenMessageClient
    {
        private static readonly Func<IConnector> _connectorCreator = () => new HttpClientConnector();

        private static readonly Func<IDataFormatter> _formmaterCreator = () => new JsonFormatter();

        private static readonly string _sendRawUrl = "/api/message/sendraw";

        private static readonly string _sendTemplateUrl = "/api/message/sendtemplate";

        private static readonly string _getSingleTemplateByCode = "/api/open/getsingletemplatebycode";

        private static readonly string _sendPhoneVerifiyCode = "/api/message/sendphoneverifiycode";

        private static readonly string _checkPhoneVerifiyCode = "/api/message/checkphoneverifiycode";

        public OpenMessageClient(Uri baseUri)
            : base(baseUri, _connectorCreator(), _formmaterCreator())
        {
        }

        public OpenMessageClient(string baseUrl)
            : this(new Uri(baseUrl))
        {
        }

        public ApiResult SendRaw(string token, string mobile, string message, string ipAddress)
        {
            var url = BuildQueryUri(_sendRawUrl);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token}
            };
            var data = DataFormatter.FromObject(new { mobile, message, IpAddress = ipAddress });
            var result = Connector.Post(url, parameters, data);
            return DataFormatter.ToObject<ApiResult<SecretKeyAuthentication>>(result);
        }

        public async Task<ApiResult> SendRawAsync(string token, string mobile, string message, string ipAddress)
        {
            var url = BuildQueryUri(_sendRawUrl);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"ipaddress", ipAddress}
            };
            var data = await DataFormatter.FromObjectAsync(new { mobile, message, IpAddress = ipAddress });
            var result = await Connector.PostAsync(url, parameters, data);
            return await DataFormatter.ToObjectAsync<ApiResult<SecretKeyAuthentication>>(result);
        }

        public ApiResult SendTemplate(string token, string mobile, long templateId, string userIpAddress,
            IDictionary<string, string> parameter = null)
        {
            var url = BuildQueryUri(_sendTemplateUrl);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"ipaddress", userIpAddress}
            };
            var data = DataFormatter.FromObject(new
            {
                templateid = templateId,
                mobile,
                parameters = DataFormatter.FromObject(parameter)
            });
            var result = Connector.Post(url, parameters, data);
            return DataFormatter.ToObject<ApiResult<SecretKeyAuthentication>>(result);
        }

        public async Task<ApiResult> SendTemplateAsync(string token, string mobile, long templateId,
            string userIpAddress, IDictionary<string, string> parameter = null)
        {
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            var url = BuildQueryUri(_sendTemplateUrl);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"ipaddress", userIpAddress}
            };
            var data = DataFormatter.FromObject(new
            {
                templateid = templateId,
                mobile,
                parameters = DataFormatter.FromObject(parameter),
                IpAddress = userIpAddress
            });
            var result = await Connector.PostAsync(url, parameters, data);
            return DataFormatter.ToObject<ApiResult<SecretKeyAuthentication>>(result);
        }

        public async Task<ApiResult<MessageTemplate>> GetTemplate(string token, long code)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"code", code.ToString()}
            };
            var url = BuildQueryUri(_getSingleTemplateByCode);
            var data = await DataFormatter.FromObjectAsync(new { });
            var result = await Connector.PostAsync(url, parameters, data);
            return DataFormatter.ToObject<ApiResult<MessageTemplate>>(result);
        }

        /// <summary>
        ///     发送手机验证码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></param>
        /// <param name="userIpAddress"></param>
        /// <returns></returns>
        public ApiResult SendMobileVerifiyCode(string token, string mobile, string userIpAddress)
        {
            var url = BuildQueryUri(_sendPhoneVerifiyCode);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"ipaddress", userIpAddress}
            };
            try
            {
                var data = DataFormatter.FromObject(new { mobile, IpAddress = userIpAddress });
                var result = Connector.Post(url, parameters, data);
                return DataFormatter.ToObject<ApiResult>(result);
            }
            catch (Exception ex)
            {
                return ApiResult.Failure(ex.Message);
            }
        }

        /// <summary>
        ///     发送手机验证码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></param>
        /// <param name="userIpAddress"></param>
        /// <returns></returns>
        public ApiResult CheckMobileVerifiyCode(string token, string mobile, string userIpAddress, long code)
        {
            var url = BuildQueryUri(_checkPhoneVerifiyCode);
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"token", token},
                {"ipaddress", userIpAddress}
            };
            var data = DataFormatter.FromObject(new { mobile, IpAddress = userIpAddress, code });
            var result = Connector.Post(url, parameters, data);
            return DataFormatter.ToObject<ApiResult>(result);
        }
    }
}