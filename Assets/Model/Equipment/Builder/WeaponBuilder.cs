using Assets.Model.Equipment.Table;
using Assets.Model.Equipment.Type;
using Assets.Model.Weapon;
using Assets.Template.Builder;

namespace Assets.Model.Equipment.Builder
{
    public class WeaponBuilder : GBuilder<string, MWeapon>
    {
        public override MWeapon Build(string arg)
        {
            return BuildHelper(arg);
        }

        private MWeapon BuildHelper(string arg)
        {
            var weapon = new MWeapon();
            var wStats = WeaponParamTable.Instance.Table[arg];
            weapon.Abilities = WeaponAbilityFactory.Instance.CreateNewObject(wStats.Abilities);
            weapon.Accuracy = wStats.Accuracy;
            weapon.APReduce = wStats.APReduce;
            weapon.ArmorIgnore = wStats.ArmorIgnore;
            weapon.ArmorPierce = wStats.ArmorPierce;
            weapon.BlockIgnore = wStats.BlockIgnore;
            weapon.Damage = wStats.Damage;
            weapon.Description = wStats.Description;
            weapon.Durability = wStats.Durability;
            weapon.FatigueCostMod = wStats.FatigueCostMod;
            weapon.InitiativeReduce = wStats.InitiativeReduce;
            weapon.MaxDurability = wStats.Durability;
            weapon.MeleeBlockChance = wStats.MeleeBlockChance;
            weapon.Name = wStats.Name;
            weapon.ParryMod = wStats.ParryMod;
            weapon.RangeMod = wStats.RangeMod;
            weapon.ShieldDamage = wStats.ShieldDamage;
            weapon.Skill = wStats.Skill;
            weapon.StaminaReduce = wStats.StaminaReduce;
            weapon.Tier = wStats.Tier;
            weapon.WpnType = wStats.Type;
            return weapon;
        }
    }
}
