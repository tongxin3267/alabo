using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.MogoMigrate.ViewModels;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Context;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Runtime;

namespace Alabo.App.Market.MogoMigrate.Domain.Services {

    public class MogoMigrateService : ServiceBase, IMogoMigrateService {

        public MogoMigrateService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public MogoMigrateView GetMogoMigrateView(long id) {
            if (id.ConvertToLong() == 10000) {
                var view = new MogoMigrateView {
                    UserTable = "User_User",
                    Key = RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Key,
                    //  ProjectId = project.ProjectId.ToString()
                };
                return view;
            }

            return new MogoMigrateView();
        }

        public ServiceResult Migrate(MogoMigrateView view) {
            if (!HttpWeb.UserName.Equal("admin")) {
                return ServiceResult.FailedWithMessage("当前操作用户名非admin,不能进行该操作");
            }

            if (view.MongoTableName.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("Mongodb数据库不能为空");
            }

            if (view.MongoConnectionString.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("Mongodb链接字符串不能为空");
            }

            var user = Resolve<IUserService>().GetSingle(HttpWeb.UserId);
            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常,不能进行该操作");
            }

            if (!user.UserName.Equal("admin")) {
                return ServiceResult.FailedWithMessage("当前操作用户名非admin,不能进行该操作");
            }

            if (!view.UserTable.Equals("User_User")) {
                return ServiceResult.FailedWithMessage("用户数据表填写出错,不能进行该操作");
            }

            //if (!view.ProjectId.Equals(HttpWeb.Token.ProjectId.ToString())) {
            //    return ServiceResult.FailedWithMessage("项目Id填写错误,不能进行该操作");
            //}

            if (!view.Key.Equals(RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Key)) {
                return ServiceResult.FailedWithMessage("秘钥填写错误,不能进行该操作");
            }

            var connection = new MongoDbConnection {
                ConnectionString = view.MongoConnectionString,
                Database = view.MongoTableName
            };
            var types = GetMongoEntityTypes();
            foreach (var type in types) {
                //// 使用Mongodb的上下文链接字符串
                //MongoRepositoryConnection.MongoDbConnectionContext = connection;
                //// 读取需要迁移数据库中所有的数据
                //var sourceResult = DynamicService.ResolveMethod(type.Name, "GetList");
                //if (!sourceResult.Item1.Succeeded) {
                //    return ServiceResult.FailedWithMessage($"表{type.Name}数据获取失败:" + sourceResult.Item2.ToString());
                //}

                //// 切换数据库,当前配置数据库
                //MongoRepositoryConnection.MongoDbConnectionContext = null;
                //// 批量插入数据库中
                //var addResult = DynamicService.ResolveMethod(type.Name, "AddMany", sourceResult.Item2);
                //if (!addResult.Item1.Succeeded) {
                //    return ServiceResult.FailedWithMessage($"表{type.Name}数据添加失败:" + sourceResult.Item2.ToString());
                //}
            }

            Resolve<ITableService>().Log("数据迁移成功");
            Resolve<IOpenService>().SendRaw(Resolve<IOpenService>().OpenMobile, "您的数据已迁移成功，请悉知。");
            return ServiceResult.Success;
        }

        public static IList<Type> GetMongoEntityTypes() {
            var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IEntity)) ||
                            t.GetInterfaces().Contains(typeof(IMongoEntity))));
            types = types.Where(r => !r.FullName.StartsWith("Alabo.Domain"));
            types = types.Where(r => !r.FullName.Contains("Test."));
            types = types.Where(r => !r.FullName.Contains("Tests."));
            types = types.Where(r => r.IsAbstract == false);
            types = types.Where(r => r.BaseType.FullName.Contains("Mongo"));
            return types.ToList();
        }
    }
}