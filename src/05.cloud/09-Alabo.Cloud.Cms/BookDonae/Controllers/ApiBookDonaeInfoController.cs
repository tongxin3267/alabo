using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Market.BookDonae.Domain.Entities;
using Alabo.App.Market.BookDonae.Domain.Services;
using Alabo.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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