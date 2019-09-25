﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;

namespace Alabo.App.Core.Admin.Domain.Repositories {

    /// <summary>
    /// TenantCreateRepository
    /// </summary>
    public class TenantCreateRepository : RepositoryEfCore<Users.Entities.User, long>, ITenantCreateRepository {

        /// <summary>
        /// TenantCreateRepository
        /// </summary>
        /// <param name="unitOfWork"></param>
        public TenantCreateRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        private List<string> TableCreateSqlList() {
            var sqlList = new List<string>
            {
                //Common_AutoConfig
                "CREATE TABLE [dbo].[Common_AutoConfig]([Id] [bigint] IDENTITY(1,1) NOT NULL,[AppName] [nvarchar](max) NULL,[Type] [nvarchar](255) NULL,[Value] [ntext] NULL,[LastUpdated] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_MessageQueue
                "CREATE TABLE [dbo].[Common_MessageQueue]([Id] [bigint] IDENTITY(1,1) NOT NULL,[TemplateCode] [bigint] NOT NULL,[Mobile] [nvarchar](30) NOT NULL,[Content] [nvarchar](500) NOT NULL,[Parameters] [nvarchar](2000) NOT NULL,[Status] [tinyint] NOT NULL,[Message] [nvarchar](max) NOT NULL,[Summary] [nvarchar](max) NOT NULL,[IpAdress] [nvarchar](50) NOT NULL,[RequestTime] [datetime2](7) NOT NULL,[SendTime] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_Record
                "CREATE TABLE [dbo].[Common_Record]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[Type] [nvarchar](255) NOT NULL,[Value] [ntext] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_Relation
                "CREATE TABLE [dbo].[Common_Relation]([Id] [bigint] IDENTITY(1,1) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[ExtraDate] [nvarchar](max) NULL,[FatherId] [bigint] NOT NULL,[Icon] [nvarchar](max) NULL,[MetaDescription] [nvarchar](max) NULL,[MetaKeywords] [nvarchar](max) NULL,[MetaTitle] [nvarchar](max) NULL,[ModifiedTime] [datetime2](7) NOT NULL,[Name] [nvarchar](255) NOT NULL,[Remark] [nvarchar](max) NULL,[SortOrder] [bigint] NOT NULL,[Status] [int] NOT NULL,[Type] [nvarchar](max) NOT NULL,[Value] [ntext] NULL,[UserId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_RelationIndex
                "CREATE TABLE [dbo].[Common_RelationIndex]([Id] [bigint] IDENTITY(1,1) NOT NULL,[EntityId] [bigint] NOT NULL,[RelationId] [bigint] NOT NULL,[Type] [nvarchar](255) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Finance_Account
                "CREATE TABLE [dbo].[Finance_Account]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[FreezeAmount] [decimal](18, 2) NOT NULL,[HistoryAmount] [decimal](18, 2) NOT NULL,[Token] [nvarchar](255) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Finance_Bill
                "CREATE TABLE [dbo].[Finance_Bill]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[OtherUserId] [bigint] NOT NULL,[Type] [int] NULL,[Flow] [int] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[AfterAmount] [decimal](18, 2) NOT NULL,[Intro] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[EntityId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Finance_Pay
                "CREATE TABLE [dbo].[Finance_Pay]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[EntityId] [nvarchar](max) NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[PayType] [int] NULL,[Status] [int] NOT NULL,[Message] [nvarchar](max) NOT NULL,[Extensions] [nvarchar](max) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[ResponseSerial] [nvarchar](max) NOT NULL,[ResponseTime] [datetime2](7) NOT NULL,[AccountPay] [nvarchar](max) NULL,[Type] [int] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Finance_Trade
                "CREATE TABLE [dbo].[Finance_Trade]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Type] [int] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[Status] [int] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[PayTime] [datetime2](7) NOT NULL,[Extension] [text] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Kpi_Kpi
                "CREATE TABLE [dbo].[Kpi_Kpi]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ModuleId] [uniqueidentifier] NOT NULL,[Type] [int] NULL,[UserId] [bigint] NOT NULL,[EntityId] [bigint] NOT NULL,[Value] [decimal](18, 2) NOT NULL,[TotalValue] [decimal](18, 2) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Market_UserRights
                "CREATE TABLE [dbo].[Market_UserRights]([Id] [bigint] IDENTITY(1,1) NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[UserId] [bigint] NOT NULL,[TotalUseCount] [bigint] NOT NULL,[TotalCount] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",

                //Share_Reward
                "CREATE TABLE [dbo].[Share_Reward]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[OrderUserId] [bigint] NOT NULL,[OrderId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[AfterAmount] [decimal](18, 2) NOT NULL,[ModuleId] [uniqueidentifier] NOT NULL,[ModuleConfigId] [bigint] NOT NULL,[Intro] [nvarchar](255) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[Status] [int] NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Task_ShareOrder
                "CREATE TABLE [dbo].[Task_ShareOrder]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[EntityId] [bigint] NULL,[Parameters] [nvarchar](2000) NULL,[Status] [int] NOT NULL,[TriggerType] [int] NOT NULL,[Summary] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[UpdateTime] [datetime2](7) NOT NULL,[Extension] [text] NULL,[ExecuteCount] [bigint] NULL,[SystemStatus] [int] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Task_TaskQueue
                "CREATE TABLE [dbo].[Task_TaskQueue]([Id] [bigint] IDENTITY(1,1) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[ExecutionTime] [datetime2](7) NOT NULL,[ExecutionTimes] [int] NOT NULL,[HandleTime] [datetime2](7) NOT NULL,[MaxExecutionTimes] [int] NOT NULL,[ModuleId] [uniqueidentifier] NOT NULL,[Parameter] [nvarchar](max) NULL,[Type] [int] NOT NULL,[UserId] [bigint] NOT NULL,[Status] [int] NOT NULL,[Message] [text] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //User_TypeUser
                "CREATE TABLE [dbo].[User_TypeUser]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserTypeId] [uniqueidentifier] NOT NULL,[TypeId] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[GradeId] [nvarchar](24) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //User_User
                "CREATE TABLE [dbo].[User_User]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserName] [nvarchar](50) NOT NULL,[Name] [nvarchar](50) NOT NULL,[Mobile] [nvarchar](20) NOT NULL,[Email] [nvarchar](255) NULL,[Status] [int] NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[ParentId] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //User_UserDetail
                "CREATE TABLE [dbo].[User_UserDetail]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[Password] [nvarchar](255) NOT NULL,[PayPassword] [nvarchar](255) NULL,[IsServiceCenter] [bit] NOT NULL,[ServiceCenterUserId] [bigint] NOT NULL,[RegionId] [bigint] NOT NULL,[Sex] [int] NOT NULL,[Birthday] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[RegisterIp] [nvarchar](max) NULL,[LoginNum] [bigint] NOT NULL,[LastLoginIp] [nvarchar](50) NULL,[LastLoginTime] [datetime2](7) NOT NULL,[ModifiedTime] [datetime2](7) NOT NULL,[OpenId] [nvarchar](255) NULL,[Remark] [nvarchar](max) NULL,[Avator] [nvarchar](255) NULL,[NickName] [nvarchar](50) NULL,[AddressId] [nvarchar](50) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //User_UserMap
                "CREATE TABLE [dbo].[User_UserMap]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[LevelNumber] [bigint] NOT NULL,[TeamNumber] [bigint] NOT NULL,[TeamSales] [decimal](18, 2) NOT NULL,[ChildNode] [varchar](max) NULL,[ParentMap] [nvarchar](max) NOT NULL,[ShopSaleInfo] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //User_UserType
                "CREATE TABLE [dbo].[User_UserType]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[UserTypeId] [uniqueidentifier] NOT NULL,[Name] [nvarchar](20) NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[EntityId] [bigint] NOT NULL,[ParentUserId] [bigint] NOT NULL,[Status] [int] NOT NULL,[ParentMap] [nvarchar](max) NULL,[Extensions] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Activity
                "CREATE TABLE [dbo].[ZKShop_Activity]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](255) NOT NULL,[StoreId] [bigint] NOT NULL,[Key] [nvarchar](255) NOT NULL,[Value] [nvarchar](max) NULL,[IsEnable] [bit] NOT NULL,[Status] [int] NOT NULL,[LimitGradeId] [uniqueidentifier] NOT NULL,[MaxStock] [bigint] NOT NULL,[UsedStock] [bigint] NOT NULL,[Extension] [nvarchar](max) NULL,[StartTime] [datetime2](7) NOT NULL,[EndTime] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ActivityRecord
                "CREATE TABLE [dbo].[ZKShop_ActivityRecord]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ActivityId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[OrderId] [bigint] NOT NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[Status] [int] NULL,[ParentId] [bigint] NULL,[ProductId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Category
                "CREATE TABLE [dbo].[ZKShop_Category]([Id] [uniqueidentifier] NOT NULL,[PartentId] [uniqueidentifier] NOT NULL,[Name] [nvarchar](max) NOT NULL,[SortOrder] [bigint] NOT NULL,[Status] [int] NOT NULL,[MetaDescription] [nvarchar](max) NULL,[MetaKeywords] [nvarchar](max) NULL,[MetaTitle] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[ModifiedTime] [datetime2](7) NOT NULL,[ExtraDate] [nvarchar](max) NULL,[Remark] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_CategoryProperty
                "CREATE TABLE [dbo].[ZKShop_CategoryProperty]([Id] [uniqueidentifier] NOT NULL,[CategoryId] [uniqueidentifier] NOT NULL,[Name] [nvarchar](max) NOT NULL,[IsSale] [bit] NOT NULL,[ControlsType] [int] NOT NULL,[SortOrder] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_CategoryPropertyValue
                "CREATE TABLE [dbo].[ZKShop_CategoryPropertyValue]([Id] [uniqueidentifier] NOT NULL,[PropertyId] [uniqueidentifier] NOT NULL,[SortOrder] [bigint] NOT NULL,[ValueName] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY NONCLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Order
                "CREATE TABLE [dbo].[ZKShop_Order]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[AddressId] [nvarchar](255) NULL,[OrderStatus] [int] NOT NULL,[OrderType] [int] NOT NULL,[TotalAmount] [decimal](18, 2) NOT NULL,[PaymentAmount] [decimal](18, 2) NOT NULL,[Extension] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,[TotalCount] [bigint] NULL,[AccountPay] [nvarchar](max) NULL,[PayId] [bigint] NULL,[DeliverUserId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_OrderAction
                "CREATE TABLE [dbo].[ZKShop_OrderAction]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[ActionUserId] [bigint] NOT NULL,[Intro] [nvarchar](max) NOT NULL,[Extensions] [text] NULL,[CreateTime] [datetime] NOT NULL,[OrderActionType] [int] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_OrderDelivery
                "CREATE TABLE [dbo].[ZKShop_OrderDelivery]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[TotalCount] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[ExpressGuid] [uniqueidentifier] NOT NULL,[ExpressNumber] [nvarchar](100) NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_OrderProduct
                "CREATE TABLE [dbo].[ZKShop_OrderProduct]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[ProductId] [bigint] NOT NULL,[SkuId] [bigint] NOT NULL,[Count] [bigint] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[FenRunAmount] [decimal](18, 2) NOT NULL,[TotalAmount] [decimal](18, 2) NOT NULL,[PaymentAmount] [decimal](18, 2) NOT NULL,[Extension] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Product
                "CREATE TABLE [dbo].[ZKShop_Product]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](250) NOT NULL,[Bn] [nvarchar](max) NOT NULL,[StoreId] [bigint] NOT NULL,[PriceStyleId] [uniqueidentifier] NOT NULL,[CategoryId] [uniqueidentifier] NOT NULL,[RegionId] [bigint] NOT NULL,[BrandId] [uniqueidentifier] NOT NULL,[PurchasePrice] [decimal](18, 2) NOT NULL,[CostPrice] [decimal](18, 2) NOT NULL,[MarketPrice] [decimal](18, 2) NOT NULL,[Price] [decimal](18, 2) NOT NULL,[DisplayPrice] [nvarchar](50) NULL,[FenRunPrice] [decimal](18, 2) NULL,[Weight] [decimal](18, 2) NOT NULL,[Stock] [bigint] NOT NULL,[SmallUrl] [nvarchar](max) NULL,[ThumbnailUrl] [nvarchar](max) NULL,[ProductStatus] [int] NOT NULL,[SortOrder] [bigint] NOT NULL,[SoldCount] [bigint] NULL,[ViewCount] [bigint] NULL,[LikeCount] [bigint] NULL,[FavoriteCount] [bigint] NULL,[ModifiedTime] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[MinCashRate] [decimal](18, 2) NULL,[Activity] [text] NULL,[IsFreeShipping] [bit] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ProductDetail
                "CREATE TABLE [dbo].[ZKShop_ProductDetail]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ProductId] [bigint] NOT NULL,[StockAlarm] [bigint] NOT NULL,[Intro] [nvarchar](max) NULL,[MobileIntro] [nvarchar](max) NULL,[PriceUnit] [nvarchar](50) NULL,[MetaTitle] [nvarchar](200) NULL,[MetaKeywords] [nvarchar](300) NULL,[MetaDescription] [nvarchar](400) NULL,[ImageJson] [nvarchar](max) NULL,[PropertyJson] [nvarchar](max) NULL,[NeedBuyerAddress] [bit] NULL,[Extension] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ProductLine
                "CREATE TABLE [dbo].[ZKShop_ProductLine]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](250) NOT NULL,[Intro] [nvarchar](max) NOT NULL,[ProductIds] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[ModifiedTime] [datetime2](7) NOT NULL,[SortOrder] [bigint] NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ProductSku
                "CREATE TABLE [dbo].[ZKShop_ProductSku]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ProductId] [bigint] NOT NULL,[Bn] [nvarchar](max) NULL,[ProductStatus] [int] NOT NULL,[BarCode] [nvarchar](max) NULL,[PurchasePrice] [decimal](18, 2) NOT NULL,[CostPrice] [decimal](18, 2) NOT NULL,[MarketPrice] [decimal](18, 2) NOT NULL,[Price] [decimal](18, 2) NOT NULL,[FenRunPrice] [decimal](18, 2) NULL,[Weight] [decimal](18, 2) NOT NULL,[Size] [decimal](18, 2) NOT NULL,[Stock] [bigint] NOT NULL,[StorePlace] [nvarchar](max) NULL,[PropertyJson] [nvarchar](max) NULL,[PropertyValueDesc] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[Modified] [datetime2](7) NOT NULL,[SpecSn] [nvarchar](max) NULL,[DisplayPrice] [nvarchar](50) NULL,[MinPayCash] [decimal](18, 2) NULL,[MaxPayPrice] [decimal](18, 2) NULL,[GradePrice] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Store
                "CREATE TABLE [dbo].[ZKShop_Store]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserID] [bigint] NOT NULL,[Name] [nvarchar](50) NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[ParentUserId] [bigint] NOT NULL,[IsPlanform] [bit] NOT NULL,[Status] [int] NOT NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //alter
                "ALTER TABLE [dbo].[Common_AutoConfig] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT ((0)) FOR [TemplateCode]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT ((1)) FOR [Status]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT (getdate()) FOR [RequestTime]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT (getdate()) FOR [SendTime]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Common_RelationIndex] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Finance_Account] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Finance_Bill] ADD  DEFAULT ((0)) FOR [EntityId]",
                "ALTER TABLE [dbo].[Task_ShareOrder] ADD  DEFAULT ((0)) FOR [EntityId]",
                "ALTER TABLE [dbo].[User_TypeUser] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[User_User] ADD  DEFAULT ((0)) FOR [ParentId]",
                "ALTER TABLE [dbo].[User_User] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[User_UserDetail] ADD  DEFAULT ('') FOR [Avator]",
                "ALTER TABLE [dbo].[User_UserMap] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[User_UserType] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_Activity] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_CateryProperty] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_CateryPropertyValue] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_Order] ADD  DEFAULT ((0)) FOR [TotalCount]",
                "ALTER TABLE [dbo].[ZKShop_OrderProduct] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_Product] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [BrandId]",
                "ALTER TABLE [dbo].[ZKShop_Product] ADD  DEFAULT ((0)) FOR [FenRunPrice]",
                "ALTER TABLE [dbo].[ZKShop_Product] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_ProductDetail] ADD  DEFAULT ((1)) FOR [NeedBuyerAddress]",
                "ALTER TABLE [dbo].[ZKShop_ProductDetail] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_ProductSku] ADD  DEFAULT ((0)) FOR [FenRunPrice]",
                "ALTER TABLE [dbo].[ZKShop_Product]  WITH CHECK ADD CHECK  (([MinCashRate]>=(0) AND [MinCashRate]<=(1)))",
                "ALTER TABLE [dbo].[ZKShop_Product]  WITH CHECK ADD CHECK(([MinCashRate]>= (0) AND[MinCashRate] <= (1)))"
            };
            var sql =
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
            return sqlList;
        }

