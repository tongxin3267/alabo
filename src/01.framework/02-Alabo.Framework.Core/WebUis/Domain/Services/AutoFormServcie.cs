using System;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.UI.Dtos;
using Alabo.AutoConfigs;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Services;
using Alabo.Helpers;
using Alabo.Linq.Dynamic;
using Alabo.UI;
using Alabo.UI.AutoForms;

namespace Alabo.App.Core.UI.Domain.Services {

    public class AutoFormServcie : ServiceBase, IAutoFormServcie {

        public AutoFormServcie(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public Tuple<ServiceResult, AutoForm> GetView(string type, object id, AutoBaseModel autoModel) {
            Type typeFind = null;
            object instanceFind = null;
            AutoForm autoForm = null;
            var checkType = Resolve<IUIBaseService>().CheckType(type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return new Tuple<ServiceResult, AutoForm>(checkType, new AutoForm());
            }

            if (instanceFind is IAutoConfig) {
                autoForm = Resolve<IApIAlaboAutoConfigService>().GetView(instanceFind.GetType(), id);
                return new Tuple<ServiceResult, AutoForm>(ServiceResult.Success, autoForm);
            } else if (instanceFind is IAutoForm set) {
                autoForm = set.GetView(id, autoModel);
                return new Tuple<ServiceResult, AutoForm>(ServiceResult.Success, autoForm);
            } else if (instanceFind is IEntity) {
                var result = DynamicService.ResolveMethod(typeFind.Name, "GetViewById", id);
                if (result.Item1.Succeeded) {
                    autoForm = AutoFormMapping.Convert(result.Item2);
                } else {
                    autoForm = AutoFormMapping.Convert(typeFind.FullName);
                }

                return new Tuple<ServiceResult, AutoForm>(ServiceResult.Success, autoForm);
            }

            return new Tuple<ServiceResult, AutoForm>(ServiceResult.FailedWithMessage("未知类型"), new AutoForm());
        }

        public ServiceResult Save(AutoFormInput autoFormInput, AutoBaseModel autoModel) {
            //config
            if (autoFormInput.TypeInstance is IAutoConfig) {
                Ioc.Resolve<IApIAlaboAutoConfigService>().Save(autoFormInput.TypeFind, autoFormInput.ModelFind);
            } else if (autoFormInput.TypeInstance is IAutoForm set) {
                var result = set.Save(autoFormInput.ModelFind, autoModel);
                if (!result.Succeeded) {
                    return ServiceResult.FailedWithMessage(result.ErrorMessages?.FirstOrDefault());
                }
            } else if (autoFormInput.TypeInstance is IEntity) {
                var result = Save(autoFormInput.TypeFind, autoFormInput.ModelFind);
                if (!result.Succeeded) {
                    return result;
                }
            }

            return ServiceResult.Success;
        }

        private ServiceResult Save(Type type, dynamic model) {
            var find = DynamicService.ResolveMethod(type.Name, "GetSingle", model.Id);
            if (!find.Item1.Succeeded) {
                return ServiceResult.FailedWithMessage("参数值为空");
            }
            var entity = find.Item2;
            if (entity == null) {
                var result = DynamicService.ResolveMethod(type.Name, "Add", model);
                if (!result.Item1.Succeeded) {
                    return ServiceResult.FailedWithMessage($"{type.Name},添加失败");
                }
            } else {
                entity = (object)model;
                var result = DynamicService.ResolveMethod(type.Name, "Update", entity);
                if (!result.Item1.Succeeded) {
                    return ServiceResult.FailedWithMessage($"{type.Name},更新失败");
                }
            }
            return ServiceResult.Success;
        }
    }
}