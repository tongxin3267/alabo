using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Shop.Store.Domain.Entities;
using Alabo.App.Shop.Store.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using System.Collections.Generic;
using Alabo.App.Shop.Product.Domain.Services;

namespace Alabo.App.Shop.Store.Controllers
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
        /// ��ȡ�˷�ģ��
        /// </summary>
        /// <param name="id">�˷�ģ���ID(MongoDB id)</param>
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
                return ApiResult.Failure("�˷�ģ�岻����");
            }
        }

        /// <summary>
        /// ��ȡ�˷�ģ��
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
                return ApiResult.Failure("�˷�ģ�岻����");
            }
        }

        /// <summary>
        /// ɾ���˷�ģ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteDeliveryTemplate([FromQuery]string id)
        {
            var count = Resolve<IProductService>().Count(s => s.DeliveryTemplateId == id);
            if (count > 0)
            {
                return ApiResult.Failure($"ɾ��ʧ��:���˷�ģ���ѱ�{count}����Ʒʹ�ã�");
            }
            var rs = Resolve<IDeliveryTemplateService>().Delete(id);

            if (rs)
            {
                return ApiResult.Success("ɾ���ɹ���");
            }

            return ApiResult.Failure("ɾ��ʧ�ܣ�");
        }

        /// <summary>
        /// �����˷�ģ��
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveDeliveryTemplate([FromBody]List<DeliveryTemplate> list)
        {
            try
            {
                foreach (var template in list)
                {
                    //ͬ���˺����˷�ģ�岻���ظ�
                    
                    var store = Resolve<IStoreService>().GetUserStore(template.UserId);
                    if (store != null)
                    {
                        template.StoreId = store.Id;
                        if (template.Id == ObjectId.Empty)
                        {
                            var count = Resolve<IDeliveryTemplateService>().Count(s => s.TemplateName == template.TemplateName && s.UserId == template.UserId);
                            if (count > 0) {
                                return ApiResult.Failure($"�˷�ģ�屣��ʧ��:��ģ���Ѵ���!");
                            }

                            Resolve<IDeliveryTemplateService>().Add(template);
                        }
                        else
                        {
                            var count = Resolve<IDeliveryTemplateService>().Count(s => s.TemplateName == template.TemplateName && s.UserId == template.UserId && s.Id != template.Id);
                            if (count > 0) {
                                return ApiResult.Failure($"�˷�ģ�屣��ʧ��:��ģ�����Ѵ���!");
                            }

                            Resolve<IDeliveryTemplateService>().Update(template);
                        }
                    }
                }

                return ApiResult.Success("�˷�ģ�屣��ɹ�!");
            }
            catch (Exception exc)
            {
                return ApiResult.Failure($"�˷�ģ�屣��ʧ��!{exc.Message}");
            }
        }
    }
}