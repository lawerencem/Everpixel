using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Armor;
using Assets.Model.Equipment.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class Weightlifter : MEquipmentSStatPerk
    {
        public Weightlifter() : base(EPerk.Weightlifter) { }

        public override void TryModEquipmentMod(Pair<object, List<StatMod>> mods)
        {
            if (mods.X.GetType() == typeof(CArmor))
            {
                var armor = mods.X as CArmor;
                if (armor.Model.Data.ArmorType == EArmorType.Heavy_Armor)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Data.Stat.Equals(ESecondaryStat.Stamina))
                            kvp.Data.Scalar *= this.Val;
                    }
                }
            }
            else if (mods.X.GetType() == typeof(CHelm))
            {
                var helm = mods.X as CHelm;
                if (helm.Model.Data.ArmorType == EArmorType.Heavy_Helm)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Data.Stat.Equals(ESecondaryStat.Stamina))
                            kvp.Data.Scalar *= this.Val;
                    }
                }
            }
        }
    }
}
