using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.App.Shop.Category.Domain.Dtos;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.Domains.Enums;
using Alabo.App.Shop.Product.Domain.Services;

namespace Alabo.App.Shop.Category.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/Category/[action]")]
    public class ApiCategoryController : ApiBaseController<Domain.Entities.Category, Guid>
    {

        public ApiCategoryController() : base()
        {
            BaseService = Resolve<ICategoryService>();
        }


        /// <summary>
        ///     根据商品分类异步加载属性面板用
        /// </summary>
        /// <param name="categoryId"></param>
        [HttpGet]
        public ApiResult<Domain.Entities.Category> ProductProperty(string categoryId)
        {

            var category = Resolve<ICategoryService>().GetSingle(Guid.Parse(categoryId));
            return ApiResult.Success(category);
        }

        /// <summary>
        ///     根据商品分类异步加载属性面板用
        /// </summary>
        /// <param name="categoryId"></param>
        [HttpGet]
        public ApiResult<CategoryView> GetView(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return ApiResult.Failure<CategoryView>("类目ID不能为空");
            }

            var category = Resolve<ICategoryService>().GetSingle(categoryId);
            //增加排序功能 从小到大
            var dispList = category.DisplayPropertys.OrderBy(s => s.SortOrder).Select(
                x => new ViewDisplayProperty
                {
                    Id = x.Id,
                    Name = x.Name,
                    ControlsType = (ViewPropertyControlsType)Enum.ToObject(typeof(ViewPropertyControlsType),
                    (int)x.ControlsType),
                    Values = x.PropertyValues.Select(y => new ViewPropertyValue { Id = y.Id, ValueName = y.ValueName }).ToList(),
                    Intro = string.Join(',', x.PropertyValues.Select(y => y.ValueName).ToList()),
                    SortOrder = x.SortOrder
                });
            //增加排序功能 从小到大
            var saleList = category.SalePropertys.OrderBy(s => s.SortOrder).Select(
               x => new ViewSaleProperty
               {
                   Id = x.Id,
                   Name = x.Name,
                   Values = x.PropertyValues.Select(y => new ViewPropertyValue { Id = y.Id, ValueName = y.ValueName }).ToList(),
                   Intro = string.Join(',', x.PropertyValues.Select(y => y.ValueName).ToList()),
                   SortOrder = x.SortOrder
               });

            var rs = new CategoryView
            {
                DisplayPropertys = dispList.ToList(),
                SalePropertys = saleList.ToList(),
                Id = category.Id,
                Name = category.Name,
                SortOrder = category.SortOrder,
                IsPartent = category.PartentId == Guid.Empty,
                PartentId = category.PartentId,
            };

            return ApiResult.Success<CategoryView>(rs);
        }

        /// <summary>
        /// 保存类目
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Save([FromBody]CategoryView view)
        {
            //如果id为空就判断是否存在
            if (view.Id != Guid.Empty)
            {
                //判断重复
                var confrim = Resolve<ICategoryService>().Count(s => s.Name == view.Name && s.PartentId != view.PartentId);
                if (confrim > 0)
                {
                    return ApiResult.Failure("已存在同名类目!");
                }
            }
            else
            {

                //判断重复
                var confrim = Resolve<ICategoryService>().Count(s => s.Name == view.Name);
                if (confrim > 0)
                {
                    return ApiResult.Failure("已存在同名类目!");
                }
            }
            //销售属性
            var saleDistinct = view.SalePropertys.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
            if (saleDistinct.Count() < view.SalePropertys.Count) {
                return ApiResult.Failure("已存在同名销售属性!");
            }

            foreach (var item in view.SalePropertys)
            {
                ///销售属性值
                var saleItemDistinct = item.Values.GroupBy(p => new { p.ValueName }).Select(g => g.First()).ToList();
                if (saleItemDistinct.Count() < item.Values.Count) {
                    return ApiResult.Failure("已存在同名销售属性值!");
                }
            }

            //商品参数
            var displayDistinct = view.DisplayPropertys.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
            if (displayDistinct.Count() < view.DisplayPropertys.Count) {
                return ApiResult.Failure("已存在同名商品参数!");
            }

            foreach (var item in view.DisplayPropertys)
            {
                ///销售属性值
                var saleItemDistinct = item.Values.GroupBy(p => new { p.ValueName }).Select(g => g.First()).ToList();
                if (saleItemDistinct.Count() < item.Values.Count) {
                    return ApiResult.Failure("已存在同名商品参数值!");
                }
            }

            var valAdd = new List<CategoryPropertyValue>();
            var valDel = new List<CategoryPropertyValue>();
            var valUpd = new List<CategoryPropertyValue>();
            var propertyDel = new List<CategoryProperty>();
            var catetory = new Domain.Entities.Category();
            // CreateNew
            if (view.Id == Guid.Empty)
            {
                catetory = new Domain.Entities.Category
                {
                    Name = view.Name,
                    PartentId = view.PartentId,
                    SortOrder = view.SortOrder
                };
                Resolve<ICategoryService>().Add(catetory);
            }
            else
            {
                catetory = Resolve<ICategoryService>().GetSingle(view.Id);
                catetory.Name = view.Name;
                catetory.PartentId = view.PartentId;
                catetory.SortOrder = view.SortOrder;
                Resolve<ICategoryService>().Update(catetory);
            }

            // CreateNew
            if (view.Id == Guid.Empty)
            {
                var property = new Domain.Entities.CategoryProperty();

                foreach (var saleProp in view.SalePropertys)
                {
                    property = ProcessProp(catetory, saleProp);

                    foreach (var vi in saleProp.Values)
                    {
                        ValueProcess(property, vi, valAdd);
                    }
                }

                foreach (var dispProp in view.DisplayPropertys)
                {
                    property = ProcessDispProp(catetory, dispProp);

                    foreach (var vi in dispProp.Values)
                    {
                        ValueProcess(property, vi, valAdd);
                    }
                }
            }
            else    // Update Exist
            {
                // var catetory = Resolve<ICategoryService>().GetSingle(view.Id);
                catetory.Name = view.Name;
                catetory.PartentId = view.PartentId;
                catetory.SortOrder = view.SortOrder;
                Resolve<ICategoryService>().Update(catetory);


                var propertyIdList = catetory.SalePropertys?.Select(x => x.Id.ToString());
                //需要删除的销售属性
                var delSalePropertyIdList = propertyIdList.Except(view.SalePropertys.Select(x => x.Id.ToString()));
                var delSalePropertyList = Resolve<ICategoryPropertyService>().GetList(string.Join(',', delSalePropertyIdList));
                propertyDel.AddRange(delSalePropertyList);
                //需要删除的商品参数
                var delDisplayPropertyIdList = catetory.DisplayPropertys?.Select(s => s.Id.ToString()).Except(view.DisplayPropertys.Select(x => x.Id.ToString()));
                var delDisplayPropertyList = Resolve<ICategoryPropertyService>().GetList(string.Join(',', delDisplayPropertyIdList));
                propertyDel.AddRange(delDisplayPropertyList);


                var property = new Domain.Entities.CategoryProperty { };

                foreach (var saleProp in view.SalePropertys)
                {
                    var loadIdList = Resolve<ICategoryPropertyValueService>().GetList(x => x.PropertyId == saleProp.Id).Select(x => x.Id.ToString());
                    var delIdList = loadIdList.Except(saleProp.Values.Select(x => x.Id.ToString()));
                    var delList = Resolve<ICategoryPropertyValueService>().GetList(string.Join(',', delIdList));
                    valDel.AddRange(delList);

                    property = ProcessProp(catetory, saleProp);

                    foreach (var vi in saleProp.Values)
                    {
                        ValueProcess(property, vi, valAdd, valUpd, delIdList);
                    }
                }

                foreach (var dispProp in view.DisplayPropertys)
                {
                    var loadIdList = Resolve<ICategoryPropertyValueService>().GetList(x => x.PropertyId == dispProp.Id).Select(x => x.Id.ToString());
                    var delIdList = loadIdList.Except(dispProp.Values.Select(x => x.Id.ToString()));
                    var delList = Resolve<ICategoryPropertyValueService>().GetList(string.Join(',', delIdList));
                    valDel.AddRange(delList);

                    property = ProcessDispProp(catetory, dispProp);

                    foreach (var vi in dispProp.Values)
                    {
                        ValueProcess(property, vi, valAdd, valUpd, delIdList);
                    }
                }
            }
            //先删除属性值
            Resolve<ICategoryPropertyValueService>().DeleteMany(valDel.Select(x => x.Id));
            //再删除属性
            Resolve<ICategoryPropertyService>().DeleteMany(propertyDel.Select(x => x.Id));
            // Resolve<ICategoryPropertyValueService>().DeleteMany(valDel.Select(x => x.Id));
            foreach (var item in valUpd)
            {
                Resolve<ICategoryPropertyValueService>().Update(item);
            }
            Resolve<ICategoryPropertyValueService>().AddMany(valAdd);

            return ApiResult.Success("保存成功!");
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteCategory(string categoryId)
        {
            var id = new Guid(categoryId);
            var check = Resolve<IProductAdminService>().CheckCategoryHasProduct(id);
            if (check)
            {
                return ApiResult.Failure("类目下有商品,不能删除!");
            }
            Resolve<ICategoryService>().Delete(id);
            return ApiResult.Success();
        }

        private CategoryProperty ProcessDispProp(Domain.Entities.Category catetory, ViewDisplayProperty dispProp)
        {
            CategoryProperty property;
            if (dispProp.Id == Guid.Empty)
            {
                property = new Domain.Entities.CategoryProperty
                {
                    CategoryId = catetory.Id,
                    Name = dispProp.Name,
                    ControlsType = (ControlsType)(int)dispProp.ControlsType,
                    SortOrder = dispProp.SortOrder
                };

                Resolve<ICategoryPropertyService>().Add(property);
            }
            else
            {
                property = Resolve<ICategoryPropertyService>().GetSingle(dispProp.Id);
                property.Name = dispProp.Name;
                property.SortOrder = dispProp.SortOrder;
                Resolve<ICategoryPropertyService>().Update(property);
            }

            return property;
        }

        private CategoryProperty ProcessProp(Domain.Entities.Category catetory, ViewSaleProperty saleProp)
        {
            CategoryProperty property;
            if (saleProp.Id == Guid.Empty)
            {
                property = new Domain.Entities.CategoryProperty
                {
                    CategoryId = catetory.Id,
                    Name = saleProp.Name,
                    IsSale = true,
                    SortOrder = saleProp.SortOrder
                };

                Resolve<ICategoryPropertyService>().Add(property);
            }
            else
            {
                property = Resolve<ICategoryPropertyService>().GetSingle(saleProp.Id);
                property.Name = saleProp.Name;
                property.SortOrder = saleProp.SortOrder;
                Resolve<ICategoryPropertyService>().Update(property);
            }

            return property;
        }

        private void ValueProcess(CategoryProperty property, ViewPropertyValue vi, List<CategoryPropertyValue> valAdd, List<CategoryPropertyValue> valUpd = null, IEnumerable<string> delIdList = null)
        {
            if (delIdList != null && delIdList.Contains(vi.Id.ToString()))
            {
                return;
            }

            if (vi.Id == Guid.Empty)
            {
                var value = new Domain.Entities.CategoryPropertyValue
                {
                    PropertyId = property.Id,
                    ValueName = vi.ValueName,
                };

                valAdd.Add(value);
            }
            else
            {
                var value = Resolve<ICategoryPropertyValueService>().GetSingle(vi.Id);
                if (value != null)
                {
                    value.ValueName = vi.ValueName;
                    valUpd.Add(value);
                }
            }
        }

    }
}