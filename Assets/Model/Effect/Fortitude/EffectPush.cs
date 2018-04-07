using Assets.Controller.Character;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;

namespace Assets.Model.Effect.Fortitude
{
    public class EffectPush : MEffect
    {
        public EffectPush() : base(EEffect.Push) { }

        public static void ProcessPush(MHit hit)
        {
            
        }

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
                    FHit.SetPushTrue(hit.Data.Flags);
                    tgt.Proxy.AddEffect(this);
                }
            }
        }
    }
}
