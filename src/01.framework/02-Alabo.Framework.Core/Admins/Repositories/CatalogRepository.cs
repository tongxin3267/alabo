using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Mongo.Context;
using Alabo.Extensions;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Helpers;
using Alabo.Users.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Alabo.Framework.Core.Admins.Repositories
{
    public class CatalogRepository : RepositoryEfCore<User, long>, ICatalogRepository
    {
        public CatalogRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     更新数据库脚本
        ///     https://www.cnblogs.com/jes_shaw/archive/2013/05/14/3077215.html
        /// </summary>
        public void UpdateDataBase() {
            var sqlList = new List<string> {
            };

            // 任务统计表
            var sql = @"CREATE TABLE [dbo].[Target_Report](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [UserId] [bigint] NOT NULL,
	                    [Name] [nchar](50) NOT NULL,
	                    [TargetId] [nchar](10) NULL,
	                    [Type] [int] NOT NULL,
	                    [MoneyTypeId] [uniqueidentifier] NULL,
	                    [Bonus] [decimal](18, 2) NOT NULL,
	                    [Contribution] [decimal](18, 2) NOT NULL,
	                    [CreateTime] [datetime2](7) NOT NULL,
                     CONSTRAINT [PK__Target_R__3214EC07FBF1C56C] PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY]";
            sqlList.Add(sql);

            // 设置索引等数据
            var uniqueList = new List<string>
            {
               SetNonclustered("Target_Report","UserId"),
               SetNonclustered("Target_Report","Type"),
            };
            sqlList.AddRange(uniqueList);
            ExecuteSql(sqlList);
        }

        #region 清空表单

        public void TruncateTable() {
            // ZKCloud项目
            DropMongoDbTable("Core_Logs");
            DropMongoDbTable("Core_Table");

            // Core项目
            DropMongoDbTable("Basic_Logs");
            DropMongoDbTable("Basic_Report");
            //用户
            DropMongoDbTable("User_UserAddress");
            DropMongoDbTable("User_GradeInfo");
            DropMongoDbTable("User_Identity");

            // 财务
            DropMongoDbTable("Asset_BankCard");

            // 任务
            DropMongoDbTable("Task_UpgradeRecord");
            DropMongoDbTable("Task_Schedule");
            DropMongoDbTable("Things_ShareOrderReport");

            // shop项目
            DropMongoDbTable("Order_Cart");

            // Open项目
            DropMongoDbTable("Attach_FootPrint");
            DropMongoDbTable("Attach_Comment");
            DropMongoDbTable("Attach_Comment");
            DropMongoDbTable("Basic_Favorite");
            DropMongoDbTable("Attach_Letter");
            DropMongoDbTable("Kpi_GradeKpi");

            // erp项目
            DropMongoDbTable("Erp_UserStock");

            // 插件
            DropMongoDbTable("Market_UserRight");
            // 以下数据为，会员初始化时使用
            var sqlList = new List<string>
            {
                // 用户相关表(5个)
                "truncate table User_User",
                "truncate table User_UserDetail",
                "truncate table User_UserMap",
                //用户权益
                "truncate table Market_UserRights",

                // 财务相关表
                "truncate table Asset_Account",
                "truncate table Asset_Bill",
                "truncate table Asset_Pay",
                "truncate table Asset_Trade",

                //绩效相关表
                "truncate table Kpi_Kpi",

                // 分润相关表
                "truncate table Things_ShareOrder",
                "truncate table Share_Reward",
                "truncate table Task_TaskQueue",

                // 订单相关表（四个）
                "truncate table Shop_Order",
                "truncate table Shop_OrderAction",
                "truncate table Shop_OrderDelivery",
                "truncate table Shop_OrderProduct",

                // 活动相关表
                "truncate table Shop_ActivityRecord",

                // 其他
                "truncate table Basic_MessageQueue",

                //客户相关
                "truncate table Insurance_Order"
            };

            ExecuteSql(sqlList);
        }

        #endregion 清空表单

        #region 获取所有的Sql表实体

        /// <summary>
        ///     获取所有的Sql表实体
        /// </summary>
        public List<string> GetSqlTable() {
            var entityNames = Ioc.Resolve<ITypeService>().GetAllEntityType().Select(r => r.Name).ToList();
            var list = new List<string>();
            var sql = "select name from sys.tables";
            using (var reader =
                RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    var tableName = reader["name"].ToStr();
                    var arrarys = tableName.SplitString("_").ToList();
                    if (arrarys.Count > 1) {
                        var entity = arrarys[1];
                        if (entityNames.Contains(entity)) {
                            list.Add(tableName);
                        }
                    }
                }
            }

            return list;
        }

        #endregion 获取所有的Sql表实体

        #region others

        /// <summary>
        ///     执行脚本
        /// </summary>
        /// <param name="sqlList"></param>
        private void ExecuteSql(List<string> sqlList) {
            foreach (var item in sqlList) {
                try {
                    RepositoryContext.ExecuteNonQuery(item);
                } catch (Exception ex) {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        #endregion others

        #region 备份脚本

        private void BackUpdateDataBase() {
            var sqlList = new List<string>
            {
                // 重构 2019年9月30日
                "drop table User_UserType",
                "drop table User_TypeUser",
                "EXEC sp_rename 'Common_AutoConfig', 'Core_AutoConfig'",
                "EXEC sp_rename 'ZKShop_Category', 'Shop_Category'",
                "EXEC sp_rename 'ZKShop_CategoryProperty', 'Shop_CategoryProperty'",
                " EXEC sp_rename 'ZKShop_CategoryPropertyValue', 'Shop_CategoryPropertyValue'",
                " EXEC sp_rename 'ZKShop_Activity', 'Shop_Activity'",
                " EXEC sp_rename 'ZKShop_OrderAction', 'Shop_OrderAction'",
                "  EXEC sp_rename 'ZKShop_OrderDelivery', 'Shop_OrderDelivery'",
                " EXEC sp_rename 'ZKShop_OrderProduct', 'Shop_OrderProduct'",
                " EXEC sp_rename 'ZKShop_ProductDetail', 'Shop_ProductDetail'",
                "EXEC sp_rename 'ZKShop_ProductSku', 'Shop_ProductSku'",
                "EXEC sp_rename 'ZKShop_Product', 'Shop_Product'",
                "EXEC sp_rename 'ZKShop_Order', 'Shop_Order'",
                "EXEC sp_rename 'ZKShop_ActivityRecord', 'Shop_ActivityRecord'",

                "EXEC sp_rename 'Common_MessageQueue', 'Basic_MessageQueue'",
                "EXEC sp_rename 'Common_Record', 'Basic_Record'",
                "EXEC sp_rename 'Common_RelationIndex', 'Basic_RelationIndex'",
                "EXEC sp_rename 'Common_Relation', 'Basic_Relation'",

                "EXEC sp_rename 'Finance_Bill', 'Asset_Bill'",
                "EXEC sp_rename 'Finance_Pay', 'Asset_Pay'",
                "EXEC sp_rename 'Finance_Account', 'Asset_Account'",
                "EXEC sp_rename 'Task_ShareOrder', 'Things_ShareOrder'",

                "drop table ZKShop_Store",
                "drop table Finance_Trade",
                " drop table ZKShop_ProductLine",
                "alter table User_UserDetail  drop column IsServiceCenter",
                " alter table User_UserDetail  drop column ServiceCenterUserId",

                " alter table User_Map  drop column ShopSaleInfo",
                "alter table User_Map  drop column TeamSales",

                "  ALTER TABLE User_UserDetail Add IdentityStatus  [int]",
                " update User_UserDetail set IdentityStatus = 1 where IdentityStatus is null",
            };

            var sql =
               @"CREATE TABLE [dbo].[User_UserDetail](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [UserId] [bigint] NOT NULL,
	                [Password] [nvarchar](255) NOT NULL,
	                [PayPassword] [nvarchar](255) NULL,
	                [RegionId] [bigint] NOT NULL,
	                [Sex] [int] NOT NULL,
	                [Birthday] [datetime2](7) NOT NULL,
	                [CreateTime] [datetime2](7) NOT NULL,
	                [RegisterIp] [nvarchar](max) NULL,
	                [LoginNum] [bigint] NOT NULL,
	                [LastLoginIp] [nvarchar](50) NULL,
	                [LastLoginTime] [datetime2](7) NOT NULL,
	                [ModifiedTime] [datetime2](7) NOT NULL,
	                [OpenId] [nvarchar](255) NULL,
	                [Remark] [nvarchar](max) NULL,
	                [Avator] [nvarchar](255) NULL,
	                [NickName] [nvarchar](50) NULL,
	                [AddressId] [nvarchar](50) NULL,
	                [IdentityStatus] [int] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);
            sqlList.Add("ALTER TABLE [dbo].[User_UserDetail] ADD  DEFAULT ('') FOR [Avator]");

            sql =
                @"CREATE TABLE [dbo].[Offline_MerchantOrder](
                        [Id][bigint] IDENTITY(1, 1) NOT NULL,
                        [UserId] BIGINT NOT NULL,
                        [MerchantStoreId] [NVARCHAR](127) NOT NULL,
                        [OrderType] INT NOT NULL default 1,
                        [PayId] [bigint] NOT NULL default 0,
                        [PaymentAmount] [decimal](18, 2) NOT NULL,
                        [TotalCount] [bigint] NOT NULL default 0,
                        [TotalAmount] [decimal](18,2) NOT NULL,
                        [OrderStatus] [int] NOT NULL,
                        [AccountPay] NVARCHAR(max) NULL DEFAULT NULL,
                        [Extension] [text] NULL DEFAULT NULL,
                        [CreateTime] [datetime2](7) NOT NULL,
                        CONSTRAINT [PK_Offline_MerchantOrder] PRIMARY KEY CLUSTERED
                        (
	                        [Id] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                      ) ON [PRIMARY]";
            sqlList.Add(sql);

            sql =
                @"CREATE TABLE [dbo].[Offline_MerchantOrderProduct](
                        [Id][bigint] IDENTITY(1, 1) NOT NULL,
                        [MerchantStoreId] [NVARCHAR](127) NOT NULL,
	                    [OrderId] BIGINT NOT NULL,
                        [MerchantProductId] [NVARCHAR](127) NOT NULL,
                        [SkuId] [NVARCHAR](127) NOT NULL,
                        [Count] [bigint] NOT NULL default 0,
                        [Amount] DECIMAL(18,2) NOT NULL,
                        [FenRunAmount] [decimal](18, 2) NOT NULL,
                        [PaymentAmount] [decimal](18, 2) NOT NULL,
                        [Extension] [text] NULL DEFAULT NULL,
                        [CreateTime] [datetime2](7) NOT NULL,
                        CONSTRAINT [PK_Offline_MerchantOrderProduct] PRIMARY KEY CLUSTERED
                        (
	                        [Id] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                     ) ON [PRIMARY]";
            sqlList.Add(sql);

            // 提现表
            sql = @"CREATE TABLE [dbo].[Asset_Withdraw](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [UserId] [bigint] NOT NULL,
	                    [BankCardId] [nvarchar](127) NULL,
	                    [MoneyTypeId] [uniqueidentifier] NOT NULL,
	                    [Type] [int] NOT NULL,
	                    [Amount] [decimal](18, 2) NOT NULL,
	                    [CheckAmount] [decimal](18, 2) NOT NULL,
	                    [Fee] [decimal](18, 2) NOT NULL,
	                    [Status] [int] NOT NULL,
	                    [CreateTime] [datetime2](7) NOT NULL,
	                    [PayTime] [datetime2](7) NOT NULL,
	                    [UserRemark] [text] NULL,
	                    [Remark] [text] NULL,
	                    [FailureReason] [text] NULL,
	                    [Extension] [text] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);

            // 转账表
            sql = @"CREATE TABLE [dbo].[Asset_Transfer](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [UserId] [bigint] NOT NULL,
	                    [OtherUserId] [bigint] NOT NULL,
	                    [MoneyTypeId] [uniqueidentifier] NOT NULL,
	                    [ConfigId] [uniqueidentifier] NOT NULL,
	                    [Amount] [decimal](18, 2) NOT NULL,
	                    [Fee] [decimal](18, 2) NOT NULL,
	                    [Intro] [text] NULL,
	                    [CreateTime] [datetime2](7) NOT NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);

            // 充值表
            sql = @"CREATE TABLE [dbo].[Asset_Recharge](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [UserId] [bigint] NOT NULL,
	                [BankCardId] [nvarchar](127) NULL,
	                [MoneyTypeId] [uniqueidentifier] NOT NULL,
	                [Type] [int] NOT NULL,
	                [Amount] [decimal](18, 2) NOT NULL,
	                [CheckAmount] [decimal](18, 2) NOT NULL,
	                [Fee] [decimal](18, 2) NOT NULL,
	                [Status] [int] NOT NULL,
	                [PayType] [int] NOT NULL,
	                [RechargeType] [int] NOT NULL,
	                [CreateTime] [datetime2](7) NOT NULL,
	                [PayTime] [datetime2](7) NOT NULL,
	                [UserRemark] [text] NULL,
	                [Remark] [text] NULL,
	                [FailureReason] [text] NULL,
	                [Extension] [text] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);

            // 退款表
            sql = @"CREATE TABLE [dbo].[Asset_Recharge](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [UserId] [bigint] NOT NULL,
	                [BankCardId] [nvarchar](127) NULL,
	                [MoneyTypeId] [uniqueidentifier] NOT NULL,
	                [Type] [int] NOT NULL,
	                [Amount] [decimal](18, 2) NOT NULL,
	                [CheckAmount] [decimal](18, 2) NOT NULL,
	                [Fee] [decimal](18, 2) NOT NULL,
	                [Status] [int] NOT NULL,
	                [PayType] [int] NOT NULL,
	                [RechargeType] [int] NOT NULL,
	                [CreateTime] [datetime2](7) NOT NULL,
	                [PayTime] [datetime2](7) NOT NULL,
	                [UserRemark] [text] NULL,
	                [Remark] [text] NULL,
	                [FailureReason] [text] NULL,
	                [Extension] [text] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);

            // 退款表
            sql = @"CREATE TABLE [dbo].[Asset_Refund](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [UserId] [bigint] NOT NULL,
	                    [MoneyTypeId] [uniqueidentifier] NOT NULL,
	                    [ConfigId] [uniqueidentifier] NOT NULL,
	                    [Amount] [decimal](18, 2) NOT NULL,
	                    [Fee] [decimal](18, 2) NOT NULL,
	                    [Status] [int] NOT NULL,
	                    [CreateTime] [datetime2](7) NOT NULL,
	                    [PayTime] [datetime2](7) NOT NULL,
	                    [UserRemark] [text] NULL,
	                    [Remark] [text] NULL,
	                    [FailureReason] [text] NULL,
	                    [Extension] [text] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);

            // 清算表
            sql = @"CREATE TABLE [dbo].[Asset_Settlement](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [UserId] [bigint] NOT NULL,
	                    [WithdrawId] [bigint] NOT NULL,
	                    [MoneyTypeId] [uniqueidentifier] NOT NULL,
	                    [ConfigId] [uniqueidentifier] NOT NULL,
	                    [Amount] [decimal](18, 2) NOT NULL,
	                    [Fee] [decimal](18, 2) NOT NULL,
	                    [Status] [int] NOT NULL,
	                    [CreateTime] [datetime2](7) NOT NULL,
	                    [Extension] [text] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            sqlList.Add(sql);

            ExecuteSql(sqlList);
        }

        #endregion 备份脚本

        #region 设置索引、主键、唯一等

        /// <summary>
        ///     设置非聚集索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column"></param>
        private string SetNonclustered(string tableName, string column) {
            var sql =
                $"CREATE NONCLUSTERED INDEX [NonClusteredIndex-{column}] ON [dbo].[{tableName}](	[{column}] ASC)WITH (PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            return sql;
        }

        /// <summary>
        ///     设置唯一索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column"></param>
        private string SetUnique(string tableName, string column) {
            var sql =
                $"CREATE unique  INDEX [uniqueIndex-{column}] ON [dbo].[{tableName}](	[{column}] ASC)WITH (PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            return sql;
        }

        /// <summary>
        ///     设置主键
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        private string SetPrimaryKey(string tableName, string key = "Id") {
            var sql =
                $"ALTER TABLE [dbo].[{tableName}] ADD  CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED ([{key}] ASC)";
            return sql;
        }

        /// <summary>
        ///     清空mongodb表
        /// </summary>
        /// <param name="tableName"></param>
        private void DropMongoDbTable(string tableName) {
            try {
                MongoRepositoryConnection.DropTable(tableName);
            } catch (Exception ex) {
                Trace.WriteLine(ex.Message);
            }
        }

        #endregion 设置索引、主键、唯一等
    }
}