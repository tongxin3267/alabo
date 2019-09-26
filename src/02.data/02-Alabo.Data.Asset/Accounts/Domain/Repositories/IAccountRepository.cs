using System;
using System.Collections.Generic;
using System.Data.Common;
using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Accounts.Domain.Repositories
{
    /// <summary>
    ///     Interface IAccountRepository
    /// </summary>
    public interface IAccountRepository : IRepository<Account, long>
    {
        /// <summary>
        ///     获取没有资产账号的所有会员Id
        /// </summary>
        IList<long> GetAllUserIdsWidthOutAccount();

        /// <summary>
        ///     Gets the account amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <returns>System.Decimal.</returns>
        decimal GetAccountAmount(long userId, Guid moneyTypeId);

        /// <summary>
        ///     Changes the account amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <param name="changedAmount">The changed amount.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ChangeAccountAmount(long userId, Guid moneyTypeId, decimal changedAmount);

        /// <summary>
        ///     Changes the freeze amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeid">The money typeid.</param>
        /// <param name="changedAmount">The changed amount.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ChangeFreezeAmount(long userId, Guid moneyTypeid, decimal changedAmount);

        /// <summary>
        ///     Updates the account amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool UpdateAccountAmount(long userId, Guid moneyTypeId, decimal amount);

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <returns>Account.</returns>
        Account GetAccount(long userId, Guid moneyTypeId);

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <returns>Account.</returns>
        Account GetAccount(long id);

        /// <summary>
        ///     Gets the user all account.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeGuid">The money type unique identifier.</param>
        /// <returns>IList&lt;Account&gt;.</returns>
        IList<Account> GetUserAllAccount(long userId, string moneyTypeGuid);

        /// <summary>
        ///     获取多个用户的账户
        /// </summary>
        /// <param name="userIds">The user ids.</param>
        /// <returns>IList&lt;Account&gt;.</returns>
        IList<Account> GetAccountByUserIds(IList<long> userIds);

        /// <summary>
        ///     支付时使用
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <returns>Account.</returns>
        Account GetAccount(DbTransaction transaction, long userId, Guid moneyTypeId);
    }
}