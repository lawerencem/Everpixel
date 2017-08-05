using Model.Combat;
using Model.Effects;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class EldritchChomp : GenericAbility
    {
        public EldritchChomp() : base(AbilitiesEnum.Eldritch_Chomp)
        {

        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictBullet(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessBullet(hit);
            if (!AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Resist))
            {
                var horror = EffectsFactory.Instance.CreateNewObject(EffectsEnum.Horror);
                horror.Container.Duration = (int)this.EffectDur;
                horror.Container.Value = this.EffectValue;
                hit.Target.Model.AddEffect(horror);
            }
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
