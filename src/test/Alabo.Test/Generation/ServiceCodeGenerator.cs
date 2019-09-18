﻿//using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories.Model;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Tenants;
using Alabo.Test.Base.Core.Model;
using Alabo.Test.Generation.CodeTemplate;

namespace Alabo.Test.Generation {

    /// <summary>
    ///     服务类代码生成
    /// </summary>
    public class ServiceCodeGenerator : CoreTest {
        private readonly string _serviceHostUrl = "http://localhost:9018";

        [Fact]
        public void asd() {
            var query = new ExpressionQuery<Order> {
                PageIndex = 1,
                PageSize = 15
            };

            var view = Resolve<IOrderService>().GetPagedList(query);
            var orders = new List<Order>();
            foreach (var item in view) {
                TimeSpan ts = DateTime.Now.Subtract(item.CreateTime);
                if (ts.Days < 7) {
                    orders.Add(item);
                }
            }

            view.Result = orders;
            var modelType = "Order".GetTypeByName();
            var result = Resolve<IAdminTableService>().ToExcel(modelType, view);
        }

        [Fact]
        public void T2() {
            var id = 188;
            var url = $@"{_serviceHostUrl}/Api/DataSync/GetProduct?id={id}";
            var json = url.HttpGet();
            var obj = json.ToObject<Product>();

            TenantContext.CurrentTenant = "tenant1";
            obj.Id = 0;

            var rs = Ioc.Resolve<IProductService>().Add(obj);
        }

        [Fact]
        public void TestOrderSync() {
            var tenantName = "tenant2";
            using (var scope = Ioc.BeginScope()) {
                //switch database
                //TenantContext.SwitchDatabase(scope, tenantName);
                //new ProductSyncService(scope).ProductDataSync();
            }

            //var webService = new OrderWebService(Ioc.BeginScope());
            //webService.SyncOrdersFromPlatform();
            //webService.SyncOrdersToPlatform();
        }

        /// <summary>
        ///     Mongodb所有的服务生成
        /// </summary>
        [Fact]
        public void MongodbCreate_Code() {
            Resolve<ITableService>().Init();
            var tables = Resolve<ITableService>().GetList(r => r.TableType == TableType.Mongodb);
            tables = tables.Where(r => !r.Type.Contains("Base")).ToList();
            var typeService = Resolve<ITypeService>();
            foreach (var item in tables) {
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
        public void MongodbCreate_Test_Code() {
            var types = BaseTemplate.GetMongoEntityTypes();
            var typeService = Resolve<ITypeService>();
            foreach (var type in types) {
                ServcieTemplate.Create(type);
                RepositroyTemplate.Create(type);
                ApiControllerTemplate.CreateApiController(type, "ObjectId");
            }
        }

        /// <summary>
        ///     SqlServer所有的服务生成
        /// </summary>
        [Fact]
        public void SqlServer_Code() {
            Resolve<ITableService>().Init();
            var tables = Resolve<ITableService>().GetList(r => r.TableType == TableType.SqlServer);
            var typeService = Resolve<ITypeService>();
            foreach (var item in tables) {
                var type = item.Type.GetTypeByFullName();
                Assert.NotNull(type);
                ServcieTemplate.Create(type);
                RepositroyTemplate.Create(type);
                ApiControllerTemplate.CreateApiController(type, "long");
            }
        }

        [Fact]
        public void TestCreateCode_testSingle() {
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
        public void TestCreateCode_testSingle_Sql() {
            ////var type = typeof(UserBind);
            ////ServcieTemplate.Create(type);
            ////RepositroyTemplate.Create(type);
            ////ApiControllerTemplate.CreateApiController(type, "long");
            ////var typeService = Resolve<ITypeService>();
            ////AdminControllerTemplate.Create(type, "long", typeService);
        }
    }
}