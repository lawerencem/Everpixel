using Assets.Data.Equipment.Table;
using Assets.Model.Equipment.Table;
using Assets.Model.Equipment.Type;
using Assets.Template.Builder;

namespace Assets.Model.Equipment.Builder
{
    public class ArmorBuilder : GBuilder<string, MArmor>
    {
        public override MArmor Build(string arg)
        {
            return BuildHelper(arg);
        }

        private MArmor BuildHelper(string arg)
        {
            var armor = new MArmor();
            var aStats = ArmorParamTable.Instance.Table[arg];
            armor.APMod = aStats.APMod;
            armor.BlockMod = aStats.BlockMod;
            armor.FlatDamageIgnore = aStats.DamageIgnore;
            armor.DamageMod = aStats.DamageMod;
            armor.DodgeMod = aStats.DodgeMod;
            armor.Durability = aStats.Durability;
            armor.FatigueMod = aStats.FatigueCost;
            armor.InitativeMod = aStats.InitativeMod;
            armor.MaxDurability = aStats.Durability;
            armor.ParryMod = aStats.ParryMod;
            armor.StaminaMod = aStats.StaminaMod;
            armor.Tier = aStats.Tier;
            armor.ArmorType = aStats.Type;
            return armor;
        }
    }
}