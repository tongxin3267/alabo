﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Mongo.Context;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.Admin.Domain.Repositories {

    public class CatalogRepository : RepositoryEfCore<User.Domain.Entities.User, long>, ICatalogRepository {

        public CatalogRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     更新数据库脚本
        ///     https://www.cnblogs.com/jes_shaw/archive/2013/05/14/3077215.html
        /// </summary>
        public void UpdateDataBase() {
            var sqlList = new List<string> {
                //// 2019-03-14 (zhangzulian)
                "alter table ZKShop_Product add  DeliveryTemplateId  nvarchar(255)",
                //"update ZKShop_Product set DeliveryTemplateId='' where DeliveryTemplateId is null", // 新增钱包地址
            };
            ExecuteSql(sqlList);
        }

        #region 清空表单

        public void TruncateTable() {
            // ZKCloud项目
            DropMongoDbTable("Base_Logs");
            DropMongoDbTable("Base_Table");

            // Core项目
            DropMongoDbTable("Common_Logs");
            DropMongoDbTable("Common_Report");
            //用户
            DropMongoDbTable("User_UserAddress");
            DropMongoDbTable("User_GradeInfo");
            DropMongoDbTable("User_Identity");

            // 财务
            DropMongoDbTable("Finance_BankCard");

            // 任务
            DropMongoDbTable("Task_UpgradeRecord");
            DropMongoDbTable("Task_Schedule");
            DropMongoDbTable("Task_ShareOrderReport");

            // shop项目
            DropMongoDbTable("Order_Cart");

            // Open项目
            DropMongoDbTable("Attach_FootPrint");
            DropMongoDbTable("Attach_Comment");
            DropMongoDbTable("Attach_Comment");
            DropMongoDbTable("Attach_Favorite");
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
                "truncate table User_UserType",
                "truncate table User_UserTypeMap",
                //用户权益
                "truncate table Market_UserRights",

                // 财务相关表
                "truncate table Finance_Account",
                "truncate table Finance_Bill",
                      "truncate table Finance_Pay",
                "truncate table Finance_Trade",

                //绩效相关表
                "truncate table Kpi_Kpi",

                // 分润相关表
                "truncate table Task_ShareOrder",
                "truncate table Share_Reward",
                "truncate table Task_TaskQueue",

                // 订单相关表（四个）
                "truncate table ZKShop_Order",
                "truncate table ZKShop_OrderAction",
                "truncate table ZKShop_OrderDelivery",
                "truncate table ZKShop_OrderProduct",

                // 活动相关表
                "truncate table ZKShop_ActivityRecord",

                // 其他
                "truncate table Common_MessageQueue",

                //客户相关
                 "truncate table Insurance_Order",
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

        #region 过期的Sql脚本

        private void OldSql() {
            // 过期的Sql脚本
            var sqlList = new List<string>
            {
                //// 总分润金额 2018年9月21日
                //"ALTER TABLE Task_ShareOrder ADD  [TotalAmount] [decimal](18, 2)",
                //"update Task_ShareOrder set TotalAmount=0 where TotalAmount is null", // 新增分润总金额

                //新增钱包地址 2018年8月21日
                "ALTER TABLE Finance_Account ADD Token  nvarchar(255)  ",
                "update Finance_Account set Token='' where Token is null", // 新增钱包地址

                // 2018年8月17日 新增UserTypeMap表
                "CREATE TABLE [dbo].[User_UserTypeMap]([Id] [bigint]  IDENTITY(1,1) NOT NULL,[UserTypeId] [uniqueidentifier] NOT NULL,[TypeUserId] [bigint] NOT NULL,[TypeId] [bigint] NOT NULL,[UserId] [bigint] NOT NULL)",
                SetPrimaryKey("User_UserTypeMap"),
                // 2018年6月21日 新增免邮费
                " ALTER TABLE ZKShop_Product Add IsFreeShipping  [bit] ",
                "update ZKShop_Product set IsFreeShipping = 0 where IsFreeShipping is null",

                // 修改头像
                "ALTER TABLE User_UserDetail",
                "ALTER COLUMN Avator nvarchar(255)",
                // 2018年6月7日 新增发货用户ID
                " ALTER TABLE ZKShop_Order Add DeliverUserId  [bigint] ",
                "update ZKShop_Order set DeliverUserId = 0 where DeliverUserId is null",

                "alter table ZKShop_ActivityRecord  drop column [Type]",
                "alter table ZKShop_ActivityRecord  drop column [ProductId]",
                "ALTER TABLE ZKShop_ActivityRecord Add ParentId bigint",
                "update ZKShop_ActivityRecord set ParentId = 0 where ParentId is null",

                // 财务交易表
                "CREATE TABLE [dbo].[Finance_Trade]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Type] [int] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[Status] [int] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[PayTime] [datetime2](7) NOT NULL,[Extension] [text] NULL)",
                " ALTER TABLE Finance_Trade ADD  CONSTRAINT [PK_Finance_Trade] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF,KPI_NORECOMPUTE = OFF,SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF,ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]",
                //删除充值，提现表
                "drop table  Finance_Recharge",
                "drop table  Finance_WithDraw",
                //删除权限表
                "drop table  User_Role",

                //2018年5月13日 活动
                " ALTER TABLE ZKShop_ActivityRecord Add Status int",
                "update ZKShop_ActivityRecord set Status = 0 where Status is null",
                " ALTER TABLE ZKShop_ActivityRecord Add ProductId bigint",
                "update ZKShop_ActivityRecord set ProductId = 0 where ProductId is null",
                " ALTER TABLE ZKShop_ActivityRecord Add OrderId bigint",
                "update ZKShop_ActivityRecord set OrderId = 0 where OrderId is null",
                //2018年5月4日 活动
                "alter table ZKShop_ActivityRecord  drop column [ActivityRecordExtension]",
                "CREATE TABLE [dbo].[ZKShop_Activity]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](255) NOT NULL,[StoreId] [bigint] NOT NULL,[Key] [nvarchar](255) NOT NULL,[Value] [nvarchar](max) NULL,[IsEnable] [bit] NOT NULL,[Status] [int] NOT NULL,[LimitGradeId] [uniqueidentifier] NOT NULL,[MaxStock] [bigint] NOT NULL,[UsedStock] [bigint] NOT NULL,[Extension] [nvarchar](max) NULL,[StartTime] [datetime2](7) NOT NULL,[EndTime] [datetime2](7) NOT NULL)",
                " ALTER TABLE[dbo].[ZKShop_Activity] ADD CONSTRAINT[PK_ZKShop_Activity] PRIMARY KEY CLUSTERED([Id] ASC)WITH(PAD_INDEX = OFF,KPI_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]",
                //2018年5月4日 活动记录
                "CREATE TABLE [dbo].[ZKShop_ActivityRecord]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ActivityId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[Type] [int] NOT NULL,[UserId] [bigint] NOT NULL,[OrderId] [bigint] NOT NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL)",
                "ALTER TABLE [dbo].[ZKShop_ActivityRecord] ADD  CONSTRAINT [PK_ZKShop_ActivityRecord] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF,KPI_NORECOMPUTE = OFF,SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF,ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]",

                // 2018年5月4日 商品活动
                "ALTER TABLE ZKShop_Product ADD Activity  text",
                "update ZKShop_Product set Activity='' where Activity is null",
                // 2018年5月2日
                " ALTER TABLE ZKShop_Order Add PayId  [bigint] ",
                "update ZKShop_Order set PayId = 0 where PayId is null",

                // 发货记录表 个和主键2018年5月2日

                "CREATE TABLE [dbo].[ZKShop_OrderDelivery]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[TotalCount] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[ExpressGuid] [uniqueidentifier] NOT NULL,[ExpressNumber] [nvarchar](100) NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL)",
                "ALTER TABLE [dbo].[ZKShop_OrderDelivery] ADD  CONSTRAINT [PK_ZKShop_OrderDelivery] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, KPI_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]",

                //2018年4月23日 修改ShareOrder
                " ALTER TABLE Task_ShareOrder Add SystemStatus int",
                "update Task_ShareOrder set SystemStatus = 0 where SystemStatus is null",

                // 2018年4月22日 修改UserMap字段
                "exec sp_rename 'User_UserMap.Extension','GradeInfo','column'", // 修改列名
                "ALTER TABLE User_UserMap ADD ShopSaleInfo  text",
                "update User_UserMap set ShopSaleInfo='' where ShopSaleInfo is null"
            };
        }

        #endregion 过期的Sql脚本

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
        /// 清空mongodb表
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

        #region others

        /// <summary>
        /// 执行脚本
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
    }
}