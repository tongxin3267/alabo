using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Domain.Enums;
using Alabo.App.Open.HuDong.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.HuDong.Modules {

    /// <summary>
    ///
    /// </summary>
    [ClassProperty(Name = "大转盘", Description = "大转盘", PageType = ViewPageType.List)]
    public class BigWheel : UIBase, IHuDong, IAutoTable<HudongRecord> {

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "id"), Field(ControlsType = ControlsType.Label, ListShow = false)]
        public string Id { get; set; }

        /// <summary>
        /// 抽奖人
        /// </summary>
        [Display(Name = "抽奖人姓名"), Field(ControlsType = ControlsType.Label, ListShow = true, IsShowBaseSerach = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 等级(中奖奖品)
        /// </summary>
        [Display(Name = "中奖奖品"), Field(ControlsType = ControlsType.Label, ListShow = true, IsShowBaseSerach = true)]
        public string Grade { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        [Display(Name = "奖品类型"), Field(ControlsType = ControlsType.Label, ListShow = true,
             DataSource = "Alabo.App.Open.HuDong.Domain.Enums.HudongAwardType")]
        public HudongAwardType HuDongActivityType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态"), Field(ControlsType = ControlsType.Label, ListShow = true)]
        public AwardStatus HuDongStatus { get; set; }

        /// <summary>
        /// 互动类型
        /// </summary>
        [Display(Name = "互动类型"), Field(ControlsType = ControlsType.Label, ListShow = true, DataSource = "Alabo.App.Open.HuDong.Domain.Enums.HuDongEnums")]
        public HuDongEnums HuDongType { get; set; }

        /// <summary>
        /// 抽奖时间
        /// </summary>
        [Display(Name = "抽奖时间"), Field(ControlsType = ControlsType.Label, ListShow = true)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获奖描述
        /// </summary>
        [Display(Name = "获奖描述"), Field(ControlsType = ControlsType.Label, ListShow = true)]
        public string Intro { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<HudongAward> DefaultAwards() {
            var list = new List<HudongAward>() {
                new HudongAward
                {
                    Grade = "一等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "二等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "三等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "四等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "五等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "六等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "七等奖",
                    Type = HudongAwardType.None
                },
                new HudongAward
                {
                    Grade = "无奖品",
                    Type = HudongAwardType.None,
                    Rate = 1,
                    Count = 9999
                }
            };

            return list;
        }

        /// <summary>
        /// 设置默认奖项
        /// </summary>
        /// <returns></returns>
        public HudongSetting Setting() {
            var setting = new HudongSetting {
                RewardCount = 8
            };
            return setting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public PageResult<HudongRecord> PageTable(object query, AutoBaseModel autoModel) {
            var plList = Resolve<IHudongRecordService>().GetPagedList(query);

            return ToPageResult(plList);
        }
    }
}