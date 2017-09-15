using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.AbilityMod
{
    public class TRexBite : MAbilityModPerk
    {
        public TRexBite() : base(EPerk.T_Rex_Bite) { }

        public override void TryModAbility(MHit hit)
        {
            base.TryModAbility(hit);
            if (hit.Data.Ability.Type == EAbility.Bite)
            {
                hit.Data.ModData.BaseDamage += this.Val;
            }
        }
    }
}
