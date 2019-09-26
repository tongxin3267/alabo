using System;
using System.Collections.Generic;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Categories.Domain.Entities;

namespace Alabo.Industry.Shop.Categories.Domain.Services {

    public interface ICategoryPropertyValueService : IService<CategoryPropertyValue, Guid> {

        CategoryPropertyValue GetGuidCategoryPropertyValue(string Guid);

        /// <summary>
        ///     根据 ProductSku 的 PropertyJson 获取对于的属性
        /// </summary>
        /// <param name="ListPropertyId"></param>
        IEnumerable<CategoryPropertyValue> GetCategoryPropertyValueList(List<Guid> ListPropertyId);
    }
}