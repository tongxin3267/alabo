using Alabo.AutoConfigs.Entities;
using Alabo.AutoConfigs.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Helpers;
using Alabo.UI.Design.AutoForms;
using System;
using System.Collections.Generic;
using Alabo.Dynamics;
using Alabo.Reflections;
using Newtonsoft.Json.Linq;

namespace Alabo.Framework.Core.WebUis.Services
{
    public class ApIAlaboAutoConfigService : ServiceBase, IApIAlaboAutoConfigService
    {
        public ApIAlaboAutoConfigService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public AutoForm GetView(Type type, object id) {
            var config = GetConfig(type, id);
            return AutoFormMapping.Convert(config);
        }

        public void Save(Type type, object model) {
            var key = type.FullName;

            var config = Resolve<IAlaboAutoConfigService>().GetConfig(type.FullName);
            if (config == null) {
                config = new AutoConfig {
                    Type = key,
                    AppName = Resolve<ITypeService>().GetAppName(type.FullName),
                    LastUpdated = DateTime.Now
                };
            }

            var classDescription = type.FullName.GetClassDescription();
            if (classDescription.ClassPropertyAttribute.PageType == ViewPageType.Edit) {
                config.Value = model.ToJsons();
            } else {
                var list = ServiceInterpreter.Eval<List<JObject>>("IAutoConfigService", "GetList", key);
                var idValue = Reflection.GetPropertyValue("Id", model);
                var guid = idValue.ConvertToGuid();
                //如果Id不存在则创建Id
                if (guid.IsGuidNullOrEmpty()) {
                    Reflection.SetPropertyValue("Id", model, Guid.NewGuid());
                } else {
                    JObject current = null;
                    foreach (var item in list) {
                        if (idValue.ToGuid() == item["Id"].ToGuid()) {
                            current = item;
                            break;
                        }
                    }
                    list.Remove(current);
                }
                list.Add(JObject.FromObject(model));
                config.Value = list.ToJsons();
            }

            Ioc.Resolve<IAlaboAutoConfigService>().AddOrUpdate(config);
        }

        public object GetConfig(Type type, object id) {
            var data = Activator.CreateInstance(type);
            // 如果包含Id的字段
            var idField = type.GetProperty("Id");
            if (idField != null) {
                if (id.IsGuidNullOrEmpty()) {
                    return data;
                }
            }
            // 重构注释
            var config = Resolve<IAlaboAutoConfigService>().GetConfig(type.FullName);
            if (config == null) {
                return data;
            }

            var classDescription = type.FullName.GetClassDescription();
            if (classDescription.ClassPropertyAttribute.PageType == ViewPageType.Edit) {
                data = config.Value.ToObject(type);
                return data;
            } else {
                var list = config.Value.ToObject<List<JObject>>();
                foreach (var item in list) {
                    if (id.ToGuid() == item["Id"].ToGuid()) {
                        data = item.ToJsons().ToObject(type);
                        break;
                    }
                }
            }
            return data;
        }
    }
}