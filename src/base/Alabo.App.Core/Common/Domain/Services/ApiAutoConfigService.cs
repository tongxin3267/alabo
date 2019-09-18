﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.UI.AutoForms;

namespace Alabo.App.Core.Common.Domain.Services {

    public class ApiAutoConfigService : ServiceBase, IApiAutoConfigService {

        public ApiAutoConfigService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public AutoForm GetView(Type type, object id) {
            var config = GetConfig(type, id);
            return AutoFormMapping.Convert(config);
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
            var config = Resolve<IAutoConfigService>().GetConfig(type.FullName);
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
                // var request = JsonConvert.DeserializeObject<List<JObject>>(config.Value);
            }

            return data;
            //var configDescription = new ClassDescription(data.GetType());

            ////获取  Json有扩展的属性
            //var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty())
            //    .ToList();

            //if (configDescription.ClassPropertyAttribute.PageType == ViewPageType.List) {
            //    var request = JsonConvert.DeserializeObject<List<JObject>>(config.Value);
            //    foreach (var item in request) {
            //        PropertyDescription.SetValue(data, item);
            //        if (data.GetType().GetProperty("Id").GetValue(data).ToString() == id.ToString()) {
            //            if (propertys.Any()) {
            //                // json 格式数据处理
            //                data = item.ToObject(type);
            //                return data;
            //            } else {
            //                return data;
            //            }
            //        }
            //    }
            //} else {
            //    var request = JsonConvert.DeserializeObject<JObject>(config.Value);
            //    PropertyDescription.SetValue(data, request);
            //    //  data = JsonMapping.HttpContextToExtension(data, type, HttpContext);
            //    return data;
            //}
        }

        public void Save(Type type, object model) {
            string key = type.FullName;

            var config = Resolve<IAutoConfigService>().GetConfig(type.FullName);
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
                var list = Ioc.Resolve<IAutoConfigService>().GetList(key);
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
            Ioc.Resolve<IAutoConfigService>().AddOrUpdate(config);
        }
    }
}