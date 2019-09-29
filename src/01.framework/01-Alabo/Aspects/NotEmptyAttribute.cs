using Alabo.Aspects.Base;
using AspectCore.DynamicProxy.Parameters;
using System;
using System.Threading.Tasks;

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
            if (string.IsNullOrWhiteSpace(Extensions.Extensions.SafeString(context.Parameter.Value)))
                throw new ArgumentNullException(context.Parameter.Name);

            return next(context);
        }
    }
}