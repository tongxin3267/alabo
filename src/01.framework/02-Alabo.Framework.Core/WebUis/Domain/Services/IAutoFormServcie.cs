using System;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.UI;

namespace Alabo.Framework.Core.WebUis.Domain.Services {

    public interface IAutoFormServcie : IService {

        /// <summary>
        /// 通过Id获取自动视图，视图Id可以为空
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        Tuple<ServiceResult, AutoForm> GetView(string type, object id, AutoBaseModel autoModel);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        ServiceResult Save(AutoFormInput viewAutoForm, AutoBaseModel autoModel);
    }
}