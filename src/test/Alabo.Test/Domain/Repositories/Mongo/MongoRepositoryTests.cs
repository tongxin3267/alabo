using Xunit;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domain.Repositories.Mongo
{
    public class MongoRepositoryTests : CoreTest
    {
        /// <summary>
        ///     Mongo ˝æ›ÃÌº”≤‚ ‘
        /// </summary>
        [Fact]
        public void AddTest()
        {
            //var product = Service<IProductService>().GetList().FirstOrDefault();

            //var userStock = new UserStock {
            //    UserId = 1,
            //    ProductId = product.Id,
            //    StoreId = product.StoreId
            //};
            //var skus = Service<IProductSkuService>().GetList(r => r.ProductId == product.Id);
            //foreach (var sku in skus) {
            //    var skuStock = new SkuStock {
            //        SkuId = sku.Id,
            //        Stock = new Random().Next(1, 2000),
            //        PropertyValueDesc = sku.PropertyValueDesc
            //    };

            //    userStock.SkuStocks.Add(skuStock);
            //}

            //var result = Service<IUserStockService>().Add(userStock);
            //Assert.True(result);
            //Service<IUserStockService>().Delete(r => r.Id == userStock.Id);
        } /*end*/
    }
}