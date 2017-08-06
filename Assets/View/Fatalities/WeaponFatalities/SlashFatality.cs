using Assets.View;
using Controller.Managers.Map;
using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;
using Model.Events.Combat;
using UnityEngine;
using View.Biomes;
using View.Characters;
using View.Scripts;

namespace View.Fatalities
{
    public class SlashFatality : GenericFatality
    {
        public SlashFatality(CMapGUIControllerHit parent, DisplayActionEvent e)
            : base(FatalityEnum.Slash, parent, e)
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
            var position = Vector3.Lerp(this._event.EventController.Source.Handle.transform.position, this._event.EventController.TargetCharController.transform.position, 0.55f);
            fatalityScript.Init(this._event.EventController.Source.Handle, position, 0.75f, ProcessHeadFatality);
        }

        private void ProcessHeadFatality()
        {
            foreach(var hit in this._event.FatalityHits)
            {
                if (hit.Target.Model.Type == ECharacterType.Humanoid)
                {
                    var head = hit.Target.SpriteHandlerDict[ViewParams.CHAR_HEAD];
                    var tgtTile = hit.TargetTile.Model.GetRandomNearbyTile(2);
                    head.transform.SetParent(tgtTile.Parent.transform);

                    var spin = head.AddComponent<HeadRotationScript>();
                    bool spinRight = true;
                    var roll = RNG.Instance.Next(2);
                    if (roll == 0)
                        spinRight = false;
                    var percent = RNG.Instance.NextDouble();
                    spin.Init(head, (float)(5f * percent), spinRight, hit.Done);
                    spin.InitHeadRotation(tgtTile.Parent, this._parent);
                    var translate = head.AddComponent<RaycastMove>();
                    translate.Init(head, tgtTile.Parent.Model.Center, 1f, spin.Done);
                    this.HandleParticles();
                }
                else
                {
                    // TODO:
                    hit.Source.Handle.transform.position = hit.Source.CurrentTile.Model.Center;
                }
                this._parent.ProcessCharacterKilled(hit.Target);
            }
            this._event.AttackFXDone();
            base.ProcessFatalityView();
        }

        private void HandleParticles()
        {
            foreach (var hit in this._event.FatalityHits)
            {
                var path = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                CMapGUIControllerParams.SLASH_FATALITY,
                CMapGUIControllerParams.PARTICLES_EXTENSION);
                var position = hit.Target.transform.position;
                var prefab = Resources.Load(path);
                var headBlood = GameObject.Instantiate(prefab) as GameObject;
                var bodyBlood = GameObject.Instantiate(prefab) as GameObject;

                bodyBlood.transform.position = position;
                bodyBlood.transform.SetParent(hit.Target.Handle.transform);

                bodyBlood.name = CMapGUIControllerParams.SLASH_FATALITY + " Particles";
                headBlood.name = CMapGUIControllerParams.SLASH_FATALITY + " Particles";

                var bodyScript = bodyBlood.AddComponent<DestroyByLifetime>();
                bodyScript.lifetime = 5f;
                var headScript = headBlood.AddComponent<DestroyByLifetime>();
                headScript.lifetime = 5f;

                var empty = new GameObject();
                headBlood.transform.SetParent(empty.transform);

                headBlood.transform.SetParent(hit.Target.SpriteHandlerDict[ViewParams.CHAR_HEAD].transform);
                var emptyScript = empty.AddComponent<DestroyByLifetime>();
                emptyScript.lifetime = 5f;
                headBlood.transform.position = hit.Target.SpriteHandlerDict[ViewParams.CHAR_HEAD].transform.position;
            }
        }
    }
}