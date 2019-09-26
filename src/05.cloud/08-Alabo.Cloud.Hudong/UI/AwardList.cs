using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Enums;
using Alabo.App.Share.HuDong.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.HuDong.Modules {

    [ClassProperty(Name = "获奖记录", PageType = ViewPageType.List)]
    public class AwardList : UIBase, IAutoTable<HudongRecord> {

        /// <summary>
        /// 获奖描述
        /// </summary>
        [Display(Name = "获奖描述"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public string Intro { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public AwardStatus HuDongStatus { get; set; }

        /// <summary>
        /// 互动类型
        /// </summary>
        [Display(Name = "互动类型"), Field(ControlsType = ControlsType.Label, ListShow = true,
            DataSource = "Alabo.App.Share.HuDong.Domain.Enums.HuDongEnums")]
        public HuDongEnums HuDongActivityType { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<HudongRecord> PageTable(object query, AutoBaseModel autoModel) {
            var plList = Resolve<IHudongRecordService>().GetPagedList(query);

            return ToPageResult(plList);
        }
    }
}