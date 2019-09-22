using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Dtos.Bill;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Finance.ViewModels.Bill;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.App.Core.Themes.DiyModels.Previews;
using Alabo.App.Core.Themes.Extensions;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using ApiResult = ZKCloud.Open.ApiBase.Models.ApiResult;

namespace Alabo.App.Core.Finance.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Bill/[action]")]
    public class ApiBillController : ApiBaseController<Bill, long> {

        public ApiBillController() : base() {
            BaseService = Resolve<IBillService>();
        }

        /// <summary>
        ///     Bills the specified parameter.
        /// </summary>
        /// <param name="parameter">����</param>
        [HttpGet]
        [Display(Description = "bill  the  soecified parameter")]
        [ApiAuth]
        public ApiResult<ListOutput> Bill([FromQuery] ListInput parameter) {
            var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes();
            var billApiInput = new BillApiInput {
                LoginUserId = parameter.LoginUserId,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize
            };

            var billList = Resolve<IFinanceAdminService>().GetApiBillPageList(billApiInput, out var count);
            var result = new ListOutput {
                StyleType = 1, //���õ�һ����ʽ
                TotalSize = count / parameter.PageSize
            };
            foreach (var item in billList.ToList()) {
                var index = new Random().Next(1, 8);
                var apiData = new ListItem {
                    Id = item.Id,
                    Intro = $"�˺�{item.AfterAmount} ʱ��{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
                    Title = $"{moneyTypes.FirstOrDefault(e => e.Id == item.MoneyTypeId)?.Name}-{item.Type.GetDisplayName()}",
                    Image = Resolve<IApiService>().ApiImageUrl($"/assets/mobile/images/icon/demo{index}.png"),
                    Url = $"/finance/bill/view?id={item.Id}".ToClientUrl(ClientType.WapH5),
                    Extra = item.Amount.ToStr()
                };
                result.ApiDataList.Add(apiData);
            }

            return ApiResult.Success(result);
        }

        /// <summary>
        ///     Bills the ��ͼ.
        /// </summary>
        /// <param name="parameter">����</param>
        [HttpGet]
        [Display(Description = "bills the ��ͼ ")]
        public ApiResult BillView([FromQuery] PreviewInput parameter) {
            var view = Resolve<IFinanceAdminService>().GetViewBillSingle(parameter.Id.ConvertToLong());
            if (view == null) {
                return ApiResult.Failure("�˵������ڻ����Ѿ�ɾ����");
            }

            if (view.Bill.UserId != parameter.LoginUserId) {
                return ApiResult.Failure("����Ȩ�鿴���˵���");
            }

            var apiOutput = Resolve<IFinanceAdminService>().GetBillOutput(view);
            var temp = apiOutput.ToKeyValues();

            return ApiResult.Success(temp);
        }

        [HttpGet]
        [Display(Description = "������ϸ")]
        public ApiResult<PageResult<ViewAdminBill>> ViewBillList([FromQuery] PagedInputDto parameter, object query) {
            var list = Resolve<IFinanceAdminService>().GetBillPageList(Query);
            PageResult<ViewAdminBill> apiRusult = new PageResult<ViewAdminBill> {
                PageCount = list.PageCount,
                Result = list,
                RecordCount = list.RecordCount,
                CurrentSize = list.CurrentSize,
                PageIndex = list.PageIndex,
                PageSize = list.PageSize,
            };

            if (list.Count < 0) {
                return ApiResult.Success(new PageResult<ViewAdminBill>());
            }
            return ApiResult.Success(apiRusult);
        }
    }
}