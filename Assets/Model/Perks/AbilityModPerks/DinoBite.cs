using Model.Abilities;
using Model.Combat;

namespace Model.Perks
{
    public class DinoBite : GenericAbilityModPerk
    {
        public DinoBite() : base(PerkEnum.Dino_Bite) { }

        public override void TryModAbility(HitInfo hit)
        {
            base.TryModAbility(hit);
            if (hit.Ability.Type == AbilitiesEnum.Bite)
                hit.ModData.BaseDamage += this.Val; // TODO: 50
        }
    }
}
