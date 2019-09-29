using System;
using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.Cloud.Asset.FacePay.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Helpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Asset.FacePay.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/FacePay/[action]")]
    public class ApiFacePayController : ApiBaseController<Domain.Entities.FacePay, ObjectId>
    {
        [HttpPost]
        public ApiResult Pay([FromBody] Domain.Entities.FacePay model)
        {
            var user = Ioc.Resolve<IUserService>().GetSingle(x => x.Id == model.UserId);
            if (user == null) return ApiResult.Failure("用户不存在!");

            var account = Resolve<IAccountService>().GetAccount(user.Id, Currency.Cny);
            if (account.Amount < model.Amount) return ApiResult.Failure("余额不足，请充值");

            var rs = Ioc.Resolve<IFacePayService>().Add(model);
            if (rs)
            {
                account.Amount = account.Amount - model.Amount;
                var rsUpdate = Resolve<IAccountService>().Update(account);

                if (rsUpdate)
                {
                    var billModel = new Bill
                    {
                        AfterAmount = account.Amount,
                        CreateTime = DateTime.Now,
                        Amount = model.Amount,
                        Flow = AccountFlow.Spending,
                        Intro = "当面付支出-" + model.Amount,
                        Type = BillActionType.Shopping,
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    Ioc.Resolve<IBillService>().AddOrUpdate(billModel);
                }

                return ApiResult.Success(rsUpdate);
            }

            return ApiResult.Failure("未能扣除余额, 当面支付失败!");
        }
    }
}