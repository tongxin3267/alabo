using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Market.BookDonae.Domain.Entities;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.App.Market.BookDonae.Domain.Services;

namespace Alabo.App.Market.BookDonae.Controllers {

    [ApiExceptionFilter]
    [Route("Api/BooksClass/[action]")]
    public class ApiBooksClassController : ApiBaseController<BooksClass, ObjectId> {

        public ApiBooksClassController() : base() {
            BaseService = Resolve<IBooksClassService>();
        }
    }
}