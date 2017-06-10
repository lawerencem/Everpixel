using Assets.Generics;
using Characters.Params;
using System.Collections.Generic;

namespace Model.Perks
{
    public class GenericEquipmentSStatPerk : GenericPerk
    {
        public GenericEquipmentSStatPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {

        }
    }
}
