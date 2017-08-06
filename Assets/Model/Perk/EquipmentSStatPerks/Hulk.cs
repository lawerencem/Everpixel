using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Equipment;

namespace Model.Perks
{
    public class Hulk : MEquipmentSStatPerk
    {
        public Hulk() : base(EPerk.Hulk) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            if (mods.X.GetType() == typeof(MWeapon))
            {
                var wpn = mods.X as MWeapon;
                if (!wpn.IsTypeOfShield())
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == Characters.ESecondaryStat.AP)
                            kvp.Scalar *= this.Val;
                    }
                }
            }
        }
    }
}
