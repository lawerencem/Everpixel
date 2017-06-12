using Model.Combat;

namespace Model.Abilities
{
    public class WeaponAbility : GenericAbility
    {
        public WeaponAbility(WeaponAbilitiesEnum type)
        {
            this.AccMod = 1;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.DamageMod = 1;
            this.DodgeMod = 1;
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this.StaminaCost = 0;
            this._type = type;
            this._typeStr = this._type.ToString().Replace("_", " ");
        }

        public WeaponAbility Copy()
        {
            var ability = new WeaponAbility((WeaponAbilitiesEnum)this._type);
            ability.AccMod = this.AccMod;
            ability.APCost = this.APCost;
            ability.ArmorIgnoreMod = this.ArmorIgnoreMod;
            ability.ArmorPierceMod = this.ArmorPierceMod;
            ability.BlockIgnoreMod = this.BlockIgnoreMod;
            ability.DamageMod = this.DamageMod;
            ability.Description = this.Description;
            ability.DodgeMod = this.DodgeMod;
            ability.StaminaCost = this.StaminaCost;
            ability.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            ability.ParryModMod = this.ParryModMod;
            ability.RangeBlockMod = this.RangeBlockMod;
            ability.ShieldDamageMod = this.ShieldDamageMod;
            return ability;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            
        }
    }
}
