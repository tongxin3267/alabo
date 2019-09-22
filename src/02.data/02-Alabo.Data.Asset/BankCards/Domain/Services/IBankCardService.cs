using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.BankCard;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    public interface IBankCardService : IService<BankCard, ObjectId> {

        /// <summary>
        /// 添加或编辑银行卡
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult AddOrUpdateBankCard(ApiBankCardInput view);

        /// <summary>
        /// 获取银行卡列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagedList<BankCard> GetUserBankCardOutputs(ViewBankCardInput parameter);

        /// <summary>
        /// 删除银行卡
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult RomoveBankCard(ApiBankCardInput view);

        /// <summary>
        /// 获取用户银行卡列表
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        IList<KeyValue> GetUserBankCardList(long loginUserId);

        /// <summary>
        /// 获取银行卡列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<BankCardOutput> GetPageList(object query);

        /// <summary>
        /// 根据用户ID获取银行卡列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BankCardOutput> GetBankCardByUserId(long userId);

        /// <summary>
        /// 删除银行卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult DeleteBankCard(string id);
    }
}