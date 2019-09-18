using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Modules.PreSells.Service
{
    /// <summary>
    /// 预售设置服务
    /// </summary>
    public class PreSellsService : ServiceBase, IPreSellsService
    {
        public PreSellsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
