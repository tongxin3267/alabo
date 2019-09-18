using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Cache;
using Alabo.Core.Extensions;
using Alabo.Core.Files;
using Alabo.Core.Helpers;
using Alabo.Core.Regex;
using Alabo.Core.UI;
using Alabo.Core.UI.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.ViewFeatures;
using Convert = System.Convert;
using Enum = System.Enum;

namespace Alabo.App.Core.Themes.Extensions {

    /// <summary>
    ///     表单扩展
    /// </summary>
    public static class TableExtensions {

        /// <summary>
        ///     构建动态表格
        ///     样式美化说明:
        ///     1.
        ///     如果数据中，包括UserId,可以在ViewModel中定义UserName，则输出时会自动加上用户链接以及等级信息：参考http://localhost:9011/Admin/Basic/List//?Service=ITaskQueueService
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="model">The model.</param>
        /// <param name="propertys">The propertys.</param>
        /// <param name="tableStyle">The table style.</param>
        public static IHtmlContent Table(this IHtmlHelper html, object model,
            IEnumerable<PropertyDescription> propertys, TableStyle tableStyle = TableStyle.Default) {
            var pathUrl = html.ViewContext.HttpContext.Request.Path.ToString(); //当前访问的Url
            var pageList = (IEnumerable<object>)model;

            if (propertys.ToList().Count < 1) {
                return html.ErrorMessage("未设置Feild相关的特性，或者属性值为空,并且ListShow=ture");
            }

            //类型
            var baseViewModelType = propertys.FirstOrDefault().ClassType;
            var classDescription = new ClassDescription(baseViewModelType);
            var htmlTable = GetDefaultTableFromCache(baseViewModelType.FullName, propertys, pageList.FirstOrDefault(),
                classDescription);

            var contentBuilder = new StringBuilder();

            // 没有内容的情况下
            if (pageList.ToList().Count == 0) {
                contentBuilder.Append(
                    $"<tr><td colspan='{htmlTable.Item2.Where(r => r.Key != "Id").ToList().Count - 2}'><span class='m-datatable--error'style='width: 100%;'>暂无数据</span></td></tr>");
            } else {
                var i = 0;
                foreach (var data in pageList) {
                    if (data == null) {
                        continue;
                    }

                    // 获取主字段显示名称
                    var mainKeyProperty = propertys.ToList().FirstOrDefault(r => r.Name == htmlTable.Item3);
                    //if (mainKeyProperty == null) {
                    //    return html.ErrorMessage("未设置主字段IsMain = true的特性，可设置标题或名称等的");
                    //}

                    var mainKeyValue = mainKeyProperty?.Property?.GetValue(data)?.ToStr();

                    // 获取Id的值
                    var IdKeyProperty = propertys.ToList().FirstOrDefault(r => r.Name == "Id");
                    var idValue = string.Empty;
                    if (IdKeyProperty != null) {
                        idValue = IdKeyProperty.Property.GetValue(data).ToStr();
                    }

                    var trClass = i % 2 != 0 ? "m-datatable__row--even" : string.Empty;
                    contentBuilder.Append(
                        $"<tr data-row='{i}' class='m-datatable__row {trClass}' style='height: 54px;'>");
                    contentBuilder.Append(
                        "<td data-field='RecordID' class='m-datatable__cell--center m-datatable__cell m-datatable__cell--check' style='width: 50px;'><span style='width: 50px;'><label class='m-checkbox m-checkbox--single m-checkbox--solid m-checkbox--brand'><input type='checkbox' value='1'><span></span></label></span></td>");
                    foreach (var item in htmlTable.Item2) {
                        var property = propertys.ToList().FirstOrDefault(r => r.Name == item.Key);
                        if (property == null) {
                            return html.ErrorMessage($"属性{item.Key}获取失败，请检查特性设置 ");
                        }

                        if (property.Property.PropertyType.FullName.Contains("System.Collections.Generic.Dictionary")) {
                            var keyList = item.Value.DeserializeJson<List<object>>();
                            var valueDic = property.Property.GetValue(data);
                            if (valueDic != null) {
                                try {
                                    var dicValues = valueDic.ToJson().DeserializeJson<Dictionary<object, object>>();
                                    foreach (var key in keyList) {
                                        dicValues.TryGetValue(key, out var dicValue);
                                        var dicWidth = property.FieldAttribute.Width;
                                        var _filedName = property.Property.Name;
                                        var tdDicContent =
                                            $@"<td data-field='{_filedName}{
                                                    key
                                                }' class='m-datatable__cell' style='width: {
                                                    dicWidth
                                                }px;'><span style='width: {dicWidth}px;'><code>{
                                                    dicValue
                                                }</code></span></td>";
                                        contentBuilder.Append(tdDicContent);
                                    }
                                } catch (Exception ex) {
                                    Console.WriteLine(ex.Message);
                                }
                            }

                            continue;
                        }

                        //如果是Id字段则不显示
                        if (item.Key.Equals("Id", StringComparison.OrdinalIgnoreCase)) {
                            continue;
                        }

                        var value = property.Property.GetValue(data);
                        if (property.FieldAttribute.ControlsType == ControlsType.ChildList) {
                            var lastIndex = pathUrl.LastIndexOf("/");
                            var childPathUrl = pathUrl.Substring(0, lastIndex);
                            // 目前只针对基础BaseController'T的基类有效
                            value =
                                $"<a href='{childPathUrl}/ChildList?Id={idValue}&field={property.Property.Name}'><i class='fa fa-eye'>{property.DisplayAttribute.Name}</i></a>"; // 字列表形式
                        } else if (property.FieldAttribute.TableDispalyStyle == TableDispalyStyle.Code) {
                            value = $"<code>{value}</code>"; // code方式显示
                        } else if (Convert.ToInt16(property.FieldAttribute.LabelColor) != 0) {
                            value =
                                $"<span  class='m-badge  m-badge--{property.FieldAttribute.LabelColor.ToString().ToLower()} m-badge--wide'>{value}</span>"; // code方式显示
                        } else if (property.Property.PropertyType.GetTypeInfo().Name == nameof(DateTime)) {
                            value = Convert.ToDateTime(value).ToTimeString(true);
                        } else if (property.Property.PropertyType.GetTypeInfo().Name == nameof(Int16) ||
                                   property.Property.PropertyType.GetTypeInfo().Name == nameof(Int64) ||
                                   property.Property.PropertyType.GetTypeInfo().Name == nameof(Int32)) {
                            value = html.Style(value, LabelStyle.Badges, LabelColor.Primary);
                        } else if (property.Property.PropertyType.GetTypeInfo().Name == nameof(Boolean) ||
                                   property.Property.PropertyType.GetTypeInfo().GetType() == typeof(bool)) {
                            value = ((bool)value).GetHtmlName();
                        } else if (property.Property.PropertyType.GetTypeInfo().Name == nameof(Enum) ||
                                   property.Property.PropertyType.BaseType == typeof(Enum) ||
                                   property.Property.PropertyType.GetTypeInfo().GetType() == typeof(Enum)) {
                            value = ((Enum)value).GetHtmlName();
                        } else if (RegexHelper.CheckMobile(value.ToStr())) {
                            value =
                                $"<a href='/Admin/Message/SendMessage?mobile={value}'><i class='fa fa-mobile-phone'>{value}</i></a>";
                        } else if (RegexHelper.CheckEmail(value.ToStr())) {
                            value = $"<a href='mailto:{value}'><i class='fa 	fa-envelope-o'>{value}</i></a>";
                        } else if (property.FieldAttribute.IsImagePreview) {
                            value =
                                $"<img src='{ImageExtensions.GetSmallUrl(value.ToString())}' width='48' height='48' class='user-pic rounded'>";
                        } else if (property.FieldAttribute.DisplayMode == DisplayMode.Grade) {
                            // 等级处理
                            var grade = Ioc.Resolve<IGradeService>()
                                .GetGrade(value.ToString().ToGuid());
                            value = $"<span class='m-badge  m-badge--wide m-badge--default'>{grade?.Name}</span";
                        } else if (!property.FieldAttribute.Link.IsNullOrEmpty()) {
                            // 链接地址格式
                            object keyValue = string.Empty;
                            var link = GetLink(property.FieldAttribute.Link, classDescription, data, out keyValue);
                            if (link.IsNullOrEmpty()) {
                                return html.ErrorMessage(
                                    $"字段{item.Key}的链接特性设置不正常,或参数没有设置ListShow=true属性，或参数值为空。设置格式：{property.FieldAttribute.Link}，请参考其他地方设置");
                            }

                            value = $"<a href='{link}'>{value}</i></a>";
                        }

                        var tdContent = item.Value.Replace("{TdContent}", value.ToStr());
                        contentBuilder.Append(tdContent);
                    }

                    contentBuilder.Append("<td data-field='actions' class='m-datatable__cell' style='width: 110px;'>");
                    //contentBuilder.Append($@"<span style='width: 110px;'>");

                    contentBuilder.Append("</div></div></span>");
                    contentBuilder.Append(@"<div class='btn-group'>
                        <a class='btn green-haze btn-outline btn-circle btn-sm' href='javascript:;' data-toggle='dropdown'> <i class='la la-cog'></i>操作<i class='fa fa-angle-down'></i></a>
                        <ul class='dropdown-menu'>");

                    // 操作链接
                    foreach (var item in classDescription.ViewLinks.Where(r =>
                        r.LinkType == LinkType.ColumnLink || r.LinkType == LinkType.ColumnLinkDelete)) {
                        object keyValue = string.Empty;
                        var link = GetLink(item.Url, classDescription, data, out keyValue);
                        if (item.LinkType == LinkType.ColumnLinkDelete) {
                            contentBuilder.Append(
                                $" <li><a class='primary-link margin-8'  data-toggle='modal' data-target='#zkcloud_delete'  ' onclick=\"DeleteData('{keyValue}','{mainKeyValue}','{link}')\" ><i class='{item.Icon}'></i>{item.Name}</a></li>");
                        } else {
                            contentBuilder.Append(
                                $" <li><a class='primary-link margin-8'  href='{link}'><i class='{item.Icon}'></i>{item.Name}</a></li>");
                        }
                    }

                    contentBuilder.Append(@" <li class='divider'> </li> </ul> </div>");
                    contentBuilder.Append("</td></tr>");
                    i++;
                }
            }

