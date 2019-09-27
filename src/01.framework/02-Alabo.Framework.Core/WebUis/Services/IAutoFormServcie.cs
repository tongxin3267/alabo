using System;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Core.WebUis.Dtos;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;

namespace Alabo.Framework.Core.WebUis.Services
{
    public interface IAutoFormServcie : IService
    {
        /// <summary>
        ///     通过Id获取自动视图，视图Id可以为空
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        Tuple<ServiceResult, AutoForm> GetView(string type, object id, AutoBaseModel autoModel);

        /// <summary>
        ///     保存
        /// </summary>
        /// <returns></returns>
        ServiceResult Save(AutoFormInput viewAutoForm, AutoBaseModel autoModel);
    }
}