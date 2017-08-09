namespace Template.Event
{
    public abstract class AEvent<T>
    {
        protected T _type;
        public T Type { get { return this._type; } }

        public AEvent(T t)
        {
            this._type = t;
        }
    }
}
