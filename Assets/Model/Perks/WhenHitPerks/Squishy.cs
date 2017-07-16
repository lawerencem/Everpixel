using Model.Combat;

namespace Model.Perks
{
    public class Squishy : GenericWhenHitPerk
    {
        public Squishy() : base(PerkEnum.Squishy)
        {

        }

        public override void TryModHit(HitInfo hit)
        {
            double delta = 0;
            if (hit.Ability.ArmorIgnoreMod < 1)
                delta += (1 - hit.Ability.ArmorIgnoreMod);
            if (hit.Source.Model.LWeapon != null && hit.Source.Model.LWeapon.ArmorIgnore < 1)
                delta += (1 - hit.Source.Model.LWeapon.ArmorIgnore);
            if (hit.Source.Model.RWeapon != null && hit.Source.Model.RWeapon.ArmorIgnore < 1)
                delta += (1 - hit.Source.Model.RWeapon.ArmorIgnore);
            var reduce = delta * this.ValPerPower;
            double dmg = hit.Dmg;
            dmg *= reduce;
            hit.Dmg = (int)dmg;
        }
    }
}
