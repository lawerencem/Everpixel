using Model.Characters;
using UnityEngine;
using View.Equipment;
using View.Mount;

namespace View.Characters
{
    public class CharacterView
    {
        public ArmorView Armor {get;set;}
        public int Deco1 { get; set; }
        public int Deco2 { get; set; }
        public int Deco3 { get; set; }
        public int Face { get; set; }
        public ArmorView Helm { get; set; }
        public WeaponView LWeapon { get; set; }
        public string Name { get; set; }
        public MountView Mount { get; set; }
        public RaceEnum Race { get; set; }
        public WeaponView RWeapon { get; set; }
        public int Torso { get; set; }
        public CharacterTypeEnum Type { get; set; }
        public Sprite[] Sprites { get; set; }
    }
}
