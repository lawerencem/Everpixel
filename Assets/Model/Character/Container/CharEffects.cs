using Assets.Model.Effect;
using Assets.Model.Injury;
using Assets.Model.OTE.DoT;
using Assets.Model.OTE.HoT;
using Assets.Model.Shield;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class CharEffects<T>
    {
        private AChar<T> _parent;

        private List<MDoT> _dots { get; set; }
        private List<MEffect> _effects { get; set; }
        private List<MHoT> _hots { get; set; }
        private List<MInjury> _injuries { get; set; }
        private List<MShield> _shields { get; set; }

        public List<MDoT> GetDots() { return this._dots; }
        public List<MEffect> GetEffects() { return this._effects; }
        public List<MHoT> GetHots() { return this._hots; }
        public List<MInjury> GetInjuries() { return this._injuries; }
        public List<MShield> GetShields() { return this._shields; }

        public void AddEffect(MEffect e) { this._effects.Add(e); }
        public void AddDoT(MDoT dot) { this._dots.Add(dot); }
        public void AddHoT(MHoT hot) { this._hots.Add(hot); }
        public void AddInjury(MInjury i) { this._injuries.Add(i); }
        public void AddShield(MShield s) { this._shields.Add(s); }

        public CharEffects(AChar<T> parent)
        {
            this._parent = parent;
            this._dots = new List<MDoT>();
            this._effects = new List<MEffect>();
            this._hots = new List<MHoT>();
            this._injuries = new List<MInjury>();
            this._shields = new List<MShield>();
        }

        public void ProcessShields()
        {
            foreach (var shield in this._shields)
                shield.ProcessTurn();
            this._shields.RemoveAll(x => x.Dur <= 0);
        }
    }
}
