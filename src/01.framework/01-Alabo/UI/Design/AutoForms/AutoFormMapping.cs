using Alabo.Cache;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.Web.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.UI.Design.AutoForms
{
    public static class AutoFormMapping
    {
        /// <summary>
        ///     get generic type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type GetGenericType(Type type)
        {
            if (type.IsGenericType) {
                return type.GenericTypeArguments[0];
            }

            return type;
        }

        #region convert from type name

        /// <summary>
        ///     将视图转成AutoForm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static AutoForm Convert<T>()
        {
            return Convert(typeof(T).FullName);
        }

        /// <summary>
        ///     将视图转成AutoForm
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static AutoForm Convert(string fullName)
        {
            var objectCache = Ioc.Resolve<IObjectCache>();
            return objectCache.GetOrSet(() =>
                {
                    var classDescription = fullName.GetClassDescription();
                    if (classDescription != null)
                    {
                        var classPropertyAttribute = classDescription.ClassPropertyAttribute;
                        var auto = new AutoForm
                        {
                            Key = fullName,
                            Name = classPropertyAttribute.Name,
                            BottonText = classPropertyAttribute.ButtonText,
                            ViewLinks = classDescription.ViewLinks,
                            Title = classPropertyAttribute.Name,
                            Service = new FormService(classPropertyAttribute.PostApi,
                                classPropertyAttribute.SuccessReturn)
                        };
                        // 构建字段

                        var groups = classPropertyAttribute.GroupName.Split(",");
                        var propertys = classDescription.Propertys;

                        if (groups.Length == 0)
                        {
                            var fieldGroup = new FieldGroup { GroupName = "default" };
                            fieldGroup.Items.AddRange(GetFormFieds(propertys));
                            auto.Groups.Add(fieldGroup);
                        }
                        else
                        {
                            // 多标签
                            long i = 0;
                            foreach (var group in groups)
                            {
                                i++;
                                var fieldGroup = new FieldGroup();
                                fieldGroup.GroupName = group;
                                var groupPropertys = propertys.Where(r => r.FieldAttribute?.GroupTabId == i).ToArray();
                                if (group.Trim().IsNullOrEmpty()) {
                                    groupPropertys = propertys;
                                }

                                fieldGroup.Items.AddRange(GetFormFieds(groupPropertys));
                                auto.Groups.Add(fieldGroup);
                            }
                        }

                        return auto;
                    }

                    return null;
                }, fullName + "AutoForm").Value;
        }

        /// <summary>
        ///     字段转换
        /// </summary>
        /// <param name="propertys"></param>
        private static IList<FormFieldProperty> GetFormFieds(PropertyDescription[] propertys)
        {
            var resultFieldProperties = new List<FormFieldProperty>();
            foreach (var item in propertys)
            {
                if (item.FieldAttribute == null || !item.FieldAttribute.EditShow) {
                    continue;
                }

                var formField = AutoMapping.SetValue<FormFieldProperty>(item.FieldAttribute);
                formField.Name = item.DisplayAttribute?.Name;
                formField.Field = item.Property.Name.ToCamelCaseString();
                formField.Type = item.FieldAttribute.ControlsType;
                formField.Maxlength = item.MaxLengthAttribute?.Length;
                formField.MinLength = item.MinLengthAttribute?.Length;
                if (item.RequiredAttribute != null) {
                    formField.Required = true;
                }

                if (formField.PlaceHolder.IsNullOrEmpty()) {
                    formField.PlaceHolder = "请输入" + formField.Name;
                }

                if (!item.FieldAttribute.Width.IsNullOrEmpty()) {
                    formField.Width = item.FieldAttribute.Width;
                }

                formField.HelpBlock = item.HelpBlockAttribute?.HelpText;
                if (formField.HelpBlock.IsNullOrEmpty()) {
                    formField.HelpBlock = formField.PlaceHolder;
                }

                formField.ListShow = item.FieldAttribute.ListShow;
                formField.EditShow = item.FieldAttribute.EditShow;

                if (!item.FieldAttribute.ApiDataSource.IsNullOrEmpty()) {
                    formField.DataSource = item.FieldAttribute.ApiDataSource;
                }
                // 如果是枚举类型
                if (item.Property.PropertyType.BaseType == typeof(System.Enum)) {
                    formField.DataSource =
                        $"Api/Common/GetKeyValuesByEnum?type={item.Property.PropertyType.FullName}";
                }

                //Json类型
                if (formField.Type == ControlsType.JsonList)
                {
                    var propertyType = item.Property.PropertyType;
                    if (propertyType.IsGenericType)
                    {
                        var genericTypeName = GetGenericType(propertyType).FullName;
                        var genericPropertys = genericTypeName.GetClassDescription().Propertys;
                        formField.JsonItems.Add(GetGroup(genericPropertys));
                    }
                }

                resultFieldProperties.Add(formField);
            }

            return resultFieldProperties;
        }

        /// <summary>
        ///     get group
        /// </summary>
        /// <param name="propertys"></param>
        /// <returns></returns>
        private static FieldGroup GetGroup(PropertyDescription[] propertys)
        {
            var fieldGroup = new FieldGroup { GroupName = "default" };
            fieldGroup.Items.AddRange(GetFormFieds(propertys));
            return fieldGroup;
        }

        #endregion convert from type name

        #region convert from object

        /// <summary>
        ///     convert to auto form from model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AutoForm Convert(object model)
        {
            //cache class descrption
            var fullName = model.GetType().FullName;
            var classDescription = fullName.GetClassDescription();
            if (classDescription == null) {
                return null;
            }

            var classPropertyAttribute = classDescription.ClassPropertyAttribute;
            var auto = new AutoForm
            {
                Key = fullName,
                Name = classPropertyAttribute.Name,
                BottonText = classPropertyAttribute.ButtonText,
                ViewLinks = classDescription.ViewLinks,
                Title = classPropertyAttribute.Name,
                Service = new FormService(classPropertyAttribute.PostApi, classPropertyAttribute.SuccessReturn)
            };
            // builder filed
            var groups = classPropertyAttribute.GroupName.Split(",");
            var propertys = classDescription.Propertys;

            if (groups.Length == 0)
            {
                var fieldGroup = new FieldGroup { GroupName = "default" };
                fieldGroup.Items.AddRange(GetFormFieds(propertys, model));
                auto.Groups.Add(fieldGroup);
            }
            else
            {
                //group
                long i = 0;
                foreach (var group in groups)
                {
                    i++;
                    var fieldGroup = new FieldGroup();
                    fieldGroup.GroupName = group;
                    var groupPropertys = propertys.Where(r => r.FieldAttribute?.GroupTabId == i).ToArray();
                    if (group.Trim().IsNullOrEmpty()) {
                        groupPropertys = propertys;
                    }

                    fieldGroup.Items.AddRange(GetFormFieds(groupPropertys, model));
                    auto.Groups.Add(fieldGroup);
                }
            }

            return auto;
        }

        /// <summary>
        ///     get form fields
        /// </summary>
        /// <param name="obj">list类型</param>
        public static IList<FieldGroup> GetFormFields(object obj)
        {
            var result = new List<FieldGroup>();
            var propertyType = obj.GetType();
            if (!propertyType.IsGenericType) {
                return result;
            }

            var genericTypeName = GetGenericType(propertyType).FullName;
            var propertyDescriptions = genericTypeName.GetClassDescription().Propertys;
            var propertyValue = obj as dynamic;
            if (propertyValue != null && propertyValue.Count > 0)
            {
                for (var i = 0; i < propertyValue.Count; i++)
                {
                    var group = GetGroup(propertyDescriptions, propertyValue[i]);
                    result.Add(group);
                }
            }
            else
            {
                var group = GetGroup(propertyDescriptions);
                result.Add(group);
            }

            return result;
        }

        /// <summary>
        ///     builder property
        /// </summary>
        /// <param name="propertys"></param>
        private static IList<FormFieldProperty> GetFormFieds(PropertyDescription[] propertys, object model)
        {
            var result = new List<FormFieldProperty>();
            var propertyInfos = model.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                //find property description
                var propertyDesc = propertys.ToList().Find(p => p.Property.Name == propertyInfo.Name);
                if (propertyDesc == null
                    || propertyDesc.FieldAttribute == null
                    || !propertyDesc.FieldAttribute.EditShow) {
                    continue;
                }

                //builder
                var formField = AutoMapping.SetValue<FormFieldProperty>(propertyDesc.FieldAttribute);
                formField.Name = propertyDesc.DisplayAttribute?.Name;
                formField.Field = propertyDesc.Property.Name.ToCamelCaseString();
                formField.Type = propertyDesc.FieldAttribute.ControlsType;
                formField.Maxlength = propertyDesc.MaxLengthAttribute?.Length;
                formField.MinLength = propertyDesc.MinLengthAttribute?.Length;
                formField.HelpBlock = propertyDesc.HelpBlockAttribute?.HelpText;
                formField.ListShow = propertyDesc.FieldAttribute.ListShow;
                formField.EditShow = propertyDesc.FieldAttribute.EditShow;
                formField.Mark = propertyDesc.FieldAttribute.Mark;

                if (propertyDesc.RequiredAttribute != null) {
                    formField.Required = true;
                }

                if (formField.PlaceHolder.IsNullOrEmpty()) {
                    formField.PlaceHolder = "请输入" + formField.Name;
                }

                if (!propertyDesc.FieldAttribute.Width.IsNullOrEmpty()) {
                    formField.Width = propertyDesc.FieldAttribute.Width;
                }

                if (!propertyDesc.FieldAttribute.ApiDataSource.IsNullOrEmpty())
                {
                    formField.DataSource = propertyDesc.FieldAttribute.ApiDataSource;
                }
                else
                {
                    if (propertyDesc.FieldAttribute.DataSourceType != null)
                    {
                        formField.DataSource =
                            $"Api/Type/GetKeyValue?type={propertyDesc.FieldAttribute.DataSourceType.Name}";
                    }
                    else
                    {
                        // enum
                        if (propertyDesc.Property.PropertyType.BaseType == typeof(System.Enum)) {
                            formField.DataSource =
                                $"Api/Type/GetKeyValue?type={propertyDesc.Property.PropertyType.Name}";
                        }
                    }
                }

                // enum
                if (propertyDesc.Property.PropertyType.BaseType == typeof(System.Enum)) {
                    formField.DataSource = $"Api/Type/GetKeyValue?type={propertyDesc.Property.PropertyType.Name}";
                }

                //Json
                if (formField.Type == ControlsType.JsonList)
                {
                    var propertyValue = propertyInfo.GetPropertyValue(model);
                    formField.JsonItems.AddRange(GetFormFields(propertyValue));
                }

                if (formField.Type == ControlsType.RelationClass && propertyDesc.FieldAttribute.DataSourceType != null) {
                    formField.DataSource =
                        $"Api/Relation/GetClassTree?Type={propertyDesc.FieldAttribute.DataSourceType.Name}";
                }

                if (formField.Type == ControlsType.RelationTags && propertyDesc.FieldAttribute.DataSourceType != null) {
                    formField.DataSource =
                        $"Api/Relation/GetTag?Type={propertyDesc.FieldAttribute.DataSourceType.Name}";
                }

                formField.Value = propertyInfo.GetPropertyValue(model);

                result.Add(formField);
            }

            return result;
        }

        /// <summary>
        ///     get group
        /// </summary>
        /// <param name="propertys"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static FieldGroup GetGroup(PropertyDescription[] propertys, object model)
        {
            var fieldGroup = new FieldGroup { GroupName = "default" };
            fieldGroup.Items.AddRange(GetFormFieds(propertys, model));
            return fieldGroup;
        }

        #endregion convert from object
    }
}