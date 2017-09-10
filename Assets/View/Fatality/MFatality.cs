using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Character.Enum;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using Assets.View.Bark;
using Assets.View.Character;
using Assets.View.Event;
using Assets.View.Script.FX;
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
        protected EFatality _type;

        public FatalityData Data { get { return this._data; } }
        public EFatality Type { get { return this._type; } }

        public MFatality(EFatality type, FatalityData data)
        {
            this._callbacks = new List<Callback>();
            this._data = data;
            this._type = type;
        }

        public virtual void Init()
        {
            var bob = this._data.Source.Handle.GetComponent<SBob>();
            if (bob != null)
                bob.Reset();
        }

        public void Start(Callback callback)
        {
            var position = this._data.Source.Handle.transform.position;
            position.y -= FatalityParams.ZOOM_Y_OFFSET;
            var zoom = this._data.Source.Handle.AddComponent<SHangCallbackZoomOut>();
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
            var existingBob = this._data.Source.Handle.GetComponent<SBob>();
            if (existingBob == null)
            {
                var bob = this._data.Source.Handle.AddComponent<SBob>();
                bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Source.Handle);
            }
        }

        protected GameObject AddBloodGeyser(CharController tgt)
        {
            var path = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        CombatGUIParams.FIGHTING_FATALITY,
                        CombatGUIParams.PARTICLES_EXTENSION);
            var position = tgt.Handle.transform.position;
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

        protected void LayFatalityDeco(Sprite sprite, CharController c)
        {
            var deco = new GameObject();
            deco.transform.position = c.Handle.transform.position;
            var renderer = deco.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = Layers.DEAD_TORSO;
            RotateTranslateUtil.Instance.RandomRotateAndTranslate(deco, CombatGUIParams.DEFAULT_OFFSET);
        }

        protected void ProcessBlood(CharController target)
        {
            foreach (var neighbor in target.Tile.GetAdjacent())
            {
                foreach (var outerNeighbor in neighbor.GetAdjacent())
                {
                    this.ProcessBloodHelper(target, 0.2);
                }
                this.ProcessBloodHelper(target, 0.5);
            }
            this.ProcessBloodHelper(target, 1.0);
        }

        private void ProcessBloodHelper(CharController target, double percent)
        {
            var data = new EvSplatterData();
            data.DmgPercent = percent;
            data.Target = target.Handle;
            var e = new EvSplatter(data);
            e.TryProcess();
        }

        protected void ProcessExplosion(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = hit.Data.Target.Current as CharController;
                    var geyser = this.AddBloodGeyser(tgt);
                    var explosionPath = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        "FightingFatalityExplosion",
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var explosion = GameObject.Instantiate(Resources.Load(explosionPath)) as GameObject;
                    explosion.transform.position = tgt.Handle.transform.position;
                    explosion.name = "BOOM";
                    var scriptOne = geyser.AddComponent<SDestroyByLifetime>();
                    var scriptTwo = explosion.AddComponent<SDestroyByLifetime>();
                    scriptOne.Init(geyser, 5f);
                    scriptOne.AddCallback(this.CallbackHandler);
                    scriptOne.AddCallback(hit.CallbackHandler);
                    scriptTwo.Init(explosion, 8f);
                    this.ProcessBlood(tgt);
                    this.ProcessGearExplosion(tgt);
                }
                else
                {
                    VHitController.Instance.ProcessDefenderHit(hit);
                }
            }
        }

        protected void ProcessNoBloodDeath(CharController tgt)
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

        protected void ProcessGearExplosion(CharController c)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                if (c.SubComponents.ContainsKey(Layers.CHAR_TORSO))
                {
                    var renderer = c.SubComponents[Layers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_HEAD))
                {
                    var renderer = c.SubComponents[Layers.CHAR_HEAD].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_HEAD_DECO_1))
                {
                    var renderer = c.SubComponents[Layers.CHAR_HEAD_DECO_1].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_HEAD_DECO_2))
                {
                    var renderer = c.SubComponents[Layers.CHAR_HEAD_DECO_2].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_FACE))
                {
                    var renderer = c.SubComponents[Layers.CHAR_FACE].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.Proxy.GetArmor() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_ARMOR].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_ARMOR], c);
                }
                if (c.Proxy.GetHelm() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_HELM].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_HELM], c);
                }
                if (c.Proxy.GetLWeapon() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_L_WEAPON].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_L_WEAPON], c);
                }
                if (c.Proxy.GetRWeapon() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_R_WEAPON].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_R_WEAPON], c);
                }
            }
            else
            {
                var renderer = c.SubComponents[Layers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
            }
        }

        protected void SetBodyComponentsNull(CharController c)
        {
            foreach (var sub in c.SubComponents)
            {
                if (!sub.Key.ToLowerInvariant().Contains("Weapon") &&
                    !sub.Key.ToLowerInvariant().Contains("Armor"))
                {
                    var renderer = sub.Value.GetComponent<SpriteRenderer>();
                    if (renderer != null)
                    {
                        renderer.sprite = null;
                    }
                }
            }
        }
    }
}
