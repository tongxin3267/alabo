using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoPreviews;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Bills.UI
{
    [ClassProperty(Name = "财务详情Autopreview")]
    public class BillViewOutput : UIBase, IAutoPreview
    {
        public long Id { get; set; }

        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 2, Width = "160")]
        [Display(Name = "编号")]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(8, '0')}";
                if (Id.ToString().Length >= 9) searSerial = $"{Id.ToString()}";

                return searSerial;
            }
        }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     对方用户
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        [Display(Name = "对方用户名")]
        public string OtherUserName { get; set; }

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
        [Display(Name = "明细")]
        public string Intro { get; set; }

        /// <summary>
        ///     交易类型
        /// </summary>
        [Display(Name = "交易类型")]
        public string AcctionType { get; set; }

        /// <summary>
        ///     资金流向
        /// </summary>
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 4, Width = "160")]
        [Display(Name = "资金流向")]
        public string Flow { get; set; }

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

        public AutoPreview GetPreview(string id, AutoBaseModel autoModel)
        {
            var model = Resolve<IBillService>().GetSingle(u => u.Id == id.ToInt64());
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var item = AutoMapping.SetValue<BillViewOutput>(model);
            var otherUserName = Resolve<IUserService>().GetSingle(u => u.Id == model.OtherUserId)?.UserName;
            item.Flow = model.Flow.GetDisplayName();
            item.MoneyTypeName = moneyTypes.FirstOrDefault(u => u.Id == model.MoneyTypeId)?.Name;
            item.UserName = Resolve<IUserService>().GetSingle(u => u.Id == model.UserId)?.UserName;
            item.AcctionType = model.Type.GetDisplayName();
            item.OtherUserName = otherUserName;
            var result = new AutoPreview
            {
                KeyValues = item.ToKeyValues()
            };

            return result;
        }
    }
}