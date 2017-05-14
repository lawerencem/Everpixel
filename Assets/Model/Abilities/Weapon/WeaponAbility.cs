namespace Model.Abilities
{
    public class WeaponAbility : GenericAbility
    {
        public double AccMod { get; set; }
        public double APMod { get; set; }
        public double ArmorIgnoreMod { get; set; }
        public double ArmorPierceMod { get; set; }
        public double BlockIgnoreMod { get; set; }
        public double DamageMod { get; set; }
        public double DodgeMod { get; set; }
        public string Description { get; set; }
        public double FatigueCostMod { get; set; }
        public double MeleeBlockChanceMod { get; set; }
        public double ParryModMod { get; set; }
        public double RangeBlockMod { get; set; }
        public double ShieldDamageMod { get; set; }
        private new WeaponAbilitiesEnum _type;
        public new WeaponAbilitiesEnum Type { get { return this._type; } }

        public WeaponAbility(WeaponAbilitiesEnum type)
        {
            this.AccMod = 1;
            this.APMod = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.DamageMod = 1;
            this.DodgeMod = 1;
            this.FatigueCostMod = 1;
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this._type = type;
        }

        public WeaponAbility Copy()
        {
            var ability = new WeaponAbility(this._type);
            ability.AccMod = this.AccMod;
            ability.APMod = this.APMod;
            ability.ArmorIgnoreMod = this.ArmorIgnoreMod;
            ability.ArmorPierceMod = this.ArmorPierceMod;
            ability.BlockIgnoreMod = this.BlockIgnoreMod;
            ability.DamageMod = this.DamageMod;
            ability.Description = this.Description;
            ability.DodgeMod = this.DodgeMod;
            ability.FatigueCostMod = this.FatigueCostMod;
            ability.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            ability.ParryModMod = this.ParryModMod;
            ability.RangeBlockMod = this.RangeBlockMod;
            ability.ShieldDamageMod = this.ShieldDamageMod;
            return ability;
        }
    }
}
