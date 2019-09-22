using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;
using String = System.String;

namespace Alabo.App.Core.Common.UI {

    /// <summary>
    /// auto config from
    /// </summary>
    public class AutoConfigForm : IAutoConfigForm {

        /// <summary>
        /// get view
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(Type type, object id) {
            return null;

            //var config = GetConfig(type, new Guid());
            //return AutoFormMapping.Convert(config);
        }

        /// <summary>
        /// save
        /// </summary>
        public void Save(Type type, dynamic model) {
            // string key = Request.Form["key"];
            string key = string.Empty;

            var config = Activator.CreateInstance(type);
            var configJson = config.ToJson();
            var id = String.Empty;
            //    var id = Request.Form["id"];
            var logName = string.IsNullOrEmpty(id) ? "添加" : "编辑";
            if (config is IAutoConfig) {
                var dataConfig = Ioc.Resolve<IAutoConfigService>().GetConfig(key);
                //  PropertyDescription.SetValue(config, Request);
                // config = (IAutoConfig)JsonMapping.HttpContextToExtension(config, type, HttpContext);
                var configProperty = type.GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var value = JsonConvert.SerializeObject(config);

                var autoConfig = new AutoConfig {
                    Type = key,
                    //AppName = configProperty.AppName,
                    LastUpdated = DateTime.Now
                };

                if (key.Equals("Alabo.App.Core.User.Domain.Callbacks.UserTypeConfig",
                    StringComparison.OrdinalIgnoreCase)) {
                    //var TypeClass = Enum.Parse(typeof(UserTypeEnum), Request.Form["TypeClass"]);
                    //config.GetType().GetProperty("Id").SetValue(config,
                    //    ((UserTypeEnum)TypeClass).GetCustomAttr<FieldAttribute>().GuidId.ToGuid());
                }

                if (configProperty.PageType == ViewPageType.List) {
                    var list = Ioc.Resolve<IAutoConfigService>().GetList(key);

                    //如果Id不存在则创建Id
                    if (config.GetType().GetProperty("Id").GetValue(config).ToGuid() == Guid.Empty) {
                        config.GetType().GetProperty("Id").SetValue(config, Guid.NewGuid());
                    } else {
                        JObject current = null;
                        foreach (var item in list) {
                            if (config.GetType().GetProperty("Id").GetValue(config).ToGuid() == item["Id"].ToGuid()) {
                                current = item;
                                break;
                            }
                        }

                        list.Remove(current);
                    }

                    if (type == typeof(UserGradeConfig)) {
                        foreach (var item in list) {
                            var temp = config as UserGradeConfig;
                            if (temp.IsDefault && temp.UserTypeId.ToString() == item["UserTypeId"].ToString()) {
                                item["IsDefault"] = "false";
                            }
                        }
                    } else if (type == typeof(UserTypeConfig)) {
                        var temp = config as UserTypeConfig;

                        foreach (var item in list) {
                            item["IsDefault"] = "false";
                            if (item["TypeClass"].ToString() !=
                                ((int)Alabo.Core.Enums.Enum.UserTypeEnum.Customer).ToString()) {
                                item["Id"] =
                                    ((UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), item["TypeClass"].ToString()))
                                    .GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
                            }
                        }
                    } else if (type == typeof(MoneyTypeConfig)) {
                        foreach (var item in list) {
                            if (item["Currency"].ToString() != ((int)Currency.Custom).ToString()) {
                                item["Id"] = ((Currency)Enum.Parse(typeof(Currency), item["Currency"].ToString()))
                                    .GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
                            }
                        }

                        foreach (var item in config.GetType().GetProperties()) {
                            //验证字段是否是Curreny
                            if (item.PropertyType.Name.Equals(nameof(Currency))) {
                                //是否启用了唯一性验证
                                if (item.GetAttribute<FieldAttribute>()?.EnumUniqu == true) {
                                    var enumType = (Currency)item.GetValue(config);
                                    //如果枚举类型大于0 则判断是否已经存在该类型如果存在则返回
                                    if (enumType >= 0) {
                                        foreach (var cfg in list) {
                                            var s = cfg.GetValue(item.Name)?.ToString();
                                            var temp = Currency.Custom;
                                            if (Enum.TryParse(s, out temp) && temp == enumType) {
                                                //ModelState.AddModelError(string.Empty,
                                                //    enumType.ToSplitList() + "类型数据已经存在");
                                                //return RedirectToAction("Edit",
                                                //    new
                                                //    {
                                                //        key = Request.Form["key"],
                                                //        id = Request.Form["id"],

                                                //    });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    list.Add(JObject.FromObject(config));
                    autoConfig.Value = JsonConvert.SerializeObject(list);
                } else {
                    autoConfig.Value = value;
                }

                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}