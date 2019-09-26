using Alabo.Helpers;

namespace Alabo.Web.Mvc.ViewModel
{
    /// <summary>
    ///     ViewModel基类
    /// </summary>
    public abstract class BaseViewModel
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