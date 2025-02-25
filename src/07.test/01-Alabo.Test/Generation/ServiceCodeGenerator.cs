﻿//using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;

using System.Linq;
using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Domains.Repositories.Model;
using Alabo.Extensions;
using Alabo.Framework.Basic.PostRoles.Domain.Entities;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Helpers;
using Alabo.Tables.Domain.Services;
using Alabo.Test.Base.Core.Model;
using Alabo.Test.Generation.CodeTemplate;
using Xunit;

namespace Alabo.Test.Generation
{
    /// <summary>
    ///     服务类代码生成
    /// </summary>
    public class ServiceCodeGenerator : CoreTest
    {
        private readonly string _serviceHostUrl = "http://localhost:9018";

        /// <summary>
        ///     Mongodb所有的服务生成
        /// </summary>
        [Fact]
        public void MongodbCreate_Code()
        {
            Resolve<ITableService>().Init();
            var tables = Resolve<ITableService>().GetList(r => r.TableType == TableType.Mongodb);
            tables = tables.Where(r => !r.Type.Contains("Base")).ToList();
            var typeService = Resolve<ITypeService>();
            foreach (var item in tables)
            {
                var type = item.Type.GetTypeByFullName();
                Assert.NotNull(type);
                ServcieTemplate.Create(type);
                RepositroyTemplate.Create(type);
                ApiControllerTemplate.CreateApiController(type, "ObjectId");
            }
        }

        /// <summary>
        ///     Mongodb所有的服务生成
        /// </summary>
        [Fact]
        public void MongodbCreate_Test_Code()
        {
            var types = BaseTemplate.GetMongoEntityTypes();
            var typeService = Resolve<ITypeService>();
            foreach (var type in types)
            {
                ServcieTemplate.Create(type);
                RepositroyTemplate.Create(type);
                ApiControllerTemplate.CreateApiController(type, "ObjectId");
            }
        }

        /// <summary>
        ///     SqlServer所有的服务生成
        /// </summary>
        [Fact]
        public void SqlServer_Code()
        {
            Resolve<ITableService>().Init();
            var tables = Resolve<ITableService>().GetList(r => r.TableType == TableType.SqlServer);
            var typeService = Resolve<ITypeService>();
            foreach (var item in tables)
            {
                var type = item.Type.GetTypeByFullName();
                Assert.NotNull(type);
                ServcieTemplate.Create(type);
                RepositroyTemplate.Create(type);
                ApiControllerTemplate.CreateApiController(type, "long");
            }
        }

        [Fact]
        public void TestCreateCode_testSingle()
        {
            var type = typeof(PostRole);
            //   var type = typeof(IAutoReportService);
            ServcieTemplate.Create(type);
            RepositroyTemplate.Create(type);
            ApiControllerTemplate.CreateApiController(type, "ObjectId");
            var typeService = Resolve<ITypeService>();
            // AdminControllerTemplate.Create(type, "ObjectId", typeService);

            // type = typeof(CustomShopRecord);
            //ServcieTemplate.Create(type);
            //RepositroyTemplate.Create(type);
            //ApiControllerTemplate.CreateApiController(type, "ObjectId");
        }

        [Fact]
        public void TestCreateCode_testSingle_Sql()
        {
            ////var type = typeof(UserBind);
            ////ServcieTemplate.Create(type);
            ////RepositroyTemplate.Create(type);
            ////ApiControllerTemplate.CreateApiController(type, "long");
            ////var typeService = Resolve<ITypeService>();
            ////AdminControllerTemplate.Create(type, "long", typeService);
        }

        [Fact]
        public void TestOrderSync()
        {
            var tenantName = "tenant2";
            using (var scope = Ioc.BeginScope())
            {
                //switch database
                //TenantContext.SwitchDatabase(scope, tenantName);
                //new ProductSyncService(scope).ProductDataSync();
            }

            //var webService = new OrderWebService(Ioc.BeginScope());
            //webService.SyncOrdersFromPlatform();
            //webService.SyncOrdersToPlatform();
        }
    }
}