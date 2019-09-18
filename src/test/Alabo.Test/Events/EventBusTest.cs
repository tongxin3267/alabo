using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using ZKCloud.Events.Default;
using ZKCloud.Events.Handlers;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Events
{
    /// <summary>
    ///     事件总线测试
    /// </summary>
    public class EventBusTest
    {
        /// <summary>
        ///     测试初始化
        /// </summary>
        public EventBusTest()
        {
            _handler = Substitute.For<IEventHandler<EventSample>>();
        }

        /// <summary>
        ///     事件处理器2
        /// </summary>
        private readonly IEventHandler<EventSample> _handler;

        /// <summary>
        ///     测试发布事件
        /// </summary>
        [Fact]
        public async Task TestPublish()
        {
            var manager = new EventHandlerManagerSample(_handler);
            var eventBus = new EventBus(manager);
            var @event = new EventSample {Name = "a"};
            await eventBus.PublishAsync(@event);
            await _handler.Received(1).HandleAsync(@event);
        }
    }
}