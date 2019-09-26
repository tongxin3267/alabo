using System.Collections.Generic;
using _01_Alabo.Cloud.Core.SendSms.Domain.Entities;
using _01_Alabo.Cloud.Core.SendSms.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Services {

    public interface ISmsSendService : IService<SmsSend, ObjectId> {
        /// <summary>
        /// д��mongo
        /// </summary>
        /// <param name="input">�ѵ绰�����״̬</param>

        ServiceResult Add(IList<SmsSend> input);

        /// <summary>
        /// ��mongo��ȡ
        /// </summary>

        IList<SmsSend> GetAll(SendState input);
    }
}