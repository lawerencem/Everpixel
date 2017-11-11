using Assets.Model.Biome;
using Assets.Template.Other;
using UnityEngine;

namespace Assets.View.Map
{
    public class MapBridge : ASingleton<MapBridge>
    {
        public Sprite[] GetBackgroundDecoSprites(EBiome b)
        {
            return MapSpriteLoader.Instance.GetBiomeBackgroundDeco(b);
        }

        public Sprite[] GetBackgroundSprites(EBiome b)
        {
            return MapSpriteLoader.Instance.GetBiomeBackground(b);
        }

        public Sprite GetHostileHoverSprite()
        {
            return MapSpriteLoader.Instance.GetHostileHoverSprite();
        }

        public Sprite GetTileHighlightSprite()
        {
            return MapSpriteLoader.Instance.GetTileHighlightSprite();
        }

        public Sprite GetPotentialAttackLocSprite()
        {
            return MapSpriteLoader.Instance.GetPotentialAttackLocSprite();
        }

        public Sprite GetSplatterSprites(int lvl)
        {
            switch(lvl)
            {
                case (1): return MapSpriteLoader.Instance.GetBloodSpatterLevelOne();
                case (2): return MapSpriteLoader.Instance.GetBloodSpatterLevelTwo();
                case (3): return MapSpriteLoader.Instance.GetBloodSpatterLevelThree();
                case (4): return MapSpriteLoader.Instance.GetBloodSpatterLevelFour();
                case (5): return MapSpriteLoader.Instance.GetBloodSpatterLevelFive();
                default: return MapSpriteLoader.Instance.GetBloodSpatterLevelOne();
            }
        }
    }
}
