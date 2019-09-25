using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    ///     Class TypeService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.Admin.Domain.Services.ITypeService" />
    public class TypeService : ServiceBase, ITypeService {

        /// <summary>
        ///     获取所有的AutoCofing类型
        /// </summary>
        /// <param name="confinName">Name of the confin.</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public IEnumerable<Type> GetAllConfigType(string confinName) {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public IEnumerable<Type> GetAllEntityType() {
            var cacheKey = "GetAllEntityType";
            if (!ObjectCache.TryGetPublic(cacheKey, out IEnumerable<Type> types)) {
                //因为遍历所有程序集，速度会有影响
                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IEntity)) ||
                                t.GetInterfaces().Contains(typeof(IMongoEntity))));
                types = types.Where(r => !r.FullName.StartsWith("Alabo.Domain"));
                types = types.Where(r => !r.FullName.Contains("Test."));
                types = types.Where(r => !r.FullName.Contains("Tests."));
                types = types.Where(t => !t.GetInterfaces().Contains(typeof(IAutoConfig)));
                if (types != null) {
                    ObjectCache.Set(cacheKey, types);
                }
            }

            return types;
        }

        /// <summary>
        ///     获取所有的Enum类型
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public IEnumerable<Type> GetAllEnumType() {
            var cacheKey = "GetAllEnumType_alltypes";
            if (!ObjectCache.TryGetPublic(cacheKey, out IEnumerable<Type> types)) {
                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes());
                types = types.Where(r => !r.FullName.Contains("Test."));
                types = types.Where(r => !r.FullName.Contains("Tests."));
                types = types.Where(r => !r.FullName.Contains("Security.")); // 安全的单元测试不包含进来
                types = types.Where(r => r.IsEnum);
            }

            return types;
        }

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public IEnumerable<Type> GetAllServiceType() {
            var cacheKey = "RuntimeAllServiceTypes";
            if (!ObjectCache.TryGetPublic(cacheKey, out IEnumerable<Type> types)) {
                //因为遍历所有程序集，速度会有影响
                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IService)) ||
                                t.GetInterfaces().Contains(typeof(IService))));

                if (types != null) {
                    ObjectCache.Set(cacheKey, types);
                }
            }

            return types;
        }

        /// <summary>
        ///     获取AutoConfig 的字典类型，可用于构建下拉菜单，复选框，表格等
        ///     其中AutoConfig中，必须要指定Main字段特性，才可以识别值object
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <param name="isAllSelect"></param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public Dictionary<string, object> GetAutoConfigDictionary(string configName, bool isAllSelect = false) {
            var config = GetAutoConfigType(configName);
            if (config == null) {
                return null;
            }
            //读取数据库问题
            var dictionary = new Dictionary<string, object>();
            if (isAllSelect) {
                dictionary.Add("-1", "所有");
            }

            //IList<JObject> list = Resolve<IAlaboAutoConfigService>().GetList(config.FullName);

            //if (!list.Any()) {
            //    return dictionary;
            //}

            //var TextField = string.Empty; //如果不存在Main特性则取Id字段显示
            //foreach (var item in config.GetProperties()) {
            //    if (item.GetAttribute<MainAttribute>() == null) {
            //        continue;
            //    }

            //    TextField = item.Name;
            //    break;
            //}

            //if (TextField.IsNullOrEmpty()) {
            //    foreach (var item in config.GetProperties()) {
            //        if (item.Name == "Name") {
            //            TextField = item.Name;
            //        }
            //    }
            //}

            //if (TextField.IsNullOrEmpty()) {
            //    TextField = "Id";
            //}

            //foreach (var data in list) {
            //    var key = data["Id"].ToString();
            //    var value = data[TextField];
            //    if (!dictionary.Keys.Contains(key)) {
            //        dictionary.Add(key, value);
            //    }
            //}

            return dictionary;
        }

        /// <summary>
        ///     Gets the type of the automatic configuration.
        ///     根据名取AutoCofing类型
        /// </summary>
        /// <param name="configName">Name of the configuration. 可以是配置名如：UserConfig ，也可以是AutoConfig的完整命名空间</param>
        /// <returns>Type.</returns>
        public Type GetAutoConfigType(string configName) {
            var config = GetAllConfigType().FirstOrDefault(e =>
                e.Name.EndsWith(configName, StringComparison.OrdinalIgnoreCase) || e.FullName == configName);
            return config;
        }

        /// <summary>
        ///     获取d单个的IService类型
        /// </summary>
        /// <param name="entityName">可以是配置名如：IAlaboUserService ，也可以是IService的完整命名空间</param>
        /// <returns>Type.</returns>
        public Type GetEntityType(string entityName) {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     获取枚举 的字典类型
        ///     可用于构建下拉菜单，复选框，表格等
        ///     其中enum为display的值
        /// </summary>
        /// <param name="enumName">Name of the enum.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public Dictionary<string, object> GetEnumDictionary(string enumName) {
            var type = enumName.GetTypeByName();
            return type == null ? null : GetEnumDictionary(type);
        }

        /// <summary>
        ///     获取枚举 的字典类型
        ///     可用于构建下拉菜单，复选框，表格等
        ///     其中enum为display的值
        /// </summary>
        /// <param name="type">The 类型.</param>
        public Dictionary<string, object> GetEnumSelectItem(Type type) {
            var selects = new Dictionary<string, object>();
            foreach (Enum item in Enum.GetValues(type)) {
                selects.Add(Convert.ToInt16(item).ToString(), item.GetDisplayName());
            }

            return selects;
        }

        /// <summary>
        ///     Dictionary<string, object>
        /// </summary>
        /// <param name="type"></param>

        public Dictionary<string, object> GetEnumDictionary(Type type) {
            var keyValuePairs = new Dictionary<string, object>();
            foreach (Enum item in Enum.GetValues(type)) {
                var value = item.GetDisplayName();
                var key = Convert.ToInt16(item);
                keyValuePairs.Add(key.ToString(), value);
            }

            return keyValuePairs;
        }

        /// <summary>
        ///     获取的单个的Enum类型
        /// </summary>
        /// <param name="enumName">可以是配置名如：UserTypeEnum ，也可以是Enum的完整命名空间</param>
        /// <returns>Type.</returns>
        public Type GetEnumType(string enumName) {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        /// <param name="serviceName">可以是配置名如：IAlaboUserService ，也可以是IService的完整命名空间</param>
        /// <returns>Type.</returns>
        public Type GetServiceType(string serviceName) {
            var type = GetAllServiceType()
                .FirstOrDefault(e => e.Name.EndsWith(serviceName, StringComparison.OrdinalIgnoreCase));
            return type;
        }

        /// <summary>
        ///     获取所有的AutoCofing类型
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public IEnumerable<Type> GetAllConfigType() {
            return Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoConfig));
        }

        public object GetEntityType(object entityName) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有的接口类型
        /// </summary>
        /// <param name="interfaceType"></param>

        public IEnumerable<Type> GetAllTypeByInterface(Type interfaceType) {
            var cacheKey = interfaceType.Name + "_alltypes";
            return ObjectCache.GetOrSetPublic(() => {
                var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                     .Where(t => t.GetInterfaces().Contains(interfaceType)));
                return types;
            }, cacheKey).Value;
        }

        public Type GetAllTypeByInterfaceAndName(Type interfaceType, string name) {
            var allTypes = GetAllTypeByInterface(interfaceType);
            if (allTypes != null) {
                var find = allTypes.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return find;
            }
            return null;
        }

        /// <summary>
        /// 获取所有的控制器Api
        /// </summary>

        public IEnumerable<Type> GetAllApiController() {
            var cacheKey = "Controller_alltypes";
            return ObjectCache.GetOrSetPublic(() => {
                var list = new List<Type>();

                var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes().Where(t => (!t.IsAbstract)));
                foreach (var item in types) {
                    var isapiType = Reflection.BaseTypeContains(item, typeof(ApiBaseController));
                    if (isapiType) {
                        list.Add(item);
                    }
                }
                return list;
            }, cacheKey).Value;
        }

        public string GetAppName(Type type) {
            if (type == null) {
                return string.Empty;
            }
            var arrays = type.FullName.ToSplitList(".");
            return arrays[3];
        }

        public string GetGroupName(Type type) {
            if (type == null) {
                return string.Empty;
            }
            var arrays = type.FullName.ToSplitList(".");
            return arrays[2];
        }

        public IEnumerable<Type> GetAllEntityService() {
            var result = new List<Type>();
            var entityName = GetAllEntityType().Select(r => r.Name);
            foreach (var item in GetAllServiceType()) {
                var name = item.Name.Substring(1, item.Name.Length - 1);
                name = name.Replace("Service", "", StringComparison.OrdinalIgnoreCase).Replace("`2", "");
                if (entityName.Contains(name)) {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        ///     根据实体类型的名称获取IService名称
        /// </summary>
        /// <param name="entityName">比如：输入User 获取IAlaboUserService</param>
        public string GetServiceTypeByEntity(string entityName) {
            if (entityName == null) {
                throw new ValidException("实体名称不能为空");
            }

            var serviceName = $"I{entityName}Service";
            return serviceName;
        }

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        public Type GetServiceTypeFromEntity(string entityName) {
            var servcieName = GetServiceTypeByEntity(entityName);
            var type = GetServiceType(servcieName);
            return type;
        }

        public string GetAppName(string fullName) {
            if (fullName.IsNullOrEmpty()) {
                return string.Empty;
            }
            var arrays = fullName.ToSplitList(".");
            if (arrays.Count < 3) {
                return string.Empty;
            }
            return arrays[3];
        }

        public string GetGroupName(string fullName) {
            if (fullName.IsNullOrEmpty()) {
                return string.Empty;
            }
            var arrays = fullName.ToSplitList(".");
            if (arrays.Count < 2) {
                return string.Empty;
            }
            return arrays[2];
        }

        public IEnumerable<EnumList> GetEnumList() {
            var allTypes = Resolve<ITypeService>().GetAllEnumType();
            var list = new List<EnumList>();
            foreach (var item in allTypes) {
                var enumType = new EnumList {
                    Name = item.Name
                };
                if (enumType.Name == typeof(ZoneTime).Name || enumType.Name == typeof(Languages).Name || enumType.Name == typeof(Country).Name || enumType.Name == typeof(ZoneTime).Name) {
                    continue;
                }
                enumType.KeyValue = new List<EnumKeyValue>();
                foreach (var enumItem in Helpers.Enums.GetItems(item)) {
                    var key = enumItem.Value;
                    var name = enumItem.Text;

                    if (name.IsNullOrEmpty()) {
                        continue;
                    }

                    var keyValue = new EnumKeyValue {
                        //     Html = html,
                        Key = key,
                        Value = name
                    };

                    enumType.KeyValue.Add(keyValue);
                }
                list.Add(enumType);
            }
            return list;
        }

        public IEnumerable<EnumList> AllKeyValues() {
            var list = new List<EnumList>();
            // 所有的AutoConfig配置,只显示列表类型
            var allConfigs = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoConfig));
            EnumList enumList = new EnumList {
                Name = "AutoConfig配置"
            };
            foreach (var item in allConfigs) {
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null && attribute.PageType == ViewPageType.List) {
                    var keyValue = new EnumKeyValue {
                        Key = item.Name,
                        Value = attribute.Name
                    };
                    enumList.KeyValue.Add(keyValue);
                }
            }
            list.Add(enumList);
            // 分类
            enumList = new EnumList {
                Name = "级联通用分类"
            };
            var relationTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IRelation));
            relationTypes = relationTypes.Where(r => r.Name.Contains("Class"));
            foreach (var item in relationTypes) {
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null) {
                    var keyValue = new EnumKeyValue {
                        Key = item.Name,
                        Value = attribute.Name
                    };
                    enumList.KeyValue.Add(keyValue);
                }
            }
            list.Add(enumList);
            // 标签
            enumList = new EnumList {
                Name = "级联通用标签"
            };
            relationTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IRelation));
            relationTypes = relationTypes.Where(r => r.Name.Contains("Tags"));
            foreach (var item in relationTypes) {
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null) {
                    var keyValue = new EnumKeyValue {
                        Key = item.Name,
                        Value = attribute.Name
                    };
                    enumList.KeyValue.Add(keyValue);
                }
            }
            list.Add(enumList);
            // 枚举
            enumList = new EnumList {
                Name = "系统枚举"
            };
            relationTypes = Resolve<ITypeService>().GetAllEnumType();
            foreach (var item in relationTypes) {
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null) {
                    var keyValue = new EnumKeyValue {
                        Key = item.Name,
                        Value = attribute.Name
                    };
                    enumList.KeyValue.Add(keyValue);
                } else {
                    var keyValue = new EnumKeyValue {
                        Key = item.Name,
                        Value = item.Name
                    };
                    enumList.KeyValue.Add(keyValue);
                }
            }
            list.Add(enumList);

            // 枚举
            enumList = new EnumList {
                Name = "自动UI"
            };
            var uiKeyValue = new EnumKeyValue {
                Key = "IAutoTable",
                Value = "IAutoTable"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoForm",
                Value = "IAutoForm"
            }; enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Key = "IAutoList",
                Value = "IAutoList"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoPreview",
                Value = "IAutoPreview"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoNews",
                Value = "IAutoNews"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoArticle",
                Value = "IAutoArticle"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoNotices",
                Value = "IAutoNotices"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoReport",
                Value = "IAutoReport"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoFaq",
                Value = "IAutoFaq"
            };
            enumList.KeyValue.Add(uiKeyValue);
            uiKeyValue = new EnumKeyValue {
                Key = "IAutoIndex",
                Value = "IAutoIndex"
            };
            enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Key = "IAutoIntro",
                Value = "IAutoIntro"
            };
            enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Key = "IAutoTask",
                Value = "IAutoTask"
            };
            enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Key = "IAutoVideo",
                Value = "IAutoVideo"
            };
            enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Key = "CatalogEntity",
                Value = "CatalogEntity"
            };
            enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Value = "SqlServcieCatalogEntity",
                Key = "SqlServcieCatalogEntity"
            };
            enumList.KeyValue.Add(uiKeyValue);

            uiKeyValue = new EnumKeyValue {
                Value = "MongodbCatalogEntity",
                Key = "MongodbCatalogEntity"
            };
            enumList.KeyValue.Add(uiKeyValue);

            list.Add(enumList);
            return list;
        }

        public TypeService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}