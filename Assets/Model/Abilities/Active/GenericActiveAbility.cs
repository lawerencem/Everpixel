using Model.Abilities.Magic;
using Model.Combat;

namespace Model.Abilities
{
    public class GenericActiveAbility : GenericAbility
    {
        public AbilityCastTypeEnum CastType { get; set; }
        public double DmgPerPower { get; set; }
        public double Duration { get; set; }
        public double FlatDamage { get; set; }
        public MagicTypeEnum MagicType { get; set; }
        public double RechargeTime { get; set; }
        public int SpellLevel { get; set; }

        public GenericActiveAbility(ActiveAbilitiesEnum type)
        {
            this.AccMod = 1;
            this.AoE = 1;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.CastType = AbilityCastTypeEnum.None;
            this.DamageMod = 1;
            this.DmgPerPower = 0;
            this.DodgeMod = 1;
            this.Duration = 0;
            this.FlatDamage = 0;
            this.MagicType = MagicTypeEnum.Physical;
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.RangeBlockMod = 1;
            this.RechargeTime = 0;
            this.ShieldDamageMod = 1;
            this.SpellLevel = 0;
            this.StaminaCost = 0;
            this._type = type;
            this._typeStr = this._type.ToString().Replace("_", " ");
        }

        public GenericActiveAbility Copy()
        {
            var ability = new GenericActiveAbility((ActiveAbilitiesEnum)this._type);
            ability.SpellLevel = this.SpellLevel;
            ability.AccMod = this.AccMod;
            ability.APCost = this.APCost;
            ability.AoE = this.AoE;
            ability.ArmorIgnoreMod = this.ArmorIgnoreMod;
            ability.ArmorPierceMod = this.ArmorPierceMod;
            ability.BlockIgnoreMod = this.BlockIgnoreMod;
            ability.CastType = this.CastType;
            ability.DamageMod = this.DamageMod;
            ability.Description = this.Description;
            ability.DodgeMod = this.DodgeMod;
            ability.Duration = this.Duration;
            ability.StaminaCost = this.StaminaCost;
            ability.MagicType = this.MagicType;
            ability.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            ability.ParryModMod = this.ParryModMod;
            ability.RangeBlockMod = this.RangeBlockMod;
            ability.RechargeTime = this.RechargeTime;
            ability.ShieldDamageMod = this.ShieldDamageMod;
            ability.SpellLevel = this.SpellLevel;
            ability.StaminaCost = this.StaminaCost;
            return ability;
        }

        public override void ProcessAbility(HitInfo hit)
        {

        }

        public bool isTileTargetable()
        {
            if (this.CastType == AbilityCastTypeEnum.Summon)
                return true;
            else
                return false;
        }
    }
}
