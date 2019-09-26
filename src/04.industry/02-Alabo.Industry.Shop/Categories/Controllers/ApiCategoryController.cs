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
        ///     ������Ʒ�����첽�������������
        /// </summary>
        /// <param name="categoryId"></param>
        [HttpGet]
        public ApiResult<Domain.Entities.Category> ProductProperty(string categoryId)
        {

            var category = Resolve<ICategoryService>().GetSingle(Guid.Parse(categoryId));
            return ApiResult.Success(category);
        }

        /// <summary>
        ///     ������Ʒ�����첽�������������
        /// </summary>
        /// <param name="categoryId"></param>
        [HttpGet]
        public ApiResult<CategoryView> GetView(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return ApiResult.Failure<CategoryView>("��ĿID����Ϊ��");
            }

            var category = Resolve<ICategoryService>().GetSingle(categoryId);
            //���������� ��С����
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
            //���������� ��С����
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
        /// ������Ŀ
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Save([FromBody]CategoryView view)
        {
            //���idΪ�վ��ж��Ƿ����
            if (view.Id != Guid.Empty)
            {
                //�ж��ظ�
                var confrim = Resolve<ICategoryService>().Count(s => s.Name == view.Name && s.PartentId != view.PartentId);
                if (confrim > 0)
                {
                    return ApiResult.Failure("�Ѵ���ͬ����Ŀ!");
                }
            }
            else
            {

                //�ж��ظ�
                var confrim = Resolve<ICategoryService>().Count(s => s.Name == view.Name);
                if (confrim > 0)
                {
                    return ApiResult.Failure("�Ѵ���ͬ����Ŀ!");
                }
            }
            //��������
            var saleDistinct = view.SalePropertys.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
            if (saleDistinct.Count() < view.SalePropertys.Count) {
                return ApiResult.Failure("�Ѵ���ͬ����������!");
            }

            foreach (var item in view.SalePropertys)
            {
                ///��������ֵ
                var saleItemDistinct = item.Values.GroupBy(p => new { p.ValueName }).Select(g => g.First()).ToList();
                if (saleItemDistinct.Count() < item.Values.Count) {
                    return ApiResult.Failure("�Ѵ���ͬ����������ֵ!");
                }
            }

            //��Ʒ����
            var displayDistinct = view.DisplayPropertys.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
            if (displayDistinct.Count() < view.DisplayPropertys.Count) {
                return ApiResult.Failure("�Ѵ���ͬ����Ʒ����!");
            }

            foreach (var item in view.DisplayPropertys)
            {
                ///��������ֵ
                var saleItemDistinct = item.Values.GroupBy(p => new { p.ValueName }).Select(g => g.First()).ToList();
                if (saleItemDistinct.Count() < item.Values.Count) {
                    return ApiResult.Failure("�Ѵ���ͬ����Ʒ����ֵ!");
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
                //��Ҫɾ������������
                var delSalePropertyIdList = propertyIdList.Except(view.SalePropertys.Select(x => x.Id.ToString()));
                var delSalePropertyList = Resolve<ICategoryPropertyService>().GetList(string.Join(',', delSalePropertyIdList));
                propertyDel.AddRange(delSalePropertyList);
                //��Ҫɾ������Ʒ����
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
            //��ɾ������ֵ
            Resolve<ICategoryPropertyValueService>().DeleteMany(valDel.Select(x => x.Id));
            //��ɾ������
            Resolve<ICategoryPropertyService>().DeleteMany(propertyDel.Select(x => x.Id));
            // Resolve<ICategoryPropertyValueService>().DeleteMany(valDel.Select(x => x.Id));
            foreach (var item in valUpd)
            {
                Resolve<ICategoryPropertyValueService>().Update(item);
            }
            Resolve<ICategoryPropertyValueService>().AddMany(valAdd);

            return ApiResult.Success("����ɹ�!");
        }

        /// <summary>
        /// ɾ����Ŀ
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
                return ApiResult.Failure("��Ŀ������Ʒ,����ɾ��!");
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