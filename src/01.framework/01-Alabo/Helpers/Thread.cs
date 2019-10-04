using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alabo.Helpers {

    /// <summary>
    ///     线程操作
    ///     此类作用：解决在同步方法中使用异步方法产生的线程死锁
    ///     死锁的主要原因是因为代码中线程对上下文的争夺
    /// </summary>
    public static class Thread {

        /// <summary>
        ///     执行多个操作，多个操作将同时执行
        /// </summary>
        /// <param name="actions">操作集合</param>
        public static void WaitAll(params Action[] actions) {
            if (actions == null) {
                return;
            }

            var tasks = new List<Task>();
            foreach (var action in actions) {
                tasks.Add(Task.Factory.StartNew(action, TaskCreationOptions.None));
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        ///     同步执行一个void类型的返回值操作
        /// </summary>
        /// <param name="task">Task method to execute</param>
        public static void RunSync(Func<Task> task) {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(async _ => {
                try {
                    await task();
                } catch (Exception e) {
                    synch.InnerException = e;
                    throw;
                } finally {
                    synch.EndMessageLoop();
                }
            }, null);
            synch.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
        }

        /// <summary>
        ///     同步执行一个Task《T》的异步任务
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="task">Task《T》 method to execute</param>
        public static T RunSync<T>(Func<Task<T>> task) {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            T ret = default;
            synch.Post(async _ => {
                try {
                    ret = await task();
                } catch (Exception e) {
                    synch.InnerException = e;
                    throw;
                } finally {
                    synch.EndMessageLoop();
                }
            }, null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            return ret;
        }
    }

    /// <summary>
    ///     带异常的异步上下文
    /// </summary>
    public class ExclusiveSynchronizationContext : SynchronizationContext {

        private readonly Queue<Tuple<SendOrPostCallback, object>> _items =
            new Queue<Tuple<SendOrPostCallback, object>>();

        private readonly AutoResetEvent _workItemsWaiting = new AutoResetEvent(false);
        private bool _done;
        public Exception InnerException { get; set; }

        public override void Send(SendOrPostCallback d, object state) {
            throw new NotSupportedException("We cannot send to our same thread");
        }

        public override void Post(SendOrPostCallback d, object state) {
            lock (_items) {
                _items.Enqueue(Tuple.Create(d, state));
            }

            _workItemsWaiting.Set();
        }

        public void EndMessageLoop() {
            Post(_ => _done = true, null);
        }

        public void BeginMessageLoop() {
            while (!_done) {
                Tuple<SendOrPostCallback, object> task = null;
                lock (_items) {
                    if (_items.Count > 0) {
                        task = _items.Dequeue();
                    }
                }

                if (task != null) {
                    task.Item1(task.Item2);
                    if (InnerException != null) // the method threw an exeption
{
                        throw new AggregateException("AsyncExtend.Run method threw an exception.", InnerException);
                    }
                } else {
                    _workItemsWaiting.WaitOne();
                }
            }
        }

        public override SynchronizationContext CreateCopy() {
            return this;
        }
    }
}