using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Particle;
using UnityEngine;

namespace Assets.View.Fatality.Weapon
{
    public class SlashFatality : MFatality
    {
        public SlashFatality(FatalityData data) : base(EFatality.Slash, data) { }

        public override void Init()
        {
            base.Init();
            var position = this._data.Source.Handle.transform.position;
            position.y -= .035f;
            var zoom = this._data.Source.Handle.AddComponent<SHangCallbackZoomOut>();
            zoom.AddCallback(this.ProcessJolt);
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG);
        }

        private void AttachBlood(GameObject head, GameObject body)
        {
            this.AttachBloodBody(body);
            this.AttachBloodHead(head);
        }

        private void AttachBloodBody(GameObject body)
        {
            var path = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                CombatGUIParams.SLASH_FATALITY,
                CombatGUIParams.PARTICLES_EXTENSION);
            var blood = ParticleController.Instance.CreateParticle(path);
            var script = blood.AddComponent<SDestroyByLifetime>();
            script.lifetime = 4;
            ParticleController.Instance.RotateParticle(blood, 180f);
            ParticleController.Instance.AttachParticle(body, blood);
        }

        private void AttachBloodHead(GameObject head)
        {
            var path = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                CombatGUIParams.SLASH_FATALITY,
                CombatGUIParams.PARTICLES_EXTENSION);
            var blood = ParticleController.Instance.CreateParticle(path);
            var script = blood.AddComponent<SDestroyByLifetime>();
            script.lifetime = 4;
            ParticleController.Instance.AttachParticle(head, blood);
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.SLASH_LERP);
            var boomerang = this._data.Source.Handle.AddComponent<SBoomerang>();
            boomerang.AddCallback(this.ProcessHead);
            boomerang.Init(this._data.Source.Handle, pos, FatalityParams.SLASH_ATTACK_SPEED);
        }

        private void ProcessHead(object o)
        {
            if (this._data.Target.Current != null && 
                this._data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                var tgt = this._data.Target.Current as CharController;
                if (tgt.Model.Type == ECharType.Humanoid)
                {
                    VCharUtil.Instance.ProcessDeadChar(tgt);

                    var head = tgt.SubComponents[Layers.CHAR_HEAD];
                    var tgtTile = tgt.Tile.GetRandomNearbyTile(1);
                    var tgtPos = RandomPositionOffset.RandomOffset(
                        tgtTile.Handle.transform.position,
                        -FatalityParams.SLASH_HEAD_OFFSET,
                        FatalityParams.SLASH_HEAD_OFFSET);
                    head.transform.SetParent(tgtTile.Handle.transform);

                    var spinData = new SRotationData();
                    spinData.Speed = (float)(FatalityParams.SLASH_ROTATION_SPEED * RNG.Instance.NextDouble()); 
                    spinData.Target = head;
                    var spinRight = RNG.Instance.RandomNegOrPos();
                    if (spinRight > 0)
                        spinData.SpinRight = true;
                    var spin = head.AddComponent<SRotation>();
                    spin.Init(spinData);

                    var raycastData = new SRaycastMoveData();
                    raycastData.Epsilon = FatalityParams.DEFAULT_EPSILON;
                    raycastData.Handle = head;
                    raycastData.Speed = FatalityParams.SLASH_RAYCAST_SPEED;
                    raycastData.Target = tgtPos;
                    var raycast = head.AddComponent<SRaycastMove>();
                    raycast.AddCallback(spin.Done);
                    raycast.AddCallback(this.CallbackHandler);
                    
                    this.AttachBlood(head, tgt.Handle);

                    raycast.Init(raycastData);
                }
            }
            else
            {
                // TODO:
            }
        }
    }
}

//        public override void Init()
//        {
//            base.Init();
//            base.InitMeleeFatality();
//            var zoom = this._event.EventController.Source.Handle.AddComponent<DramaticHangCallbackZoomOut>();
//            var position = this._event.EventController.Source.Handle.transform.position;
//            position.y -= 0.35f;
//            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG, this.ProcessFatality);
//        }

//        protected override void ProcessFatality()
//        {
//            var fatalityScript = this._event.EventController.Source.Handle.AddComponent<BoomerangFatalityScript>();
//            var position = Vector3.Lerp(this._event.EventController.Source.Handle.transform.position, this._event.EventController.TargetCharController.transform.position, 0.55f);
//            fatalityScript.Init(this._event.EventController.Source.Handle, position, 0.75f, ProcessHeadFatality);
//        }

//        private void ProcessHeadFatality()
//        {
//            foreach (var hit in this._event.FatalityHits)
//            {
//                if (hit.Target.Model.Type == ECharacterType.Humanoid)
//                {
//                    var head = hit.Target.SpriteHandlerDict[ViewParams.CHAR_HEAD];
//                    var tgtTile = hit.TargetTile.Model.GetRandomNearbyTile(2);
//                    head.transform.SetParent(tgtTile.Parent.transform);

//                    var spin = head.AddComponent<HeadRotationScript>();
//                    bool spinRight = true;
//                    var roll = RNG.Instance.Next(2);
//                    if (roll == 0)
//                        spinRight = false;
//                    var percent = RNG.Instance.NextDouble();
//                    spin.Init(head, (float)(5f * percent), spinRight, hit.Done);
//                    spin.InitHeadRotation(tgtTile.Parent, this._parent);
//                    var translate = head.AddComponent<RaycastMove>();
//                    translate.Init(head, tgtTile.Parent.Model.Center, 1f, spin.Done);
//                    this.HandleParticles();
//                }
//                else
//                {
//                    // TODO:
//                    hit.Source.Handle.transform.position = hit.Source.CurrentTile.Model.Center;
//                }
//                this._parent.ProcessCharacterKilled(hit.Target);
//            }
//            this._event.AttackFXDone();
//            base.ProcessFatalityView();
//        }

//        private void HandleParticles()
//        {
//            foreach (var hit in this._event.FatalityHits)
//            {
//                var path = StringUtil.PathBuilder(
//                CMapGUIControllerParams.EFFECTS_PATH,
//                CMapGUIControllerParams.SLASH_FATALITY,
//                CMapGUIControllerParams.PARTICLES_EXTENSION);
//                var position = hit.Target.transform.position;
//                var prefab = Resources.Load(path);
//                var headBlood = GameObject.Instantiate(prefab) as GameObject;
//                var bodyBlood = GameObject.Instantiate(prefab) as GameObject;

//                bodyBlood.transform.position = position;
//                bodyBlood.transform.SetParent(hit.Target.Handle.transform);

//                bodyBlood.name = CMapGUIControllerParams.SLASH_FATALITY + " Particles";
//                headBlood.name = CMapGUIControllerParams.SLASH_FATALITY + " Particles";

//                var bodyScript = bodyBlood.AddComponent<DestroyByLifetime>();
//                bodyScript.lifetime = 5f;
//                var headScript = headBlood.AddComponent<DestroyByLifetime>();
//                headScript.lifetime = 5f;

//                var empty = new GameObject();
//                headBlood.transform.SetParent(empty.transform);

//                headBlood.transform.SetParent(hit.Target.SpriteHandlerDict[ViewParams.CHAR_HEAD].transform);
//                var emptyScript = empty.AddComponent<DestroyByLifetime>();
//                emptyScript.lifetime = 5f;
//                headBlood.transform.position = hit.Target.SpriteHandlerDict[ViewParams.CHAR_HEAD].transform.position;
//            }
//        }
//    }
//}
