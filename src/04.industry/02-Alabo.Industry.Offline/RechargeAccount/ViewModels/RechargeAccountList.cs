using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Industry.Offline.RechargeAccount.Services;
using Alabo.Maps;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Offline.RechargeAccount.ViewModels
{
    /// <summary>
    /// 储值记录
    /// </summary>
    [ClassProperty(Name = "储值记录", PageType = ViewPageType.List)]
    public class RechargeAccountList : UIBase, IAutoTable<RechargeAccountList>, IAutoList
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名"), Field(ControlsType = ControlsType.Label, ListShow = true, SortOrder = 1, IsShowBaseSerach = true)]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名"), Field(ControlsType = ControlsType.Label, ListShow = false)]
        public string UserName { get; set; }

        /// <summary>
        /// 储值金额
        /// </summary>
        [Display(Name = "储值金额"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal StoreAmount { get; set; }

        /// <summary>
        /// 到账金额
        /// </summary>
        [Display(Name = "到账金额"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal ArriveAmount { get; set; }

        /// <summary>
        /// 赠送兑换券
        /// </summary>
        [Display(Name = "赠送消费额"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal GiveChangeAmount { get; set; }

        /// <summary>
        /// 赠送购物券
        /// </summary>
        [Display(Name = "赠送优惠券"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal GiveBuyAmount { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        [Display(Name = "赠送积分"), Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间"), Field(ControlsType = ControlsType.Label, ListShow = true)]
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
            var orderQuery = new ExpressionQuery<Entities.RechargeAccountLog>
            {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            var model = Resolve<IRechargeAccountLogService>().GetPagedList(orderQuery);

            var list = new List<AutoListItem>();
            foreach (var item in model)
            {
                var apiData = new AutoListItem
                {
                    Image = GetAvator(item.UserId),
                    Value = item.ArriveAmount,
                    Title = "储值金额：" + item.StoreAmount,
                    Intro = item.CreateTime.ToString(),
                    Id = item.Id,
                };
                list.Add(apiData);
            }
            return ToPageList(list, model);
        }

        public string GetAvator(long userId)
        {
            var avator = Resolve<IApiService>().ApiImageUrl(Resolve<IApiService>().ApiUserAvator(userId));
            return avator;
        }

        public PageResult<RechargeAccountList> PageTable(object query, AutoBaseModel autoModel)
        {
            var list = Resolve<IRechargeAccountLogService>().GetPagedList(query);

            var rsList = list.MapTo<PagedList<RechargeAccountList>>();
            rsList.ForEach(x => x.Name = x.UserName);

            return ToPageResult(rsList);
        }

        public Type SearchType()
        {
            throw new NotImplementedException();
        }
    }
}
