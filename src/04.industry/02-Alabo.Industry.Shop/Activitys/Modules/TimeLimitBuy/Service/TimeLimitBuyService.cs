using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Modules.TimeLimitBuy.Service
{
    /// <summary>
    /// 预售设置服务
    /// </summary>
    public class TimeLimitBuyService : ServiceBase, ITimeLimitBuyService
    {
        public TimeLimitBuyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
