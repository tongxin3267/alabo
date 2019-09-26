using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.UserRightss.Domain.Dtos {

    public class ViewUserRights : UIBase, IAutoTable<ViewUserRights> {

        /// <summary>
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     会员等级
        /// </summary>
        [Field(ListShow = true, SortOrder = 2, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "会员等级")]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Field(ListShow = true, SortOrder = 1, EditShow = true,IsShowBaseSerach = true,ControlsType = ControlsType.TextBox)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }


        /// <summary>
        ///     使用数量
        /// </summary>
        [Field(ListShow = true, SortOrder = 3, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "使用数量")]
        public long? TotalUseCount { get; set; } = 0;

        /// <summary>
        ///     总数量
        /// </summary>
        [Field(ListShow = true, SortOrder = 4, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "总数量")]
        public long RemainCount { get; set; } = 0;

        /// <summary>
        ///     开通时间
        /// </summary>
        [Field(ListShow = true, SortOrder = 5, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Display(Name = "开通时间")]
        public DateTime CreateTime { get; set; }



        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<ViewUserRights> PageTable(object query, AutoBaseModel autoModel) {
            var usergradelist = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();

            //var model = Resolve<IUserService>().GetViewUserPageList(userInput);

            var userRightses = Resolve<IUserRightsService>().GetList(u => u.UserId == autoModel.BasicUser.Id);


            var result = new List<ViewUserRights>();
            foreach (var item in userRightses) {

                var view = AutoMapping.SetValue<ViewUserRights>(item);
                var userModel = Resolve<IUserService>().GetSingle(item.Id);

                view.CreateTime = userModel.CreateTime;
                view.UserName = userModel.UserName;
                view.RemainCount = item.TotalCount;//剩余数量
                view.TotalUseCount = item.TotalUseCount;//使用数量
                view.Name = usergradelist.FirstOrDefault(u => u.Id == view.GradeId)?.Name;//等级名称
                result.Add(view);
            }

            return ToPageResult(PagedList<ViewUserRights>.Create(result, userRightses.Count, 15, 1));
        }
    }
}