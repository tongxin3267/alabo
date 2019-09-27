using System;
using Alabo.Domains.Services;
using Alabo.UI.Design.AutoForms;

namespace Alabo.Framework.Core.WebUis.Services
{
    public interface IApIAlaboAutoConfigService : IService
    {
        /// <summary>
        ///     get view
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        AutoForm GetView(Type type, object id);

        /// <summary>
        ///     save
        /// </summary>
        /// <returns></returns>
        void Save(Type type, object model);
    }
}