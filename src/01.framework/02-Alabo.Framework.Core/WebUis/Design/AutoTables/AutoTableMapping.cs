using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alabo.AutoConfigs;
using Alabo.Cache;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.ViewFeatures;

namespace Alabo.UI.AutoTables
{
    public class AutoTableMapping
    {
        public static AutoTable Convert(string fullName)
        {
            var objectCache = Ioc.Resolve<IObjectCache>();
            return objectCache.GetOrSet(() =>
                {
                    var type = fullName.GetTypeByFullName();
                    var classDescription = fullName.GetClassDescription();
                    if (classDescription == null) {
                        return null;
                    }

                    var classPropertyAttribute = classDescription.ClassPropertyAttribute;
                    var auto = new AutoTable
                    {
                        Key = fullName,
                        Name = classPropertyAttribute.Name,
                        ApiUrl = GetApiUrl(type, classDescription.ClassPropertyAttribute.ListApi),
                        Icon = classPropertyAttribute.Icon
                    };

                    #region  如果不包含操作按钮，则不显示操作这一列

                    // 如果不包含操作按钮，则不显示操作这一列
                    auto.TableActions = GetActions(type, classPropertyAttribute); // 操作链接
                    if (auto.TableActions != null &&
                        auto.TableActions.Any(r => r.ActionType == TableActionType.ColumnAction))
                    {
                        // 构建字段
                        var actionColumn = new TableColumn
                        {
                            Label = "操作",
                            Prop = "action"
                        };
                        auto.Columns.Add(actionColumn);
                    }

                    #endregion


                    #region 列相关的字段操作

                    var propertys = classDescription.Propertys;
                    foreach (var item in propertys) {
                        if (item.FieldAttribute != null && item.FieldAttribute.ListShow)
                        {
                            if (item.Name == "Id") {
                                continue;
                            }

                            var tableColumn = new TableColumn
                            {
                                Label = item.DisplayAttribute?.Name,
                                Prop = item.Property.Name.ToFristLower(),
                                Width = item.FieldAttribute.Width
                            };
                            tableColumn = GetType(tableColumn, item.Property, item.FieldAttribute);
                            if (!item.FieldAttribute.Width.IsNullOrEmpty()) {
                                tableColumn.Width = item.FieldAttribute.Width;
                            }

                            auto.Columns.Add(tableColumn);
                        }
                    }

                    #endregion


                    auto.SearchOptions = GetSearchOptions(propertys); // 搜索相关链接
                    auto.Tabs = GetTabs(propertys); // Tabs导航
                    return auto;
                }, fullName + "AutoTable").Value;
        }

        /// <summary>
        ///     获取Api地址
        /// </summary>
        /// <returns></returns>
        private static string GetApiUrl(Type type, string apiUrl)
        {
            var config = Activator.CreateInstance(type);
            // 如果是继承了IAutoTable，则优先使用/Api/Auto/Table接口
            if (config is IAutoTable) {
                return "/Api/Auto/Table";
            }

            if (!apiUrl.IsNullOrEmpty()) {
                return apiUrl;
            }

            if (apiUrl.IsNotNullOrEmpty()) {
                return "/Api/Auto/Table";
            }
            //if (GetTableType(type) == TableType.AutoConfig)
            //{
            //    var url = $"Api/AutoConfig/List?Key={type.FullName}";
            //    return url;
            //}

            return apiUrl;
        }

        private static List<TableAction> GetAction(Type type)
        {
            var list = new List<TableAction>();

            try
            {
                list = type.GetMethod("Actions").Invoke(type.GetInstanceByType(), null) as List<TableAction>;
            }
            catch (Exception ex)
            {
                // 子类 Actions() 可能没实现的情况抛错
            }

            return list;
        }

        public static TableColumn GetType(TableColumn tableColumn, PropertyInfo propertyInfo,
            FieldAttribute fieldAttributes)
        {
            if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
            {
                tableColumn.Type = "enum";
                tableColumn.Options = propertyInfo.Name;
                return tableColumn;
            }

            if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool))
            {
                tableColumn.Type = "bool";
                return tableColumn;
            }

