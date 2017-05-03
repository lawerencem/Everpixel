using Generics;

namespace Model.Equipment
{
    public class WeaponBuilder : GenericBuilder<string, GenericWeapon>
    {
        public override GenericWeapon Build(string arg)
        {
            return BuildHelper(arg);
        }

        private GenericWeapon BuildHelper(string arg)
        {
            var weapon = new GenericWeapon();
            var wStats = WeaponParamTable.Instance.Table[arg];
            weapon.Accuracy = wStats.Accuracy;
            weapon.APReduce = wStats.APReduce;
            weapon.ArmorIgnore = wStats.ArmorIgnore;
            weapon.ArmorPierce = wStats.ArmorPierce;
            weapon.BlockIgnore = wStats.BlockIgnore;
            weapon.Damage = wStats.Damage;
            weapon.Description = wStats.Description;
            weapon.Durability = wStats.Durability;
            weapon.FatigueCost = wStats.FatigueCost;
            weapon.FatigueReduce = wStats.FatigueReduce;
            weapon.InitiativeReduce = wStats.InitiativeReduce;
            weapon.MaxDurability = wStats.Durability;
            weapon.Name = wStats.Name;
            weapon.ParryChance = wStats.ParryChance;
            weapon.Range = wStats.Range;
            weapon.ShieldDamage = wStats.ShieldDamage;
            weapon.Tier = wStats.Tier;
            return weapon;
        }
    }
}
