using Characters.Params;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Shields;

namespace Model.Perks
{
    public class Enrage : GenericWhenHitPerk
    {
        public Enrage() : base(PerkEnum.Enrage)
        {

        }

        public override void TryModHit(Hit hit)
        {
            if (!hit.IsHeal)
            {
                var mod = new SecondaryStatModifier(SecondaryStatsEnum.Parry, (int)this.Dur, this.Val);
                // TODO: Buff Event
            }
        }
    }
}
