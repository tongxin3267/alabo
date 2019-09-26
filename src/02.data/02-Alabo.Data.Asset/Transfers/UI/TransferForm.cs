﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Asset.Transfers.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoPreviews;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    public class TransferForm : UIBase, IAutoPreview {

        /// <summary>
        ///     对方用户名
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true, SortOrder = 10003, Width = "160")]
        [Display(Name = "对方用户名")]
        public string OtherUserName { get; set; }

        /// <summary>
        ///
        ///     金额
        /// </summary>
        [Display(Name = "金额")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true, SortOrder = 1, Width = "160")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     明细说明
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true, SortOrder = 10012, Width = "160")]
        [Display(Name = "备注")]
        public string Intro { get; set; } = "无";

        public AutoPreview GetPreview(string id, AutoBaseModel autoModel) {
            //TODO 2019年9月25日 转账优化
            //  var model = Resolve<ITradeService>().GetSingle(u => u.Id == id.ToInt64() && u.Type == TradeType.Transfer);
            //  var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //  var item = AutoMapping.SetValue<TransferForm>(model);
            //item.Serial = model.Serial;
            //item.MoneyTypeName = moneyTypes.FirstOrDefault(u => u.Id == model.MoneyTypeId)?.Name;
            //item.Intro = model.TradeExtension?.TradeRemark?.UserRemark;
            //if (model.TradeExtension.Transfer != null) {
            //    item.OtherUserName = Resolve<IUserService>().GetSingle(u => u.Id == model.TradeExtension.Transfer.OtherUserId)?.UserName;
            //}

            //var result = new AutoPreview {
            //    KeyValues = item.ToKeyValues()
            //};
            //return result;
            return null;
        }
    }
}