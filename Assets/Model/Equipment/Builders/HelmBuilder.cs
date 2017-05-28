using Generics;
using Model.Abilities;

namespace Model.Equipment
{
    public class HelmBuilder : GenericBuilder<string, GenericHelm>
    {
        public override GenericHelm Build(string arg)
        {
            return BuildHelper(arg);
        }

        private GenericHelm BuildHelper(string arg)
        {
            var helm = new GenericHelm();
            var aStats = ArmorParamTable.Instance.Table[arg];
            helm.APReduce = aStats.APReduce;
            helm.BlockReduce = aStats.BlockReduce;
            helm.DamageIgnore = aStats.DamageIgnore;
            helm.DamageReduction = aStats.DamageReduction;
            helm.DodgeMod = aStats.DodgeMod;
            helm.Durability = aStats.Durability;
            helm.FatigueCost = aStats.FatigueCost;
            helm.InitativeReduce = aStats.InitiativeReduce;
            helm.MaxDurability = aStats.Durability;
            helm.ParryReduce = aStats.ParryReduce;
            helm.StaminaReduce = aStats.StaminaReduce;
            helm.Tier = aStats.Tier;
            return helm;
        }
    }
}