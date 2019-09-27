using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Attributes;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Reflections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZKCloud.Open.DynamicExpression;
using Convert = System.Convert;

namespace Alabo.Linq.Dynamic
{
    public static class DynamicService
    {
        /// <summary>
        ///     动态调取Service服务
        /// </summary>
        /// <param name="entityName">实体名称：比如User,Order,Product等</param>
        public static object Resolve(string entityName)
        {
            if (entityName.EndsWith("Service")) entityName = entityName.Substring(0, entityName.Length - 7);

            if (entityName != "Identity")
                if (entityName.StartsWith("I"))
                    entityName = entityName.Substring(1, entityName.Length - 1);

            var unitOfWork = Ioc.Resolve<IUnitOfWork>();
            var repositoryType = $"{entityName}Repository".GetTypeByName();
            if (repositoryType == null)
            {
                var servcieType = $"{entityName}Service".GetTypeByName();
                var service = Activator.CreateInstance(servcieType, unitOfWork);
                return service;
            }
            else
            {
                var repository = Activator.CreateInstance(repositoryType, unitOfWork);
                var servcieType = $"{entityName}Service".GetTypeByName();
                var service = Activator.CreateInstance(servcieType, unitOfWork, repository);
                return service;
            }
        }

        /// <summary>
        ///     动态获取List对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> ResolveList<T>(Expression<Func<T, bool>> predicate = null)
        {
            var entityName = typeof(T).Name;
            var service = Resolve(entityName);
            var target = new Interpreter().SetVariable("service", service);
            if (predicate == null)
            {
                var result = target.Eval("service.GetList()");
                return (IEnumerable<T>)result;
            }
            else
            {
                var parameters = new[]
                {
                    new Parameter("predicate", predicate)
                };
                var result = target.Eval("service.GetList(predicate)", parameters);
                return (IEnumerable<T>)result;
            }
        }

        /// <summary>
        ///     动态调取Service服务
        /// </summary>
        /// <param name="entityName">实体名称：比如User,Order,Product等</param>
        public static Tuple<ServiceResult, object> ResolveMethod(string serviceString, string methodString,
            params object[] paramaters)
        {
            return ResolveMethod(serviceString, methodString, false, paramaters);
        }

        /// <summary>
        ///     动态调用IService 服务
        /// </summary>
        /// <param name="serviceString">服务名称:比如IServicer</param>
        /// <param name="methodString">方法名称：比如GetSingle</param>
        /// <param name="runInUrl">是否可以在Url中运行</param>
        /// <param name="paramaters">参数</param>
        public static Tuple<ServiceResult, object> ResolveMethod(string serviceString, string methodString,
            bool runInUrl = false, params object[] paramaters)
        {
            //var type = Reflection.GetAllServices().FirstOrDefault(e => e.Name.EndsWith(serviceString, StringComparison.OrdinalIgnoreCase));

            var instanse = Resolve(serviceString);
            if (instanse == null)
                return Tuple.Create(ServiceResult.FailedWithMessage($"未找到{serviceString}服务，检查拼写是否正确"), new object());

            var methods = instanse.GetType().GetMethods()
                .Where(e => e.Name.Equals(methodString, StringComparison.OrdinalIgnoreCase));
            if (methods == null || methods.Count() < 0)
                return Tuple.Create(ServiceResult.FailedWithMessage($"未找到{methodString}方法，检查拼写是否正确"), new object());

            var method = methods.FirstOrDefault();
            if (method == null)
                return Tuple.Create(ServiceResult.FailedWithMessage($"未找到{methodString}方法，检查拼写是否正确"), new object());

            var instanseExpression = Expression.Convert(Expression.Constant(instanse), instanse.GetType());

            // 参数类型是否与当前方法匹配，同时获取参数
            var isMatching = false;
            ConstantExpression[] parameterExpressions = null;

            foreach (var item in methods)
            {
                method = item;
                isMatching = true;
                if (paramaters != null && paramaters.Length > 0)
                {
                    parameterExpressions = new ConstantExpression[paramaters.Length];
                    var methodParameters = method.GetParameters();
                    // 参数数量是否相同
                    if (methodParameters.Count() != paramaters.Length)
                    {
                        isMatching = false;
                        continue;
                    }

                    for (var i = 0; i < paramaters.Length; i++)
                        try
                        {
                            if (methodParameters[i].ParameterType == typeof(Guid))
                                parameterExpressions[i] = Expression.Constant(Guid.Parse(paramaters[i].ToString()),
                                    methodParameters[i].ParameterType);
                            else
                                parameterExpressions[i] = Expression.Constant(
                                    Convert.ChangeType(paramaters[i], methodParameters[i].ParameterType),
                                    methodParameters[i].ParameterType);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            isMatching = false;
                        }
                }

                if (isMatching) break;
            }

            // 如果方法在URl中执行，检查方法的安全设置
            if (runInUrl)
            {
                var attribute = method.GetAttribute<MethodAttribute>();
                if (attribute == null || attribute.RunInUrl == false)
                    return Tuple.Create(ServiceResult.FailedWithMessage("该方法未非安全方法，不能通过Url方式来执行，请设置Method特性"),
                        new object());
            }

            if (isMatching == false)
                return Tuple.Create(ServiceResult.FailedWithMessage($"参数传递不正确，，检查参数是否与{serviceString}.{method}中的方法一样"),
                    new object());

            var callExpression = parameterExpressions == null
                ? Expression.Call(instanseExpression, method)
                : Expression.Call(instanseExpression, method, parameterExpressions);
            if (method.ReturnType == typeof(void))
            {
                var lambdaExpression = Expression.Lambda<Action>(callExpression, null);
                lambdaExpression.Compile()();
                return Tuple.Create(ServiceResult.Success, new object());
            }
            else
            {
                var convertExpression = Expression.Convert(callExpression, typeof(object));
                var lambdaExpression = Expression.Lambda<Func<object>>(convertExpression, null);
                var result = lambdaExpression.Compile()();
                return Tuple.Create(ServiceResult.Success, result);
            }
        }
    }
}