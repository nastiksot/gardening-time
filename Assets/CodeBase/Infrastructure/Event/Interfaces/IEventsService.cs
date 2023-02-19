using System;
using StansAssets.Foundation.Patterns;

namespace CodeBase.Infrastructure.Event
{
    public interface IEventsService
    {
        public void Subscribe<T>(Action<T> listener) where T : IEvent;

        public void Unsubscribe<T>(Action<T> listener) where T : IEvent;

        public void Post<T>(T @event) where T : IEvent;
    }
}
