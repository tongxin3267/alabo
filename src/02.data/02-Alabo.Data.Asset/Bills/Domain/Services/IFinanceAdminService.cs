using Alabo.App.Asset.Accounts.Dtos;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Dtos;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using System.Collections.Generic;

namespace Alabo.App.Asset.Bills.Domain.Services
{
    /// <summary>
    ///     财务后台处理函数
    /// </summary>
    public interface IFinanceAdminService : IService
    {
        PagedList<ViewUserAccounts> GetViewUserPageList(UserInput userInput);

        PagedList<ViewAdminBill> GetViewBillPageList(BillInput userInput);

        IList<Bill> GetApiBillPageList(BillApiInput billApiInput, out long count);

        ViewAdminBill GetViewBillSingle(long id);

        BillViewApiOutput GetBillOutput(ViewAdminBill view);

        PagedList<ViewAdminBill> GetBillPageList(object query);
    }
}