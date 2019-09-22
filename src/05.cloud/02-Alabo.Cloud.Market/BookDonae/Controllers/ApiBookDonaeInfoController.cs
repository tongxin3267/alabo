using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;

using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Market.BookDonae.Domain.Entities;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Market.BookDonae.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Market.BookDonae.Controllers {

    [ApiExceptionFilter]
    [Route("Api/BookDonaeInfo/[action]")]
    public class ApiBookDonaeInfoController : ApiBaseController<BookDonaeInfo, ObjectId> {
        
       

        public ApiBookDonaeInfoController() : base() {
       
        }

        [HttpGet]
        public ApiResult GetBook(string bookName) {
            if (bookName.IsNullOrEmpty()) {
                return ApiResult.Failure("需传入书名");
            }
            var book = Resolve<IBookDonaeInfoService>().GetSingle(u => u.Name == bookName);
            if (book == null) {
                return ApiResult.Failure("不存在该书籍");
            }
            return ApiResult.Success(book);
        }
    }
}