            if (propertyInfo.PropertyType == typeof(long) || propertyInfo.PropertyType == typeof(short) ||
                propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(long))
            {
                tableColumn.Type = "int";
                return tableColumn;
            }

            if (fieldAttributes.ControlsType == ControlsType.ImagePreview ||
                fieldAttributes.ControlsType == ControlsType.AlbumUploder)
            {
                tableColumn.Type = "image";
                return tableColumn;
            }

            if (fieldAttributes.ControlsType == ControlsType.Icon)
            {
                tableColumn.Type = "icon";
                return tableColumn;
            }

            if (!fieldAttributes.Link.IsNullOrEmpty())
            {
                tableColumn.Type = "link";
                return tableColumn;
            }

            if (fieldAttributes.TableDispalyStyle == TableDispalyStyle.Code)
            {
                tableColumn.Type = "code";
                return tableColumn;
            }

            return tableColumn;
        }


        #region 获取操作链接

        /// <summary>
        ///     获取操作链接
        ///     图标        ///
        ///     http://ui.5ug.com/metronic_v5.5.5/theme/default/dist/default/components/icons/flaticon.html
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<TableAction> GetActions(Type type, ClassPropertyAttribute classProperty)
        {
            var instance = Activator.CreateInstance(type);
            var list = new List<TableAction>();
            if (instance is IAutoTable set)
            {
                list = set.Actions();
            }
            else if (instance is IAutoConfig)
            {
                // 如果是自动配置，则只有编辑和删除

                list.Add(new TableAction("编辑", $"/Admin/AutoConfig/Edit?Type={type.Name}"));
                list.Add(new TableAction("删除", "/api/AutoConfig/Delete"));

                // 表格右上角显示快捷操作
                list.Add(new TableAction($"新增{classProperty.Name}", $"/Admin/AutoConfig/Edit?Type={type.Name}",
                    TableActionType.QuickAction));
            }
            else if (instance is IEntity)
            {
                // 如果实体继承了Action,则Action 实体通用删除
                list.Add(new TableAction("编辑", $"/Admin/{type.Name}/Edit"));
                list.Add(new TableAction("删除", $"/Api/{type.Name}/QueryDelete"));
                list.Add(new TableAction($"新增{classProperty.Name}", $"/Admin/{type.Name}/Edit",
                    TableActionType.QuickAction));
            }

            return list;
        }

        #endregion

        #region 获取搜索框

        /// <summary>
        ///     获取搜索框,
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static SearchOptions GetSearchOptions(PropertyDescription[] propertys)
        {
            var list = new List<SearchOptionForm>();
            var advancedList = new List<SearchOptionForm>();
            foreach (var item in propertys) {
                if (item.FieldAttribute != null)
                {
                    var type = item.Property.PropertyType;
                    var form = new SearchOptionForm
                    {
                        Prop = item.Property.Name,
                        Label = item.DisplayAttribute?.Name,
                        ModelValue = type.IsEnum ? type.FullName : item.Name,
                        Type = item.FieldAttribute.ControlsType.ToString(),
                        SortOrder = item.FieldAttribute.SortOrder
                    };
                    if (item.FieldAttribute.IsShowBaseSerach) {
                        list.Add(form);
                    }

                    if (item.FieldAttribute.IsShowAdvancedSerach) {
                        advancedList.Add(form);
                    }
                }
            }

            return new SearchOptions
            {
                Forms = list.OrderBy(l => l.SortOrder).ToList(),
                AdvancedForms = advancedList.OrderBy(l => l.SortOrder).ToList()
            };
        }

        #endregion

        #region 获取标签

        public static List<KeyValue> GetTabs(PropertyDescription[] propertys)
        {
            var result = new List<KeyValue>();
            foreach (var item in propertys)
            {
                var type = item.Property.PropertyType;
                if (item.FieldAttribute != null && item.FieldAttribute.IsTabSearch && type.IsEnum)
                {
                    result = KeyValueExtesions.EnumToKeyValues(type).ToList();
                    if (result != null) {
                        result.ForEach(r => r.Name = item.Name);
                    }

                    break;
                }
            }

            return result;
        }

        #endregion
    }
}