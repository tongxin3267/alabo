using Microsoft.AspNetCore.Mvc;
using System;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.App.Shop.Category.ViewModels;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Category.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/CategoryPropertyValue/[action]")]
    public class ApiCategoryPropertyValueController : ApiBaseController<CategoryPropertyValue, Guid>
    {

        public ApiCategoryPropertyValueController() : base()
        {
            BaseService = Resolve<ICategoryPropertyValueService>();

        }

        /// <summary>
        /// �¼���Ŀ����ֵ
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add([FromBody]CategoryPropertyValueView modelInput)
        {
           
            try
            {
                if (modelInput == null)
                {
                    return ApiResult.Failure("��Ŀ����id����Ϊ��!");
                }

                var propertyId = modelInput.PropertyId;
                var name = modelInput.Name;

                if (propertyId == Guid.Empty)
                {
                    return ApiResult.Failure("��Ŀ����id����Ϊ��!");
                }

                if (string.IsNullOrEmpty(name))
                {
                    return ApiResult.Failure("��Ŀ����ֵ����Ϊ��!");
                }

                var model = Resolve<ICategoryPropertyValueService>().GetSingle(x => x.PropertyId == propertyId && x.ValueName == name);
                if (model != null)
                {
                    return ApiResult.Failure("��Ŀ����ֵ�Ѿ�����!");
                }
                var newmodel = new CategoryPropertyValue { PropertyId = propertyId, ValueName = name };
                var rs = Resolve<ICategoryPropertyValueService>().Add(newmodel);
                if (rs)
                {
                   
                    return ApiResult.Success(newmodel, "��Ŀ����ֵ�����ɹ�!");
                }

                return ApiResult.Failure("��Ŀ����ֵ����ʧ��!");
            }
            catch (Exception exc)
            {
                return ApiResult.Failure("��Ŀ����ֵ����ʧ��!" + exc.Message);
            }
        }
    }
}