﻿using Assets.Controller.Character;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Character.Table;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class BiteFatality : MFatality
    {
        private Sprite _oldSprite;
        private Vector3 _origin;
        private SpriteRenderer _renderer;
        private Sprite[] _sprites;

        public BiteFatality(FatalityData data) : base(EFatality.Bite, data)
        {
            this._data.CustomPostFatalityBarks.Add("Nom nom nom");
            //this._data.CustomPostFatalityBarks.Add("Tastes like chicken...");
            //this._data.CustomPostFatalityBarks.Add("Where's the beef!?");
            this._data.CustomPreFatalityBarks.Add("Get in my belly!!!");
        }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessSource);
        }

        private void ProcessSource(object o)
        {
            this._origin = this._data.Source.Handle.transform.position;
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SJolt>();
            attack.Init(this.Data.Source.Handle, pos, FatalityParams.FATALITY_ATTACK_SPEED);

            if (this._data.Source.Proxy.Type == Model.Character.Enum.ECharType.Critter)
            {
                this._sprites = CharSpriteLoader.Instance.GetCritterSprites(this._data.Source.View.Name);
                int index = CritterAttackSpriteTable.Instance.Table[this._data.Source.View.Name];
                this._renderer = this._data.Source.Handle.GetComponent<SpriteRenderer>();
                this._oldSprite = this._renderer.sprite;
                this._renderer.sprite = this._sprites[index];
            }
            this.ProcessTargets();
        }

        private void ProcessTargets()
        {
            foreach(var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = hit.Data.Target.Current as CharController;

                    var scaleData = new SScaleData();
                    scaleData.ScalePerFrame = 0.99f;
                    scaleData.Target = tgt.Handle;
                    var scale = tgt.Handle.AddComponent<SScale>();
                    scale.Init(scaleData);

                    var rotateData = new SRotationData();
                    var roll = RNG.Instance.RandomNegOrPos();
                    if (roll > 0)
                        rotateData.SpinRight = true;
                    else
                        rotateData.SpinRight = false;
                    rotateData.Speed = Mathf.Abs(RNG.Instance.GetRandomBetweenRange(12f));
                    rotateData.Target = tgt.Handle;
                    var rotate = tgt.Handle.AddComponent<SRotation>();
                    rotate.Init(rotateData);

                    var trackData = new STrackMoveData();
                    trackData.Epsilon = FatalityParams.DEFAULT_EPSILON;
                    trackData.Handle = tgt.Handle;
                    trackData.Speed = 1.2f;
                    trackData.Target = this._data.Source.Handle;
                    var track = tgt.Handle.AddComponent<STrackMoveThenDelete>();
                    track.Init(trackData);

                    var callbackDelay = tgt.Handle.AddComponent<SDelayCallback>();
                    callbackDelay.AddCallback(this.ProcessPostFatality);
                    callbackDelay.AddCallback(hit.CallbackHandler);
                    callbackDelay.AddCallback(rotate.Done);
                    callbackDelay.Init(FatalityParams.DEFAULT_DUR);

                    VCharUtil.Instance.AssignDeadLayer(tgt);
                }
            }
        }

        private void ProcessPostFatality(object o)
        {
            this._renderer.sprite = this._oldSprite;
            if (o.GetType().Equals(typeof(SDelayCallback)))
            {
                var script = o as SDelayCallback;
                var tgt = script.gameObject;
                GameObject.Destroy(tgt);
            }
            var joltback = this._data.Source.Handle.GetComponent<SJolt>();
            if (joltback == null)
            {
                joltback = this.Data.Source.Handle.AddComponent<SJolt>();
                joltback.AddCallback(this.CallbackHandler);
                joltback.Init(
                    this._data.Source.Handle, 
                    this._origin, 
                    FatalityParams.FATALITY_ATTACK_SPEED);
            }
        }
    }
}