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
        /// 新加类目属性值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add([FromBody]CategoryPropertyValueView modelInput)
        {
           
            try
            {
                if (modelInput == null)
                {
                    return ApiResult.Failure("类目属性id不能为空!");
                }

                var propertyId = modelInput.PropertyId;
                var name = modelInput.Name;

                if (propertyId == Guid.Empty)
                {
                    return ApiResult.Failure("类目属性id不能为空!");
                }

                if (string.IsNullOrEmpty(name))
                {
                    return ApiResult.Failure("类目属性值不能为空!");
                }

                var model = Resolve<ICategoryPropertyValueService>().GetSingle(x => x.PropertyId == propertyId && x.ValueName == name);
                if (model != null)
                {
                    return ApiResult.Failure("类目属性值已经存在!");
                }
                var newmodel = new CategoryPropertyValue { PropertyId = propertyId, ValueName = name };
                var rs = Resolve<ICategoryPropertyValueService>().Add(newmodel);
                if (rs)
                {
                   
                    return ApiResult.Success(newmodel, "类目属性值新增成功!");
                }

                return ApiResult.Failure("类目属性值新增失败!");
            }
            catch (Exception exc)
            {
                return ApiResult.Failure("类目属性值新增失败!" + exc.Message);
            }
        }
    }
}