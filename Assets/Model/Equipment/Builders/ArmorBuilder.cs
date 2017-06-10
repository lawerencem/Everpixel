using Generics;
using Model.Abilities;

namespace Model.Equipment
{
    public class ArmorBuilder : GenericBuilder<string, GenericArmor>
    {
        public override GenericArmor Build(string arg)
        {
            return BuildHelper(arg);
        }

        private GenericArmor BuildHelper(string arg)
        {
            var armor = new GenericArmor();
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
            armor.Type = aStats.Type;
            return armor;
        }
    }
}