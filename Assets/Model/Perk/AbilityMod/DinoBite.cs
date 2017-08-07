using Assets.Model.Ability.Enum;
using Assets.Model.Combat;

namespace Assets.Model.Perk.AbilityMod
{
    public class DinoBite : MAbilityModPerk
    {
        public DinoBite() : base(EPerk.Dino_Bite) { }

        public override void TryModAbility(Hit hit)
        {
            base.TryModAbility(hit);
            if (hit.Ability.Type == EAbility.Bite)
                hit.ModData.BaseDamage += this.Val;
        }
    }
}
