﻿using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using Assets.View.Bark;
using Assets.View.Character;
using Assets.View.Event;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Fatality
{
    public class MFatality : ICallback
    {
        private Callback _postZoomCallback;

        protected int _callbackQty = 0;

        protected List<Callback> _callbacks;
        protected FatalityData _data;
        protected Dictionary<Guid, AScript> _fatalityMap;
        protected EFatality _type;

        public FatalityData Data { get { return this._data; } }
        public EFatality Type { get { return this._type; } }

        public MFatality(EFatality type, FatalityData data)
        {
            this._callbacks = new List<Callback>();
            this._data = data;
            this._fatalityMap = new Dictionary<Guid, AScript>();
            this._type = type;
        }

        public virtual void Init()
        {
            var bob = this._data.Source.GameHandle.GetComponent<SBob>();
            if (bob != null)
                bob.Reset();
        }

        public void Start(Callback callback)
        {
            var position = this._data.Source.GameHandle.transform.position;
            position.y -= FatalityParams.ZOOM_Y_OFFSET;
            var zoom = this._data.Source.GameHandle.AddComponent<SHangCallbackZoomOut>();
            if (BarkManager.Instance.IsPreFatalityBark())
            {
                this._postZoomCallback = callback;
                zoom.AddCallback(this.BarkCallback);
            }
            else
            {
                zoom.AddCallback(callback);
            }
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG);
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            if (this._postZoomCallback == null)
                BarkManager.Instance.ProcessPostFatalityBark(this._data);
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        protected void BarkCallback(object o)
        {
            BarkManager.Instance.ProcessPreFatalityBark(this.Data, this._postZoomCallback);
        }

        protected void AddBob(object o)
        {
            if (this._data.Source != null && this._data.Source.Equals(CombatManager.Instance.GetCurrentlyActing()))
            {
                var bob = this._data.Source.GameHandle.GetComponent<SBob>();
                if (bob == null)
                {
                    bob = this._data.Source.GameHandle.AddComponent<SBob>();
                    bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Source.GameHandle);
                }
            }
        }

        protected GameObject AddBloodGeyser(CChar tgt)
        {
            var path = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        CombatGUIParams.FIGHTING_FATALITY,
                        CombatGUIParams.PARTICLES_EXTENSION);
            var position = tgt.GameHandle.transform.position;
            var boom = Resources.Load(path);
            var particles = GameObject.Instantiate(boom) as GameObject;
            particles.transform.position = position;
            particles.name = CombatGUIParams.FIGHTING_FATALITY + " Particles";
            return particles;
        }

        protected virtual void CallbackHandler(object o)
        {
            this._callbackQty++;
            if (this._callbackQty == (this._data.FatalHits.Count + this._data.NonFatalHits.Count))
            {
                GUIManager.Instance.SetComponentActiveForLifetime(GameObjectTags.FATALITY_BANNER, true, 4f);
                this.AddBob(this);
                this.DoCallbacks();
            }
        }

        protected GameObject LayFatalityDecoRandomPosition(Sprite sprite, CChar c)
        {
            var deco = this.LayFatalityDeco(sprite, c, SortingLayers.DEAD_TORSO);
            RotateTranslateUtil.Instance.RandomRotateAndTranslate(deco, CombatGUIParams.DEFAULT_OFFSET);
            return deco;
        }

        protected GameObject LayFatalityDeco(Sprite sprite, CChar c, string layer)
        {
            var deco = new GameObject();
            deco.transform.position = c.GameHandle.transform.position;
            var renderer = deco.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = layer;
            return deco;
        }

        protected void ProcessBlood(CChar target)
        {
            foreach (var neighbor in target.Tile.GetAdjacent())
            {
                foreach (var outerNeighbor in neighbor.GetAdjacent())
                {
                    this.ProcessBloodHelper(target, 0.2);
                }
                this.ProcessBloodHelper(target, 0.5);
            }
            this.ProcessBloodHelper(target, 1.0, true);
        }

        private void ProcessBloodHelper(CChar target, double percent, bool fatality = false)
        {
            var data = new EvSplatterData();
            data.DmgPercent = percent;
            data.Fatality = fatality;
            data.Target = target.GameHandle;
            var e = new EvSplatter(data);
            e.TryProcess();
        }

        protected void ProcessExplosion(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                {
                    var tgt = hit.Data.Target.Current as CChar;
                    var geyser = this.AddBloodGeyser(tgt);
                    var explosionPath = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        CombatGUIParams.FIGHTING_FATALITY_EXPLOSION,
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var explosion = GameObject.Instantiate(Resources.Load(explosionPath)) as GameObject;
                    explosion.transform.position = tgt.GameHandle.transform.position;
                    explosion.name = "BOOM";
                    var scriptOne = geyser.AddComponent<SDestroyByLifetime>();
                    var scriptTwo = explosion.AddComponent<SDestroyByLifetime>();
                    scriptOne.Init(geyser, 5f);
                    scriptOne.AddCallback(this.CallbackHandler);
                    scriptOne.AddCallback(hit.CallbackHandler);
                    scriptTwo.Init(explosion, 8f);
                    this.ProcessBlood(tgt);
                    VCharUtil.Instance.ProcessGearExplosion(tgt);
                }
                else
                {
                    VHitController.Instance.ProcessDefenderHit(hit);
                }
            }
            foreach (var hit in this._data.NonFatalHits)
            {
                hit.CallbackHandler(this);
                this.CallbackHandler(null);
            }
        }

        protected void ProcessNoBloodDeath(CChar tgt)
        {
            VCharUtil.Instance.AssignDeadEyes(tgt);
            VCharUtil.Instance.AssignDeadLayer(tgt);
            VCharUtil.Instance.AssignDeadWeapons(tgt);
            VCharUtil.Instance.RandomTranslateRotateOnDeath(tgt);
        }

        protected void ProcessNonFatal(object o)
        {
            foreach (var nonFatal in this._data.NonFatalHits)
                VHitController.Instance.ProcessDefenderHit(nonFatal);
        }
    }
}
