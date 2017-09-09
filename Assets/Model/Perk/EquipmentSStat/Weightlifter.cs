using Assets.Model.Character.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class Weightlifter : MEquipmentSStatPerk
    {
        public Weightlifter() : base(EPerk.Weightlifter) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            //if (mods.X.GetType() == typeof(MArmor))
            //{
            //    var armor = mods.X as MArmor;
            //    if (armor.ArmorType == EArmorType.Heavy_Armor)
            //    {
            //        foreach(var kvp in mods.Y)
            //        {
            //            if (kvp.Type == ESecondaryStat.Stamina)
            //                kvp.Scalar *= this.Val;
            //        }
            //    }
            //}
            //else if (mods.X.GetType() == typeof(MHelm))
            //{
            //    var helm = mods.X as MHelm;
            //    if (helm.ArmorType == EArmorType.Heavy_Helm)
            //    {
            //        foreach (var kvp in mods.Y)
            //        {
            //            if (kvp.Type == ESecondaryStat.Stamina)
            //                kvp.Scalar *= this.Val;
            //        }
            //    }
            //}
        }
    }
}
