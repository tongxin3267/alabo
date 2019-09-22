using System.Linq;
using Xunit;
using ZKCloud.App.Core.Admin.Domain.Services;
using ZKCloud.Domains.Base.Services;
using ZKCloud.Domains.Repositories.Model;
using ZKCloud.Extensions;
using ZKCloud.Linq.Dynamic;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domain.Base {

    public class ITableServiceTest : CoreTest {

        /// <summary>
        ///     所有的实体服务测试，看是否配置正确，非常重要
        /// </summary>
        [Fact]
        public void AllEntityServcie_Test() {
            var entitysList = Resolve<ITableService>()
                .GetList(r => r.TableType == TableType.Mongodb || r.TableType == TableType.SqlServer);
            foreach (var item in entitysList) {
                var servcieName = Resolve<ITypeService>().GetServiceTypeByEntity(item.Key);
                Assert.True(!servcieName.IsNullOrEmpty());

                var serviceType = Resolve<ITypeService>().GetServiceTypeFromEntity(item.Key);
                Assert.NotNull(serviceType);

                var interfaces = serviceType.GetInterfaces();

                var baseService = interfaces.FirstOrDefault();

                var idArgument = baseService.GenericTypeArguments[1];
                Assert.NotNull(idArgument);

                var entityType = item.Type.GetTypeByFullName();

                Assert.NotNull(entityType);

                var idColumn = item.Columns.FirstOrDefault(r => r.Key == "Id");
                Assert.Equal(idColumn.Type, idArgument.Name);

                // 动态服务测试
                var dynamicService = DynamicService.Resolve(item.Key);
                Assert.NotNull(dynamicService);
            }
        }

        /// <summary>
        ///     初始化所有的表单
        /// </summary>
        [Fact]
        public void InitTest() {
            // Resolve<ICatalogService>().UpdateDatabase();
            //  Resolve<ITableService>().DeleteAll();
            Resolve<ITableService>().Init();
            var tables = Resolve<ITableService>().GetList();
            Assert.True(tables.Any());

            var find = tables.FirstOrDefault(r => r.Type == "ZKCloud.App.Cms.Articles.Domain.Entities.Article");
            Assert.NotNull(find);
            Assert.True(find.Key == "Article");
            Assert.True(find.TableName == "CMS_Article");

            foreach (var itemTable in tables) {
                if (itemTable.TableType == TableType.Mongodb || itemTable.TableType == TableType.Mongodb) {
                    Assert.NotNull(itemTable.TableName);
                }

                Assert.NotNull(itemTable.Key);
            }

            Assert.True(tables.Count() > 120);
        }
    }
}