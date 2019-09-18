using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Dtos;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.App.Core.Themes.DiyModels.Previews;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Open.Share.Domain.Dto;
using Alabo.App.Open.Share.Domain.Entities;
using Alabo.App.Open.Share.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Mapping;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Open.Share.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Reward/[action]")]
    public class ApiRewardController : ApiBaseController<Reward, long> {

        public ApiRewardController() : base() {
            BaseService = Resolve<IRewardService>();
        }

        ///// <summary>
        ///// Lists the specified parameter.
        ///// </summary>
        ///// <param name="parameter">参数</param>
        ///// <returns>ApiResult&lt;ZKListApiOutput&gt;.</returns>
        //[HttpGet]
        //[Display(Description = "列出指定的参数")]
        //[ApiAuthorize]
        //public ApiResult<ListOutput> List([FromQuery]RewardApiInput parameter) {
        //    var rewardInput = new RewardInput {
        //        PageIndex = parameter.PageIndex,
        //        UserId = parameter.LoginUserId,
        //        PageSize = parameter.PageSize
        //    };

        //    var model = Resolve<IRewardService>().GetViewRewardPageList(rewardInput, this.HttpContext);
        //    var apiOutput = new ListOutput {
        //        TotalSize = model.PageCount,
        //        StyleType = 1
        //    };
        //    foreach (var item in model) {
        //        var apiData = new ListItem {
        //            Id = (object)item.Reward.Id,
        //            Intro = $"{item.ShareModule.Name}-账后{item.Reward.AfterAmount} {item.Reward.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
        //            Title = $"{item.OrderUser.GetUserName()}-{item.MoneyType?.Name}",
        //            Image = Resolve<IApiService>().ApiUserAvator(item.OrderUser.Id),
        //            Url = $"/share/show?id={item.Reward.Id}",
        //            Extra = item.Reward.Amount.ToStr()
        //        };
        //        apiOutput.ApiDataList.Add(apiData);
        //    }
        //    return ApiResult.Success(apiOutput);
        //}

        /// <summary>
        /// Shows the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>ApiResult&lt;RewardApiOutput&gt;.</returns>
        [HttpGet]
        [Display(Description = "列出指定的参数")]
        [ApiAuth]
        public ApiResult<RewardApiOutput> Show([FromQuery]ApiBaseInput parameter) {
            if (parameter.EntityId.ConvertToLong() <= 0) {
                return ApiResult.Failure<RewardApiOutput>("输入参数不合法！");
            }
            var model = new RewardApiOutput();
            var rewardView = Resolve<IRewardService>().GetRewardView(parameter.EntityId.ConvertToLong());
            if (rewardView == null) {
                return ApiResult.Failure<RewardApiOutput>("该分润记录不存在！");
            }
            model.Reward = rewardView.Reward;
            var userOutput = AutoMapping.SetValue<UserOutput>(rewardView.OrderUser);
            userOutput.GradeName = Resolve<IGradeService>().GetGrade(rewardView.OrderUser.GradeId)?.Name;
            userOutput.Status = rewardView.OrderUser.Status.GetDisplayName();
            userOutput.UserName = rewardView.OrderUser.GetUserName();
            userOutput.Avator = Resolve<IApiService>().ApiUserAvator(rewardView.OrderUser.Id);
            model.OrderUser = userOutput;

            model.MoneyTypeName = Resolve<IAutoConfigService>().MoneyTypes().First(r => r.Id == rewardView.Reward.MoneyTypeId).Name;
            return ApiResult.Success(model);
        }

        /// <summary>
        ///分润数据
        /// </summary>
        [HttpGet]
        [Display(Description = "分润数据")]
        [ApiAuth]
        public ApiResult<ListOutput> List([FromQuery]ListInput parameter) {
            var rewardInput = new RewardInput {
                PageIndex = parameter.PageIndex,
                UserId = parameter.LoginUserId,
                PageSize = parameter.PageSize
            };

            var model = Resolve<IRewardService>().GetViewRewardPageList(rewardInput, this.HttpContext);
            var apiOutput = new ListOutput {
                TotalSize = model.PageCount,
                StyleType = 1
            };
            var users = Resolve<IUserService>().GetList();
            foreach (var item in model) {
                var orderUser = users.FirstOrDefault(u => u.Id == item.OrderUserId);

                var apiData = new ListItem {
                    Id = item.Reward.Id,
                    Intro = $"{item.Reward.CreateTime:yyyy-MM-dd hh:ss}",
                    Title = $"{orderUser.UserName}",
                    Image = Resolve<IApiService>().ApiUserAvator((item.OrderUser?.Id).ConvertToLong()),
                    Url = $"/pages/index?path=share_show&id={item.Reward.Id}",
                    Extra = item.Reward.Amount.ToStr()
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        ///     分润详情
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "分润详情")]
        [ApiAuth]
        public ApiResult<List<KeyValue>> Preview([FromQuery] PreviewInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<List<KeyValue>>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var view = Resolve<IRewardService>().GetRewardView(parameter.Id.ConvertToLong());
            if (view == null) {
                return ApiResult.Failure<List<KeyValue>>("该分润记录不存在！");
            }
            //var result = view.Reward.ToKeyValues();
            var result = AutoMapping.SetValue<RewardPriviewOutput>(view.Reward);
            result.MoneyTypeName = view.MoneyTypeName;
            //result.StatusName = view.Status.GetDisplayName();
            result.OrderUserName = view.OrderUser.UserName;

            return ApiResult.Success(result.ToKeyValues());
        }

        [HttpGet]
        [Display(Description = "启用配置")]
        public ApiResult<PagedList<Reward>> RewardList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IRewardService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }
    }
}