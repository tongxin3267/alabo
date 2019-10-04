using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Exceptions;
using ZKCloud.Open.DynamicExpression;

namespace Alabo.Dynamics
{
    /// <summary>
    /// 方法动态调用
    /// 常用方法：调用为依赖的解决方案
    /// </summary>
    public static class ServiceInterpreter
    {
        /// <summary>
        ///     动态调用无参数方法
        /// </summary>
        /// <param name="service">服务名称，区分大小写：比如UserService,TypeService</param>
        /// <param name="method">无参数方法名称：</param>
        /// <returns></returns>
        public static T Eval<T>(string service, string method) {
            var serviceResolve = DynamicService.Resolve(service);
            if (serviceResolve == null) {
                throw new ValidException("服务类型输入错误,未找到");
            }
            var target = new Interpreter().SetVariable("entityService", serviceResolve);
            var evalString = $"entityService.{method}()";
            var result = target.Eval(evalString);
            return (T)result;
        }

        /// <summary>
        /// 动态调用一个参数的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service">服务名称，区分大小写：比如UserService,TypeService</param>
        /// <param name="method">方法名称：</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static T Eval<T>(string service, string method, object parameter) {
            var serviceResolve = DynamicService.Resolve(service);
            if (serviceResolve == null) {
                throw new ValidException("服务类型输入错误,未找到");
            }
            var parameters = new[]
            {
                new Parameter("parameter", parameter)
            };
            var target = new Interpreter().SetVariable("entityService", serviceResolve);
            var evalString = $"entityService.{method}(parameter)";
            var result = target.Eval(evalString, parameters);
            return (T)result;
        }
    }
}