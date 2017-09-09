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
            weapon.AccuracyMod = wStats.AccuracyMod;
            weapon.APMod = wStats.APMod;
            weapon.ArmorIgnore = wStats.ArmorIgnore;
            weapon.ArmorPierce = wStats.ArmorPierce;
            weapon.BlockIgnore = wStats.BlockIgnore;
            weapon.CustomBullet = wStats.CustomBullet;
            weapon.CustomFatality = wStats.CustomFatality;
            weapon.Damage = wStats.Damage;
            weapon.Description = wStats.Description;
            weapon.Durability = wStats.MaxDurability;
            weapon.FatigueMod = wStats.FatigueMod;
            weapon.InitiativeMod = wStats.InitiativeMod;
            weapon.MaxDurability = wStats.MaxDurability;
            weapon.MeleeBlockChance = wStats.MeleeBlockChance;
            weapon.Name = wStats.Name;
            weapon.ParryMod = wStats.ParryMod;
            weapon.RangeMod = wStats.RangeMod;
            weapon.ShieldDamagePercent = wStats.ShieldDamagePercent;
            weapon.Skill = wStats.Skill;
            weapon.SpriteFXPath = wStats.SpriteFXPath;
            weapon.StaminaMod = wStats.StaminaMod;
            weapon.Tier = wStats.Tier;
            weapon.WpnType = wStats.Type;
            return weapon;
        }
    }
}
