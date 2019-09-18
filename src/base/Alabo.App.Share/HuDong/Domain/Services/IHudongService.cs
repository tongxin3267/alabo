using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.HuDong.Domain.Services {

    /// <summary>
    /// �����
    /// </summary>
    public interface IHudongService : IService<Hudong, ObjectId> {

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <returns></returns>
        List<EnumKeyValue> GetAwardType();

        /// <summary>
        /// ��ȡ���ͼ
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, Hudong> GetView(HuDongViewInput viewInput);

        /// <summary>
        /// ת�̻�ȡ�齱��Ϣ
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, Hudong> GetAwards(HuDongViewInput viewInput);

        /// <summary>
        /// ��༭�����
        /// </summary>
        /// <param name="huDong"></param>
        /// <returns></returns>
        ServiceResult Edit(Hudong huDong);

        /// <summary>
        /// ���ݻID�����ͻ�ȡ��¼
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Hudong GetSingle(ObjectId id, string key);

        /// <summary>
        /// �齱����
        /// </summary>
        /// <param name="drawInput"></param>
        /// <returns></returns>
        ServiceResult Draw(DrawInput drawInput);
    }
}