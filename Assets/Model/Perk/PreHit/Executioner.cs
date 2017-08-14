using Assets.Model.Combat;

namespace Assets.Model.Perk.PreHit
{
    public class Executioner : MPreHitPerk
    {
        public Executioner() : base(EPerk.Executioner)
        {

        }

        public override void TryModHit(Hit hit)
        {
            //base.TryModHit(hit);
            //var maxHp = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.HP);
            //if (hit.Target.Model.GetCurrentHP() / maxHp <= 0.25)
            //{
            //    hit.Ability.DamageMod *= this.Val;
            //}
        }
    }
}
