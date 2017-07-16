using Model.Characters;
using Model.Combat;

namespace Model.Perks
{
    public class Executioner : GenericPreHitPerk
    {
        public Executioner() : base(PerkEnum.Executioner)
        {

        }

        public override void TryModHit(HitInfo hit)
        {
            base.TryModHit(hit);
            var maxHp = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
            if (hit.Target.Model.GetCurrentHP() / maxHp <= 0.25)
            {
                hit.Ability.DamageMod *= this.Val;
            }
        }
    }
}
