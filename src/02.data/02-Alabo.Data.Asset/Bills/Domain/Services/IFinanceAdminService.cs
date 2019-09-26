using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.Bill;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.ViewModels.Account;
using Alabo.App.Core.Finance.ViewModels.Bill;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     财务后台处理函数
    /// </summary>
    public interface IFinanceAdminService : IService {

        PagedList<ViewUserAccounts> GetViewUserPageList(UserInput userInput);

        PagedList<ViewAdminBill> GetViewBillPageList(BillInput userInput);

        IList<Bill> GetApiBillPageList(BillApiInput billApiInput, out long count);

        ViewAdminBill GetViewBillSingle(long id);

        BillViewApiOutput GetBillOutput(ViewAdminBill view);

        PagedList<ViewAdminBill> GetBillPageList(object query);
    }
}