using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Admins.Configs;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Categories.Domain.Repositories;

namespace Alabo.Industry.Shop.Categories.Domain.Services
{
    public class CategoryPropertyService : ServiceBase<CategoryProperty, Guid>, ICategoryPropertyService
    {
        public CategoryPropertyService(IUnitOfWork unitOfWork, IRepository<CategoryProperty, Guid> repository) : base(
            unitOfWork, repository)
        {
        }

        public ServiceResult AddOrUpdateOrDelete(CategoryProperty model, string fieldJson)
        {
            var fields = fieldJson.DeserializeJson<List<DataField>>();
            foreach (var item in fields)
                if (item.SortOrder.IsNullOrEmpty() || item.FieldName.IsNullOrEmpty())
                    return ServiceResult.FailedWithMessage("属性值或者排序不能为空");
            //保存属性
            var result = ServiceResult.Success;

            var find = GetByIdNoTracking(model.Id);
            if (find == null)
            {
                Add(model);
            }
            else
            {
                find.CategoryId = model.CategoryId;
                find.Name = model.Name;
                find.DisplayValue = model.DisplayValue;
                find.PropertyValueJson = model.PropertyValueJson;
                find.IsSale = model.IsSale;
                find.ControlsType = model.ControlsType;
                find.SortOrder = model.SortOrder;
                Update(find);
            }

            #region 属性值添加与编辑

            model = Resolve<ICategoryPropertyService>().GetSingle(model.Id, model.IsSale);
            var valueIds = model.PropertyValues.Select(r => r.Id).ToList();
            var addList = new List<CategoryPropertyValue>();
            var updateList = new List<CategoryPropertyValue>();

            foreach (var item in fields) //名称为空不做处理
                if (!item.FieldName.IsNullOrEmpty())
                {
                    if (Guid.TryParse(item.Key, out var valueId) && valueIds.Contains(valueId))
                    {
                        //更新属性值
                        var propertyValue = model.PropertyValues.Find(r => r.Id == valueId);
                        if (propertyValue.ValueName != item.FieldName || propertyValue.SortOrder != item.SortOrder)
                        {
                            propertyValue.ValueName = item.FieldName;
                            propertyValue.SortOrder = item.SortOrder;
                            updateList.Add(propertyValue);
                        }
                    }
                    else
                    {
                        ///新增属性值
                        var propertyValue = new CategoryPropertyValue
                        {
                            ValueName = item.FieldName,
                            SortOrder = item.SortOrder,
                            PropertyId = model.Id
                        };
                        addList.Add(propertyValue);
                    }
                }

            //修改属性值

            #endregion 属性值添加与编辑

            #region 属性值删除

            var filedKeys = fields.Select(r => r.Key).ToList();
            var fieldKeysGuid = new List<Guid>();
            foreach (var item in filedKeys)
                if (Guid.TryParse(item, out var valueId))
                    fieldKeysGuid.Add(valueId);

            var valueDeleteIds = new List<Guid>();
            foreach (var item in model.PropertyValues)
                if (!fieldKeysGuid.Contains(item.Id))
                    valueDeleteIds.Add(item.Id);

            #endregion 属性值删除

            var context = Repository<ICategoryRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                if (valueDeleteIds.Count > 0)
                    Resolve<ICategoryPropertyValueService>().Delete(r => valueDeleteIds.Any(e => e == r.Id));

                if (updateList.Count > 0)
                    foreach (var item in updateList)
                        Resolve<ICategoryPropertyValueService>().Update(item);

                if (addList.Count > 0) Resolve<ICategoryPropertyValueService>().AddMany(addList);

                context.SaveChanges();
                context.CommitTransaction();
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }

            return result;
        }

        public List<CategoryProperty> GetList(Guid categoryId, bool isSale)
        {
            var categoryPropertys = Resolve<ICategoryPropertyService>()
                .GetList(p => p.CategoryId == categoryId && p.IsSale == isSale).OrderBy(r => r.SortOrder).ToList();
            if (categoryPropertys != null && categoryPropertys.Count() > 0)
                foreach (var categoryProperty in categoryPropertys)
                {
                    var valueNames = string.Empty;
                    var categoryPropertyValues = Resolve<ICategoryPropertyValueService>()
                        .GetList(p => p.PropertyId == categoryProperty.Id).OrderBy(r => r.SortOrder).ToList();
                    foreach (var categoryPropertyValue in categoryPropertyValues)
                        if (categoryPropertyValue != null && categoryPropertyValue.ValueName != null &&
                            categoryPropertyValue.ValueName != "")
                            valueNames += categoryPropertyValue.ValueName + ",";

                    if (valueNames.IndexOf(",") != -1) valueNames = valueNames.Substring(0, valueNames.Length - 1);
                    //  categoryProperty.ValuesName = valueNames;
                }

            return categoryPropertys;
        }

        public CategoryProperty GetSingle(Guid id, bool isSale)
        {
            var property = GetSingle(r => r.Id == id && r.IsSale == isSale);
            if (property != null)
                property.PropertyValues = Resolve<ICategoryPropertyValueService>()
                    .GetList(r => r.PropertyId == property.Id).OrderBy(r => r.SortOrder).ToList();

            return property;
        }

        /// <summary>
        ///     根据 ProductSku 的 PropertyJson 获取对于的属性
        /// </summary>
        /// <param name="ListPropertyId"></param>
        public IEnumerable<CategoryProperty> GetCategoryPropertyList(List<Guid> ListPropertyId)
        {
            // return Repository.GetList(w => ListPropertyId.Contains(w.Id) && w.IsSale).OrderBy(s => s.SortOrder);
            return null;
        }
    }
}