using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.Enum;
using Alabo.App.Core.Common.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using ZKCloud.Open.Message.Models;
using MessageQueue = Alabo.App.Core.Common.Domain.Entities.MessageQueue;

namespace Alabo.App.Core.Common.Domain.Services {

    public class MessageQueueService : ServiceBase<MessageQueue, long>, IMessageQueueService {

        public MessageQueueService(IUnitOfWork unitOfWork, IRepository<MessageQueue, long> repository) : base(
            unitOfWork, repository) {
        }

        // private MessageManager _messageManager;
        //public MessageQueueService(MessageManager messageManager) {
        //
        //}

        public void Add(MessageQueue entity) {
            Repository<IMessageQueueRepository>().Add(entity);
            ObjectCache.Set("MessageIsAllSend_Cache", false);
        }

        public void AddRawQueue(string mobile, string content, string ipAdress) {
            var queue = new MessageQueue {
                Mobile = mobile,
                IpAdress = ipAdress,
                RequestTime = DateTime.Now,
                SendTime = DateTime.Now,
                Status = MessageStatus.Pending,
                Content = content
            };
            Add(queue);
        }

        public void AddTemplateQueue(long code, string mobile, string ipAdress,
            IDictionary<string, string> parameters = null) {
            var queue = new MessageQueue {
                Mobile = mobile,
                Parameters = parameters.ToJson(),
                IpAdress = ipAdress,
                RequestTime = DateTime.Now,
                SendTime = DateTime.Now,
                Status = MessageStatus.Pending,
                TemplateCode = code
            };
            Add(queue);
        }

        public void Cancel(long id) {
            Repository<IMessageQueueRepository>().Cancel(id);
        }

        public void ErrorQueue(long id, string message) {
            Repository<IMessageQueueRepository>().ErrorQueue(id, message);
        }

        public void ErrorQueue(long id, string message, string summary) {
            Repository<IMessageQueueRepository>().ErrorQueue(id, message, summary);
        }

        public void ErrorQueue(long id, string message, Exception exception) {
            Repository<IMessageQueueRepository>().ErrorQueue(id, message, exception);
        }

        public MessageQueue GetSingle(long id) {
            return Repository<IMessageQueueRepository>().GetSingle(id);
        }

        public IList<long> GetUnHandledIdList() {
            return Repository<IMessageQueueRepository>().GetUnHandledIdList();
        }

        public void HandleQueue(long id, string message = null, string summary = null) {
            Repository<IMessageQueueRepository>().HandleQueue(id, message, summary);
        }

        public void HandleQueueAndUpdateContent(long id, string message = null, string summary = null) {
            Repository<IMessageQueueRepository>().HandleQueueAndUpdateContent(id, message, summary);
        }

        public async Task HandleQueueAsync(long queueId) {
            var queue = GetSingle(queueId);
            if (queue == null) {
                throw new MessageQueueHandleException(queueId, $"message queue with id {queueId} not found.");
            }

            try {
                ErrorQueue(queueId, "message send with no result!");
                MessageResult messageResult = null;
                if (queue.TemplateCode > 0) {
                    IDictionary<string, string> parameters = null;
                    //  await _messageManager.SendTemplateAsync(queue.TemplateCode, queue.Mobile, parameters);
                }

                if (messageResult == null) {
                    ErrorQueue(queueId, "message send with no result!");
                } else if (messageResult.Type == ResultType.Success) {
                    HandleQueueAndUpdateContent(queueId, "message send open service  success!", messageResult.Message);
                } else {
                    ErrorQueue(queueId, $"message send {messageResult.Type}!", messageResult.Message);
                }
            } catch (Exception e) {
                ErrorQueue(queueId, e.Message, e);
            }
        }
    }
}