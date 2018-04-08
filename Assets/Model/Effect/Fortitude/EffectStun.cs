using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Combat.Hit;
using Assets.Template.Util;
using Assets.View.Particle;

namespace Assets.Model.Effect.Fortitude
{
    public class EffectStun : MEffect
    {
        private const string STUN = "Stun";

        public EffectStun() : base(EEffect.Stun) { }

        public override void TryProcessHit(MHit hit, bool prediction)
        {
            base.TryProcessHit(hit, prediction);
            if (base.CheckConditions(hit))
            {
                var calc = new ResistCalculator();
                var tgt = hit.Data.Target.Current as CChar;
                var offense = hit.Data.Source.Proxy.GetStat(this.Data.OffensiveResist);
                if (!prediction && !calc.DidResist(tgt, this.Data.Resist, offense))
                {
                    FHit.SetStunTrue(hit.Data.Flags);
                    hit.AddEffect(this);
                    tgt.Proxy.AddEffect(this);
                }
            }
        }
    }
}
