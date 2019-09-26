using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.Industry.Shop.Activitys.Modules.TimeLimitBuy.Service
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
