using Assets.Model.Effect;
using Assets.Model.Injury;
using Assets.Model.OTE.DoT;
using Assets.Model.OTE.HoT;
using Assets.Model.Barrier;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class CharEffects
    {
        private List<MDoT> _dots { get; set; }
        private List<MEffect> _effects { get; set; }
        private List<MHoT> _hots { get; set; }
        private List<MBarrier> _barriers { get; set; }

        public List<MDoT> GetDots() { return this._dots; }
        public List<MEffect> GetEffects() { return this._effects; }
        public List<MHoT> GetHots() { return this._hots; }
        
        public List<MBarrier> GetBarriers() { return this._barriers; }

        public void AddEffect(MEffect e) { this._effects.Add(e); }
        public void AddDoT(MDoT dot) { this._dots.Add(dot); }
        public void AddHoT(MHoT hot) { this._hots.Add(hot); }
        public void AddBarrier(MBarrier s) { this._barriers.Add(s); }

        public CharEffects()
        {
            this._dots = new List<MDoT>();
            this._effects = new List<MEffect>();
            this._hots = new List<MHoT>();
            this._barriers = new List<MBarrier>();
        }

        public void ProcessShields()
        {
            foreach (var shield in this._barriers)
                shield.ProcessTurn();
            this._barriers.RemoveAll(x => x.Dur <= 0);
        }
    }
}
