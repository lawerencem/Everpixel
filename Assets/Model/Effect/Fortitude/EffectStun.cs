using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
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
                if (!calc.DidResist(tgt, this.Data.Resist, offense))
                {
                    FHit.SetStunTrue(hit.Data.Flags);
                    tgt.Proxy.AddEffect(this);
                }
            }
        }

        public static void ProcessStunFX(CChar c)
        {
            var stun = c.Proxy.GetEffects().GetEffects().Find(x => x.Type == EEffect.Stun);
            if (stun == null)
            {
                var path = StringUtil.PathBuilder(
                    CombatGUIParams.EFFECTS_PATH,
                    "Stun",
                    CombatGUIParams.PARTICLES_EXTENSION);
                var particles = ParticleController.Instance.CreateParticle(path);
                if (particles != null)
                    DecoUtil.AttachParticles(particles, c.Handle);
                VCombatController.Instance.DisplayText("Stunned", c);
            }
        }
    }
}
