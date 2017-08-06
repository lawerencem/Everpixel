using Model.Characters;
using UnityEngine;
using View.Equipment;
using View.Mount;

namespace View.Characters
{
    public class CharacterView
    {
        public ArmorView Armor {get;set;}
        public int AttackSpriteIndex { get; set; }
        public int Face { get; set; }
        public int Head { get; set; }
        public int HeadDeco1 { get; set; }
        public int HeadDeco2 { get; set; }
        public ArmorView Helm { get; set; }
        public WeaponView LWeapon { get; set; }
        public string Name { get; set; }
        public MountView Mount { get; set; }
        public ERace Race { get; set; }
        public WeaponView RWeapon { get; set; }
        public int Torso { get; set; }
        public int TorsoDeco1 { get; set; }
        public int TorsoDeco2 { get; set; }
        public ECharacterType Type { get; set; }
        public Sprite[] Sprites { get; set; }
    }
}
