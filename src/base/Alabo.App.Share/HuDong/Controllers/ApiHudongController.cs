using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Open.HuDong.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Helpers;
using System.Collections.Generic;
using Alabo.Extensions;
using Alabo.App.Open.HuDong.Dtos;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Core.Extensions;

namespace Alabo.App.Open.HuDong.Controllers {

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
        /// ��ȡ�齱���ݣ������ȡ��ת������
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
        /// �齱,����һ����ֵ
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
        /// ��ȡ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetHudongType() {
            var result = Resolve<IHudongService>().GetAwardType();

            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��ȡ���ͼ
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
        /// �༭
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit([FromBody]Hudong model) {
            if (model == null) {
                return ApiResult.Failure($"modelΪ��");
            } else {
                if (model.Awards != null) {
                    decimal sumRate = 0;
                    model.Awards.ForEach(a => sumRate += a.Rate);
                    if (sumRate > 100) {
                        return ApiResult.Failure($"�񽱼��ʲ��ܴ���100%");
                    }
                    if (sumRate <= 0) {
                        return ApiResult.Failure($"�񽱼��ʱ������0%");
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
                    //    return ApiResult.Failure($"��������ֻ��Ϊ����");
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
                list.Add(new HudongAward { Intro = $"{name}��{i + 1}" });
            }

            return list;
        }
    }
}