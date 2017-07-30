using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Equipment;

namespace Model.Perks
{
    public class Colossus : GenericEquipmentSStatPerk
    {
        public Colossus() : base(PerkEnum.Colossus) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            if (mods.X.GetType() == typeof(GenericArmor))
            {
                var armor = mods.X as GenericArmor;
                if (armor.ArmorType == ArmorTypeEnum.Heavy_Armor)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == Characters.SecondaryStatsEnum.AP)
                            kvp.Scalar *= this.Val;
                    }
                }
            }
            else if (mods.X.GetType() == typeof(GenericHelm))
            {
                var helm = mods.X as GenericHelm;
                if (helm.ArmorType == ArmorTypeEnum.Heavy_Helm)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == Characters.SecondaryStatsEnum.AP)
                            kvp.Scalar *= this.Val;
                    }
                }
            }
        }
    }
}
