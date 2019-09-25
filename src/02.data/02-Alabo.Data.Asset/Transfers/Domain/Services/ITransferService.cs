using System.Collections.Generic;
using Alabo.App.Asset.Transfers.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Dtos.Transfer;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.ViewModels.Transfer;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     转账
    /// </summary>
    public interface ITransferService : IService<Transfer, long> {

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
        ///     用户转账列表
        /// </summary>
        PagedList<Trade> GetUserList(TransferApiInput parameter);

        /// <summary>
        ///     获取所有支持转账配置
        /// </summary>
        IList<KeyValue> GetTransferConfig();

        /// <summary>
        ///     转账详情
        /// </summary>
        /// <param name="id">主键ID</param>
        ViewAdminTransfer GetAdminTransfer(long id);

        /// <summary>
        ///     转账Edit
        /// </summary>
        /// <param name="id">主键ID</param>
        ViewHomeTransfer GetView(long id);
    }
}