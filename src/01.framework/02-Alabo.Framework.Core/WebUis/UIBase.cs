using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Cache;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Core.WebUis.Design.AutoLists;
using Alabo.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.Core.WebUis
{
    public abstract class UIBase : BaseViewModel, IUI
    {
        #region 转换成自动表单

        /// <summary>
        ///     转换成自动表单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected AutoForm ToAutoForm(object model)
        {
            if (model != null) {
                return AutoFormMapping.Convert(model);
            }

            return null;
        }

        #endregion 转换成自动表单

        #region 转换成Id

        /// <summary>
        ///     转换成Id
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected TKey ToId<TKey>(object id)
        {
            if (typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long)) {
                id = id.ConvertToLong(0);
            }

            if (typeof(TKey) == typeof(Guid)) {
                id = id.ConvertToGuid();
            }

            if (typeof(TKey) == typeof(string)) {
                id = id.ToStr();
            }

            if (typeof(TKey) == typeof(ObjectId)) {
                id = ObjectId.Parse(id.ToStr());
            }

            return (TKey) id;
        }

        #endregion 转换成Id

        #region 将前台URL查询参数转化成对象

        /// <summary>
        ///     将前台URL查询参数转化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToQuery<T>() where T : new()
        {
            var query = HttpWeb.HttpContext.ToDictionary();
            var model = query.ToJson().ToObject<T>();
            return model;
        }

        #endregion 将前台URL查询参数转化成对象


        #region 转换成删除链接

        /// <summary>
        ///     转换成删除链接
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TableAction ToDeleteAction(string name, string url)
        {
            var tableAction = new TableAction
            {
                Name = name,
                Url = url,
                IconType = Flaticon.Delete,
                Type = ActionLinkType.Delete
            };
            return tableAction;
        }

        #endregion 转换成访问链接


        #region 转换成删除链接

        /// <summary>
        ///     转换成删除链接
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TableAction ToDialogAction(string name, string url, Flaticon flaticon = Flaticon.Dashboard)
        {
            var tableAction = new TableAction
            {
                Name = name,
                Url = url,
                IconType = flaticon,
                Type = ActionLinkType.Dialog
            };
            return tableAction;
        }

        #endregion 转换成访问链接


        #region 转换成PageList对象

        /// <summary>
        ///     转换成前端AutoList对象
        /// </summary>
        /// <typeparam name="TOutput">输出类型</typeparam>
        /// <param name="pageList">数据源</param>
        /// <returns></returns>
        protected PageResult<AutoListItem> ToPageList<TInput>(IEnumerable<AutoListItem> list,
            PagedList<TInput> pageList)
        {
            if (pageList == null) {
                return null;
            }

            var apiRusult = new PageResult<AutoListItem>
            {
                PageCount = pageList.PageCount,
                Result = list.ToList(),
                RecordCount = pageList.RecordCount,
                CurrentSize = pageList.CurrentSize,
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize
            };
            return apiRusult;
        }

        #endregion

        #region 基础对象

        /// <summary>
        ///     获取数据操作对象服务
        /// </summary>
        public T Repository<T>() where T : IRepository
        {
            return Ioc.Resolve<T>();
        }

        /// <summary>
        ///     缓存
        /// </summary>
        [JsonIgnore]
        [BsonIgnore]
        public IObjectCache ObjectCache => Ioc.Resolve<IObjectCache>();

        #endregion 基础对象

        #region 转换成前端分页类类型

        /// <summary>
        ///     转换成前端分页类类型
        /// </summary>
        /// <typeparam name="TInput">输入类型</typeparam>
        /// <typeparam name="TOutput">输出类型</typeparam>
        /// <param name="pageList">数据源</param>
        /// <returns></returns>
        protected PageResult<TOutput> ToPageResult<TOutput, TInput>(PagedList<TInput> pageList)
        {
            if (pageList == null) {
                return null;
            }

            var newPageList = new PagedList<TOutput>();
            pageList.ForEach(r =>
            {
                var t = AutoMapping.SetValue<TOutput>(r);
                newPageList.Add(t);
            });
            var apiRusult = new PageResult<TOutput>
            {
                PageCount = pageList.PageCount,
                Result = newPageList,
                RecordCount = pageList.RecordCount,
                CurrentSize = pageList.CurrentSize,
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize
            };
            return apiRusult;
        }

        /// <summary>
        ///     转换成前端分页类类型
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <param name="pageList">数据源</param>
        /// <returns></returns>
        protected PageResult<T> ToPageResult<T>(PagedList<T> pageList)
        {
            if (pageList == null) {
                return null;
            }

            var apiRusult = new PageResult<T>
            {
                PageCount = pageList.PageCount,
                Result = pageList,
                RecordCount = pageList.RecordCount,
                CurrentSize = pageList.CurrentSize,
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize
            };
            return apiRusult;
        }

        #endregion 转换成前端分页类类型


        #region 转换成访问链接

        /// <summary>
        ///     转换成访问链接
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TableAction ToLinkAction(string name, string url, Flaticon flaticon = Flaticon.Interface5)
        {
            var tableAction = new TableAction
            {
                Name = name,
                Url = url,
                IconType = flaticon,
                Type = ActionLinkType.Link
            };
            return tableAction;
        }

        public static TableAction ToLinkAction(string name, TableActionType tableActionType, Type formType,
            Flaticon flaticon = Flaticon.Interface5)
        {
            var tableAction = new TableAction
            {
                Name = name,
                FormType = formType.Name,
                IconType = flaticon,
                Type = ActionLinkType.Dialog,
                ActionType = tableActionType
            };
            return tableAction;
        }


        public static TableAction ToLinkAction(string name, string url, TableActionType tableActionType,
            Flaticon flaticon = Flaticon.Interface5)
        {
            var tableAction = new TableAction
            {
                Name = name,
                Url = url,
                IconType = flaticon,
                Type = ActionLinkType.Link,
                ActionType = tableActionType
            };
            return tableAction;
        }


        public static TableAction ToLinkAction(string name, string url, ActionLinkType linkType,
            TableActionType tableActionType, Flaticon flaticon = Flaticon.Interface5)
        {
            var tableAction = new TableAction
            {
                Name = name,
                Url = url,
                IconType = flaticon,
                Type = linkType,
                ActionType = tableActionType
            };
            return tableAction;
        }

        #endregion 转换成访问链接
    }
}