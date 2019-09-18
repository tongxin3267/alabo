using System;
using System.Collections.Generic;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Category.Domain.Services {

    public interface ICategoryPropertyValueService : IService<CategoryPropertyValue, Guid> {

        CategoryPropertyValue GetGuidCategoryPropertyValue(string Guid);

        /// <summary>
        ///     根据 ProductSku 的 PropertyJson 获取对于的属性
        /// </summary>
        /// <param name="ListPropertyId"></param>
        IEnumerable<CategoryPropertyValue> GetCategoryPropertyValueList(List<Guid> ListPropertyId);
    }
}