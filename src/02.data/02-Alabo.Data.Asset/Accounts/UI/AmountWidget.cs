using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Helpers;
using Alabo.UI.Design.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Asset.Accounts.UI
{
    /// <summary>
    /// </summary>
    public class AmountWidget : IWidget
    {
        /// <summary>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public object Get(string json)
        {
            var dic = json.ToObject<Dictionary<string, string>>();
            var autoAmountItem = new AutoAmountItem();
            //前端传值需注意大小写 userId为必传项
            dic.TryGetValue("userId", out var userId);
            if (userId.IsNullOrEmpty()) return null;
            var billList = Ioc.Resolve<IBillService>()
                .GetList(u => u.UserId == userId.ToInt64() && u.Flow == AccountFlow.Income);

            #region 获取账户余额

            var userAccount = Ioc.Resolve<IAccountService>().GetSingle(u =>
                u.UserId == userId.ToInt64() && u.MoneyTypeId == Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000"));
            autoAmountItem.Account = userAccount.Amount;

            #endregion 获取账户余额

            #region 获取今日收益

            var todayBillList = Ioc.Resolve<IBillService>().GetList(u =>
                u.Flow == AccountFlow.Income && u.UserId == userId.ToInt64() &&
                u.CreateTime.Day == DateTime.Now.Day && u.CreateTime.Year == DateTime.Now.Year &&
                u.CreateTime.Month == DateTime.Now.Month);
            if (todayBillList != null || todayBillList.Count() > 0)
            {
                var Amount = new decimal();
                foreach (var item in todayBillList) Amount += item.Amount;

                autoAmountItem.TodayEarnings = Amount;
            }

            #endregion 获取今日收益

            #region 获取本月收益

            var monthBillList = Ioc.Resolve<IBillService>().GetList(u =>
                u.Flow == AccountFlow.Income && u.UserId == userId.ToInt64() &&
                u.CreateTime.Year == DateTime.Now.Year && u.CreateTime.Month == DateTime.Now.Month);

            if (monthBillList != null || monthBillList.Count() > 0)
            {
                var Amount = new decimal();
                foreach (var item in monthBillList) Amount += item.Amount;

                autoAmountItem.MonthEarnings = Amount;
            }

            #endregion 获取本月收益

            #region 获取累计收益

            if (billList.Count > 0)
            {
                var Amount = new decimal();
                foreach (var item in monthBillList) Amount += item.Amount;
                autoAmountItem.TotalEarnings = Amount;
            }

            #endregion 获取累计收益

            #region 获取最后一笔收益

            if (billList.Count > 0)
            {
                var bill = billList.OrderByDescending(u => u.Id).FirstOrDefault();
                if (bill != null) autoAmountItem.LastEarnings = bill.Amount;
            }

            #endregion 获取最后一笔收益

            #region 获取会员等级名称

            var user = Ioc.Resolve<IUserService>().GetSingle(u => u.Id == userId.ToInt64());
            var userGradeList = Ioc.Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            autoAmountItem.UserGradeName = userGradeList.FirstOrDefault(u => u.Id == user.GradeId).Name;

            #endregion 获取会员等级名称

            return autoAmountItem;
        }
    }

    /// <summary>
    /// </summary>
    public class AutoAmountItem
    {
        /// <summary>
        ///     会员等级名称
        /// </summary>
        public string UserGradeName { get; set; }

        /// <summary>
        ///     账户余额
        /// </summary>
        public decimal Account { get; set; }

        /// <summary>
        ///     最后一笔收益
        /// </summary>
        public decimal LastEarnings { get; set; }

        /// <summary>
        ///     本日收益
        /// </summary>
        public decimal TodayEarnings { get; set; }

        /// <summary>
        ///     本月收益
        /// </summary>
        public decimal MonthEarnings { get; set; }

        /// <summary>
        ///     累计收益
        /// </summary>
        public decimal TotalEarnings { get; set; }
    }
}