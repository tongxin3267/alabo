using Alabo.App.Agent.Citys.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Agent.Citys.Domain.Dtos {

    public class CityAutoTable : UIBase, IAutoTable<CityAutoTable> {
        #region

        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
        IsShowBaseSerach = true, IsShowAdvancedSerach = true, Operator = Datas.Queries.Enums.Operator.Contains, Width = "80", SortOrder = 1)]
        public string Name { get; set; }

        [Display(Name = "联系电话")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
        IsShowBaseSerach = true, IsShowAdvancedSerach = true, Operator = Datas.Queries.Enums.Operator.Contains, Width = "80", SortOrder = 2)]
        public string Mobile { get; set; }

        [Display(Name = "级称")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true,
        IsShowBaseSerach = false, IsShowAdvancedSerach = true, Operator = Datas.Queries.Enums.Operator.Contains, Width = "80", SortOrder = 3)]
        public string GradeName { get; set; }

        [Display(Name = "区域")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
         Width = "80", SortOrder = 4)]
        public string RegionArea { get; set; }

        /// <summary>
        /// 以交款比例显示状态，如15万，已交款5万，状态为 33%
        /// </summary>
        [Display(Name = "用户状态")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, IsShowBaseSerach = false, IsShowAdvancedSerach = false, Width = "80", SortOrder = 5)]
        public string StatusDes { get; set; }

        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "80", SortOrder = 6)]
        public string ParentName { get; set; }

        [Display(Name = "推荐人Id")]
        [Field(ControlsType = ControlsType.Hidden, ListShow = false,
        IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "80", SortOrder = 6)]
        public long ParentUserId { get; set; }

        [Display(Name = "注册日期")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
         ListShow = true, Width = "100", SortOrder = 7)]
        public DateTime CreateTime { get; set; }

        #endregion

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<CityAutoTable> PageTable(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();
            dic.Add("ParentUserId", autoModel.BasicUser.Id.ToString());

            var list = Resolve<ICityService>().GetPagedList(dic.ToJson());
            var citys = new List<CityAutoTable>();
            var result = new PagedList<CityAutoTable>();
            foreach (var item in list) {
                var model = new CityAutoTable() {
                    RegionArea = item.RegionName,
                    CreateTime = item.CreateTime,
                    //GradeName = Resolve<IGradeService>().GetGrade(item.GradeId) != null ? Resolve<IGradeService>().GetGrade(item.GradeId).GetDisplayName() : "--",
                    Mobile = item.UserName,
                    Name = string.IsNullOrEmpty(item.Name) ? "--" : item.Name,
                    ParentName = item.ParentUserName,
                    StatusDes = item.Status.GetDisplayName()
                };
                result.Add(model);
            }
            return ToPageResult(result);
        }
    }
}