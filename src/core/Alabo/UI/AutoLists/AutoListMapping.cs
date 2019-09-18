using Alabo.Cache;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI.AutoTables;

namespace Alabo.UI.AutoLists
{
    public static class AutoListMapping
    {
        public static AutoList Convert(string fullName)
        {
            var objectCache = Ioc.Resolve<IObjectCache>();
            return objectCache.GetOrSet(() =>
                {
                    var type = fullName.GetTypeByFullName();

                    var classDescription = fullName.GetClassDescription();
                    if (classDescription != null)
                    {
                        var classPropertyAttribute = classDescription.ClassPropertyAttribute;

                        var auto = new AutoList
                        {
                            Key = fullName,
                            Name = classPropertyAttribute.Name,
                            Icon = classPropertyAttribute.Icon
                        };

                        var propertys = classDescription.Propertys;
                        auto.SearchOptions = AutoTableMapping.GetSearchOptions(propertys);

                        //tabs
                        auto.Tabs = AutoTableMapping.GetTabs(propertys);

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
                                tableColumn = AutoTableMapping.GetType(tableColumn, item.Property, item.FieldAttribute);
                                if (!item.FieldAttribute.Width.IsNullOrEmpty()) {
                                    tableColumn.Width = item.FieldAttribute.Width;
                                }
                            }
                        }

                        return auto;
                    }

                    return null;
                }, fullName + "AutoList").Value;
        }
    }
}