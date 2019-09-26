using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Offline.Merchants.Domain.Dtos;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.App.Offline.Merchants.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Offline.Merchants.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantStore/[action]")]
    public class ApiMerchantStoreController : ApiBaseController<MerchantStore, ObjectId>
    {
        public ApiMerchantStoreController()
            : base()
        {
            BaseService = Resolve<IMerchantStoreService>();
        }

        [HttpGet]
        public IActionResult GetMerchantStoreId(long userId)
        {
            var merchant = Resolve<IMerchantService>().GetSingle(x => x.UserId == userId);
            if (merchant != null)
            {
                var merchantStore = Resolve<IMerchantStoreService>().GetSingle(x => x.MerchantId == merchant.Id.ToString());

                return Json(merchantStore.Id);
            }

            return Json("");
        }

        /// <summary>
        /// ��ȡ�û�������Ϣ
        /// </summary>
        [HttpGet]
        [ApiAuth]
        [Display(Description = "��ȡ�û�������Ϣ")]
        public ApiResult<MerchantStore> GetMerchantStore(long userId)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<MerchantStore>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = new MerchantStore();
            var store = Resolve<IMerchantStoreService>().GetMerchantStore(userId);
            if (store.Count > 0)
            {
                result = store.First();
            }
            return ApiResult.Success(result);
        }

        [HttpGet]
        [Display(Description = "��ȡ���̼�ֵ��")]
        public ApiResult<List<KeyValue>> GetMerchantStoreKeyValues(long userId)
        {
            //get merchant store
            var merchantStores = Resolve<IMerchantStoreService>().GetMerchantStore(userId);
            if (merchantStores.Count <= 0)
            {
                return ApiResult.Failure<List<KeyValue>>("��ǰ�̼�û�п�ͨ�ŵ�");
            }
            var keyValues = new List<KeyValue>();
            foreach (var item in merchantStores)
            {
                keyValues.Add(new KeyValue { Key = item.Id, Value = item.Name });
            }

            return ApiResult.Success(keyValues);
        }
    }
}