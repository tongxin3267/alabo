using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Cache;
using Alabo.Dependency;
using Alabo.Framework.Basic.Notifications.Domain.Services;
using Alabo.Regexs;
using Alabo.Schedules.Job;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using ZKCloud.Open;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.Message.Models;
using ZKCloud.Open.Message.Services;
using MessageQueue = Alabo.Framework.Basic.Notifications.Domain.Entities.MessageQueue;

namespace Alabo.Framework.Basic.Notifications.Job
{
    public class MessageQueueJob : JobBase
    {
        private IAdminMeesageApiClient _adminMessageApiClient;
        private IMessageApiClient _messageApiClient;
        private RestClientConfiguration _restClientConfiugration;

        public override TimeSpan? GetInterval()
        {
            return TimeSpan.FromSeconds(5);
        }

        protected override async Task Execute(IJobExecutionContext context, IScope scope)
        {
            FirstWaiter(context, scope, TimeSpan.FromMinutes(5));
            var config = scope.Resolve<IConfiguration>();
            _restClientConfiugration = new RestClientConfiguration(config);
            _messageApiClient = new MessageApiClient(_restClientConfiugration.OpenApiUrl);
            _adminMessageApiClient = new AdminMessageApiClient(_restClientConfiugration.OpenApiUrl);

            var objectCache = scope.Resolve<IObjectCache>();
            objectCache.TryGet("MessageIsAllSend_Cache", out bool sendState);
            if (sendState == false)
            {
                scope.Resolve<IObjectCache>().Set("MessageIsAllSend_Cache", true);
                var messageQueueService = scope.Resolve<IMessageQueueService>();
                var unHandledIdList = messageQueueService.GetUnHandledIdList();
                if (unHandledIdList != null && unHandledIdList.Count > 0) {
                    foreach (var id in unHandledIdList) {
                        try
                        {
                            await HandleQueueAsync(messageQueueService, id);
                        }
                        catch (Exception ex)
                        {
                            scope.Resolve<IObjectCache>().Set("MessageIsAllSend_Cache", false);
                            throw new ArgumentNullException(ex.Message);
                        }
                    }
                }
            }
        }

        private async Task HandleQueueAsync(IMessageQueueService messageQueueService, long queueId)
        {
            var queue = messageQueueService.GetSingle(queueId);
            // var result = await _adminMessageApiClient.GetAccount(_serverAuthenticationManager.Token.Token);
            if (queue == null) {
                throw new MessageQueueHandleException(queueId, $"message queue with id {queueId} not found.");
            }

            RegexHelper.CheckMobile(queue.Mobile);
            MessageResult messageResult = null;
            if (queue.TemplateCode > 0) {
                messageResult = await SendTemplateAsync(queue);
            } else {
                messageResult = await SendRawAsync(queue);
            }

            if (messageResult == null) {
                messageQueueService.ErrorQueue(queueId, "message send with no result!");
            } else if (messageResult.Type == ResultType.Success) {
                messageQueueService.HandleQueueAndUpdateContent(queueId, "message send open service  success!",
                    messageResult.Message);
            } else {
                messageQueueService.ErrorQueue(queueId, $"message send {messageResult.Type}!",
                    messageResult.Message);
            }
        }

        /// <summary>
        ///     发送单条 使用异步方法发送短信
        /// </summary>
        /// <param name="queue"></param>
        private async Task<MessageResult> SendRawAsync(MessageQueue queue)
        {
            if (string.IsNullOrWhiteSpace(queue.Mobile)) {
                throw new ArgumentNullException(nameof(queue.Mobile));
            }

            if (string.IsNullOrWhiteSpace(queue.Content)) {
                throw new ArgumentNullException(nameof(queue.Content));
            }

            RegexHelper.CheckMobile(queue.Mobile);
            var result = await _messageApiClient.SendRawAsync(Token.Token,
                queue.Mobile,
                queue.Content, queue.IpAdress);
            if (result.Status != ResultStatus.Success) {
                throw new ServerApiException(result.Status, result.MessageCode, result.Message);
            }

            return ParseResult(result);
        }

        private async Task<MessageResult> SendTemplateAsync(MessageQueue queue)
        {
            if (string.IsNullOrWhiteSpace(queue.Mobile)) {
                throw new ArgumentNullException(nameof(queue.Mobile));
            }

            if (queue.TemplateCode <= 0) {
                throw new ArgumentNullException(nameof(queue.TemplateCode));
            }

            var template = GetTemplate(queue.TemplateCode);
            if (template == null) {
                throw new ArgumentNullException(nameof(template));
            }

            if (template.Status != MessageTemplateStatus.Verified) {
                throw new ArgumentNullException(nameof(template));
            }

            RegexHelper.CheckMobile(queue.Mobile);
            var parameters = JsonConvert.DeserializeObject<IDictionary<string, string>>(queue.Parameters);
            var result = await _messageApiClient.SendTemplateAsync(Token.Token,
                queue.Mobile, template.Id, queue.IpAdress, parameters);
            if (result.Status != ResultStatus.Success) {
                throw new ServerApiException(result.Status, result.MessageCode, result.Message);
            }

            return ParseResult(result);
        }

        /// <summary>
        ///     根据模板编号获取模板
        /// </summary>
        /// <param name="code"></param>
        public MessageTemplate GetTemplate(long code)
        {
            if (code <= 0) {
                throw new ArgumentNullException(nameof(code));
            }

            var result = _messageApiClient.GetTemplate(Token.Token, code);
            var entity = result.Result.Result;
            return entity;
        }

        private MessageResult ParseResult(ApiResult rawResult)
        {
            var messageResult = new MessageResult();
            if (rawResult.Status == ResultStatus.Success)
            {
                messageResult.Message = "send to open success";
                messageResult.Type = ResultType.Success;
            }
            else
            {
                messageResult.Message = "send to open faild";
                messageResult.Type = ResultType.Faild;
            }

            return messageResult;
        }
    }
}