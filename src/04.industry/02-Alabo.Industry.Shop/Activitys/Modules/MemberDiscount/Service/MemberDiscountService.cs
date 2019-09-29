using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.Industry.Shop.Activitys.Modules.MemberDiscount.Service
{
    /// <summary>
    ///     预售设置服务
    /// </summary>
    public class MemberDiscountService : ServiceBase, IMemberDiscountService
    {
        public MemberDiscountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}