using Alabo.Cache;
using Alabo.Datas.UnitOfWorks;
using Alabo.Extensions;
using Alabo.Helpers;
using System;
using System.Collections.Generic;
using ZKCloud.Open.DynamicExpression;

namespace Alabo.Linq.Dynamic
{
    /// <summary>
    ///     Class UserDynamicService.
    ///     动态获取用户
    /// </summary>
    public static class EntityDynamicService
    {
        #region 动态获取用户列表

        /// <summary>
        ///     Gets the users by identifier.
        ///     动态获取用户列表
        /// </summary>
        /// <param name="userIds">The user ids.</param>
        /// <returns>IEnumerable&lt;System.Object&gt;.</returns>
        public static IEnumerable<object> GetUserListByIds(IEnumerable<long> userIds)
        {
            var userService = DynamicService.Resolve("User");

            var target = new Interpreter().SetVariable("userService", userService);
            var parameters = new[]
            {
                new Parameter("userIds", userIds)
            };
            var users = (IEnumerable<object>)target.Eval("userService.GetList(userIds)", parameters);
            return users;
        }

        #endregion 动态获取用户列表

        #region 动态获取用户等级

        /// <summary>
        ///     Gets the user grade.
        ///     动态获取用户等级
        /// </summary>
        /// <param name="gradeId">The grade identifier.</param>
        /// <returns>dynamic.</returns>
        public static dynamic GetUserGrade(Guid gradeId)
        {
            if (gradeId.IsGuidNullOrEmpty()) return null;
            // 获取等级
            var gardeService = DynamicService.Resolve("GradeService");
            var gradeTarget = new Interpreter().SetVariable("gardeService", gardeService);
            // 获取等级
            var parameters = new[]
            {
                new Parameter("gradeId", gradeId)
            };
            var gradeConfig = (dynamic)gradeTarget.Eval("gardeService.GetGrade(gradeId)", parameters);
            return gradeConfig;
        }

        #endregion 动态获取用户等级

        #region 动态获取订单

        /// <summary>
        ///     Gets the single user.
        ///     动态获取订单
        /// </summary>
        /// <param name="orderId">Name of the user.</param>
        /// <returns>dynamic.</returns>
        public static dynamic GetSingleOrder(long orderId)
        {
            var orderService = DynamicService.Resolve("Order");
            var target = new Interpreter().SetVariable("orderService", orderService);
            var parameters = new[]
            {
                new Parameter("orderId", orderId)
            };
            var user = (dynamic)target.Eval("orderService.GetSingle(orderId)", parameters);
            return user;
        }

        #endregion 动态获取订单

        #region 动态获取店铺

        /// <summary>
        ///     动态获取店铺
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static dynamic GetStore(long userId)
        {
            var orderService = DynamicService.Resolve("Store");
            var target = new Interpreter().SetVariable("storeService", orderService);
            var parameters = new[]
            {
                new Parameter("userId", userId)
            };
            var user = (dynamic)target.Eval("storeService.GetUserStore(userId)", parameters);
            return user;
        }

        #endregion 动态获取店铺

        #region 动态获取城市合伙人

        /// <summary>
        ///     动态获取店铺
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static dynamic GetCity(long userId)
        {
            var orderService = DynamicService.Resolve("City");
            var target = new Interpreter().SetVariable("cityService", orderService);
            var parameters = new[]
            {
                new Parameter("userId", userId)
            };
            var user = (dynamic)target.Eval("cityService.GetCityByUserId(userId)", parameters);
            return user;
        }

        #endregion 动态获取城市合伙人

        #region 动态获取trade

        /// <summary>
        ///     Gets the single trade.
        ///     动态获取trade
        /// </summary>
        /// <param name="tradeId">Name of the trade.</param>
        /// <returns>dynamic.</returns>
        public static dynamic GetSingleTrade(long tradeId)
        {
            var tradeService = DynamicService.Resolve("Trade");
            var target = new Interpreter().SetVariable("tradeService", tradeService);
            var parameters = new[]
            {
                new Parameter("tradeId", tradeId)
            };
            var trade = (dynamic)target.Eval("tradeService.GetSingle(tradeId)", parameters);
            return trade;
        }

        #endregion 动态获取trade

        #region 动态获取所有枚举

