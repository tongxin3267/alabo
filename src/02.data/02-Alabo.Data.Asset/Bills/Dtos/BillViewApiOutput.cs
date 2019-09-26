using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis.Design.AutoPreviews;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Bills.Dtos {

    [ClassProperty(Name = "财务详情", Description = "财务详情")]
    public class BillViewApiOutput : EntityDto, IAutoPreview {

        /// <summary>
        ///     使用Id做序列号，10位数序列号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(SortOrder = 2)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     对方用户
        /// </summary>
        [Field(SortOrder = 3)]
        [Display(Name = "对方用户名")]
        public string OtherUserName { get; set; }

        /// <summary>
        ///     金额
        /// </summary>
        [Field(SortOrder = 4)]
        [Display(Name = "金额")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     本次变动后账户的货币量
        ///     账后金额大于0,
        /// </summary>
        [Field(SortOrder = 5)]
        [Display(Name = "账后金额")]
        public decimal AfterAmount { get; set; }

        /// <summary>
        ///     明细说明
        /// </summary>
        [Field(SortOrder = 6)]
        [Display(Name = "明细")]
        public string Intro { get; set; }

        /// <summary>
        ///     编号
        /// </summary>
        [Field(SortOrder = 1)]
        [Display(Name = "编号")]
        public string SerialNum { get; set; }

        /// <summary>
        ///     交易类型
        /// </summary>
        [Field(SortOrder = 7)]
        [Display(Name = "交易类型")]
        public string AcctionType { get; set; }

        /// <summary>
        ///     资金流向
        /// </summary>
        public string Flow { get; set; }

        /// <summary>
        ///     货币名称
        /// </summary>
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Field(SortOrder = 10)]
        [Display(Name = "交易时间")]
        public string CreateTime { get; set; }

        public AutoPreview GetPreview(string id, AutoBaseModel autoModel) {
            var model = Resolve<IBillService>().GetSingle(u => u.Id == id.ToInt64());
            var temp = new AutoPreview();
            temp.KeyValues = model.ToKeyValues();
            return temp;
        }
    }
}