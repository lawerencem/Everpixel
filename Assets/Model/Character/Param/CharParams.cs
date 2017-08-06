using Assets.Generics;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Model.Classes;
using Model.Equipment;
using Model.Mounts;
using Model.Parties;
using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Character.Param
{
    public class CharParams
    {
        public CharParams()
        {
            this.Abilities = new List<EAbility>();
            this.BaseClasses = new Dictionary<EClass, int>();
            this.DefaultWpnAbilities = new List<EAbility>();
            this.Perks = new List<EPerk>();
        }
        
        public List<EAbility> Abilities { get; set; }
        public ArmorParams Armor { get; set; }
        public Dictionary<EClass, int> BaseClasses { get; set; }
        public List<EAbility> DefaultWpnAbilities { get; set; }
        public ArmorParams Helm { get; set; }
        public WeaponParams LWeapon { get; set; }
        public MountParams Mount { get; set; }
        public string Name { get; set; }
        public List<EPerk> Perks { get; set; }
        public WeaponParams RWeapon { get; set; }
        public ERace Race { get; set; }
        public EStartCol StartRow { get; set; }
        public ECharacterType Type { get; set; }
    }
}
