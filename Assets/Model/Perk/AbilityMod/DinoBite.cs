using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.AbilityMod
{
    public class DinoBite : MAbilityModPerk
    {
        public DinoBite() : base(EPerk.Dino_Bite) { }

        public override void TryModAbility(Hit hit)
        {
            base.TryModAbility(hit);
            if (hit.Data.Ability.Type == EAbility.Bite)
                hit.Data.ModData.BaseDamage += this.Val;
        }
    }
}
