using Model.Abilities;
using Model.Classes;
using Model.Equipment;
using Model.Mounts;
using Model.Parties;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterPerkCollection
    {
        public List<GenericEquipmentSStatPerk> EquipmentSStatPerks;

        public CharacterPerkCollection()
        {
            this.EquipmentSStatPerks = new List<GenericEquipmentSStatPerk>();
        }
    }
}
