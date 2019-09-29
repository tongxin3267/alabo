using Alabo.Helpers;
using Alabo.UI;

namespace Alabo.Web.Mvc.ViewModel
{
    /// <summary>
    ///     ViewModel基类
    /// </summary>
    public abstract class BaseViewModel : UIBase
    {
        /// <summary>
        ///     Resolves this instance.
        /// </summary>
        public T Resolve<T>()
        {
            return Ioc.Resolve<T>();
        }
    }
}