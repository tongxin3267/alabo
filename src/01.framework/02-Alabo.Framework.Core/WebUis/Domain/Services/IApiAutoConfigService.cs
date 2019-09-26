using System;
using Alabo.Domains.Services;
using Alabo.Framework.Core.WebUis.Design.AutoForms;

namespace Alabo.Framework.Core.WebUis.Domain.Services
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