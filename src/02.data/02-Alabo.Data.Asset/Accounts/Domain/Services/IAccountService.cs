using System;
using System.Collections.Generic;
using System.Data.Common;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Account;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.ViewModels.Account;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     用户资产接口
    /// </summary>
    public interface IAccountService : IService<Account, long> {

        /// <summary>
        ///     获取用户的资产账户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moneyTypeId">货币类型MoneyType ID</param>
        Account GetAccount(long userId, Guid moneyTypeId);

        Account GetAccount(DbTransaction transation, long userId, Guid moneyTypeId);

        Account GetAccount(DbTransaction transation, long userid, Currency currency);

        Account GetAccount(long id);

        Account GetAccount(long userid, Currency currency);

        /// <summary>
        ///     获取所有用户的正常激活账户
        /// </summary>
        /// <param name="userId">用户Id</param>
        IList<Account> GetUserAllAccount(long userId);

        /// <summary>
        ///     获取账户的金额
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moneyTypeId">货币类型ID</param>
        decimal GetAccountAmount(long userId, Guid moneyTypeId);

        /// <summary>
        ///     更改账户的金额
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moneyTypeId">货币类型ID</param>
        /// <param name="changedAmount">更加的金额，可以为正，也可以为负数，减少的时候为负数</param>
        bool ChangeAccountAmount(long userId, Guid moneyTypeId, decimal changedAmount);

        /// <summary>
        ///     更新账户的金额
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moneyTypeId"></param>
        /// <param name="amount"></param>
        bool UpdateAccountAmount(long userId, Guid moneyTypeId, decimal amount);

        /// <summary>
        ///     创建用户资产账户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="config"></param>
        ServiceResult CreateAccount(long userId, MoneyTypeConfig config);

        void InitSingleUserAccount(long userId);

        /// <summary>
        ///     生成区块链钱包地址
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="config"></param>
        string GetToken(long userId, MoneyTypeConfig config);

        /// <summary>
        ///     初始化钱包地址
        /// </summary>
        void InitToken();

        /// <summary>
        ///     获取钱包地址
        /// </summary>
        /// <param name="query"></param>
        PagedList<ViewBlockChain> GetBlockChainList(object query);

        /// <summary>
        ///     初始化所有没有资产账号的会员
        /// </summary>
        void InitAllUserIdsWidthOutAccount();

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ServiceResult Add(RechargeAccountInput input);

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void AddVoid(RechargeAccountInput input);
    }
}