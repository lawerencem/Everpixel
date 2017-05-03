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
            return BiomeSpriteLoader.Instance.GetBiomeBackgroundDeco(b);
        }

        public Sprite[] GetBackgroundSprites(BiomeEnum b)
        {
            return BiomeSpriteLoader.Instance.GetBiomeBackground(b);
        }

        public Sprite GetMovePathSprite()
        {
            return BiomeSpriteLoader.Instance.GetMoveTileSprite();
        }
    }
}
