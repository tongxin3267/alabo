using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.AutoConfigs.Services;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Framework.Basic.Grades.Domain.Configs {

    /// <summary>
    ///     等级特权
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "等级特权", Icon = "fa fa-birthday-cake", Description = "等级特权", PageType = ViewPageType.List,
        SortOrder = 22)]
    public class GradePrivilegesConfig : AutoConfigBase, IAutoConfig {

        public void SetDefault() {
            var list = Ioc.Resolve<IAlaboAutoConfigService>().GetList<GradePrivilegesConfig>();
            if (list.Count <= 0) {
                var configs = new List<GradePrivilegesConfig>();
                var config = new GradePrivilegesConfig {
                    Icon = "/wwwroot/static/images/GradePrivileges/AdvancePayments01.png",
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698002"),
                    Name = "先行赔付",
                    Intro =
                        "当您提交退货信息后，商家拒绝履行“7天无理由退货”承诺的情况下，中酷平台在审核您提交的充要证据后，将按《中华人民共和国消费者权益保护法》、《产品质量法》及其他相关法律规定，依据单方面的判断决定实施先行赔付退款，或不予接受您先行赔付付款申请"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698003"),
                    Icon = "/wwwroot/static/images/GradePrivileges/FreightSubsidies03.png",
                    Name = "退货运费补贴",
                    Intro = "退货运费补贴是由中酷平台提供解决消费者退货运费问题的服务，是一种消费者保障服务"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698004"),
                    Icon = "/wwwroot/static/images/GradePrivileges/Delivery01.png",
                    Name = "72小时发货",
                    Intro = "中酷平台一般默认72小时内为您发货，部分商品会标注具体的发货时间，您可以在商品详情页进行查看"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698005"),
                    Icon = "/wwwroot/static/images/GradePrivileges/Refund05.png",
                    Name = "7天无理由退款",
                    Intro = "签收时间以物流公司官网显示的物流签收时间为准。如有准确签收时间的，以该签收时间后168小时为7天，如物流信息不完整，则无理由退货时间为确认收货后的168小时。"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698006"),
                    Icon = "/wwwroot/static/images/GradePrivileges/FreePackageMail08.png",
                    Name = "免费包邮",
                    Intro = "部分商品品或购买商品满足一定条件下，即享受免费包邮"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698007"),
                    Icon = "/wwwroot/static/images/GradePrivileges/IdentityNameplate05.png",
                    Name = "身份铭牌",
                    Intro = "身份的象征，彰显您的会员身份！会随着等级不同发生变化，当真是牛逼闪闪的身份铭牌！"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698008"),
                    Icon = "/wwwroot/static/images/GradePrivileges/Integral05.png",
                    Name = "积分抵现",
                    Intro = "积分可以抵兑的金额上限，谁说积分不能当钱花？在中酷平台，积分就是可以当钱花！并且根据等级不同，每一个订单能使用积分替代的金额都会不同，去试试！"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698009"),
                    Icon = "/wwwroot/static/images/GradePrivileges/FreeTrial05.png",
                    Name = "免费试用",
                    Intro = "中酷平台宴请小伙伴，中酷平台内会出现按等级用户专门使用活动哟~而且部分小伙伴还可以突然获得一张免单体验卷哟！"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698010"),
                    Icon = "/wwwroot/static/images/GradePrivileges/Evaluation05.png",
                    Name = "评价红名展示",
                    Intro = "想让你的评论脱颖而出？想让更多小伙伴接受你的意见？拥有这样拉轰的特权，能让您在众多小伙伴中脱颖而出"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698011"),
                    Icon = "/wwwroot/static/images/GradePrivileges/FastRefund05.png",
                    Name = "极速退款",
                    Intro = "不小心买错了？买了去不了了？突然改变主意了？都不要紧！立刻为你退款！如此特权，还会害怕买错团购嘛？"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698012"),
                    Icon = "/wwwroot/static/images/GradePrivileges/PriorityAccess03.png",
                    Name = "客服优先接入",
                    Intro = "享受服务绿色通道，VIP客服为您快速处理问题，专家团一站式跟进，让您无后顾之忧！服务时间：8：00-24：00"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698013"),
                    Icon = "/wwwroot/static/images/GradePrivileges/IntegralFeedback01.png",
                    Name = "积分回馈",
                    Intro = "在多个场景使用支付宝付款即可获得积分，积分可兑换权益。使用支付宝越多积分越多"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698014"),
                    Icon = "/wwwroot/static/images/GradePrivileges/BirthdayPrivilege01.png",
                    Name = "生日特权",
                    Intro = "专享生日祝福，生日当天让支付宝因你而变"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698015"),
                    Icon = "/wwwroot/static/images/GradePrivileges/GlobalVIP01.png",
                    Name = "全球VIP",
                    Intro = "依据不同的会员等级，为你定制海外当地知名商户购物&吃喝玩乐、官网消费专属权益"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698016"),
                    Icon = "/wwwroot/static/images/GradePrivileges/CashDiscount01.png",
                    Name = "兑换折扣",
                    Intro = "部分权益积分兑换享有折扣价，会员等级越高折扣越大。黄金9.5折，铂金9折，钻石8.5折"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698017"),
                    Icon = "/wwwroot/static/images/GradePrivileges/ExclusiveFor01.png",
                    Name = "专属兑换",
                    Intro = "部分优质权益仅限钻石会员使用积分兑换，更多更好的权益只为你服务，让你的积分更有价值"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698018"),
                    Icon = "/wwwroot/static/images/GradePrivileges/ExtremeClaims01.png",
                    Name = "极速理赔",
                    Intro = "钻石会员专享极速理赔服务，专享通道优先处理。理赔申请审核通过后24小时内完成理赔"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698019"),
                    Icon = "/wwwroot/static/images/GradePrivileges/SpendBaiPrivileges01.png",
                    Name = "花呗特权",
                    Intro = "钻石会员若忘记还款，在还款日后三天内归还全部逾期贷款，将不收取这三天的应计利息，专享三天免息宽限期"
                };
                list.Add(config);

                config = new GradePrivilegesConfig {
                    Id = Guid.Parse("E97CCE1E-1478-49BD-BFC7-E73A5D698020"),
                    Icon = "/wwwroot/static/images/GradePrivileges/PriorityExperience01.png",
                    Name = "优先体验",
                    Intro = "新产品上线前会特邀钻石会员优先体验，反馈建议将作为产品优化的参考"
                };
                list.Add(config);

                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,

                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(list)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }

        #region

        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = true, SortOrder = 1, IsImagePreview = true)]
        [Display(Name = "特权图标")]
        public string Icon { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "权益名称")]
        [Main]
        public string Name { get; set; }

        [Field(ControlsType = ControlsType.TextBox)]
        [Display(Name = "权益介绍")]
        public string Intro { get; set; }

        #endregion
    }
}