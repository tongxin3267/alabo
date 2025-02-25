﻿using Alabo.Events.Handlers;
using Alabo.Helpers;
using System.Collections.Generic;

namespace Alabo.Events.Default {

    /// <summary>
    ///     事件处理器服务
    /// </summary>
    public class EventHandlerManager : IEventHandlerManager {

        /// <summary>
        ///     获取事件处理器列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        public List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent {
            return Ioc.ResolveAll<IEventHandler<TEvent>>();
        }
    }
}