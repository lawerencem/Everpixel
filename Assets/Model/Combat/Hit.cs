using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Effect;
using Assets.Model.Event.Combat;
using System;
using System.Collections.Generic;
using Template.CB;

namespace Assets.Model.Combat
{
    public class Hit : ICallback
    {
        private List<Callback> _callbacks;
        private List<MEffect> _effects;
        private List<MCombatEv> _events;
        private FHit _flags;

        public MAbility Ability { get; set; }
        public ChancePrediction Chances { get; set; }
        public bool FXProcessed { get; set; }
        public HitModData ModData { get; set; }
        public bool IsFinished { get; set; }
        public bool IsHeal { get; set; }
        public int Dmg { get; set; }
        public CharController Source { get; set; }
        public CharController Target { get; set; }
        public TileController TargetTile { get; set; }

        public FHit GetFlags() { return this._flags; }

        public Hit(AbilityArgContainer arg)
        {
            this._callbacks = new List<Callback>();
            this._effects = new List<MEffect>();
            this._events = new List<MCombatEv>();
            this._flags = new FHit();
            //this._effects = new List<Effect>();

            //this.Chances = new ChancePrediction();
            //this.IsFinished = false;
            //this.ModData = new HitModData();
            //this.Source = s;
            //if (t.Model.Current != null && t.Model.Current.GetType().Equals(typeof(CharController)))
            //    this.Target = t.Model.Current as CharController;
            //this.TargetTile = t;
            //this.Ability = a;
            //this._callBack = callback;
            //this.Flags = new FHit();
        }

        public void Done()
        {
            this.IsFinished = true;
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void AddEffect(MEffect e)
        {
            this._effects.Add(e);
        }

        public void AddEvent(MCombatEv e)
        {
            e.AddCallback(this.Callback);
            this._events.Add(e);
            this._events.Sort((x, y) => x.Priority - y.Priority);
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            throw new NotImplementedException();
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        private void Callback(object o)
        {

        }
    }
}
