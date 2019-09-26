using System;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Categories.Domain.Services;
using Alabo.Industry.Shop.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Categories.Controllers
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