using Assets.Model.Equipment.Table;
using Assets.Model.Equipment.Type;
using Generics;
using Model.Abilities;

namespace Assets.Model.Equipment.Builder
{
    public class ArmorBuilder : GenericBuilder<string, MArmor>
    {
        public override MArmor Build(string arg)
        {
            return BuildHelper(arg);
        }

        private MArmor BuildHelper(string arg)
        {
            var armor = new MArmor();
            var aStats = ArmorParamTable.Instance.Table[arg];
            armor.APReduce = aStats.APReduce;
            armor.BlockReduce = aStats.BlockReduce;
            armor.DamageIgnore = aStats.DamageIgnore;
            armor.DamageReduction = aStats.DamageReduction;
            armor.DodgeMod = aStats.DodgeMod;
            armor.Durability = aStats.Durability;
            armor.FatigueCost = aStats.FatigueCost;
            armor.InitativeReduce = aStats.InitiativeReduce;
            armor.MaxDurability = aStats.Durability;
            armor.ParryReduce = aStats.ParryReduce;
            armor.StaminaReduce = aStats.StaminaReduce;
            armor.Tier = aStats.Tier;
            armor.ArmorType = aStats.Type;
            return armor;
        }
    }
}