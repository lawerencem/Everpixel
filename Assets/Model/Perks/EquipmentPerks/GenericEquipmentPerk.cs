using Assets.Generics;
using Characters.Params;
using Model.Characters;
using Model.Equipment;
using System.Collections.Generic;

namespace Model.Perks
{
    public class GenericEquipmentPerk : GenericPerk
    {
        public GenericEquipmentPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryProcessAdd(GenericCharacter character, object equipment)
        {

        }

        public virtual void TryProcessRemove(GenericCharacter character, object equipment)
        {

        }
    }
}
