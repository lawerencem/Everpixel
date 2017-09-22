using UnityEngine;

namespace Assets.Model.Combat.Hit
{
    public class HitTextDisplayData
    {
        public float Delay { get; set; }
        public float Dur { get; set; }
        public Color Color { get; set; }
        public GameObjectTags Target { get; set; }
        public string Text { get; set; }
        public float YOffset { get; set; }
    }
}
