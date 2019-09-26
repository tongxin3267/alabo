using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis;
using Alabo.Core.WebUis.Design.AutoPreviews;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "充值详情(autopreview)")]
    public class RechargeViewOutPut : UIBase, IAutoPreview {

        /// <summary>
        /// 编号
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 2, Width = "160")]
        [Display(Name = "编号")]
        public string Serial { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     金额
        /// </summary>
        [Display(Name = "金额")]
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 1, Width = "160")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     本次变动后账户的货币量
        ///     账后金额大于0,
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        [Display(Name = "账后金额")]
        public decimal AfterAmount { get; set; }

        /// <summary>
        ///     明细说明
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        [Display(Name = "充值备注")]
        public string Intro { get; set; } = "无";

        /// <summary>
        ///     交易类型
        /// </summary>
        [Display(Name = "交易类型")]
        public string AcctionType { get; set; }

        /// <summary>
        ///     货币名称
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 3, Width = "160")]
        [Display(Name = "货币名称")]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Field(SortOrder = 199999)]
        [Display(Name = "交易时间")]
        public string CreateTime { get; set; }

        public AutoPreview GetPreview(string id, AutoBaseModel autoModel) {
            //var model = Resolve<ITradeService>().GetSingle(u => u.Id == id.ToInt64() && u.Type == TradeType.Recharge);
            //var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //var item = AutoMapping.SetValue<RechargeViewOutPut>(model);

            //item.Serial = model.Serial;
            //item.MoneyTypeName = moneyTypes.FirstOrDefault(u => u.Id == model.MoneyTypeId)?.Name;
            //item.Intro = model.TradeExtension?.TradeRemark?.UserRemark;
            //item.UserName = Resolve<IUserService>().GetSingle(u => u.Id == model.UserId)?.UserName;
            //item.AcctionType = model.Type.GetDisplayName();
            //var result = new AutoPreview {
            //    KeyValues = item.ToKeyValues()
            //};

            //return result;
            return null;
        }
    }
}