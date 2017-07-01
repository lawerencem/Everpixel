using Model.Abilities;
using Model.Combat;

namespace Model.Perks
{
    public class Squishy : GenericOnHitPerk
    {
        public Squishy() : base(PerkEnum.Squishy)
        {

        }

        public override void TryModHit(HitInfo hit)
        {
            double dmgReduce = 1;
            if (hit.Ability.ArmorIgnoreMod < 1)
                dmgReduce *= hit.Ability.ArmorIgnoreMod;
            if (hit.Source.Model.LWeapon != null && hit.Source.Model.LWeapon.ArmorIgnore < 1)
                dmgReduce *= hit.Source.Model.LWeapon.ArmorIgnore;
            if (hit.Source.Model.RWeapon != null && hit.Source.Model.RWeapon.ArmorIgnore < 1)
                dmgReduce *= hit.Source.Model.RWeapon.ArmorIgnore;
            var dmg = hit.Dmg * dmgReduce;
            hit.Dmg = (int)dmg;
        }
    }
}
