using Assets.Generics;
using Assets.Model.Character.Param;
using Characters.Params;
using System.Collections.Generic;

namespace Model.Perks
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
