using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Modules.BuyPermision.Service
{
    /// <summary>
    /// 预售设置服务
    /// </summary>
    public class BuyPermisionService : ServiceBase, IBuyPermisionService
    {
        public BuyPermisionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
