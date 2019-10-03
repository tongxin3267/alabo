using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Themes.Extensions;
using Alabo.UI;
using Alabo.UI.Design.AutoLists;
using Alabo.UI.Design.AutoPreviews;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.UI.AutoFrom
{
    /// <summary>
    ///     会员管理
    /// </summary>
    [ClassProperty(Name = "会员管理", Description = "会员管理")]
    public class RecommendAutoList : UIBase, IAutoList, IAutoPreview
    {
        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out var userId);
            dic.TryGetValue("pageIndex", out var pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }

            var userInput = new UserInput
            {
                PageIndex = (int)pageIndex,
                ParentId = userId.ToInt64(),
                PageSize = 15
            };
            var model = Resolve<IUserService>().GetViewUserPageList(userInput);

            var users = Resolve<IUserDetailService>().GetList();

            var list = new List<AutoListItem>();
            foreach (var item in model)
            {
                // var grade = Resolve<IGradeService>().GetGrade(item.GradeId);
                var apiData = new AutoListItem
                {
                    Title = item.UserName.ReplaceHtmlTag(), //标题
                    Intro = $"{item.Mobile} {item.CreateTime.ToString("yyyy-MM-dd hh:ss")}", //简介
                    Value = item.GradeName, //会员等级
                    Image = Resolve<IApiService>()
                        .ApiImageUrl(item.Avator), //users.FirstOrDefault(u => u.UserId == item.Id)?.Avator,//左边头像
                    Id = item.Id, //id
                    Url = $"/pages/index?path=user_view&id={item.Id}".ToClientUrl(ClientType.WapH5) //详情链接
                };
                list.Add(apiData);
            }

            return ToPageList(list, model);
        }

        public Type SearchType()
        {
            return typeof(RecommendAutoList);
        }

        public AutoPreview GetPreview(string id, AutoBaseModel autoModel)
        {
            var model = Resolve<IUserDetailService>().GetUserOutput(id.ToInt64());
            var temp = new AutoPreview();
            temp.KeyValues = model.ToKeyValues();
            return temp;
        }

        #region 属性

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(IsShowBaseSerach = true, ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 1)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>

        [Display(Name = "手机")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 1)]
        public string Mobile { get; set; }

        /// <summary>
        ///     用户等级Id 每个类型对应一个等级
        /// </summary>
        [Display(Name = "用户等级Id")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.RadioButton,
            DataSourceType = typeof(UserGradeConfig), GroupTabId = 1, SortOrder = 1)]
        public Guid GradeId { get; set; }

        #endregion 属性
    }
}