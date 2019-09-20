using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Store.Domain.Entities;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Agent.Citys.UI.AutoForms {

    [ClassProperty(Name = "供应商")]
    public class SupplierAutoTable : UIBase, IAutoTable<SupplierAutoTable> {

        #region 供应商清单属性

        /// <summary>
        /// 商家名称
        /// </summary>
        [Display(Name = "商家名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
            IsTabSearch = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Operator = Datas.Queries.Enums.Operator.Contains,
            Width = "80", SortOrder = 2)]
        public string StoreName { get; set; }

        /// <summary>
        /// 品类
        /// </summary>
        [Display(Name = "品类")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
    IsTabSearch = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Operator = Datas.Queries.Enums.Operator.Contains,
    Width = "80", SortOrder = 2)]
        public string Category { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
          IsTabSearch = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Operator = Datas.Queries.Enums.Operator.Contains,
           Width = "80",
           SortOrder = 3)]
        public string Mobile { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Display(Name = "负责人")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
          IsTabSearch = true, IsShowBaseSerach = true, IsShowAdvancedSerach = false, Operator = Datas.Queries.Enums.Operator.Contains,
           Width = "80",
           SortOrder = 3)]
        public string MasterName { get; set; }

        /// <summary>
        ///     商家等级
        /// </summary>
        [Display(Name = "商家等级")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
          IsTabSearch = true, IsShowBaseSerach = false, IsShowAdvancedSerach = false, Operator = Datas.Queries.Enums.Operator.Contains,
           Width = "80",
           SortOrder = 3)]
        public string SupplierGrade { get; set; }

        /// <summary>
        ///     推荐人级称
        /// </summary>
        [Display(Name = "推荐人级称")]
        public string ParentGrade { get; set; }

        /// <summary>
        ///     Gets or sets the name of the status.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true,
            IsShowBaseSerach = true,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeStatus", Width = "80",
            SortOrder = 9)]
        public string StatusName { get; set; }

        /// <summary>
        ///     开通时间
        /// </summary>
        [Display(Name = "开通时间")]
        [Field(ControlsType = ControlsType.DateTimeRang,
            ListShow = true, Width = "100", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        #endregion 供应商清单属性

        public List<TableAction> Actions() {
            return new List<TableAction>() {
                new TableAction("详情","url",TableActionType.ColumnAction)
            };
        }

        public PageResult<SupplierAutoTable> PageTable(object query, AutoBaseModel autoModel) {
            var result = new PagedList<SupplierAutoTable>();
            var querys = ToQuery<Store>();
            querys.ParentUserId = autoModel.BasicUser.Id;

            var list = Resolve<IStoreService>().GetPagedList(querys);
            if (list.Count > 0) {
                list.ForEach(p => {
                    // var gradeInfo = Resolve<IGradeService>().GetSupplierGrade(p.GradeId).Name;
                    var user = Resolve<IUserService>().GetSingle(p.UserId);

                    var model = new SupplierAutoTable();
                    model.StoreName = p.Name;
                    model.Mobile = user.Mobile;
                    model.MasterName = user.Name;
                    //   model.SupplierGrade = gradeInfo == null ? "--" : gradeInfo;
                    model.StatusName = EnumExtensions.GetDisplayName(p.Status);
                    model.CreateTime = p.CreateTime;
                    result.Add(model);
                });
            }
            return ToPageResult(result);
        }
    }
}