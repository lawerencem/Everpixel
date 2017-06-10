using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Equipment;

namespace Model.Perks
{
    public class Weightlifter : GenericEquipmentSStatPerk
    {
        protected const double VALUE = 1.25;

        public Weightlifter() : base(PerkEnum.Weightlifter) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            if (mods.X.GetType() == typeof(GenericArmor))
            {
                var armor = mods.X as GenericArmor;
                if (armor.Type == ArmorTypeEnum.Heavy_Armor)
                {
                    foreach(var kvp in mods.Y)
                    {
                        if (kvp.Type == Characters.SecondaryStatsEnum.Stamina)
                            kvp.Scalar *= VALUE;
                    }
                }
            }
            else if (mods.X.GetType() == typeof(GenericHelm))
            {
                var helm = mods.X as GenericHelm;
                if (helm.Type == ArmorTypeEnum.Heavy_Helm)
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == Characters.SecondaryStatsEnum.Stamina)
                            kvp.Scalar *= VALUE;
                    }
                }
            }
        }
    }
}
