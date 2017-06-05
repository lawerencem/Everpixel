using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class DamageCharacterEvent : CombatEvent
    {
        public HitInfo Hit { get; set; }

        public DamageCharacterEvent(CombatEventManager parent, HitInfo hit)
            : base(CombatEventEnum.DamageCharacter, parent)
        {
            this.Hit = hit;
            if (AttackEventFlags.HasFlag(AttackEventFlags.Flags.Dodge, hit.Flags.CurFlags) ||
                AttackEventFlags.HasFlag(AttackEventFlags.Flags.Parry, hit.Flags.CurFlags))
            {
                // Do nothing
            }
            else
                this.Hit.Target.Model.ModifyHP(hit.Dmg, hit.IsHeal);
            this.RegisterEvent();
        }
    }
}