        /// <summary>
        ///     Gets the single user.
        ///     动态获取获取所有的活动
        /// </summary>
        /// <returns>dynamic.</returns>
        public static IEnumerable<Type> GetAllEnum()
        {
            var service = DynamicService.Resolve("TypeService");
            var target = new Interpreter().SetVariable("service", service);
            var result = target.Eval("service.GetAllEnumType()");
            return (IEnumerable<Type>)result;
        }

        #endregion 动态获取所有枚举

        #region 动态获取所有级联

        /// <summary>
        ///     Gets the single user.
        ///     动态获取所有级联
        /// </summary>
        /// <returns>dynamic.</returns>
        public static IEnumerable<Type> GetAllRelationTypes()
        {
            var service = DynamicService.Resolve("Relation");
            var target = new Interpreter().SetVariable("service", service);
            var result = target.Eval("service.GetAllTypes()");
            return (IEnumerable<Type>)result;
        }

        #endregion 动态获取所有级联

        #region 动态获取所有AutoConfig

        /// <summary>
        ///     Gets the single user.
        ///     动态获取获取所有的活动
        /// </summary>
        /// <returns>dynamic.</returns>
        public static IEnumerable<Type> GetAllAutoConfig()
        {
            var service = DynamicService.Resolve("AutoConfig");
            var target = new Interpreter().SetVariable("service", service);
            var result = target.Eval("service.GetAllTypes()");
            return (IEnumerable<Type>)result;
        }

        #endregion 动态获取所有AutoConfig

        #region 动态增加用户的库存

        /// <summary>
        ///     Gets the single user.
        ///     动态增加用户的库存
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns>dynamic.</returns>
        public static dynamic UpdateUserStock(long orderId)
        {
            var service = DynamicService.Resolve("UserStock");
            var target = new Interpreter().SetVariable("userStockService", service);
            var parameters = new[]
            {
                new Parameter("orderId", orderId)
            };
            var user = (dynamic)target.Eval("userStockService.UpdateUserStock(orderId)", parameters);
            return user;
        }

        #endregion 动态增加用户的库存

        #region 动态获取所有的表单

        public static List<string> GetSqlTable()
        {
            var unitOfWork = Ioc.Resolve<IUnitOfWork>();
            var repositoryType = "CatalogRepository".GetTypeByName();
            var repository = Activator.CreateInstance(repositoryType, unitOfWork);
            var target = new Interpreter().SetVariable("repository", repository);
            var user = (dynamic)target.Eval("repository.GetSqlTable()");
            return user;
        }

        #endregion 动态获取所有的表单

        #region 动态获取用户

        /// <summary>
        ///     Gets the single user.
        ///     动态获取用户
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>dynamic.</returns>
        public static dynamic GetSingleUser(string userName)
        {
            var objectCache = Ioc.Resolve<IObjectCache>();
            return objectCache.GetOrSet(() =>
                {
                    var userService = DynamicService.Resolve("User");

                    var target = new Interpreter().SetVariable("userService", userService);
                    var parameters = new[]
                    {
                        new Parameter("userName", userName)
                    };
                    var user = (dynamic)target.Eval("userService.GetSingle(userName)", parameters);
                    return user;
                }, "dynamic_GetSingleUser" + userName, TimeSpan.FromHours(1)).Value;
        }

        /// <summary>
        ///     Gets the single user.
        ///     动态获取用户
        /// </summary>
        /// <param name="userId">Name of the user.</param>
        /// <returns>dynamic.</returns>
        public static dynamic GetSingleUser(long userId)
        {
            var userService = DynamicService.Resolve("User");
            var target = new Interpreter().SetVariable("userService", userService);
            var parameters = new[]
            {
                new Parameter("userId", userId)
            };
            var user = (dynamic)target.Eval("userService.GetSingle(userId)", parameters);
            return user;
        }

        /// <summary>
        ///     Determines whether the specified 会员 identifier is admin.
        ///     是否是员工
        /// </summary>
        /// <param name="userId">会员Id</param>
        public static dynamic IsAdmin(long userId)
        {
            var userService = DynamicService.Resolve("User");
            ;
            var target = new Interpreter().SetVariable("userService", userService);
            var parameters = new[]
            {
                new Parameter("userId", userId)
            };
            var user = (dynamic)target.Eval("userService.IsAdmin(userId)", parameters);
            return user;
        }

        #endregion 动态获取用户
    }
}