        /// <summary>
        /// init tenant sql
        /// </summary>
        /// <returns></returns>
        public void InitTenantSql() {
            var sqlList = new List<string>
            {
                //Common_AutoConfig
                "CREATE TABLE [dbo].[Common_AutoConfig]([Id] [bigint] IDENTITY(1,1) NOT NULL,[AppName] [nvarchar](max) NULL,[Type] [nvarchar](255) NULL,[Value] [ntext] NULL,[LastUpdated] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_MessageQueue
                "CREATE TABLE [dbo].[Common_MessageQueue]([Id] [bigint] IDENTITY(1,1) NOT NULL,[TemplateCode] [bigint] NOT NULL,[Mobile] [nvarchar](30) NOT NULL,[Content] [nvarchar](500) NOT NULL,[Parameters] [nvarchar](2000) NOT NULL,[Status] [tinyint] NOT NULL,[Message] [nvarchar](max) NOT NULL,[Summary] [nvarchar](max) NOT NULL,[IpAdress] [nvarchar](50) NOT NULL,[RequestTime] [datetime2](7) NOT NULL,[SendTime] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_Record
                "CREATE TABLE [dbo].[Common_Record]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[Type] [nvarchar](255) NOT NULL,[Value] [ntext] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_Relation
                "CREATE TABLE [dbo].[Common_Relation]([Id] [bigint] IDENTITY(1,1) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[ExtraDate] [nvarchar](max) NULL,[FatherId] [bigint] NOT NULL,[Icon] [nvarchar](max) NULL,[MetaDescription] [nvarchar](max) NULL,[MetaKeywords] [nvarchar](max) NULL,[MetaTitle] [nvarchar](max) NULL,[ModifiedTime] [datetime2](7) NOT NULL,[Name] [nvarchar](255) NOT NULL,[Remark] [nvarchar](max) NULL,[SortOrder] [bigint] NOT NULL,[Status] [int] NOT NULL,[Type] [nvarchar](max) NOT NULL,[Value] [ntext] NULL,[UserId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Common_RelationIndex
                "CREATE TABLE [dbo].[Common_RelationIndex]([Id] [bigint] IDENTITY(1,1) NOT NULL,[EntityId] [bigint] NOT NULL,[RelationId] [bigint] NOT NULL,[Type] [nvarchar](255) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Finance_Account
                "CREATE TABLE [dbo].[Finance_Account]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[FreezeAmount] [decimal](18, 2) NOT NULL,[HistoryAmount] [decimal](18, 2) NOT NULL,[Token] [nvarchar](255) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Finance_Bill
                "CREATE TABLE [dbo].[Finance_Bill]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[OtherUserId] [bigint] NOT NULL,[Type] [int] NULL,[Flow] [int] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[AfterAmount] [decimal](18, 2) NOT NULL,[Intro] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[EntityId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Finance_Pay
                "CREATE TABLE [dbo].[Finance_Pay]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[EntityId] [nvarchar](max) NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[PayType] [int] NULL,[Status] [int] NOT NULL,[Message] [nvarchar](max) NOT NULL,[Extensions] [nvarchar](max) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[ResponseSerial] [nvarchar](max) NOT NULL,[ResponseTime] [datetime2](7) NOT NULL,[AccountPay] [nvarchar](max) NULL,[Type] [int] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Finance_Trade
                "CREATE TABLE [dbo].[Finance_Trade]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Type] [int] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[Status] [int] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[PayTime] [datetime2](7) NOT NULL,[Extension] [text] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Kpi_Kpi
                "CREATE TABLE [dbo].[Kpi_Kpi]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ModuleId] [uniqueidentifier] NOT NULL,[Type] [int] NULL,[UserId] [bigint] NOT NULL,[EntityId] [bigint] NOT NULL,[Value] [decimal](18, 2) NOT NULL,[TotalValue] [decimal](18, 2) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Market_UserRights
                "CREATE TABLE [dbo].[Market_UserRights]([Id] [bigint] IDENTITY(1,1) NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[UserId] [bigint] NOT NULL,[TotalUseCount] [bigint] NOT NULL,[TotalCount] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",

                //Share_Reward
                "CREATE TABLE [dbo].[Share_Reward]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[OrderUserId] [bigint] NOT NULL,[OrderId] [bigint] NOT NULL,[MoneyTypeId] [uniqueidentifier] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[AfterAmount] [decimal](18, 2) NOT NULL,[ModuleId] [uniqueidentifier] NOT NULL,[ModuleConfigId] [bigint] NOT NULL,[Intro] [nvarchar](255) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[Status] [int] NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //Task_ShareOrder
                "CREATE TABLE [dbo].[Task_ShareOrder]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[EntityId] [bigint] NULL,[Parameters] [nvarchar](2000) NULL,[Status] [int] NOT NULL,[TriggerType] [int] NOT NULL,[Summary] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[UpdateTime] [datetime2](7) NOT NULL,[Extension] [text] NULL,[ExecuteCount] [bigint] NULL,[SystemStatus] [int] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //Task_TaskQueue
                "CREATE TABLE [dbo].[Task_TaskQueue]([Id] [bigint] IDENTITY(1,1) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[ExecutionTime] [datetime2](7) NOT NULL,[ExecutionTimes] [int] NOT NULL,[HandleTime] [datetime2](7) NOT NULL,[MaxExecutionTimes] [int] NOT NULL,[ModuleId] [uniqueidentifier] NOT NULL,[Parameter] [nvarchar](max) NULL,[Type] [int] NOT NULL,[UserId] [bigint] NOT NULL,[Status] [int] NOT NULL,[Message] [text] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //User_TypeUser
                "CREATE TABLE [dbo].[User_TypeUser]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserTypeId] [uniqueidentifier] NOT NULL,[TypeId] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[GradeId] [nvarchar](24) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //User_User
                "CREATE TABLE [dbo].[User_User]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserName] [nvarchar](50) NOT NULL,[Name] [nvarchar](50) NOT NULL,[Mobile] [nvarchar](20) NOT NULL,[Email] [nvarchar](255) NULL,[Status] [int] NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[ParentId] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //User_UserDetail
                "CREATE TABLE [dbo].[User_UserDetail]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[Password] [nvarchar](255) NOT NULL,[PayPassword] [nvarchar](255) NULL,[IsServiceCenter] [bit] NOT NULL,[ServiceCenterUserId] [bigint] NOT NULL,[RegionId] [bigint] NOT NULL,[Sex] [int] NOT NULL,[Birthday] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[RegisterIp] [nvarchar](max) NULL,[LoginNum] [bigint] NOT NULL,[LastLoginIp] [nvarchar](50) NULL,[LastLoginTime] [datetime2](7) NOT NULL,[ModifiedTime] [datetime2](7) NOT NULL,[OpenId] [nvarchar](255) NULL,[Remark] [nvarchar](max) NULL,[Avator] [nvarchar](255) NULL,[NickName] [nvarchar](50) NULL,[AddressId] [nvarchar](50) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //User_UserMap
                "CREATE TABLE [dbo].[User_UserMap]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[LevelNumber] [bigint] NOT NULL,[TeamNumber] [bigint] NOT NULL,[TeamSales] [decimal](18, 2) NOT NULL,[ChildNode] [varchar](max) NULL,[ParentMap] [nvarchar](max) NOT NULL,[ShopSaleInfo] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //User_UserType
                "CREATE TABLE [dbo].[User_UserType]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[UserTypeId] [uniqueidentifier] NOT NULL,[Name] [nvarchar](20) NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[EntityId] [bigint] NOT NULL,[ParentUserId] [bigint] NOT NULL,[Status] [int] NOT NULL,[ParentMap] [nvarchar](max) NULL,[Extensions] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Activity
                "CREATE TABLE [dbo].[ZKShop_Activity]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](255) NOT NULL,[StoreId] [bigint] NOT NULL,[Key] [nvarchar](255) NOT NULL,[Value] [nvarchar](max) NULL,[IsEnable] [bit] NOT NULL,[Status] [int] NOT NULL,[LimitGradeId] [uniqueidentifier] NOT NULL,[MaxStock] [bigint] NOT NULL,[UsedStock] [bigint] NOT NULL,[Extension] [nvarchar](max) NULL,[StartTime] [datetime2](7) NOT NULL,[EndTime] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ActivityRecord
                "CREATE TABLE [dbo].[ZKShop_ActivityRecord]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ActivityId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[OrderId] [bigint] NOT NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[Status] [int] NULL,[ParentId] [bigint] NULL,[ProductId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Category
                "CREATE TABLE [dbo].[ZKShop_Category]([Id] [uniqueidentifier] NOT NULL,[PartentId] [uniqueidentifier] NOT NULL,[Name] [nvarchar](max) NOT NULL,[SortOrder] [bigint] NOT NULL,[Status] [int] NOT NULL,[MetaDescription] [nvarchar](max) NULL,[MetaKeywords] [nvarchar](max) NULL,[MetaTitle] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[ModifiedTime] [datetime2](7) NOT NULL,[ExtraDate] [nvarchar](max) NULL,[Remark] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_CategoryProperty
                "CREATE TABLE [dbo].[ZKShop_CategoryProperty]([Id] [uniqueidentifier] NOT NULL,[CategoryId] [uniqueidentifier] NOT NULL,[Name] [nvarchar](max) NOT NULL,[IsSale] [bit] NOT NULL,[ControlsType] [int] NOT NULL,[SortOrder] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_CategoryPropertyValue
                "CREATE TABLE [dbo].[ZKShop_CategoryPropertyValue]([Id] [uniqueidentifier] NOT NULL,[PropertyId] [uniqueidentifier] NOT NULL,[SortOrder] [bigint] NOT NULL,[ValueName] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY NONCLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Order
                "CREATE TABLE [dbo].[ZKShop_Order]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[AddressId] [nvarchar](255) NULL,[OrderStatus] [int] NOT NULL,[OrderType] [int] NOT NULL,[TotalAmount] [decimal](18, 2) NOT NULL,[PaymentAmount] [decimal](18, 2) NOT NULL,[Extension] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,[TotalCount] [bigint] NULL,[AccountPay] [nvarchar](max) NULL,[PayId] [bigint] NULL,[DeliverUserId] [bigint] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_OrderAction
                "CREATE TABLE [dbo].[ZKShop_OrderAction]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[ActionUserId] [bigint] NOT NULL,[Intro] [nvarchar](max) NOT NULL,[Extensions] [text] NULL,[CreateTime] [datetime] NOT NULL,[OrderActionType] [int] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_OrderDelivery
                "CREATE TABLE [dbo].[ZKShop_OrderDelivery]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[StoreId] [bigint] NOT NULL,[TotalCount] [bigint] NOT NULL,[UserId] [bigint] NOT NULL,[ExpressGuid] [uniqueidentifier] NOT NULL,[ExpressNumber] [nvarchar](100) NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_OrderProduct
                "CREATE TABLE [dbo].[ZKShop_OrderProduct]([Id] [bigint] IDENTITY(1,1) NOT NULL,[OrderId] [bigint] NOT NULL,[ProductId] [bigint] NOT NULL,[SkuId] [bigint] NOT NULL,[Count] [bigint] NOT NULL,[Amount] [decimal](18, 2) NOT NULL,[FenRunAmount] [decimal](18, 2) NOT NULL,[TotalAmount] [decimal](18, 2) NOT NULL,[PaymentAmount] [decimal](18, 2) NOT NULL,[Extension] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_PresaleProduct
                "CREATE TABLE [dbo].[ZKShop_PresaleProduct]([Id] [bigint] IDENTITY(1,1) NOT NULL,[StoreId] [bigint] NOT NULL,[ProductId] [bigint] NOT NULL,[SkuId] [bigint] NULL,[PriceStyleId] [uniqueidentifier] NOT NULL,[Price] [decimal](18, 2) NOT NULL,[VirtualPrice] [decimal](18, 2) NOT NULL,[Stock] [bigint] NOT NULL,[QuantitySold] [bigint] NOT NULL,[Status] [int] NOT NULL,[Sort] [bigint] NOT NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]",
                //ZKShop_Product
                "CREATE TABLE [dbo].[ZKShop_Product]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](250) NOT NULL,[Bn] [nvarchar](max) NOT NULL,[StoreId] [bigint] NOT NULL,[PriceStyleId] [uniqueidentifier] NOT NULL,[CategoryId] [uniqueidentifier] NOT NULL,[RegionId] [bigint] NOT NULL,[BrandId] [uniqueidentifier] NOT NULL,[PurchasePrice] [decimal](18, 2) NOT NULL,[CostPrice] [decimal](18, 2) NOT NULL,[MarketPrice] [decimal](18, 2) NOT NULL,[Price] [decimal](18, 2) NOT NULL,[DisplayPrice] [nvarchar](50) NULL,[FenRunPrice] [decimal](18, 2) NULL,[Weight] [decimal](18, 2) NOT NULL,[Stock] [bigint] NOT NULL,[SmallUrl] [nvarchar](max) NULL,[ThumbnailUrl] [nvarchar](max) NULL,[ProductStatus] [int] NOT NULL,[SortOrder] [bigint] NOT NULL,[SoldCount] [bigint] NULL,[ViewCount] [bigint] NULL,[LikeCount] [bigint] NULL,[FavoriteCount] [bigint] NULL,[ModifiedTime] [datetime2](7) NOT NULL,[CreateTime] [datetime2](7) NOT NULL,[MinCashRate] [decimal](18, 2) NULL,[Activity] [text] NULL,[IsFreeShipping] [bit] NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ProductDetail
                "CREATE TABLE [dbo].[ZKShop_ProductDetail]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ProductId] [bigint] NOT NULL,[StockAlarm] [bigint] NOT NULL,[Intro] [nvarchar](max) NULL,[MobileIntro] [nvarchar](max) NULL,[PriceUnit] [nvarchar](50) NULL,[MetaTitle] [nvarchar](200) NULL,[MetaKeywords] [nvarchar](300) NULL,[MetaDescription] [nvarchar](400) NULL,[ImageJson] [nvarchar](max) NULL,[PropertyJson] [nvarchar](max) NULL,[NeedBuyerAddress] [bit] NULL,[Extension] [text] NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ProductLine
                "CREATE TABLE [dbo].[ZKShop_ProductLine]([Id] [bigint] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](250) NOT NULL,[Intro] [nvarchar](max) NOT NULL,[ProductIds] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[ModifiedTime] [datetime2](7) NOT NULL,[SortOrder] [bigint] NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_ProductSku
                "CREATE TABLE [dbo].[ZKShop_ProductSku]([Id] [bigint] IDENTITY(1,1) NOT NULL,[ProductId] [bigint] NOT NULL,[Bn] [nvarchar](max) NULL,[ProductStatus] [int] NOT NULL,[BarCode] [nvarchar](max) NULL,[PurchasePrice] [decimal](18, 2) NOT NULL,[CostPrice] [decimal](18, 2) NOT NULL,[MarketPrice] [decimal](18, 2) NOT NULL,[Price] [decimal](18, 2) NOT NULL,[FenRunPrice] [decimal](18, 2) NULL,[Weight] [decimal](18, 2) NOT NULL,[Size] [decimal](18, 2) NOT NULL,[Stock] [bigint] NOT NULL,[StorePlace] [nvarchar](max) NULL,[PropertyJson] [nvarchar](max) NULL,[PropertyValueDesc] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,[Modified] [datetime2](7) NOT NULL,[SpecSn] [nvarchar](max) NULL,[DisplayPrice] [nvarchar](50) NULL,[MinPayCash] [decimal](18, 2) NULL,[MaxPayPrice] [decimal](18, 2) NULL,[GradePrice] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //ZKShop_Store
                "CREATE TABLE [dbo].[ZKShop_Store]([Id] [bigint] IDENTITY(1,1) NOT NULL,[UserID] [bigint] NOT NULL,[Name] [nvarchar](50) NOT NULL,[GradeId] [uniqueidentifier] NOT NULL,[ParentUserId] [bigint] NOT NULL,[IsPlanform] [bit] NOT NULL,[Status] [int] NOT NULL,[Extension] [nvarchar](max) NULL,[CreateTime] [datetime2](7) NOT NULL,PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                //alter
                "ALTER TABLE [dbo].[Common_AutoConfig] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT ((0)) FOR [TemplateCode]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT ((1)) FOR [Status]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT (getdate()) FOR [RequestTime]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT (getdate()) FOR [SendTime]",
                "ALTER TABLE [dbo].[Common_MessageQueue] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Common_RelationIndex] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Finance_Account] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[Finance_Bill] ADD  DEFAULT ((0)) FOR [EntityId]",
                "ALTER TABLE [dbo].[Task_ShareOrder] ADD  DEFAULT ((0)) FOR [EntityId]",
                "ALTER TABLE [dbo].[User_TypeUser] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[User_User] ADD  DEFAULT ((0)) FOR [ParentId]",
                "ALTER TABLE [dbo].[User_User] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[User_UserDetail] ADD  DEFAULT ('') FOR [Avator]",
                "ALTER TABLE [dbo].[User_UserMap] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[User_UserType] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_Activity] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_CateryProperty] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_CateryPropertyValue] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_Order] ADD  DEFAULT ((0)) FOR [TotalCount]",
                "ALTER TABLE [dbo].[ZKShop_OrderProduct] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_Product] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [BrandId]",
                "ALTER TABLE [dbo].[ZKShop_Product] ADD  DEFAULT ((0)) FOR [FenRunPrice]",
                "ALTER TABLE [dbo].[ZKShop_Product] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_ProductDetail] ADD  DEFAULT ((1)) FOR [NeedBuyerAddress]",
                "ALTER TABLE [dbo].[ZKShop_ProductDetail] ADD  DEFAULT (getdate()) FOR [CreateTime]",
                "ALTER TABLE [dbo].[ZKShop_ProductSku] ADD  DEFAULT ((0)) FOR [FenRunPrice]",
                "ALTER TABLE [dbo].[ZKShop_Product]  WITH CHECK ADD CHECK  (([MinCashRate]>=(0) AND [MinCashRate]<=(1)))",
                "ALTER TABLE [dbo].[ZKShop_Product]  WITH CHECK ADD CHECK(([MinCashRate]>= (0) AND[MinCashRate] <= (1)))"
            };

            ExecuteSql(sqlList);
        }

        /// <summary>
        /// create database
        /// </summary>
        /// <param name="databaseName"></param>
        public void CreateDatabase(string databaseName) {
            var sqlList = new List<string>
            {
                $"CREATE DATABASE {databaseName};",
                $"USE {databaseName};",
            };
            sqlList.AddRange(TableCreateSqlList());

            ExecuteSql(sqlList);
        }

        /// <summary>
        /// is exists database
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public bool IsExistsDatabase(string databaseName) {
            var sql = $"select COUNT(1) From master.dbo.sysdatabases where name='{databaseName.ToLower()}'";
            var obj = RepositoryContext.ExecuteScalar(sql);
            return obj.ToInt16() > 0;
        }

        /// <summary>
        /// execute sql
        /// </summary>
        /// <param name="sqlList"></param>
        public void ExecuteSql(List<string> sqlList) {
            foreach (var item in sqlList) {
                try {
                    RepositoryContext.ExecuteNonQuery(item);
                } catch (Exception ex) {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteDatabase(string databaseName) {
            var sqlList = new List<string>
            {
                $"Drop DATABASE {databaseName};"
            };
            sqlList.AddRange(TableCreateSqlList());

            ExecuteSql(sqlList);
        }
    }
}