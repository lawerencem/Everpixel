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
        public CrushFatality(CMapGUIControllerHit parent, DisplayActionEvent e)
            : base(FatalityEnum.Crush, parent, e)
        {

        }

        public override void Init()
        {
            base.Init();
            base.InitMeleeFatality();
            var zoom = this._event.EventController.Source.Handle.AddComponent<DramaticHangCallbackZoomOut>();
            var position = this._event.EventController.Source.Handle.transform.position;
            position.y -= 0.35f;
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG, this.ProcessFatality);
        }

        protected override void ProcessFatality()
        {
            var fatalityScript = this._event.EventController.Source.Handle.AddComponent<BoomerangFatalityScript>();
            var position = Vector3.Lerp(this._event.EventController.Source.Handle.transform.position, this._event.EventController.Target.transform.position, 0.55f);
            fatalityScript.CallBackThree = base.Done;
            fatalityScript.Init(this._event.EventController.Source.Handle, position, 0.75f, ProcessCrushFatality);
        }

        private void ProcessCrushFatality()
        {
            foreach(var hit in this._event.FatalityHits)
            {
                this._parent.ProcessSplatter(5, this._event.EventController.TargetCharController.CurrentTile);
                var path = StringUtil.PathBuilder(
                    CMapGUIControllerParams.EFFECTS_PATH,
                    CMapGUIControllerParams.CRUSH_FATALITY,
                    CMapGUIControllerParams.PARTICLES_EXTENSION);
                var position = this._event.EventController.TargetCharController.transform.position;
                var prefab = Resources.Load(path);
                var particles = GameObject.Instantiate(prefab) as GameObject;
                particles.transform.position = position;
                particles.name = CMapGUIControllerParams.CRUSH_FATALITY + " Particles";
                var lifetime = particles.AddComponent<DestroyByLifetime>();
                lifetime.lifetime = 5f;
                this._parent.ProcessCharacterKilled(hit.Target);
                hit.Done();
            }
            base.ProcessFatalityView();
        }
    }
}