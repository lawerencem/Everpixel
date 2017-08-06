using Model.Abilities;
using Model.Combat;

namespace Model.Perks
{
    public class TRexBite : MAbilityModPerk
    {
        public TRexBite() : base(EPerk.T_Rex_Bite) { }

        public override void TryModAbility(Hit hit)
        {
            base.TryModAbility(hit);
            if (hit.Ability.Type == EAbility.Bite)
            {
                hit.ModData.BaseDamage += this.Val;
            }
        }
    }
}
