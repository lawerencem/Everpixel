using System.Collections.Generic;
using UnityEngine;

namespace Model.Abilities.Shapeshift
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

        public ShapeshiftInfo Copy()
        {
            var copy = new ShapeshiftInfo();
            copy.CharAttackHead = this.CharAttackHead;
            copy.CharAttackTorso = this.CharAttackTorso;
            copy.CharHead = this.CharHead;
            copy.CharHeadDead = this.CharHeadDead;
            copy.CharHeadFlinch = this.CharHeadFlinch;
            copy.CharTorso = this.CharTorso;
            return copy;
        }
    }
}
