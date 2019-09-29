using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Categories.Domain.Repositories;

namespace Alabo.Industry.Shop.Categories.Domain.Services
{
    public class CategoryPropertyValueService : ServiceBase<CategoryPropertyValue, Guid>, ICategoryPropertyValueService
    {
        public CategoryPropertyValueService(IUnitOfWork unitOfWork, IRepository<CategoryPropertyValue, Guid> repository)
            : base(unitOfWork, repository)
        {
        }

        public CategoryPropertyValue GetGuidCategoryPropertyValue(string Guid)
        {
            var value = Resolve<ICategoryPropertyValueService>().GetSingle(r => r.Id == Guid.ToGuid());
            if (value != null) return value;

            return null;
        }

        /// <summary>
        ///     根据 ProductSku 的 PropertyJson 获取对于的属性
        /// </summary>
        /// <param name="ListPropertyId"></param>
        public IEnumerable<CategoryPropertyValue> GetCategoryPropertyValueList(List<Guid> ListPropertyId)
        {
            return Resolve<ICategoryPropertyValueService>().GetList(w => ListPropertyId.Contains(w.Id));
        }

        /// <summary>
        ///     根据商品的GUID 获取 属性值
        /// </summary>
        /// <param name="ProductGuid"></param>
        public IEnumerable<CategoryPropertyValue> GetProductGuidList(Guid ProductGuid)
        {
            var crQuery = Repository<ICategoryPropertyRepository>().GetList();
            var listId = (from cr in crQuery
                where cr.CategoryId == ProductGuid
                select cr.Id).ToList();

            return Resolve<ICategoryPropertyValueService>().GetList(w => listId.Contains(w.PropertyId));
        }
    }
}