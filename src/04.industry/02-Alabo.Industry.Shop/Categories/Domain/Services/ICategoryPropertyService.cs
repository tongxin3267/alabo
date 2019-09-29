using System;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Categories.Domain.Entities;

namespace Alabo.Industry.Shop.Categories.Domain.Services
{
    public interface ICategoryPropertyService : IService<CategoryProperty, Guid>
    {
        /// <summary>
        ///     批量添加更新或删除属性值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fieldJson"></param>
        ServiceResult AddOrUpdateOrDelete(CategoryProperty model, string fieldJson);

        /// <summary>
        ///     根据类目获取属性列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="isSale"></param>
        List<CategoryProperty> GetList(Guid categoryId, bool isSale);

        /// <summary>
        ///     获取属性
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="isSale"></param>
        CategoryProperty GetSingle(Guid id, bool isSale);

        /// <summary>
        ///     根据 ProductSku 的 PropertyJson 获取对于的属性
        /// </summary>
        /// <param name="ListPropertyId"></param>
        IEnumerable<CategoryProperty> GetCategoryPropertyList(List<Guid> ListPropertyId);
    }
}