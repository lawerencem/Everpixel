using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Event;
using Assets.View.Particle;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class SlashFatality : MFatality
    {
        public SlashFatality(FatalityData data) : base(EFatality.Slash, data) { }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessJolt);
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
            script.Init(blood, 4f);
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
            script.Init(blood, 4f);
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
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            attack.AddCallback(this.ProcessHead);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessHead(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = this._data.Target.Current as CharController;
                    if (tgt.Proxy.Type == ECharType.Humanoid)
                    {
                        VCharUtil.Instance.ProcessDeadChar(tgt);

                        var head = tgt.SubComponents[Layers.CHAR_HEAD];
                        var tgtTile = tgt.Tile;
                        var tgtPos = RandomPositionOffset.RandomOffset(
                            tgtTile.Handle.transform.position, 
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
                        this.AttachBlood(head, tgt.Handle);
                        
                        raycast.AddCallback(this.AddBloodPool);
                        raycast.AddCallback(spin.Done);

                        var callbackDelay = head.AddComponent<SDelayCallback>();
                        callbackDelay.AddCallback(this.CallbackHandler);
                        callbackDelay.AddCallback(hit.CallbackHandler);
                        callbackDelay.Init(FatalityParams.DEFAULT_DUR);

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
