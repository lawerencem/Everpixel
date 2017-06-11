using Model.Abilities;
using Model.Classes;
using Model.Equipment;
using Model.Mounts;
using Model.Parties;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterParams
    {
        public CharacterParams()
        {
            this.ActiveAbilities = new List<ActiveAbilitiesEnum>();
            this.BaseClasses = new Dictionary<ClassEnum, int>();
            this.DefaultWpnAbilities = new List<WeaponAbilitiesEnum>();
            this.Perks = new List<PerkEnum>();
        }
        
        public List<ActiveAbilitiesEnum> ActiveAbilities { get; set; }
        public ArmorParams Armor { get; set; }
        public Dictionary<ClassEnum, int> BaseClasses { get; set; }
        public List<WeaponAbilitiesEnum> DefaultWpnAbilities { get; set; }
        public ArmorParams Helm { get; set; }
        public WeaponParams LWeapon { get; set; }
        public MountParams Mount { get; set; }
        public string Name { get; set; }
        public List<PerkEnum> Perks { get; set; }
        public WeaponParams RWeapon { get; set; }
        public RaceEnum Race { get; set; }
        public StartingColEnum StartRow { get; set; }
        public CharacterTypeEnum Type { get; set; }
    }
}
