using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Enums {

    /// <summary>
    ///     左边菜单导航
    ///     根据枚举来构建左侧菜单
    /// </summary>
    [ClassProperty(Name = "左侧菜单导航")]
    public enum SideBarType {

        /// <summary>
        ///     全屏
        /// </summary>
        FullScreen = 100,

        /// <summary>
        ///     会员
        ///     会员左边菜单导航
        /// </summary>
        UserSideBar = 201,

        /// <summary>
        ///     控制面板顶部菜单
        /// </summary>
        ControlSideBar = 509,

        /// <summary>
        ///     绩效
        /// </summary>
        KpiConfigSideBar = 314,

        /// <summary>
        ///     绩效
        /// </summary>
        KpiSideBar = 214,

        /// <summary>
        ///     用户自动升级
        /// </summary>
        GradeInfoSideBar = 315,

        #region 用户类型菜单

        /// <summary>
        ///     股东
        ShareHoldersSideBar = 203,

        /// <summary>
        ///     员工
        /// </summary>
        EmployeesSideBar = 506,

        /// <summary>
        ///     OEM
        /// </summary>
        OemSideBar = 304,

        /// <summary>
        ///     合伙伙伴
        /// </summary>
        StrategicPartnersSideBar = 305,

        /// <summary>
        ///     门店
        /// </summary>
        /// 市代理
        ServiceCenterSideBar = 217,

        /// <summary>
        ///     省代理
        /// </summary>
        ProvinceSideBar = 204,

        /// <summary>
        ///     城市合伙人
        /// </summary>
        CitySideBar = 205,

        /// <summary>
        ///     区代理
        /// </summary>
        CountySideBar = 206,

        /// <summary>
        ///     大区
        /// </summary>
        RegionalSideBar = 207,

        /// <summary>
        ///     圈主（商圈）
        /// </summary>
        CircleSideBar = 404,

        /// <summary>
        ///     商家(门店),表示线下实体店
        ///     类型Merchants，一定要有线下实体店，在实现会员锁定分润等等有特殊的用途
        /// </summary>
        MerchantsSideBar = 799,

        /// <summary>
        ///     供应商
        /// </summary>
        SupplierSideBar = 210,

        /// <summary>
        ///     平台
        /// </summary>
        PlatformSideBar = 102,

        /// <summary>
        ///     微代理
        /// </summary>
        AgentSideBar = 105,

        /// <summary>
        ///     商学院
        /// </summary>
        SchoolSideBar = 107,

        /// <summary>
        ///     合资公司
        /// </summary>
        VentureCompanySideBar = 108,

        /// <summary>
        ///     分公司
        /// </summary>
        BranchCompnaySideBar = 109,

        /// <summary>
        ///     创客
        /// </summary>
        ChuangKeSideBar = 110,

        /// <summary>
        ///     服务员
        /// </summary>
        WaiterSideBar = 111,

        /// <summary>
        ///     内部合伙人（合作伙伴）
        /// </summary>
        PartnerSideBar = 202,

        #endregion 用户类型菜单

        /// <summary>
        ///     统计报表
        /// </summary>
        ReportSideBar = 113,

        /// <summary>
        ///     Diy左侧菜单
        /// </summary>
        DiySideBar = 505,

        /// <summary>
        ///     分润顶部菜单
        /// </summary>
        FenRunSideBar = 219,

        /// <summary>
        ///     商城顶部菜单
        /// </summary>
        ShopSideBar = 209,

        #region CMS 左侧菜单

        /// <summary>
        ///     CMS顶部
        /// </summary>
        CmsSideBar = 220,

        /// <summary>
        ///     文章 CMS左侧菜单
        /// </summary>
        ArticleSideBarSideBar = 80,

        /// <summary>
        ///     评论 CMS左侧菜单
        /// </summary>
        CommentSideBar = 81,

        /// <summary>
        ///     帮助 CMS左侧菜单
        /// </summary>
        HelpSideBar = 82,

        /// <summary>
        ///     图片 CMS左侧菜单
        /// </summary>
        ImagesSideBar = 84,

        /// <summary>
        ///     视频 CMS左侧菜单
        /// </summary>
        VideoSideBar = 85,

        /// <summary>
        ///     下载 CMS左侧菜单
        /// </summary>
        DownSideBar = 86,

        /// <summary>
        ///     问答 CMS左侧菜单
        /// </summary>
        QuestionSideBar = 87,

        /// <summary>
        ///     招聘 CMS左侧菜单
        /// </summary>
        JobSideBar = 88,

        /// <summary>
        ///     头条 CMS左侧菜单
        /// </summary>
        NewsSideBar = 89,

        /// <summary>
        ///     消息 CMS左侧菜单
        /// </summary>
        MessageSideBar = 90,

        /// <summary>
        ///     消息 CMS左侧菜单
        /// </summary>
        NoteBookSideBar = 91,

        /// <summary>
        ///     单页面 CMS左侧菜单
        /// </summary>
        SinglePageSideBar = 92,

        /// <summary>
        ///     工单 CMS左侧菜单
        /// </summary>
        WorkOrderSideBar = 93,

        /// <summary>
        ///     会员通知 CMS左侧菜单
        /// </summary>
        UserNoticeSideBar = 94,

        #endregion CMS 左侧菜单

        /// <summary>
        ///     客户自定义菜单
        /// </summary>
        CustomerSideBar = 2000,

        /// <summary>
        ///     第三方支付左侧菜单
        /// </summary>
        ApiStoreSideBar = 402,

        /// <summary>
        ///     商品配置左侧菜单
        ///     商品管理左侧标签
        /// </summary>
        ProductSideBar = 213,

        /// <summary>
        ///     订单左侧菜单
        /// </summary>
        OrderSideBar = 211,

        /// <summary>
        ///     客服中心顶部菜单
        /// </summary>
        CustomerServiceSideBar = 216,

        /// <summary>
        ///     奖金池
        /// </summary>
        BonusPoolBar = 186,

        /// <summary>
        ///     创世纪专用
        /// </summary>
        CustomerInsurance = 230,

        /// <summary>
        ///     等级考核
        /// </summary>
        GradeKpiSideBar = 850,

        /// <summary>
        ///     表格
        /// </summary>
        TableSideBar = 20100,

        /// <summary>
        ///     第三方API接口
        /// </summary>
        ApiSideBar = 507,

        /// <summary>
        ///     Erp
        /// </summary>
        ErpSideBar = 212,

        /// <summary>
        ///     报表顶部菜单
        /// </summary>
        ReportRormSideBar = 508,

        /// <summary>
        ///     活动
        /// </summary>
        ActivitysSideBar = 504,

        /// <summary>
        ///     应用顶部菜单
        /// </summary>
        ApplicationSideBar = 502,

        /// <summary>
        ///     模式超市顶部菜单
        /// </summary>
        ModelSupermarketSideBar = 503,

        /// -----财务----
        /// <summary>
        ///     财务顶部
        /// </summary>
        FinancialSideBar = 208,

        #region 财务侧边栏

        /// <summary>
        ///     区块链钱包左边菜单
        /// </summary>
        FinanceBlockChainSideBar = 121,

        /// <summary>
        ///     用户资产左边菜单
        /// </summary>
        UserAssetsSidebar = 122,

        /// <summary>
        ///     财务明细左边菜单
        /// </summary>
        FinanceSideBar = 123,

        /// <summary>
        ///     提现左边菜单
        /// </summary>
        WithDrawSideBar = 124,

        /// <summary>
        ///     充值左边菜单
        /// </summary>
        RechargeSideBar = 125,

        /// <summary>
        ///     转账左边菜单
        /// </summary>
        TransferSideBar = 126,

        /// <summary>
        ///     银行卡左边菜单
        /// </summary>
        BankCardSideBar = 127,

        /// <summary>
        ///     资产管理
        /// </summary>
        AssetsSideBar = 128,

        /// <summary>
        ///     收银台
        /// </summary>
        FinancePaySideBar = 129,

        #endregion 财务侧边栏

        #region 客服中心

        /// <summary>
        ///     短信平台左边栏
        /// </summary>
        ShortMessageSideBar = 55,

        /// <summary>
        ///     工单系统左边栏
        /// </summary>
        WorkListSideBar = 56,

        /// <summary>
        ///     国家区域左边栏
        /// </summary>
        NationalRegionSideBar = 58,

        /// <summary>
        ///     API文档左边栏
        /// </summary>
        ApiFileSideBar = 59,

        /// <summary>
        ///     平台功能左边栏
        /// </summary>
        PlatformFunctionSideBar = 60,

        /// <summary>
        ///     向导
        /// </summary>
        GuideSideBar = 61,

        /// <summary>
        ///     日志侧边栏
        /// </summary>
        LogSideBar = 19,

        /// <summary>
        ///     授权查询
        /// </summary>
        AuthorizationSideBar = 62,

        /// <summary>
        ///     数据初始
        /// </summary>
        DataInitial = 63,

        /// <summary>
        ///     智能数据同步初始
        /// </summary>
        DataSynchronization = 65,

        /// <summary>
        ///     数据结构左边栏
        /// </summary>
        DataStructureSideBar = 64,

        #endregion 客服中心

        #region 第三方接口

        /// <summary>
        ///     微信小程序
        /// </summary>
        SmallProgramSideBar = 720,

        /// <summary>
        ///     账户资产支付
        /// </summary>
        AccountAssetsSideBar = 721,

        /// <summary>
        ///     短信管理
        /// </summary>
        MesssageManagementSideBar = 722,

        /// <summary>
        ///     公众号微信支付
        /// </summary>
        PublicNumberSideBar = 723,

        /// <summary>
        ///     微信支付
        /// </summary>
        WeChatSideBar = 724,

        /// <summary>
        ///     支付宝支付
        /// </summary>
        AlipaySideBar = 725,

        /// <summary>
        ///     PayPal支付
        /// </summary>
        PayPalSideBar = 726,

        /// <summary>
        ///     易宝支付
        /// </summary>
        YeePaySideBar = 727,

        /// <summary>
        ///     QQ钱包支付
        /// </summary>
        PurseSideBar = 278,

        /// <summary>
        ///     京东支付
        /// </summary>
        JDSideBar = 729,

        /// <summary>
        ///     网银支付
        /// </summary>
        NetSilverSideBar = 730,

        /// <summary>
        ///     物流追踪
        /// </summary>
        LogisticsTrackingSideBar = 731,

        #endregion 第三方接口

        #region 第三方接口

        /// <summary>
        ///     会员管理
        /// </summary>
        MemberManagementSideBar = 190,

        /// <summary>
        ///     会员添加
        /// </summary>
        MemberAddSideBar = 191,

        /// <summary>
        ///     会员等级
        /// </summary>
        MemberGradeSideBar = 192,

        /// <summary>
        ///     组织架构图
        /// </summary>
        OrganizationalChartSideBar = 193,

        /// <summary>
        ///     实名认证
        /// </summary>
        IdentitySideBar = 195,

        /// <summary>
        ///     用户地址
        /// </summary>
        UserAddressSideBar = 196,

        /// <summary>
        ///     用户选项
        /// </summary>
        UserQrCodeSideBar = 199,

        #endregion 第三方接口

        #region 员工

        /// <summary>
        ///     员工管理
        /// </summary>
        EmployeeManagementSideBar = 620,

        /// <summary>
        ///     岗位权限
        /// </summary>
        PostAuthoritySideBar = 621,

        /// <summary>
        ///     添加员工
        /// </summary>
        AddStaffSideBar = 622,

        /// <summary>
        ///     员工等级
        /// </summary>
        EmployeeRankSideBar = 623,

        /// <summary>
        ///     员工配置
        /// </summary>
        StaffingSideBar = 624,

        #endregion 员工

        #region 创世纪

        /// <summary>
        ///     保单管理
        /// </summary>
        PolicyManagementSideBar = 750,

        /// <summary>
        ///     添加保单(测试用)
        /// </summary>
        AddManagementSideBar = 751,

        /// <summary>
        ///     分润基数设置
        /// </summary>
        BaseSideBar = 752,

        /// <summary>
        ///     奖金类型设置
        /// </summary>
        TypeSideBar = 753,

        /// <summary>
        ///     KPI绩效考核设置
        /// </summary>
        AchievementsSideBar = 754,

        DiyOpenSideBar = 755,

        DiySiteBar = 756,

        #endregion 创世纪

        #region Erp

        /// <summary>
        ///     虚拟库存
        /// </summary>
        StockSideBar = 20,

        /// <summary>
        ///     用户库存
        /// </summary>
        UserInventorySideBar = 21,

        #endregion Erp

        #region Kpi

        /// <summary>
        ///     绩效配置
        /// </summary>
        PerformanceSideBar = 820,

        /// <summary>
        ///     晋升机制
        /// </summary>
        PromotionSideBar = 823,

        /// <summary>
        ///     降职机制
        /// </summary>
        DemotionSideBar = 831,

        #endregion Kpi

        #region 报表

        /// <summary>
        ///     直推会员等级报表
        /// </summary>
        DirectSideBar = 420,

        /// <summary>
        ///     间推会员等级报表
        /// </summary>
        IndirectSideBar = 421,

        /// <summary>
        ///     团队会员等级报表
        /// </summary>
        TeamSideBar = 422,

        /// <summary>
        ///     会员销售统计表
        /// </summary>
        MemberSideBar = 423,

        /// <summary>
        ///     直推会员销售统计表
        /// </summary>
        DirectTableSideBar = 424,

        /// <summary>
        ///     间推会员销售统计表
        /// </summary>
        IndirectTableSideBar = 425,

        /// <summary>
        ///     团队销售统计表
        /// </summary>
        TeamTableSideBar = 426,

        #endregion 报表

        #region 分润

        /// <summary>
        ///     分润数据
        /// </summary>
        ShareSideBar = 11,

        /// <summary>
        ///     我的分润配置
        /// </summary>
        IShareSideBar = 12,

        /// <summary>
        ///     模式超市
        /// </summary>
        SupermarketSideBar = 13,

        /// <summary>
        ///     分润订单
        /// </summary>
        ShareOrderSideBar = 14,

        /// <summary>
        ///     分润数据精算
        /// </summary>
        ActuarySideBar = 15,

        /// <summary>
        ///     分润数据精算
        /// </summary>
        GoldPoolSideBar = 16,

        /// <summary>
        ///     任务队列
        /// </summary>
        TaskQueueSideBar = 17,

        /// <summary>
        ///     会员升级记录
        /// </summary>
        upgradeSideBar = 18,

        #endregion 分润

        #region 订单

        /// <summary>
        ///     订单管理
        /// </summary>
        OrderManagementSideBar = 761,

        /// <summary>
        ///     购物车
        /// </summary>
        CartSideBar = 762,

        /// <summary>
        ///     商品收藏夹
        /// </summary>
        ProductCollectSideBar = 763,

        /// <summary>
        ///     商品足迹
        /// </summary>
        ProductFontfrontSideBar = 764,

        /// <summary>
        ///     发货记录
        /// </summary>
        DeliverSideBar = 765,

        /// <summary>
        ///     订单配置
        /// </summary>
        OrderConfigSideBar = 769,

        #endregion 订单

        #region 供应商

        /// <summary>
        ///     店铺管理
        /// </summary>
        StoreSideBar = 31,

        /// <summary>
        ///     店铺添加
        /// </summary>
        StoreAddSideBar = 32,

        /// <summary>
        ///     店铺等级
        /// </summary>
        StoreGradeSideBar = 33,

        /// <summary>
        ///     运费模板管理
        /// </summary>
        TemplateSideBar = 34,

        /// <summary>
        ///     商品配送配置
        /// </summary>
        DistributionSideBar = 35,

        /// <summary>
        ///     常用快递
        /// </summary>
        CommonlySideBar = 36,

        /// <summary>
        ///     入住规则
        /// </summary>
        CheckSideBar = 37,

        #endregion 供应商

        #region 商城

        /// <summary>
        ///     商品管理
        /// </summary>
        CommoditySideBar = 931,

        /// <summary>
        ///     添加商品
        /// </summary>
        AddCommoditySideBar = 933,

        /// <summary>
        ///     类目管理
        /// </summary>
        CategorySideBar = 934,

        /// <summary>
        ///     商品分类
        /// </summary>
        ClassificationSideBar = 935,

        /// <summary>
        ///     商品标签
        /// </summary>
        LabelSideBar = 936,

        /// <summary>
        ///     产品线
        /// </summary>
        ProductLineSideBar = 937,

        /// <summary>
        ///     商品统计数据
        /// </summary>
        StatisticsSideBar = 938,

        /// <summary>
        ///     商品发布设置
        /// </summary>
        ReleaseSideBar = 939,

        /// <summary>
        ///     商城模型配置
        /// </summary>
        ModelSideBar = 940,

        #endregion 商城

        /// <summary>
        ///     会员关系网
        /// </summary>
        RelationshipIndexSideBar = 971,

        /// <summary>
        ///     数据合并
        /// </summary>
        MogoMigrateSideBar = 972,

        // 用户权益
        UserRightsSideBar = 973,

        // 用户权益
        ShareOrderTestSideBar = 974,

        //团队介绍
        TeamIntroSideBar = 975,

        //宣传材料
        PromotionalMaterialSideBar = 976,

        //成功案例
        SuccessfulCasesSideBar = 977
    }
}