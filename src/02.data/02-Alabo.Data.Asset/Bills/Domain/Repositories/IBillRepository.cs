using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Dtos;
using Alabo.Domains.Repositories;
using System.Collections.Generic;
using System.Data.Common;

namespace Alabo.App.Asset.Bills.Domain.Repositories
{
    public interface IBillRepository : IRepository<Bill, long>
    {
        void AddSingleNative(Bill bill);

        IList<Bill> GetBillList(BillInput userInput, out long count);

        IList<Bill> GetApiBillList(BillApiInput billApiInput, out long count);

        /// <summary>
        ///     支付冻结金额
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="bill"></param>
        /// <param name="account"></param>
        void Pay(DbTransaction transaction, Bill bill, Account account);
    }
}