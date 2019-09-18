using System;
using Alabo.Dependency;
using Alabo.UI.AutoForms;

namespace Alabo.App.Core.Common.UI {

    /// <summary>
    /// IAutoConfigForm
    /// </summary>
    public interface IAutoConfigForm : IScopeDependency {

        /// <summary>
        /// get view
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        AutoForm GetView(Type type, object id);

        /// <summary>
        /// save
        /// </summary>
        /// <returns></returns>
        void Save(Type type, dynamic model);
    }
}