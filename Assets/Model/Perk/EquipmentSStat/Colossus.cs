using System.Collections.Generic;
using Assets.Generics;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Type;
using Assets.Model.Equipment.Enum;
using Assets.Model.Character.Enum;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class Colossus : MEquipmentSStatPerk
    {
        public Colossus() : base(EPerk.Colossus) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            if (mods.X.GetType() == typeof(MArmor))
            {
                var armor = mods.X as MArmor;
                if (armor.ArmorType == EArmorType.Heavy_Armor)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == ESecondaryStat.AP)
                            kvp.Scalar *= this.Val;
                    }
                }
            }
            else if (mods.X.GetType() == typeof(MHelm))
            {
                var helm = mods.X as MHelm;
                if (helm.ArmorType == EArmorType.Heavy_Helm)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == ESecondaryStat.AP)
                            kvp.Scalar *= this.Val;
                    }
                }
            }
        }
    }
}
