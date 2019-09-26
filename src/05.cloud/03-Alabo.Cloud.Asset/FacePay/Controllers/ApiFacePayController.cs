using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Face = Alabo.App.Market.FacePay.Domain.Entities;
using Alabo.App.Market.FacePay.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Helpers;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;

namespace Alabo.App.Market.FacePay.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/FacePay/[action]")]
    public class ApiFacePayController : ApiBaseController<Domain.Entities.FacePay, ObjectId>
    {
        
       

        public ApiFacePayController(
            ) : base()
        {
        }

        [HttpPost]
        public ApiResult Pay([FromBody] Face.FacePay model)
        {
            var user = Ioc.Resolve<IUserService>().GetSingle(x => x.Id == model.UserId);
            if (user == null)
            {
                return ApiResult.Failure($"用户不存在!");
            }

            var account = Resolve<IAccountService>().GetAccount(user.Id, Currency.Cny);
            if (account.Amount < model.Amount)
            {
                return ApiResult.Failure("余额不足，请充值");
            }

            var rs = Ioc.Resolve<IFacePayService>().Add(model);
            if (rs)
            {
                account.Amount = account.Amount - model.Amount;
                var rsUpdate = Resolve<IAccountService>().Update(account);

                if (rsUpdate)
                {
                    Bill billModel = new Bill()
                    {
                        AfterAmount = account.Amount,
                        CreateTime = DateTime.Now,
                        Amount = model.Amount,
                        Flow = AccountFlow.Spending,
                        Intro = "当面付支出-" + model.Amount,
                        Type = BillActionType.Shopping,
                        UserId = user.Id,
                        UserName = user.UserName,
                    };
                    Ioc.Resolve<IBillService>().AddOrUpdate(billModel);

                }

                return ApiResult.Success(rsUpdate);
            }

            return ApiResult.Failure("未能扣除余额, 当面支付失败!");
        }
    }
}