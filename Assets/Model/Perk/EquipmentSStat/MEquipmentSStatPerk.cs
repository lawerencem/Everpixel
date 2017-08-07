using Assets.Generics;
using Assets.Model.Character.Param;
using System.Collections.Generic;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class MEquipmentSStatPerk : MPerk
    {
        public MEquipmentSStatPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {

        }
    }
}
