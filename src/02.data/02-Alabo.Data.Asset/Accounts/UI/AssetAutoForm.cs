using System;
using Alabo.Domains.Entities;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Design.AutoLists;

namespace Alabo.App.Asset.Accounts.UI
{
    /// <summary>
    ///     资产
    /// </summary>
    public class AssetAutoForm : UIBase, IAutoForm, IAutoList
    {
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            ////初始化账户
            //Resolve<IAccountService>().InitSingleUserAccount(autoModel.BasicUser.Id);
            //var montypeList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //var accountList = Resolve<IFinanceService>().GetAllUserAccount(autoModel.BasicUser.Id);
            //var rsAccList = accountList.MapTo<List<AccountApiView>>();
            //rsAccList.ForEach(x => x.MoneyName = montypeList.FirstOrDefault(y => y.Id == x.MoneyTypeId).Name);
            ////var view = new
            ////{
            ////    User = autoModel.BasicUser,
            ////    AccountList = rsAccList,
            ////    UserId = autoModel.BasicUser.Id,
            ////    ActionType = 0,
            ////    MoneyTypeId = _financeManager.MoneyTypes.FirstOrDefault().Id,
            ////};

            //return ToPageList(rsAccList);
            return null;
        }

        public Type SearchType()
        {
            throw new NotImplementedException();
        }
    }
}