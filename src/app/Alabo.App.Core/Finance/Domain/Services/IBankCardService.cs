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
        /// ��ӻ�༭���п�
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult AddOrUpdateBankCard(ApiBankCardInput view);

        /// <summary>
        /// ��ȡ���п��б�
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagedList<BankCard> GetUserBankCardOutputs(ViewBankCardInput parameter);

        /// <summary>
        /// ɾ�����п�
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult RomoveBankCard(ApiBankCardInput view);

        /// <summary>
        /// ��ȡ�û����п��б�
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        IList<KeyValue> GetUserBankCardList(long loginUserId);

        /// <summary>
        /// ��ȡ���п��б�
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<BankCardOutput> GetPageList(object query);

        /// <summary>
        /// �����û�ID��ȡ���п��б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BankCardOutput> GetBankCardByUserId(long userId);

        /// <summary>
        /// ɾ�����п�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult DeleteBankCard(string id);
    }
}