using System.Collections.Generic;
using System.Linq;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Web.ViewFeatures;

namespace Alabo.UI.Design.AutoPreviews
{
    /// <summary>
    ///     AutoPreviewMapping
    /// </summary>
    public static class AutoPreviewMapping
    {
        /// <summary>
        ///     convert to auto preview of type name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static AutoPreview Convert(string fullName)
        {
            var instance = fullName.GetInstanceByName();
            return Convert(instance);
        }

        /// <summary>
        ///     convert to auto preview of model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AutoPreview Convert(object model)
        {
            //cache class descrption
            var fullName = model.GetType().FullName;
            var classDescription = fullName.GetClassDescription();
            if (classDescription == null) return null;

            var classPropertyAttribute = classDescription.ClassPropertyAttribute;
            return new AutoPreview
            {
                Key = fullName,
                Name = classPropertyAttribute.Name,
                Icon = classPropertyAttribute.Icon,
                KeyValues = GetKeyValues(classDescription.Propertys, model)
            };
        }

        /// <summary>
        ///     builder property
        /// </summary>
        /// <param name="propertys"></param>
        private static IList<KeyValue> GetKeyValues(PropertyDescription[] propertys, object model)
        {
            var result = new List<KeyValue>();
            var propertyInfos = model.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                //find property description
                var propertyDesc = propertys.ToList().Find(p => p.Property.Name == propertyInfo.Name);
                if (propertyDesc == null
                    || propertyDesc.FieldAttribute == null
                    || !propertyDesc.FieldAttribute.EditShow)
                    continue;

                //check type
                var fieldType = propertyDesc.Property.PropertyType;
                if (fieldType.IsGenericType || fieldType.IsArray || fieldType.IsClass) continue;

                //enum
                var value = propertyInfo.GetPropertyValue(model);
                if (fieldType.IsEnum) value = value.GetDisplayName();

                //builder
                var field = new KeyValue
                {
                    Name = propertyDesc.DisplayAttribute?.Name,
                    Icon = propertyDesc.FieldAttribute.Icon,
                    Key = propertyDesc.DisplayAttribute?.Name,
                    Value = value,
                    SortOrder = propertyDesc.FieldAttribute.SortOrder
                };

                result.Add(field);
            }

            return result;
        }
    }
}