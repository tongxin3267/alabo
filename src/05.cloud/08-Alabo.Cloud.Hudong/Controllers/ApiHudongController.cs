using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Services;
using Alabo.App.Share.HuDong.Dtos;
using Alabo.Extensions;
using Alabo.Helpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.HuDong.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Hudong/[action]")]
    public class ApiHudongController : ApiBaseController<Hudong, ObjectId> {

        ///
        public ApiHudongController() : base(
            ) {
            //_userManager = userManager;
            BaseService = Resolve<IHudongService>();
        }

        /// <summary>
        /// 获取抽奖内容，比如获取大转盘内容
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetAwards([FromQuery] HuDongViewInput viewInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<Hudong>(this.FormInvalidReason());
            }

            var result = Resolve<IHudongService>().GetAwards(viewInput);

            if (result.Item1.Succeeded == false) {
                return ApiResult.Failure(result);
            } else {
                return ApiResult.Success(result);
            }
        }

        /// <summary>
        /// 抽奖,返回一个数值
        /// </summary>
        /// <param name="drawInput"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Draw(DrawInput drawInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<Hudong>(this.FormInvalidReason());
            }

            var result = Resolve<IHudongService>().Draw(drawInput);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取奖品类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetHudongType() {
            var result = Resolve<IHudongService>().GetAwardType();

            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取活动视图
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Hudong> GetView([FromQuery] HuDongViewInput viewInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<Hudong>(this.FormInvalidReason());
            }

            var result = Resolve<IHudongService>().GetView(viewInput);
            return ToResult(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit([FromBody]Hudong model) {
            if (model == null) {
                return ApiResult.Failure($"model为空");
            } else {
                if (model.Awards != null) {
                    decimal sumRate = 0;
                    model.Awards.ForEach(a => sumRate += a.Rate);
                    if (sumRate > 100) {
                        return ApiResult.Failure($"获奖几率不能大于100%");
                    }
                    if (sumRate <= 0) {
                        return ApiResult.Failure($"获奖几率必须大于0%");
                    }

                    model.Awards.ForEach(a => {
                        a.AwardId = Guid.NewGuid();
                    });

                    //bool tf = true;
                    //int i = 0;
                    //model.Awards.ForEach(a => {
                    //    if(!int.TryParse(a.Count.ToString(),out i))
                    //    {
                    //        tf = false;
                    //        return;
                    //    }
                    //});
                    //if(!tf)
                    //{
                    //    return ApiResult.Failure($"奖项数量只能为整数");
                    //}
                }
            }

            var rs = Ioc.Resolve<IHudongService>().Edit(model);

            return ApiResult.Success(rs);
        }

        private List<HudongAward> AwardList(string type) {
            var name = type?.GetInstanceByName()?.GetType()?.FullName?.GetClassDescription()?.ClassPropertyAttribute
                ?.Name;
            var list = new List<HudongAward>();
            for (int i = 0; i < 8; i++) {
                list.Add(new HudongAward { Intro = $"{name}获奖{i + 1}" });
            }

            return list;
        }
    }
}