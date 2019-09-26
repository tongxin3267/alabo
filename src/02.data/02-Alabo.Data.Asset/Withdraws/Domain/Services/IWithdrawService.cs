using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Asset.Withdraws.Domain.Entities;
using Alabo.App.Asset.Withdraws.Dtos;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.App.Asset.Withdraws.Domain.Services {

    public interface IWithdrawService : IService<Withdraw, long> {

        /// <summary>
        ///     ����
        ///     ֻ֧�����������
        ///     ����������������ת���������
        /// </summary>
        /// <param name="withDrawInput">��������</param>
        ServiceResult Add(WithDrawInput withDrawInput);

        /// <summary>
        ///     �û�ɾ�����֣���ȡ�����֣���˳ɹ������ּ�¼����ɾ��
        /// </summary>
        /// <param name="userId">��ԱId</param>
        /// <param name="id">Id��ʶ</param>
        ServiceResult Delete(long userId, long id);

        /// <summary>
        ///     ��ȡ��������
        /// </summary>
        /// <param name="userId">��ԱId</param>
        /// <param name="id">Id��ʶ</param>
        WithDrawShowOutput GetSingle(long userId, long id);

        /// <summary>
        ///     ��̨��ҳ
        /// </summary>
        /// <param name="query">��ѯ</param>
        PagedList<WithDrawOutput> GetPageList(object query);

        /// <summary>
        ///     ��̨��ҳ
        /// </summary>
        /// <param name="query">��ѯ</param>
        PagedList<ViewAdminWithDraw> GetAdminPageList(object query);

        /// <summary>
        ///     ��ȡ����֧�����ֵĻ�������
        /// </summary>
        /// <param name="userId">��ԱId</param>
        IList<KeyValue> GetWithDrawMoneys(long userId);

        /// <summary>
        ///     �������
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        ServiceResult Check(DefaultHttpContext httpContext);

        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <param name="httpContext"></param>
        //
        //ServiceResult FinalCheck(DefaultHttpContext httpContext);

        /// <summary>
        ///     ��������
        /// </summary>
        /// <param name="id">Id��ʶ</param>
        ViewAdminWithDraw GetAdminWithDraw(long id);

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult WithDrawCheck(ViewWithDrawCheck view);
    }
}