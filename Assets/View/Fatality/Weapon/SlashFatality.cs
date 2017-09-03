using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Event;
using Assets.View.Map;
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
            position.y -= FatalityParams.ZOOM_Y_OFFSET;
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

        private void AddBloodPool(object o)
        {
            if (o.GetType().Equals(typeof(SRaycastMove)))
            {
                var script = o as SRaycastMove;
                var data = new EvSplatterData();
                data.DmgPercent = 1;
                data.Target = script.GetData().Handle;
                var e = new EvSplatter(data);
                e.TryProcess();
            }
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
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = this._data.Target.Current as CharController;
                    if (tgt.Model.Type == ECharType.Humanoid)
                    {
                        VCharUtil.Instance.ProcessDeadChar(tgt);

                        var head = tgt.SubComponents[Layers.CHAR_HEAD];
                        var tgtTile = tgt.Tile;
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

                        raycast.AddCallback(hit.CallbackHandler);
                        raycast.AddCallback(this.AddBob);
                        raycast.AddCallback(this.AddBloodPool);
                        raycast.Init(raycastData);
                    }
                    else
                    {
                        VHitController.Instance.ProcessDefenderHit(hit);
                    }
                }
            }
        }
    }
}
