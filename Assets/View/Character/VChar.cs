using Assets.Model.Character.Enum;
using Assets.View.Equipment;
using Assets.View.Mount;
using UnityEngine;

namespace Assets.View.Character
{
    public class VChar
    {
        public VArmor Armor {get;set;}
        public int AttackSpriteIndex { get; set; }
        public int Face { get; set; }
        public int Head { get; set; }
        public int HeadDeco1 { get; set; }
        public int HeadDeco2 { get; set; }
        public VArmor Helm { get; set; }
        public VWeapon LWeapon { get; set; }
        public string Name { get; set; }
        public VMount Mount { get; set; }
        public ERace Race { get; set; }
        public VWeapon RWeapon { get; set; }
        public int Torso { get; set; }
        public int TorsoDeco1 { get; set; }
        public int TorsoDeco2 { get; set; }
        public ECharType Type { get; set; }
        public Sprite[] Sprites { get; set; }
    }
}
