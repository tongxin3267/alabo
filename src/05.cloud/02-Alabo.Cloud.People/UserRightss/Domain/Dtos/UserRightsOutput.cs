using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Market.UserRightss.Domain.Enums;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Market.UserRightss.Domain.Dtos {

    /// <summary>
    ///     会员权益模型
    /// </summary>
    public class UserRightsOutput : UIBase, IAutoTable<UserRightsOutput> {

        /// <summary>
        /// 是否已经登记地址
        /// </summary>
        public bool IsRegion { get; set; } = true;

        /// <summary>
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     会员等级
        /// </summary>
        [Field(ListShow = true, SortOrder = 20, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "会员等级")]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 当前会员等级
        /// </summary>
        public string CurrnetGradeName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        ///     升级价格
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        ///     图标
        /// </summary>

        public string Icon { get; set; }

        /// <summary>
        ///     简介
        /// </summary>

        public string Intro { get; set; }

        /// <summary>
        ///     背景图片
        /// </summary>
        public string BackGroundImage { get; set; }

        /// <summary>
        ///     开通方式
        /// </summary>
        public UserRightOpenType OpenType { get; set; }

        public string ThemeColor { get; set; }

        /// <summary>
        ///     提交按钮文字
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        ///     权益列表
        /// </summary>
        public IList<PrivilegesItem> Privileges { get; set; } = new List<PrivilegesItem>();

        /// <summary>
        ///     剩余数量
        /// </summary>
        [Field(ListShow = true, SortOrder = 20, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "剩余数量")]
        public long RemainCount { get; set; } = 0;

        /// <summary>
        ///     剩余数量
        /// </summary>
        [Field(ListShow = true, SortOrder = 20, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "总数量")]
        public long? TotalUseCount { get; set; } = 0;

        /// <summary>
        ///     单个用户的总数量
        ///     原则上与等级权益配置数量相同
        ///     考虑到可以单独为用户设置的情况
        /// </summary>
        public long TotalCount { get; set; } = 0;

        /// <summary>
        /// 用户等级是否开放
        /// </summary>
        public bool IsOpen { get; set; } = false;

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<UserRightsOutput> PageTable(object query, AutoBaseModel autoModel) {
            var input = ToQuery<UserRightsOrderInput>();
            var userRightses = Resolve<IUserRightsService>().GetList(u => u.UserId == input.UserId);
            var result = new List<UserRightsOutput>();
            foreach (var item in userRightses) {
                var view = AutoMapping.SetValue<UserRightsOutput>(item);
                result.Add(view);
            }

            return ToPageResult(PagedList<UserRightsOutput>.Create(result, userRightses.Count, 15, 1));
        }
    }

    /// <summary>
    ///     等级权益
    /// </summary>
    public class PrivilegesItem {

        /// <summary>
        ///     特权图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     等级名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     详细说明
        /// </summary>
        public string Intro { get; set; }
    }
}