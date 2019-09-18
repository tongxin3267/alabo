﻿using System;
using System.Collections.Generic;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public interface IMessageQueueRepository : IRepository<MessageQueue, long> {

        void Add(MessageQueue entity);

        void HandleQueue(long id, string message = null, string summary = null);

        void HandleQueueAndUpdateContent(long id, string message = null, string summary = null);

        void ErrorQueue(long id, string message);

        void ErrorQueue(long id, string message, string summary);

        void ErrorQueue(long id, string message, Exception exception);

        void Cancel(long id);

        MessageQueue GetSingle(long id);

        IList<long> GetUnHandledIdList();
    }
}