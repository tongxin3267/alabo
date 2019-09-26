using System;
using System.Collections.Generic;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;

namespace Alabo.Data.People.Users.ViewModels {

    /// <summary>
    ///     会员管理，账户信息
    /// </summary>
    public class ViewUserDetailInfo {

        public ViewUserDetailInfo() {
            UserTypeInfos = new List<UserTypeInfo>();
            UserAccountInfos = new List<UserAccountInfo>();
            UserBillInfos = new List<UserBillInfo>();
        }

        public List<UserTypeInfo> UserTypeInfos { get; set; }
        public List<UserAccountInfo> UserAccountInfos { get; set; }
        public List<UserBillInfo> UserBillInfos { get; set; }

        public void AddType(UserTypeInfo type) {
            UserTypeInfos.Add(type);
        }

        public void AddAccount(UserAccountInfo account) {
            UserAccountInfos.Add(account);
        }

        public void AddBill(UserBillInfo bill) {
            UserBillInfos.Add(bill);
        }
    }

    /// <summary>
    ///     会员类型
    /// </summary>
    public class UserTypeInfo {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public Guid GradeId { get; set; }
        public string GradeName { get; set; }
        public DateTime CreateTime { get; set; }
        public Status Status { get; set; }
    }

    /// <summary>
    ///     资产明细
    /// </summary>
    public class UserAccountInfo {
        public long Id { get; set; }
        public string UserName { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal FreezeAmount { get; set; }
        public decimal HistoryAmount { get; set; }
        public DateTime LastOptTime { get; set; }
        public Status Status { get; set; }
    }

    /// <summary>
    ///     交易记录
    /// </summary>
    public class UserBillInfo {
        public long Id { get; set; }
        public string Serial { get; set; }
        public string BillTypeName { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal AfterAmount { get; set; }
        public string OtherUserName { get; set; }
        public BillActionType TradeType { get; set; }
        public string Intro { get; set; }
        public BillStatus BillStatus { get; set; }
    }
}