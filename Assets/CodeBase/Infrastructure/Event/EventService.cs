using System;
using StansAssets.Foundation.Patterns;

namespace CodeBase.Infrastructure.Event
{
    public class EventsService : IEventsService
    {
        readonly EventBus m_EventBusInstance;

        public EventsService()
        {
            m_EventBusInstance = new EventBus();
        }

        public void Subscribe<T>(Action<T> listener) where T : IEvent
        {
            m_EventBusInstance.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEvent
        {
            m_EventBusInstance.Unsubscribe(listener);
        }

        public void Post<T>(T @event) where T : IEvent
        {
            m_EventBusInstance.Post(@event);
        }
    }
}
