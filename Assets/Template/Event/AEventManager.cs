using System.Collections.Generic;

namespace Template.Event
{
    public abstract class AEventManager<T>
    {
        protected List<T> _events;

        public abstract void RegisterEvent(T t);
        protected abstract void TryProcessEvent(T t);
    }
}
