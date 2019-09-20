using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alabo.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        ///     将List对象绑定SelectListItem中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static List<SelectListItem> ToSelectList<T>(this List<T> t, string text, string value)
        {
            var selectListItem = new List<SelectListItem>();
            foreach (var item in t)
            {
                var propers = item.GetType().GetProperty(text);
                var valpropers = item.GetType().GetProperty(value);
                selectListItem.Add(new SelectListItem
                {
                    Text = propers.GetValue(item, null).ToString(),
                    Value = valpropers.GetValue(item, null).ToString()
                });
            }

            return selectListItem;
        }
    }
}