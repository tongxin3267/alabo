using System;
using ZKCloud.Domains.Repositories.EFCore;
using ZKCloud.Domains.Repositories.Model;
using System.Linq;
using ZKCloud.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.App.Core.Api.Filter;
using ZKCloud.App.Core.Common;
using MongoDB.Bson;
using ZKCloud.App.Core.User;
using ZKCloud.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Domains.Services;
using ZKCloud.Web.Mvc.Attributes;
using ZKCloud.Web.Mvc.Controllers;
using ZKCloud.App.Core.ApiStore.Sms.Entities;
using ZKCloud.App.Core.Api.Controller;
using ZKCloud.App.Core.ApiStore.Sms.Services;

namespace ZKCloud.App.Core.ApiStore.Sms.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/SmsSend/[action]")]
    public class ApiSmsSendController : ApiBaseController<SmsSend, ObjectId>
    {
        private readonly AutoConfigManager _autoConfigManager;
        private readonly UserManager _userManager;
        public ApiSmsSendController(RestClientConfig restClientConfig, AutoConfigManager autoConfigManager) //: base(restClientConfiguration) 
        {
            //_userManager = userManager;
            BaseService = Resolve<ISmsSendService>();
            _autoConfigManager = autoConfigManager;
        }

    }
}
