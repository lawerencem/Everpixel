using Assets.Data.Equipment.Table;
using Assets.Model.Equipment.Type;
using Assets.Template.Builder;

namespace Assets.Model.Equipment.Builder
{
    public class HelmBuilder : GBuilder<string, MHelm>
    {
        public override MHelm Build(string arg)
        {
            return BuildHelper(arg);
        }

        private MHelm BuildHelper(string arg)
        {
            var helm = new MHelm();
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
            helm.ArmorType = aStats.Type;
            return helm;
        }
    }
}