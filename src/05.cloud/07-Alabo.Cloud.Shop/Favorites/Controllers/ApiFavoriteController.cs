using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Cloud.Shop.Favorites.Domain.Entities;
using Alabo.Cloud.Shop.Favorites.Domain.Services;
using Alabo.Cloud.Shop.Favorites.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Shop.Favorites.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Favorite/[action]")]
    public class ApiFavoriteController : ApiBaseController<Favorite, ObjectId> {

        public ApiFavoriteController() : base() {
            BaseService = Resolve<IFavoriteService>();
        }

        /// <summary>
        /// �����Ʒ�Ƿ��ղ�
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ApiResult Check([FromQuery] FavoriteInput parameter) {
            if (parameter == null) {
                return ApiResult.Failure();
            }
            var result = Resolve<IFavoriteService>().GetSingle(r => r.EntityId == parameter.EntityId && r.UserId == parameter.LoginUserId && r.Type == parameter.Type);
            if (result != null) {
                return ApiResult.Success();
            } else {
                return ApiResult.Failure();
            }
        }

        /// <summary>
        ///    ����ղأ�������Ʒ���û������¡����̵������ղأ�
        /// </summary>
        [HttpGet]
        [Display(Description = "�ղ�")]
        public ApiResult Add([FromQuery] FavoriteInput parameter) {
            var result = Resolve<IFavoriteService>().Add(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///    ɾ���ղأ�������Ʒ���û������¡����̵������ղأ�
        /// </summary>
        [HttpGet]
        [Display(Description = "ɾ���ղ�")]
        public ApiResult Remove([FromQuery] FavoriteInput parameter) {
            var result = Resolve<IFavoriteService>().Remove(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///    �ղ��б�������Ʒ���û������¡����̵������ղأ�
        /// </summary>
        [HttpGet]
        [Display(Description = "�ҵ��ղ�")]
        public ApiResult List([FromQuery] long loginUserId) {
            var result = Resolve<IFavoriteService>().GetList(u => u.UserId == loginUserId);
            result = result.Select(s => {
                s.Image = Resolve<IApiService>().ApiImageUrl(s.Image);
                return s;
            }).ToList();
            return ApiResult.Success(result);
        }
    }
}