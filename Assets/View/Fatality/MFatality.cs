using Assets.Controller.Manager.GUI;

namespace Assets.View.Fatality
{
    public class MFatality
    {
        protected FatalityData _data;
        protected EFatality _type;

        public FatalityData Data { get { return this._data; } }
        public EFatality Type { get { return this._type; } }

        public MFatality(EFatality type, FatalityData data)
        {
            this._data = data;
            this._type = type;
        }

        public virtual void Init()
        {
            //CMapGUIController.Instance.ClearDecoratedTiles();
            //foreach (var hit in this._event.FatalityHits)
            //{
            //    if (hit.Target != null)
            //        foreach (var particle in hit.Target.Particles)
            //            GameObject.Destroy(particle);
            //}
        }

        public void ProcessFatalityView()
        {
            GUIManager.Instance.DeactivateComponentByLifetime(GameObjectTags.BANNER, 4);
            //BarkManager.Instance.ProcessFatalityBark(this._event);
        }

        protected virtual void InitMeleeFatality()
        {
            //var bob = this._event.EventController.Source.Handle.GetComponent<BobbingScript>();
            //if (bob != null)
            //    GameObject.Destroy(bob);
        }

        protected virtual void InitBulletFatality()
        {
            //var bob = this._event.EventController.Source.Handle.GetComponent<BobbingScript>();
            //if (bob != null)
            //    GameObject.Destroy(bob);
        }

        protected void Done()
        {
            //var bob = this._event.EventController.Source.Handle.AddComponent<BobbingScript>();
            //bob.Init(PER_FRAME, PER_FRAME_DIST, this._event.EventController.Source.Handle);
        }

        protected virtual void ProcessFatality()
        {

        }
    }
}
