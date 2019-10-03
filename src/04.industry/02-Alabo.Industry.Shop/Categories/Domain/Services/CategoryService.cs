using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.Categories.Domain.Services
{
    public class CategoryService : ServiceBase<Category, Guid>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category, Guid> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     获取单条记录
        /// </summary>
        /// <param name="guid"></param>
        public Category GetSingle(Guid guid)
        {
            var category = GetSingle(r => r.Id == guid);
            if (category != null)
            {
                //读取所有类目属性
                var propertys = Resolve<ICategoryPropertyService>().GetList(r => r.CategoryId == category.Id)
                    ?.OrderBy(r => r.SortOrder).ToList();
                var propertyIds = propertys.Select(r => r.Id).ToList();

                // 读取所有类目属性值
                var query = new ExpressionQuery<CategoryPropertyValue>();
                query.And(r => propertyIds.Contains(r.PropertyId));
                query.OrderByAscending(e => e.SortOrder);
                var propertyValues = Resolve<ICategoryPropertyValueService>().GetList(query).ToList();

                // 规格，销售用
                var salePropertys = propertys.Where(r => r.IsSale).ToList();
                category.SalePropertys = new List<CategoryProperty>();
                foreach (var item in salePropertys)
                {
                    item.PropertyValues = propertyValues.Where(r => r.PropertyId == item.Id).ToList();
                    category.SalePropertys.Add(item);
                }

                // 商品参数,显示用
                var displayPropertys = propertys.Where(r => r.IsSale == false).ToList(); //规格，销售用
                category.DisplayPropertys = new List<CategoryProperty>();
                foreach (var item in displayPropertys)
                {
                    item.PropertyValues = propertyValues.Where(r => r.PropertyId == item.Id).ToList();
                    category.DisplayPropertys.Add(item);
                }
            }

            return category;
        }

        /// <summary>
        ///     处理商品属性
        /// </summary>
        /// <param name="product"></param>
        /// <param name="request"></param>
        public string AddOrUpdateOrDeleteProductCategoryData(Product product,
            HttpRequest request)
        {
            var category = GetSingle(product.CategoryId);
            if (category == null) {
                return string.Empty;
            }

            var productCategory = new Category(); // 需要保存的类目数据信息

            /// 处理商品规格，销售属性
            var salePropertys = new List<CategoryProperty>();
            foreach (var item in category.SalePropertys)
            {
                var productCategoryProperty = new CategoryProperty
                {
                    Name = item.Name,
                    Id = item.Id,
                    SortOrder = item.SortOrder,
                    IsSale = item.IsSale,
                    PropertyValues = new List<CategoryPropertyValue>()
                }; // 所有的销售属性

                foreach (var propertyValue in item.PropertyValues)
                {
                    var value = request.Form["sale_" + propertyValue.Id];
                    if (!value.IsNullOrEmpty())
                    {
                        propertyValue.ValueAlias = value; // 别名显示
                        productCategoryProperty.PropertyValues.Add(propertyValue);
                    }
                }

                salePropertys.Add(productCategoryProperty);
            }

            /// 商品参数处理
            var displayPropertys = new List<CategoryProperty>();
            foreach (var item in category.DisplayPropertys)
            {
                var productDisplayProperty = new CategoryProperty
                {
                    // 所有显示属性
                    Name = item.Name,
                    Id = item.Id,
                    SortOrder = item.SortOrder,
                    IsSale = item.IsSale,
                    PropertyValues = new List<CategoryPropertyValue>()
                };
                //复选框多条记录

                if (item.ControlsType == ControlsType.CheckBox)
                {
                    foreach (var propertyValue in item.PropertyValues)
                    {
                        var value = request.Form["display_" + propertyValue.Id];
                        if (!value.IsNullOrEmpty())
                        {
                            productDisplayProperty.DisplayValue += value; // 多个属性值合并在一起
                            productDisplayProperty.PropertyValues.Add(propertyValue);
                        }
                    }
                }
                else
                {
                    //其他框一条记录
                    var value = request.Form["display_" + item.Id];
                    if (!value.IsNullOrEmpty()) {
                        productDisplayProperty.DisplayValue = value; // 单个属性值
                    }
                }

                displayPropertys.Add(productDisplayProperty);
            }

            productCategory.SalePropertys = salePropertys; // 规格
            productCategory.DisplayPropertys = displayPropertys; // 规格
            return productCategory.ToJson(); // 保存到数据库
        }

        public Tuple<ServiceResult, Category> Delete(Guid id)
        {
            var result = ServiceResult.Success;
            if (id == Guid.Empty) {
                return Tuple.Create(ServiceResult.FailedWithMessage("删除失败"), new Category());
            }

            var model = Resolve<ICategoryService>().GetSingle(id);
            var context = Ioc.Resolve<IUserRepository>().RepositoryContext;
            context.BeginTransaction();
            Resolve<ICategoryPropertyValueService>().Delete(r => r.PropertyId == id);
            Resolve<ICategoryPropertyService>().Delete(r => r.CategoryId == id);
            Resolve<ICategoryService>().Delete(r => r.Id == id);
            context.CommitTransaction();
            return Tuple.Create(result, model);
        }
    }
}