            var htmlContnet = htmlTable.Item1.Replace("{{TbodyContent}}", contentBuilder.ToString());
            return html.Raw(htmlContnet);
        }

        /// <summary>
        ///     获取s the default table from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="propertys">The propertys.</param>
        /// <param name="model"></param>
        /// <param name="classDescription"></param>
        private static Tuple<string, IDictionary<string, string>, string> GetDefaultTableFromCache(string key,
            IEnumerable<PropertyDescription> propertys, object model, ClassDescription classDescription) {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            var tupleResult = Tuple.Create(string.Empty, dictionary, string.Empty);
            var cacheKey = "TableExtensionTh_" + key;
            if (!Ioc.Resolve<IObjectCache>().TryGet(cacheKey, out tupleResult)) {

                #region 模板

                var stringBuilder = new StringBuilder();
                stringBuilder.Append(
                    "<table class='m-datatable__table' style='display: block; min-height: 300px;  overflow-x:auto;'>");
                stringBuilder.Append(@"<thead class='m-datatable__head'>
            <tr class='m-datatable__row' style='height: 55px;'>
                <th data-field='RecordID' class='m-datatable__cell--center m-datatable__cell m-datatable__cell--check' style='width: 50px;'>
                    <span style='width: 50px;'>
                        <label class='m-checkbox m-checkbox--single m-checkbox--all m-checkbox--solid m-checkbox--brand'>
                            <input type='checkbox'><span></span>
                        </label>
                    </span>
                </th>
                {{ThContent}}
                <th data-field='Actions' class='m-datatable__cell m-datatable__cell--sort'  style='width: 110px;'><span style='width: 110px;'>操作</span></th>
            </tr>
            </thead>");

                #endregion 模板

                var mainKey = string.Empty; // 主字段名称
                var thBuilder = new StringBuilder();
                foreach (var item in propertys) {
                    var _width = item.FieldAttribute.Width.ConvertToLong(120);
                    var _name = item.DisplayAttribute.Name;
                    //获取主字段显示名称
                    if (item.FieldAttribute.IsMain) {
                        mainKey = item.Name;
                    }

                    //如果是Id字段则不显示
                    if (item.Property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)) {
                        continue;
                    }

                    var _filedName = item.Name;

                    #region 如果字段是字典类型

                    // 如果字段是字典类型
                    if (item.Property.PropertyType.FullName.Contains("System.Collections.Generic.Dictionary")) {

                        #region 字典类型处理

                        if (!item.FieldAttribute.DataSource.IsNullOrEmpty()) {
                            var dictionarySource = new Dictionary<string, object>();
                            //如果Mark标记为Type，表示DataSource是类型的全命名空间，参考ViewActivityPage.ConfigDictionary的设置
                            //ActivityService.GetPageList的实现
                            if (item.FieldAttribute.Mark == "Type") {
                                var typePropertyName = item.FieldAttribute.DataSource;
                                var typeProperty =
                                    classDescription.Propertys.FirstOrDefault(r => r.Name == typePropertyName);
                                if (typeProperty != null) {
                                    //var type = (Type)typeProperty.Property.GetValue(model);
                                    var type = propertys.FirstOrDefault().ClassType;
                                    var listPropertys = Ioc.Resolve<IClassService>()
                                        .GetListPropertys(type.FullName);
                                    listPropertys.Foreach(r => {
                                        dictionarySource.Add(r.Name, r.DisplayAttribute.Name);
                                    });
                                }
                            } else {
                                dictionarySource = Ioc.Resolve<ITypeService>()
                                    .GetAutoConfigDictionary(item.FieldAttribute.DataSource);
                            }

                            var keyList = new List<object>();
                            var tdList = new List<string>();
                            foreach (var dic in dictionarySource) {
                                var th =
                                    $@"<th data-field='{
                                            dic.Key
                                        }' class='m-datatable__cell m-datatable__cell--sort'  style='width: {
                                            _width
                                        }px;'><span style='width: {_width}px;'>{dic.Value}</span></th>";
                                var td =
                                    $@"<td data-field='{_filedName}{dic.Key}' class='m-datatable__cell' style='width: {
                                            _width
                                        }px;'><span style='width: {_width}px;'>{{TdContent}}</span></td>";

                                keyList.Add(dic.Key);
                                tdList.Add(td);
                                thBuilder.Append(th);
                            }

                            var tuple = new Tuple<List<string>, List<object>>(tdList, keyList);
                            dictionary.Add(_filedName, keyList.ToJson());
                        }

                        #endregion 字典类型处理
                    } else {
                        var th =
                            $@"<th data-field='{
                                    _filedName
                                }' class='m-datatable__cell m-datatable__cell--sort'  style='width: {
                                    _width
                                }px;'><span style='width: {_width}px;'>{_name}</span></th>";
                        var td =
                            $@"<td data-field='{_filedName}' class='m-datatable__cell' style='width: {
                                    _width
                                }px;'><span style='width: {_width}px;'>{{TdContent}}</span></td>";
                        dictionary.Add(_filedName, td);
                        thBuilder.Append(th);
                    }

                    #endregion 如果字段是字典类型
                }

                #region 模板替换

                stringBuilder.Append("<tbody class='m-datatable__body' style=''>");
                stringBuilder.Append("{{TbodyContent}}");
                stringBuilder.Append(" </tbody></table>");
                var html = stringBuilder.ToString().Replace("{{ThContent}}", thBuilder.ToString());

                tupleResult = Tuple.Create(html, dictionary, mainKey);
                Ioc.Resolve<IObjectCache>().Set(cacheKey, tupleResult);

                #endregion 模板替换
            }

            return tupleResult;
        }

        #region 获取链接地址

        /// <summary>
        ///     获取链接地址
        /// </summary>
        /// <param name="linkTemplate">The link template.</param>
        /// <param name="classDescription">The class description.</param>
        /// <param name="data">The data.</param>
        /// <param name="key">主键值</param>
        private static string GetLink(string linkTemplate, ClassDescription classDescription, object data,
            out object key) {
            var filedName = StringHelper.SubstringBetween(linkTemplate, "[[", "]]");
            object keyValue = string.Empty;
            //支持多个变量替换
            while (!filedName.IsNullOrEmpty()) {
                var property = classDescription.Propertys.ToList()
                    .FirstOrDefault(r => r.Name.Equals(filedName, StringComparison.OrdinalIgnoreCase));
                if (property == null) {
                    key = keyValue;
                    return linkTemplate;
                }

                var value = property.Property.GetValue(data);
                keyValue = value;
                if (value.IsNullOrEmpty()) {
                    key = keyValue;
                    return linkTemplate;
                }

                linkTemplate = linkTemplate.Replace($"[[{filedName}]]", value.ToStr());
                filedName = StringHelper.SubstringBetween(linkTemplate, "[[", "]]");
            }

            key = keyValue;
            return linkTemplate;
        }

        #endregion 获取链接地址
    }
}