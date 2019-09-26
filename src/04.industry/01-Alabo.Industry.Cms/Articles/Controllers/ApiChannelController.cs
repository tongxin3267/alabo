using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Articles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Channel/[action]")]
    public class ApiChannelController : ApiBaseController<Channel, ObjectId> {

        public ApiChannelController() : base() {
            BaseService = Resolve<IChannelService>();
        }


        [HttpGet]
        public ApiResult GetChannelClassType(string channelId)
        {
            var channel = Resolve<IChannelService>().GetSingle(channelId);
            var type = Resolve<IChannelService>().GetChannelClassType(channel);
            return ApiResult.Success(type.Name);
        }

        [HttpGet]
        public ApiResult GetChannelTagType(string channelId)
        {
            var channel = Resolve<IChannelService>().GetSingle(channelId);
            var type = Resolve<IChannelService>().GetChannelTagType(channel);
            return ApiResult.Success(type.Name);
        }
    }
}