using Microsoft.AspNetCore.Http;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Activitys
{
    /// <summary>
    /// IActivity
    /// </summary>
    public interface IActivity
    {
        /// <summary>
        /// get default value.
        /// </summary>
        /// <returns></returns>
        object GetDefaultValue(ActivityEditInput activityEdit, Activity Activity);

        /// <summary>
        /// get auto form
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        AutoForm GetAutoForm(object obj);

        /// <summary>
        ///     Sets the value.
        ///     根据前端页面表单HttpContext设置内容
        ///     設置后的值，使用ReturnObject返回
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        ServiceResult SetValue(HttpContext httpContext);

        /// <summary>
        /// set value
        /// </summary>
        ServiceResult SetValueOfRule(object rules);

        ///// <summary>
        ///// 商品展示用
        ///// <span>标签：重点突出
        ///// </summary>
        ///// <returns></returns>
        //List<string> ProductShow();
    }
}