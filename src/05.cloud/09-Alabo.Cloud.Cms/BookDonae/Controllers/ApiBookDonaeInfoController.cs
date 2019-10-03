using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Cloud.Cms.BookDonae.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Cms.BookDonae.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/BookDonaeInfo/[action]")]
    public class ApiBookDonaeInfoController : ApiBaseController<BookDonaeInfo, ObjectId>
    {
        [HttpGet]
        public ApiResult GetBook(string bookName)
        {
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