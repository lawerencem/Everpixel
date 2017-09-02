namespace Assets.Template.CB
{
    public delegate void Callback(object o);

    public interface ICallback
    {
        void AddCallback(Callback callback);
        void DoCallbacks();
        void SetCallback(Callback callback);
    }
}
