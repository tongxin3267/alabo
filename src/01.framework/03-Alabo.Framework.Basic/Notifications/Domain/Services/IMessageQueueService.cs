using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Notifications.Domain.Entities;

namespace Alabo.Framework.Basic.Notifications.Domain.Services
{
    public interface IMessageQueueService : IService<MessageQueue, long>
    {
        void Add(MessageQueue entity);

        void HandleQueue(long id, string message = null, string summary = null);

        void HandleQueueAndUpdateContent(long id, string message = null, string summary = null);

        void ErrorQueue(long id, string message);

        void ErrorQueue(long id, string message, string summary);

        void ErrorQueue(long id, string message, Exception exception);

        void Cancel(long id);

        MessageQueue GetSingle(long id);

        IList<long> GetUnHandledIdList();

        Task HandleQueueAsync(long queueId);

        void AddRawQueue(string mobile, string content, string ipAdress);

        void AddTemplateQueue(long code, string mobile, string ipAdress, IDictionary<string, string> parameters = null);
    }
}