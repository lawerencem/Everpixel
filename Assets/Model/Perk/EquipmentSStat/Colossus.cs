using Assets.Model.Character.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class Colossus : MEquipmentSStatPerk
    {
        public Colossus() : base(EPerk.Colossus) { }

        public override void TryModEquipmentMod(Pair<object, List<StatMod>> mods)
        {
            //if (mods.X.GetType() == typeof(CArmor))
            //{
            //    var armor = mods.X as CArmor;
            //    if (armor.Model.Data.ArmorType == EArmorType.Heavy_Armor)
            //    {
            //        foreach (var kvp in mods.Y)
            //        {
            //            if (kvp.Type == ESecondaryStat.AP)
            //                kvp.Scalar *= this.Val;
            //        }
            //    }
            //}
            //else if (mods.X.GetType() == typeof(CHelm))
            //{
            //    var helm = mods.X as CHelm;
            //    if (helm.Model.Data.ArmorType == EArmorType.Heavy_Helm)
            //    {
            //        foreach (var kvp in mods.Y)
            //        {
            //            if (kvp.Type == ESecondaryStat.AP)
            //                kvp.Scalar *= this.Val;
            //        }
            //    }
            //}
        }
    }
}
