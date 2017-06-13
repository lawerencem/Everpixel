using Model.Abilities.Magic;
using Model.Combat;

namespace Model.Abilities
{
    public class GenericActiveAbility : GenericAbility
    {
        public AbilityCastTypeEnum CastType { get; set; }
        public MagicTypeEnum MagicType { get; set; }

        public GenericActiveAbility(ActiveAbilitiesEnum type)
        {
            this.AccMod = 1;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.CastType = AbilityCastTypeEnum.None;
            this.DamageMod = 1;
            this.DodgeMod = 1;
            this.MagicType = MagicTypeEnum.Fighting;
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this.StaminaCost = 0;
            this._type = type;
            this._typeStr = this._type.ToString().Replace("_", " ");
        }

        public GenericActiveAbility Copy()
        {
            var ability = new GenericActiveAbility((ActiveAbilitiesEnum)this._type);
            ability.AccMod = this.AccMod;
            ability.APCost = this.APCost;
            ability.ArmorIgnoreMod = this.ArmorIgnoreMod;
            ability.ArmorPierceMod = this.ArmorPierceMod;
            ability.BlockIgnoreMod = this.BlockIgnoreMod;
            ability.CastType = this.CastType;
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
