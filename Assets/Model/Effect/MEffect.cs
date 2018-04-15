using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Zone;
using Assets.Model.Zone.Duration;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using Assets.View;
using Assets.View.Map;
using Assets.View.Particle;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Model.Effect
{
    public class MEffectData
    {
        public EAbility AbilityCondition { get; set; }
        public ECastType CastCondition { get; set; }
        public int Duration { get; set; }
        public string ParticlePath { get; set; }
        public ESecondaryStat OffensiveResist { get; set; }
        public EResistType Resist { get; set; }
        public List<int> SpriteIndexes { get; set; }
        public int SpritesMax { get; set; }
        public int SpritesMin { get; set; }
        public string SpritesPath { get; set; }
        public string SummonKey { get; set; }
        public string WeaponCondition { get; set; }
        public EEffect Type { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public MEffectData()
        {
            this.SpriteIndexes = new List<int>();
            this.WeaponCondition = "None";
        }

        public void CallbackHandler(object o)
        {
            throw new NotImplementedException();
        }
    }

    public class MEffect : ICallback, ICallbackHandler
    {
        private List<Callback> _callbacks;
        private MEffectData _data;
        private EEffect _type;

        public MEffectData Data { get { return this._data; } }
        public EEffect Type { get { return this._type; } }

        public void SetData(MEffectData data) { this._data = data; }
        
        public MEffect(EEffect type)
        {
            this._callbacks = new List<Callback>();
            this._type = type;
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void ApplyEffectFx(CTile tile)
        {
            if (tile.Model.GetCurrentOccupant() != null)
            {
                if (tile.Model.GetCurrentOccupant().GetType() == typeof(CChar))
                {
                    var util = new DecoUtil();
                    var particles = ParticleController.Instance.CreateParticle(this.Data.ParticlePath);
                    var tgt = tile.Model.GetCurrentOccupant() as CChar;
                    util.AttachEffectParticlesToChar(tgt, particles, this.Type);
                }
            }
            var placeHolder = new GameObject();
            var script = placeHolder.AddComponent<SDestroyByLifetime>();
            script.AddCallback(this.CallbackHandler);
            script.Init(placeHolder, 0.2f);
        }

        public void CallbackHandler(object o)
        {
            this.DoCallbacks();
        }

        public void SetCallback(Callback callback)
        {
            throw new NotImplementedException();
        }

        public virtual bool CheckConditions(MHit hit)
        {
            if (this.Data.AbilityCondition != EAbility.None)
                if (hit.Data.Ability.Type != this.Data.AbilityCondition)
                    return false;
            if (this.Data.CastCondition != ECastType.None)
                if (hit.Data.Ability.Data.CastType != this.Data.CastCondition)
                    return false;
            if (this.Data.WeaponCondition != "None")
            {
                if (hit.Data.Ability.Data.ParentWeapon == null)
                    return false;
                else
                {
                    var name = hit.Data.Ability.Data.ParentWeapon.Data.Name;
                    if (!name.Contains(this.Data.WeaponCondition))
                        return false;
                }
            }
            return true;
        }

        public virtual MEffectData CloneData()
        {
            var data = new MEffectData();
            data.AbilityCondition = this.Data.AbilityCondition;
            data.CastCondition = this.Data.CastCondition;
            data.Duration = this.Data.Duration;
            data.ParticlePath = this.Data.ParticlePath;
            data.SummonKey = this.Data.SummonKey;
            data.WeaponCondition = this.Data.WeaponCondition;
            data.X = this.Data.X;
            data.Y = this.Data.Y;
            data.Z = this.Data.Z;
            return data;
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public virtual void TryProcessHit(MHit hit, bool prediction) { }
        public virtual void TryProcessTurn(MHit hit) { }

        protected DurationZoneData GetDurationZoneData(MHit hit)
        {
            var data = new DurationZoneData();
            data.SpriteIndexes = this.Data.SpriteIndexes.ToList();
            data.SpritesPath = this.Data.SpritesPath;
            data.Source = hit.Data.Source;
            data.X = this.Data.X;
            data.Y = this.Data.Y;
            data.Z = this.Data.Z;
            return data;
        }

        protected void ProcessZoneFX(ZoneData data, MHit hit)
        {
            var sprite = MapSpriteLoader.Instance.GetZoneSprite(data);
            var handle = new GameObject();
            handle.name = data.SpritesPath;
            var renderer = handle.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = Layers.TILE_DECO;
            renderer.transform.SetParent(hit.Data.Target.Handle.transform);
            renderer.transform.position = hit.Data.Target.Handle.transform.position;
            RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                handle,
                ViewParams.SPLATTER_VARIANCE);
        }
    }
}
