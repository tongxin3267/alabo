using Xunit;
using ZKCloud.App.Cms.Articles.Domain.Services;
using ZKCloud.App.Cms.Support.Domain.Services;
using ZKCloud.App.Core.Common.Domain.Services;
using ZKCloud.App.Core.Finance.Domain.Services;
using ZKCloud.App.Core.Reports.Domain.Services;
using ZKCloud.App.Core.Tasks.Domain.Services;
using ZKCloud.App.Core.Themes.Domain.Services;
using ZKCloud.App.Core.User.Domain.Services;
using ZKCloud.App.Core.UserType.Domain.Services;
using ZKCloud.App.Open.Attach.Domain.Services;
using ZKCloud.App.Open.Share.Domain.Services;
using ZKCloud.App.Shop.Activitys.Domain.Services;
using ZKCloud.App.Shop.AfterSale.Domain.Services;
using ZKCloud.App.Shop.Category.Domain.Services;
using ZKCloud.App.Shop.Order.Domain.Services;
using ZKCloud.App.Shop.Product.Domain.Services;
using ZKCloud.App.Shop.Store.Domain.Services;
using ZKCloud.Domains.Base.Services;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domains.Services {

    /// <summary>
    ///     数据库结构测试
    /// </summary>
    public class DomainEntityTest : CoreTest {

        [Fact]
        public void Account_Test() {
            var result = Resolve<IAccountService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Activity_Test() {
            var result = Resolve<IActivityService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ActivityRecord_Test() {
            var result = Resolve<IActivityRecordService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Article_Test() {
            var result = Resolve<IArticleService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void AutoConfig_Test() {
            var result = Resolve<IAutoConfigService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Bill_Test() {
            var result = Resolve<IBillService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Cart_Test() {
            var result = Resolve<ICartService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Category_Test() {
            var result = Resolve<ICategoryService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void CategoryProperty_Test() {
            var result = Resolve<ICategoryPropertyService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void CategoryPropertyValue_Test() {
            var result = Resolve<ICategoryPropertyValueService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Channel_Test() {
            var result = Resolve<IChannelService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Circle_Test() {
            var result = Resolve<ICircleService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Comment_Test() {
            var result = Resolve<ICommentService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Extend_Test() {
            var result = Resolve<IExtendService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Favorite_Test() {
            var result = Resolve<IFavoriteService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Footprint_Test() {
            var result = Resolve<IFootprintService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void GradeKpi_Test() {
            var result = Resolve<IGradeKpiService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Kpi_Test() {
            var result = Resolve<IKpiService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Letter_Test() {
            var result = Resolve<ILetterService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Logs_Test() {
            var result = Resolve<ILogsService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void MessageQueue_Test() {
            var result = Resolve<IMessageQueueService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Order_Test() {
            var result = Resolve<IOrderService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void OrderAction_Test() {
            var result = Resolve<IOrderActionService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void OrderDelivery_Test() {
            var result = Resolve<IOrderDeliveryService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void OrderProduct_Test() {
            var result = Resolve<IOrderProductService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Pay_Test() {
            var result = Resolve<IPayService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Product_Test() {
            var result = Resolve<IProductService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ProductBuyStock_Test() {
            var result = Resolve<IProductBuyStockService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ProductDetail_Test() {
            var result = Resolve<IProductDetailService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ProductLine_Test() {
            var result = Resolve<IProductLineService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ProductSku_Test() {
            var result = Resolve<IProductSkuService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Record_Test() {
            var result = Resolve<IRecordService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Refund_Test() {
            var result = Resolve<IRefundService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Region_Test() {
            var result = Resolve<IRegionService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Relation_Test() {
            var result = Resolve<IRelationService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void RelationIndex_Test() {
            var result = Resolve<IRelationIndexService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Report_Test() {
            var result = Resolve<IReportService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Reward_Test() {
            var result = Resolve<IRewardService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Schedule_Test() {
            var result = Resolve<IScheduleService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ShareOrder_Test() {
            var result = Resolve<IShareOrderService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ShareOrderReport_Test() {
            var result = Resolve<IShareOrderReportService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void SinglePage_Test() {
            var result = Resolve<ISinglePageService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Special_Test() {
            var result = Resolve<ISpecialService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void StorageFile_Test() {
            var result = Resolve<IStorageFileService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void Store_Test() {
            var result = Resolve<IStoreService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Table_Test() {
            var result = Resolve<ITableService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void TaskQueueReport_Test() {
            var result = Resolve<ITaskQueueService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Theme_Test() {
            var result = Resolve<IThemeService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ThemeData_Test() {
            var result = Resolve<IThemeDataService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void ThemePage_Test() {
            var result = Resolve<IThemePageService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void Trade_Test() {
            var result = Resolve<ITradeService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void User_Action_Test() {
            //var result = Resolve<IUserActionService>().FirstOrDefault();
            //Assert.True(true);
        }

        [Fact]
        public void User_Detail_Test() {
            var result = Resolve<IUserDetailService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void User_Test() {
            var result = Resolve<IUserService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void User_User_Test() {
            var result = Resolve<IUserService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void UserAction_Test() {
            //var result = Resolve<IUserActionService>().FirstOrDefault();

            //Assert.True(true);
        }

        [Fact]
        public void UserDetail_Test() {
            var result = Resolve<IUserDetailService>().FirstOrDefault();
            Assert.True(true);
        }

        [Fact]
        public void UserMap_Test() {
            var result = Resolve<IUserDetailService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void UserStock_Test() {
            var result = Resolve<IUserStockService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void UserType_Test() {
            var result = Resolve<IUserTypeService>().FirstOrDefault();

            Assert.True(true);
        }

        [Fact]
        public void WorkOrder_Test() {
            var result = Resolve<IWorkOrderService>().FirstOrDefault();
            Assert.True(true);
        }
    }
}