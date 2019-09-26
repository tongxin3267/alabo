using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Orders.PcDtos;

namespace Alabo.Industry.Shop.Orders.Domain.Services {

    /// <summary>
    /// 前台订单操作统一接口
    /// </summary>
    public interface IOrderApiService : IService {

        /// <summary>
        ///     获取PC端订单列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="expressionQuery"></param>
        PagedList<ApiOrderListOutput> GetPageList(object query, ExpressionQuery<Entities.Order> expressionQuery);
    }
}