using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Alabo.Aspects.Base;

namespace Alabo.Aspects
{
    /// <summary>
    ///     验证不能为空
    /// </summary>
    public class NotEmptyAttribute : ParameterInterceptorBase
    {
        /// <summary>
        ///     执行
        /// </summary>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            if (string.IsNullOrWhiteSpace(Extensions.Extensions.SafeString(context.Parameter.Value))) {
                throw new ArgumentNullException(context.Parameter.Name);
            }

            return next(context);
        }
    }
}