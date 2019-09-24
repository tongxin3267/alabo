using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Alabo.App.Open.Tasks;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Share.Share.Domain.Dto;
using Alabo.App.Share.Share.ViewModels;
using Alabo.App.Share.Tasks;
using Alabo.App.Share.Tasks.Base;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Share.Domain.Services {

    using Reward = Entities.Reward;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Reward" />
    public interface IRewardService : IService<Reward, long> {
        /// <summary>
        /// Gets the view reward page list.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="context">上下文</param>

        PagedList<ViewAdminReward> GetViewRewardPageList(RewardInput userInput, HttpContext context);

        /// <summary>
        /// 暂时解决导出表格用
        /// </summary>
        /// <returns></returns>
        PagedList<ViewAdminReward> GetRewardList(object query);

        /// <summary>
        /// 获取分润详情
        /// </summary>
        /// <param name="Id">Id标识</param>

        ViewAdminReward GetRewardView(long Id);

        /// <summary>
        /// 添加或更新分红记录
        /// </summary>
        /// <param name="entity">The entity.</param>

        void AddOrUpdate(Reward entity);

        /// <summary>
        /// 批量添加或更新分红记录
        /// </summary>
        /// <param name="soucre">The soucre.</param>

        void AddOrUpdate(IEnumerable<Reward> soucre);

        /// <summary>
        /// 获取一条分红记录
        /// </summary>
        /// <param name="id">c分红记录ID</param>

        Reward GetSingle(long? id);

        /// <summary>
        /// 获取后台商品列表，moduleID为空时，获取所有的列表
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="isEnable">The is enable.</param>

        IList<ViewShareModuleList> GetViewShareModuleList(HttpContext context, Guid moduleId, string name, int? isEnable);

        /// <summary>
        /// 获取视图模块
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="Id">Id标识</param>
        /// <param name="copy">The copy.</param>

        ViewModuleConfig GetViewModuleConfig(HttpContext context, Guid moduleId, long Id, int copy);

        /// <summary>
        /// Tries the 获取 模块 特性.
        /// </summary>
        /// <param name="id">Id标识</param>

        ShareModulesAttribute TryGetModuleAttribute(string id);

        /// <summary>
        /// Gets the user page.
        /// </summary>
        /// <param name="query">查询</param>

        PagedList<ViewHomeReward> GetUserPage(object query);
    }
}