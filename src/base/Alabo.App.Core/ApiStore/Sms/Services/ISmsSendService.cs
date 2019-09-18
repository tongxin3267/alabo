using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.App.Core.ApiStore.Sms.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.ApiStore.Sms.Services {

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