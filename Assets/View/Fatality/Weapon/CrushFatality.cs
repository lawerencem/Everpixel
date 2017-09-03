using Assets.Template.Script;

namespace Assets.View.Fatality.Weapon
{
    public class CrushFatality : MFatality
    {
        public CrushFatality(FatalityData data) : base(EFatality.Crush, data) {}

        public override void Init()
        {
            base.Init();
            var position = this._data.Source.Handle.transform.position;
            position.y -= FatalityParams.ZOOM_Y_OFFSET;
            var zoom = this._data.Source.Handle.AddComponent<SHangCallbackZoomOut>();
            zoom.AddCallback(this.ProcessJolt);
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG);
        }

        private void ProcessJolt(object o)
        {

        }

        //protected override void ProcessFatality()
        //{
        //    var fatalityScript = this._event.EventController.Source.Handle.AddComponent<BoomerangFatalityScript>();
        //    var position = Vector3.Lerp(this._event.EventController.Source.Handle.transform.position, this._event.EventController.Target.transform.position, 0.55f);
        //    fatalityScript.CallBackThree = base.Done;
        //    fatalityScript.Init(this._event.EventController.Source.Handle, position, 0.75f, ProcessCrushFatality);
        //}

        //private void ProcessCrushFatality()
        //{
        //    foreach (var hit in this._event.FatalityHits)
        //    {
        //        this._parent.ProcessSplatter(5, this._event.EventController.TargetCharController.CurrentTile);
        //        var path = StringUtil.PathBuilder(
        //            CMapGUIControllerParams.EFFECTS_PATH,
        //            CMapGUIControllerParams.CRUSH_FATALITY,
        //            CMapGUIControllerParams.PARTICLES_EXTENSION);
        //        var position = this._event.EventController.TargetCharController.transform.position;
        //        var prefab = Resources.Load(path);
        //        var particles = GameObject.Instantiate(prefab) as GameObject;
        //        particles.transform.position = position;
        //        particles.name = CMapGUIControllerParams.CRUSH_FATALITY + " Particles";
        //        var lifetime = particles.AddComponent<DestroyByLifetime>();
        //        lifetime.lifetime = 5f;
        //        this._parent.ProcessCharacterKilled(hit.Target);
        //        hit.Done();
        //    }
        //    this._event.AttackFXDone();
        //    base.ProcessFatalityView();
        //}
    }
}
