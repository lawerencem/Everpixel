using Assets.Data.Equipment.Table;
using Assets.Model.Equipment.Armor;
using Assets.Template.Builder;

namespace Assets.Model.Equipment.Builder
{
    public class ArmorBuilder : GBuilder<string, CArmor>
    {
        public override CArmor Build(string arg)
        {
            return BuildHelper(arg);
        }

        private CArmor BuildHelper(string arg)
        {
            var model = new MArmor();
            var aStats = ArmorParamTable.Instance.Table[arg];
            model.Data.APMod = aStats.APMod;
            model.Data.BlockMod = aStats.BlockMod;
            model.Data.FlatDamageIgnore = aStats.DamageIgnore;
            model.Data.DamageMod = aStats.DamageMod;
            model.Data.DodgeMod = aStats.DodgeMod;
            model.Data.Durability = aStats.Durability;
            model.Data.FatigueMod = aStats.FatigueCost;
            model.Data.InitativeMod = aStats.InitativeMod;
            model.Data.MaxDurability = aStats.Durability;
            model.Data.ParryMod = aStats.ParryMod;
            model.Data.StaminaMod = aStats.StaminaMod;
            model.Data.Tier = aStats.Tier;
            model.Data.ArmorType = aStats.Type;
            var controller = new CArmor();
            controller.SetModel(model);
            controller.SetParams(aStats);
            return controller;
        }
    }
}
