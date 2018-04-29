namespace Assets.Template.Event
{
    public interface IParentEvent
    {
        void AddChildAction(IChildEvent child);
    }
}
