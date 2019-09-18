using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Modules.MemberDiscount.Service
{
    /// <summary>
    /// 预售设置服务
    /// </summary>
    public class MemberDiscountService : ServiceBase, IMemberDiscountService
    {
        public MemberDiscountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
