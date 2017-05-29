using Generics;
using Model.Biomes;
using System.Collections.Generic;
using UnityEngine;

namespace View.Biomes
{
    public class MapBridge : AbstractSingleton<MapBridge>
    {
        public Sprite[] GetBackgroundDecoSprites(BiomeEnum b)
        {
            return MapSpriteLoader.Instance.GetBiomeBackgroundDeco(b);
        }

        public Sprite[] GetBackgroundSprites(BiomeEnum b)
        {
            return MapSpriteLoader.Instance.GetBiomeBackground(b);
        }

        public Sprite GetHostileHoverSprite()
        {
            return MapSpriteLoader.Instance.GetHostileHoverSprite();
        }

        public Sprite GetMovePathSprite()
        {
            return MapSpriteLoader.Instance.GetMoveTileSprite();
        }

        public Sprite GetPotentialAttackLocSprite()
        {
            return MapSpriteLoader.Instance.GetPotentialAttackLocSprite();
        }

        public Sprite GetSplatterLevelOneSprite()
        {
            return MapSpriteLoader.Instance.GetBloodSpatterLevelOne();
        }

        public Sprite GetSplatterLevelFourSprite()
        {
            return MapSpriteLoader.Instance.GetBloodSpatterLevelFour();
        }

        public Sprite GetSplatterLevelFiveSprite()
        {
            return MapSpriteLoader.Instance.GetBloodSpatterLevelFive();
        }
    }
}
