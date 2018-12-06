using DDDSample.Core.SharedKernel.ExtMethods;
using DDDSample.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDSample.Core.Service
{
    public class TransientDomainEventPubSub :
        IDisposable,
        ITransientDomainEventSubscriber,
        ITransientDomainEventPublisher
    {
        private static AsyncLocal<Dictionary<Type, List<object>>> handlers = new AsyncLocal<Dictionary<Type, List<object>>>();

        #region Constructors

        public TransientDomainEventPubSub()
        {
        }

        #endregion Constructors

        #region Properties

        public Dictionary<Type, List<object>> Handlers
        {
            get => handlers.Value ?? (handlers.Value = new Dictionary<Type, List<object>>());
        }

        #endregion Properties

        #region Interface Methods

        public void Subscribe<T>(Action<T> handler)
        {
            GetHandlersOf<T>().Add(handler);
        }

        public void Subscribe<T>(Func<T, Task> handler)
        {
            GetHandlersOf<T>().Add(handler);
        }

        public async Task PublishAsync<T>(T publishedEvent)
        {
            foreach (var handler in GetHandlersOf<T>())
            {
                try
                {
                    switch (handler)
                    {
                        case Action<T> action:
                            action(publishedEvent);
                            break;

                        case Func<T, Task> action:
                            await action(publishedEvent);
                            break;

                        default:
                            break;
                    }
                }
                catch
                {
                    //Logging
                }
            }
        }

        public void Dispose()
        {
            foreach (var handlersOfT in Handlers.Values)
            {
                handlersOfT.Clear();
            }
            Handlers.Clear();
        }

        #endregion Interface Methods

        #region Private Methods

        private ICollection<object> GetHandlersOf<T>()
        {
            return Handlers.GetValueOrDefault(typeof(T)) ?? (Handlers[typeof(T)] = new List<object>());
        }

        #endregion Private Methods
    }
}