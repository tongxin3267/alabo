using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.App.Market.FacePay.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.FacePay.Domain.Dtos
{

    public class ViewFacePay : UIBase, IAutoTable<ViewFacePay>, IAutoList
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 1, IsShowBaseSerach = true)]
        public string Name { get; set; }

        /// <summary>
        /// 等级ID
        /// </summary>
        [Display(Name = "等级Id")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = false, SortOrder = 2)]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [Display(Name = "等级")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 3, IsShowBaseSerach = true)]
        public string GradeName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 4)]
        public decimal Amount { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = "时间")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 5)]
        public DateTime CreateTime { get; set; }

        public List<TableAction> Actions()
        {
            return new List<TableAction>();
        }

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var orderQuery = new ExpressionQuery<Entities.FacePay>
            {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };

            orderQuery.And(e => e.UserId == userId.ToLong());
            var model = Resolve<IFacePayService>().GetPagedList(orderQuery);
            var list = new List<AutoListItem>();
            foreach (var item in model.ToList())
            {
                var apiData = new AutoListItem
                {
                    Image = GetAvator(item.UserId),
                    Title = "当面付-" + item.Amount+"元",
                    Intro ="付款时间："+ item.CreateTime.ToString(),
                    Value = item.UserName
                };
                list.Add(apiData);
            }
            return ToPageList(list, model);
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetAvator(long userId)
        {
            var avator = Resolve<IApiService>().ApiImageUrl(Resolve<IApiService>().ApiUserAvator(userId));
            return avator;
        }

        public PageResult<ViewFacePay> PageTable(object query, AutoBaseModel autoModel)
        {
            var list = Resolve<IFacePayService>().GetPagedList<ViewFacePay>(query);
            var rsList = list.MapTo<PagedList<ViewFacePay>>();

            return ToPageResult(rsList);
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}