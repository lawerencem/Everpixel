using Assets.Controller.Equipment.Weapon;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class Hulk : MEquipmentSStatPerk
    {
        public Hulk() : base(EPerk.Hulk) { }

        public override void TryModEquipmentMod(Pair<object, List<StatMod>> mods)
        {
            if (mods.X.GetType() == typeof(CWeapon))
            {
                var wpn = mods.X as CWeapon;
                if (!wpn.IsTypeOfShield())
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Data.Stat.Equals(ESecondaryStat.AP))
                            kvp.Data.Scalar *= this.Val;
                    }
                }
            }
        }
    }
}
