using System.Collections.Generic;
using Alabo.App.Asset.Transfers.Domain.Entities;
using Alabo.App.Asset.Transfers.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Asset.Transfers.Domain.Services
{
    /// <summary>
    ///     转账
    /// </summary>
    public interface ITransferService : IService<Transfer, long>
    {
        /// <summary>
        ///     转账
        /// </summary>
        /// <param name="view"></param>
        ServiceResult Add(TransferAddInput view);

        /// <summary>
        ///     获取转账详情
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="userId">用户Id</param>
        TransferDetail GetSingle(long id, long userId);

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query"></param>
        PagedList<TransferOutput> GetPageList(object query);

        /// <summary>
        ///     获取所有支持转账配置
        /// </summary>
        IList<KeyValue> GetTransferConfig();

        /// <summary>
        ///     转账详情
        /// </summary>
        /// <param name="id">主键ID</param>
        ViewAdminTransfer GetAdminTransfer(long id);
    }
}