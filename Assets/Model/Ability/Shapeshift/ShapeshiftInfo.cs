using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Ability.Shapeshift
{
    public class ShapeshiftInfo
    {
        public Dictionary<string, Sprite> OldSpriteHandlerSprites { get; set; }
        public int CharAttackHead { get; set; }
        public int CharAttackTorso { get; set; }
        public int CharHead { get; set; }
        public int CharHeadDead { get; set; }
        public int CharHeadFlinch { get; set; }
        public int CharTorso { get; set; }

        public ShapeshiftInfo()
        {
            this.CharAttackHead = -1;
            this.CharAttackTorso = -1;
            this.CharHead = -1;
            this.CharHeadDead = -1;
            this.CharHeadFlinch = -1;
            this.CharHeadFlinch = -1;
            this.OldSpriteHandlerSprites = new Dictionary<string, Sprite>();
        }

        public ShapeshiftInfo Clone()
        {
            var clone = new ShapeshiftInfo();
            clone.CharAttackHead = this.CharAttackHead;
            clone.CharAttackTorso = this.CharAttackTorso;
            clone.CharHead = this.CharHead;
            clone.CharHeadDead = this.CharHeadDead;
            clone.CharHeadFlinch = this.CharHeadFlinch;
            clone.CharTorso = this.CharTorso;
            return clone;
        }
    }
}
