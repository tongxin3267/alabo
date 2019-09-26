using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Activitys.Modules.MemberDiscount.Model
{
    /// <summary>
    /// 会员折扣(会员等级)
    /// </summary>
    [ClassProperty(Name = "会员折扣")]
    public class MemberDiscountActivity : BaseViewModel, IActivity
    {
        /// <summary>
        /// 会员等级
        /// </summary>
        [Display(Name = "设置会员等级价")]
        [Field(ControlsType = ControlsType.JsonList, ListShow = true, EditShow = true)]
        [HelpBlock("如果折扣为空或者0，则不设置折扣")]
        public List<MemberDiscountActivityItem> DiscountList { get; set; } = new List<MemberDiscountActivityItem>();

        /// <summary>
        /// get auto form
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public AutoForm GetAutoForm(object obj)
        {
            //data
            if (obj == null)
            {
                return null;
            }
            var discountList = obj.MapTo<MemberDiscountActivity>()?.DiscountList;
            if (discountList == null || discountList.Count <= 0)
            {
                return null;
            }

            //builder auto form
            var fieldGroups = AutoFormMapping.GetFormFields(discountList).ToList();
            fieldGroups.ForEach(group =>
            {
                var fields = new List<FormFieldProperty>();
                var gradeItem = group.Items.ToList().Find(i => i.Field == "gradeItems");
                var jsonItems = gradeItem?.JsonItems;
                jsonItems.Foreach(item =>
                {
                    var tempField = item.Items.ToList().Find(i => i.Field == "price");
                    if (tempField == null)
                    {
                        return;
                    }
                    tempField.Name = item.Items.ToList().Find(i => i.Field == "name").Value?.ToString();
                    tempField.Field = item.Items.ToList().Find(i => i.Field == "id").Value?.ToString();
                    fields.Add(tempField);
                });
                group.Items.Remove(gradeItem);
                //add
                group.Items.AddRange(fields);
            });
            var autoForm = AutoFormMapping.Convert<MemberDiscountActivity>();
            autoForm.Groups[0].Items[0].JsonItems = fieldGroups;
            return autoForm;
        }

        /// <summary>
        /// get default value
        /// </summary>
        public object GetDefaultValue(ActivityEditInput activityEdit, Activity activity)
        {
            if (activityEdit.ProductId <= 0)
            {
                return null;
            }
            var isDefault = true;
            var result = new MemberDiscountActivity();
            if (!string.IsNullOrWhiteSpace(activity.Value))
            {
                isDefault = false;
                result = JsonConvert.DeserializeObject<MemberDiscountActivity>(activity.Value);
            }
            //grade price
            var productSkus = Resolve<IProductSkuService>().GetGradePrice(activityEdit.ProductId).ToList();
            if (isDefault)
            {
                result.DiscountList = productSkus.Select(g => GetDefaultSku(g)).ToList();
                return result;
            }

            //update data
            var discountList = new List<MemberDiscountActivityItem>();
            productSkus.ForEach(item =>
            {
                var tempDiscount = result.DiscountList.Find(p => p.ProductSkuId == item.Id);
                if (tempDiscount == null)
                {
                    discountList.Add(GetDefaultSku(item));
                }
                else
                {
                    //productSku
                    tempDiscount.GradeItems.ForEach(grade =>
                    {
                        var tempGrade = item.GradePriceList.Find(g => g.Id == grade.Id);
                        if (tempGrade != null)
                        {
                            if (grade.Name != tempGrade.Name)
                            {
                                grade.Name = tempGrade.Name;
                            }
                        }
                        else
                        {
                            tempDiscount.GradeItems.Remove(grade);
                        }
                    });
                    discountList.Add(tempDiscount);
                }
            });
            result.DiscountList = discountList.OrderBy(p => p.ProductSkuId).ToList();

            return result;
        }

        private MemberDiscountActivityItem GetDefaultSku(ProductSku productSku)
        {
            return new MemberDiscountActivityItem
            {
                ProductSkuId = productSku.Id,
                Name = productSku.PropertyValueDesc,
                Bn = productSku.Bn,
                Price = productSku.Price,
                GradeItems = productSku.GradePriceList.MapTo<List<ProductSkuGradeItem>>()
            };
        }

        public ServiceResult SetValue(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// set value
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        public ServiceResult SetValueOfRule(object rules)
        {
            var model = rules.ToObject<MemberDiscountActivity>();
            if (model != null && model.DiscountList.Count > 0)
            {
                //get all grade
                var userGrades = Resolve<IGradeService>().GetUserGradeList().ToList();
                model.DiscountList.ForEach(item =>
                {
                    item.GradeItems.ForEach(grade =>
                    {
                        var tempGrade = userGrades.Find(g => g.Id == grade.Id);
                        if (tempGrade != null)
                        {
                            grade.Name = tempGrade.Name;
                        }
                    });
                });
            }
            var result = new ServiceResult(true);
            result.ReturnObject = model;
            return result;
        }
    }

    /// <summary>
    /// item
    /// </summary>
    public class MemberDiscountActivityItem
    {
        /// <summary>
        /// SkuID
        /// </summary>
        [Display(Name = "SkuID")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public long ProductSkuId { get; set; }

        /// <summary>
        /// 规格属性名称
        /// </summary>
        [Display(Name = "规格属性名称")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public string Name { get; set; }

        /// <summary>
        /// 商品货号
        /// </summary>
        [Display(Name = "商品货号")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public string Bn { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public decimal Price { get; set; }

        /// <summary>
        /// grade items
        /// </summary>
        [Display(Name = "等级价")]
        [Field(ControlsType = ControlsType.JsonList, ListShow = true, EditShow = true)]
        public List<ProductSkuGradeItem> GradeItems { get; set; }
    }

    /// <summary>
    /// grade item
    /// </summary>
    public class ProductSkuGradeItem
    {
        /// <summary>
        /// 会员等级
        /// </summary>
        [Display(Name = "等级ID")]
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, EditShow = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        [Display(Name = "等级名称")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public string Name { get; set; }

        /// <summary>
        /// 会员价
        /// </summary>
        [Display(Name = "会员价")]
        [Field(ControlsType = ControlsType.Decimal, ListShow = true, EditShow = true, Width = "60")]
        public decimal Price { get; set; }
    }
}