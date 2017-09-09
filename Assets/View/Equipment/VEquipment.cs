using UnityEngine;

namespace Assets.View.Equipment
{
    public class VEquipment
    {
        public string Name { get; set; }
        public int SpriteIndex { get; set; }
        public Sprite[] Sprites { get; set; }

        public VEquipment()
        {
            this.SpriteIndex = -1;
        }
    }
}
