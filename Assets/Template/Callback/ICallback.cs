namespace Template.Callback
{
    public delegate void Callback();

    public interface ICallback
    {
        void AddCallback(Callback callback);
        void Callback();
        void SetCallback(Callback callback);
    }
}
