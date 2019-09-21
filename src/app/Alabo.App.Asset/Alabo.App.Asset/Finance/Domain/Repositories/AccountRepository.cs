using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Extensions;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    /// <summary>
    ///     Class AccountRepository.
    /// </summary>
    /// <seealso cref="Alabo.App.Core.Finance.Domain.Repositories.IAccountRepository" />
    internal class AccountRepository : RepositoryEfCore<Account, long>, IAccountRepository {

        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     Gets the account amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <returns>System.Decimal.</returns>
        public decimal GetAccountAmount(long userId, Guid moneyTypeId) {
            var sql = $"select Amount from Finance_Account where UserId={userId} and MoneyTypeId='{moneyTypeId}'";
            var result = RepositoryContext.ExecuteScalar(sql);
            if (result == null || result == DBNull.Value) {
                return -1;
            }

            return Convert.ToDecimal(result);
        }

        /// <summary>
        ///     Changes the account amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <param name="changedAmount">The changed amount.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ChangeAccountAmount(long userId, Guid moneyTypeId, decimal changedAmount) {
            var historyUpdateSql = string.Empty;
            if (changedAmount > 0) {
                historyUpdateSql = $" ,HistoryAmount=HistoryAmount+{changedAmount}";
            }

            var sql =
                $"update Finance_Account set Amount=Amount+{changedAmount} {historyUpdateSql} where UserId={userId} and MoneyTypeId='{moneyTypeId}'";
            return RepositoryContext.ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moneyTypeid"></param>
        /// <param name="changedAmount"></param>
        public bool ChangeFreezeAmount(long userId, Guid moneyTypeid, decimal changedAmount) {
            var sql =
                $"update Finance_Account set FreezeAmount=FreezeAmount+{changedAmount} where UserId={userId} and MoneyTypeId='{moneyTypeid}'";
            return RepositoryContext.ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        ///     Updates the account amount.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool UpdateAccountAmount(long userId, Guid moneyTypeId, decimal amount) {
            var sql = $@"update Finance_Account set Amount={amount},
                            HistoryAmount=(case when {amount}>=Amount then HistoryAmount+{
                    amount
                }-Amount else HistoryAmount end)
                            where UserId={userId} and MoneyTypeId='{moneyTypeId}'";
            return RepositoryContext.ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <returns>Account.</returns>
        public Account GetAccount(long userId, Guid moneyTypeId) {
            var sql = $"select * from Finance_Account where MoneyTypeId='{moneyTypeId}' and UserId={userId}";
            Account result = null;
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                if (dr.Read()) {
                    result = ReadAccount(dr);
                }
            }

            return result;
        }

        /// <summary>
        ///     支付时使用
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeId">The money type identifier.</param>
        /// <returns>Account.</returns>
        public Account GetAccount(DbTransaction transaction, long userId, Guid moneyTypeId) {
            var sql = $"select * from Finance_Account where MoneyTypeId='{moneyTypeId}' and UserId={userId}";
            Account result = null;
            using (var dr = RepositoryContext.ExecuteDataReader(transaction, sql)) {
                if (dr.Read()) {
                    result = ReadAccount(dr);
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the user all account.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moneyTypeGuid">The money type unique identifier.</param>
        /// <returns>IList&lt;Account&gt;.</returns>
        public IList<Account> GetUserAllAccount(long userId, string moneyTypeGuid) {
            IList<Account> result = new List<Account>();
            if (userId <= 0) {
                return result;
            }

            if (moneyTypeGuid.IsNullOrEmpty()) {
                return result;
            }

            var sql = $"select * from Finance_Account where   UserId={userId} and MoneyTypeId In ('{moneyTypeGuid}')";
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadAccount(dr));
                }
            }

            return result;
        }

        /// <summary>
        ///     获取多个用户的账户
        /// </summary>
        /// <param name="userIds">The user ids.</param>
        /// <returns>IList&lt;Account&gt;.</returns>
        public IList<Account> GetAccountByUserIds(IList<long> userIds) {
            var sql = $"select * from Finance_Account where   UserId in  ({userIds.ToSqlString()})";
            IList<Account> result = new List<Account>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadAccount(dr));
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <returns>Account.</returns>
        public Account GetAccount(long id) {
            var sql = $"select * from Finance_Account where Id='{id}'";
            Account result = null;
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                if (dr.Read()) {
                    result = ReadAccount(dr);
                }
            }

            return result;
        }

        public IList<long> GetAllUserIdsWidthOutAccount() {
            var sql =
                "select Id from User_User where Id not in (select  DISTINCT UserId from Finance_Account ) order by id ";
            IList<long> result = new List<long>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(dr["Id"].ConvertToLong(0));
                }
            }

            return result;
        }

        /// <summary>
        ///     Reads the account.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>Account.</returns>
        private Account ReadAccount(IDataReader dr) {
            var result = new Account {
                Id = dr.Read<long>("Id"),
                UserId = dr.Read<long>("UserId"),
                Amount = dr.Read<decimal>("Amount"),
                FreezeAmount = dr.Read<decimal>("FreezeAmount"),
                HistoryAmount = dr.Read<decimal>("HistoryAmount"),
                MoneyTypeId = dr.Read<Guid>("MoneyTypeId")
            };
            return result;
        }
    }
}