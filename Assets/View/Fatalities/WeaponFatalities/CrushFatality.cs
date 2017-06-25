using Controller.Managers.Map;
using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Events.Combat;
using UnityEngine;

namespace View.Fatalities
{
    public class CrushFatality : GenericFatality
    {
        public CrushFatality(CMapGUIControllerHit parent, DisplayHitStatsEvent e)
            : base(FatalityEnum.Crush, parent, e)
        {

        }

        public override void Init()
        {
            base.Init();
            base.InitMeleeFatality();
            var zoom = this._event.Hit.Source.Handle.AddComponent<DramaticHangCallbackZoomOut>();
            var position = this._event.Hit.Source.Handle.transform.position;
            position.y -= 0.35f;
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG, this.ProcessFatality);
        }

        protected override void ProcessFatality()
        {
            var fatalityScript = this._event.Hit.Source.Handle.AddComponent<BoomerangFatalityScript>();
            var position = Vector3.Lerp(this._event.Hit.Source.Handle.transform.position, this._event.Hit.Target.transform.position, 0.55f);
            fatalityScript.CallBackThree = base.Done;
            fatalityScript.Init(this._event.Hit.Source.Handle, position, 0.75f, ProcessCrushFatality);
        }

        private void ProcessCrushFatality()
        {
            this._parent.ProcessSplatter(5, this._event.Hit.Target.CurrentTile);
            var path = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                CMapGUIControllerParams.CRUSH_FATALITY,
                CMapGUIControllerParams.PARTICLES_EXTENSION);
            var position = this._event.Hit.Target.transform.position;
            var prefab = Resources.Load(path);
            var particles = GameObject.Instantiate(prefab) as GameObject;
            particles.transform.position = position;
            particles.name = CMapGUIControllerParams.CRUSH_FATALITY + " Particles";
            var lifetime = particles.AddComponent<DestroyByLifetime>();
            lifetime.lifetime = 5f;
            this._parent.ProcessCharacterKilled(this._event.Hit.Target);
            base.ProcessFatalityView(this._event);
        }
    }
}