using Assets.Generics;
using Assets.Model.Character;
using Characters.Params;
using Model.Characters;
using Model.Equipment;
using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Perks.EquipmentPerks
{
    public class MEquipmentPerk : MPerk
    {
        public MEquipmentPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryProcessAdd(MChar character, object equipment)
        {

        }

        public virtual void TryProcessRemove(MChar character, object equipment)
        {

        }
    }
}
