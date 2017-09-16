using Assets.Model.Effect;
using Assets.Model.Equipment.Armor;
using Assets.Model.Equipment.Table;
using Assets.Model.Equipment.Weapon;
using Assets.Model.Weapon;
using Assets.Template.Builder;

namespace Assets.Model.Equipment.Builder
{
    public class WeaponBuilder : GBuilder<string, CWeapon>
    {
        public override CWeapon Build(string arg)
        {
            return BuildHelper(arg);
        }

        private CWeapon BuildHelper(string arg)
        {
            var weapon = new CWeapon();
            var wStats = WeaponParamTable.Instance.Table[arg];
            var model = new MWeapon();
            weapon.SetModel(model);
            weapon.Model.Data.Abilities = WeaponAbilityFactory.Instance.CreateNewObject(wStats.Abilities);
            weapon.Model.Data.AccuracyMod = wStats.AccuracyMod;
            weapon.Model.Data.APMod = wStats.APMod;
            weapon.Model.Data.ArmorIgnore = wStats.ArmorIgnore;
            weapon.Model.Data.ArmorPierce = wStats.ArmorPierce;
            weapon.Model.Data.BlockIgnore = wStats.BlockIgnore;
            weapon.Model.Data.CustomBullet = wStats.CustomBullet;
            weapon.Model.Data.CustomFatality = wStats.CustomFatality;
            weapon.Model.Data.Damage = wStats.Damage;
            weapon.Model.Data.Description = wStats.Description;
            weapon.Model.Data.Durability = wStats.MaxDurability;
            foreach (var effect in wStats.Effects)
            {
                var data = effect.CloneData();
                var clone = EffectBuilder.Instance.BuildEffect(data, effect.Type);
                weapon.Model.Data.Effects.Add(clone);
            }
            weapon.Model.Data.Embed = wStats.Embed;
            weapon.Model.Data.FatigueMod = wStats.FatigueMod;
            weapon.Model.Data.InitiativeMod = wStats.InitiativeMod;
            weapon.Model.Data.MaxDurability = wStats.MaxDurability;
            weapon.Model.Data.MeleeBlockChance = wStats.MeleeBlockChance;
            weapon.Model.Data.Name = wStats.Name;
            weapon.Model.Data.ParryMod = wStats.ParryMod;
            weapon.Model.Data.RangeMod = wStats.RangeMod;
            weapon.Model.Data.ShieldDamagePercent = wStats.ShieldDamagePercent;
            weapon.Model.Data.Skill = wStats.Skill;
            weapon.Model.Data.SpriteFXPath = wStats.SpriteFXPath;
            weapon.Model.Data.StaminaMod = wStats.StaminaMod;
            weapon.Model.Data.Tier = wStats.Tier;
            weapon.Model.Data.WpnType = wStats.Type;
            weapon.SetParams(wStats);
            return weapon;
        }
    }
}
