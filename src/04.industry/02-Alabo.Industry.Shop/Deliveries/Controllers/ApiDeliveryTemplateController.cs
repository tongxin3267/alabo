using System;
using System.Collections.Generic;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.Products.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Deliveries.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/DeliveryTemplate/[action]")]
    public class ApiDeliveryTemplateController : ApiBaseController<DeliveryTemplate, ObjectId>
    {

        public ApiDeliveryTemplateController() : base()
        {
            BaseService = Resolve<IDeliveryTemplateService>();
        }

        /// <summary>
        /// 获取运费模板
        /// </summary>
        /// <param name="id">运费模板的ID(MongoDB id)</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetDeliveryTemplateSingle([FromQuery]string id)
        {
            var template = Resolve<IDeliveryTemplateService>().GetSingle(id);

            if (template != null)
            {
                return ApiResult.Success(template);
            }
            else
            {
                return ApiResult.Failure("运费模板不存在");
            }
        }

        /// <summary>
        /// 获取运费模板
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetDeliveryTemplateList([FromQuery]long storeId)
        {
            var template = Resolve<IDeliveryTemplateService>().GetList(x => x.StoreId == storeId);

            if (template != null)
            {
                return ApiResult.Success(template);
            }
            else
            {
                return ApiResult.Failure("运费模板不存在");
            }
        }

        /// <summary>
        /// 删除运费模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteDeliveryTemplate([FromQuery]string id)
        {
            var count = Resolve<IProductService>().Count(s => s.DeliveryTemplateId == id);
            if (count > 0)
            {
                return ApiResult.Failure($"删除失败:该运费模板已被{count}个商品使用！");
            }
            var rs = Resolve<IDeliveryTemplateService>().Delete(id);

            if (rs)
            {
                return ApiResult.Success("删除成功！");
            }

            return ApiResult.Failure("删除失败！");
        }

        /// <summary>
        /// 保存运费模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveDeliveryTemplate([FromBody]List<DeliveryTemplate> list)
        {
            try
            {
                foreach (var template in list)
                {
                    //同意账号下运费模板不能重复
                    
                    var store = Resolve<IShopStoreService>().GetUserStore(template.UserId);
                    if (store != null)
                    {
                        template.StoreId = store.Id;
                        if (template.Id == ObjectId.Empty)
                        {
                            var count = Resolve<IDeliveryTemplateService>().Count(s => s.TemplateName == template.TemplateName && s.UserId == template.UserId);
                            if (count > 0) {
                                return ApiResult.Failure($"运费模板保存失败:该模板已存在!");
                            }

                            Resolve<IDeliveryTemplateService>().Add(template);
                        }
                        else
                        {
                            var count = Resolve<IDeliveryTemplateService>().Count(s => s.TemplateName == template.TemplateName && s.UserId == template.UserId && s.Id != template.Id);
                            if (count > 0) {
                                return ApiResult.Failure($"运费模板保存失败:该模板名已存在!");
                            }

                            Resolve<IDeliveryTemplateService>().Update(template);
                        }
                    }
                }

                return ApiResult.Success("运费模板保存成功!");
            }
            catch (Exception exc)
            {
                return ApiResult.Failure($"运费模板保存失败!{exc.Message}");
            }
        }
    }
}