using Model.Classes;
using Model.Equipment;
using Model.Mounts;
using Model.Parties;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterParams
    {
        public CharacterParams()
        {
            this.BaseClasses = new Dictionary<ClassEnum, int>();
        }

        public ArmorParams Armor { get; set; }
        public Dictionary<ClassEnum, int> BaseClasses { get; set; }
        public ArmorParams Helm { get; set; }
        public WeaponParams LWeapon { get; set; }
        public MountParams Mount { get; set; }
        public string Name { get; set; }
        public WeaponParams RWeapon { get; set; }
        public RaceEnum Race { get; set; }
        public StartingColEnum StartRow { get; set; }
        public CharacterTypeEnum Type { get; set; }
    }
}
