using System.Collections.Generic;
using System.Data.Common;
using Alabo.App.Core.Finance.Domain.Dtos.Bill;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    public interface IBillRepository : IRepository<Bill, long> {

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