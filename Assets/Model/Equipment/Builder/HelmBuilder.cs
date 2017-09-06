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
            helm.APMod = aStats.APMod;
            helm.BlockMod = aStats.BlockMod;
            helm.DamageIgnore = aStats.DamageIgnore;
            helm.DamageMod = aStats.DamageMod;
            helm.DodgeMod = aStats.DodgeMod;
            helm.Durability = aStats.Durability;
            helm.FatigueMod = aStats.FatigueCost;
            helm.InitativeMod = aStats.InitativeMod;
            helm.MaxDurability = aStats.Durability;
            helm.ParryMod = aStats.ParryMod;
            helm.StaminaMod = aStats.StaminaMod;
            helm.Tier = aStats.Tier;
            helm.ArmorType = aStats.Type;
            return helm;
        }
    }
}