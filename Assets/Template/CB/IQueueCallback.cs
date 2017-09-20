namespace Assets.Template.CB
{
    public delegate void QueueCallback(object o);

    public interface IQueueCallback
    {
        void AddQueueCallback(int priority, QueueCallback callback);
        void DoQueueCallbacks();
    }
}
