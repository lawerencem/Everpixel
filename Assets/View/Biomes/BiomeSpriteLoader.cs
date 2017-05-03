using Generics;
using Generics.Utilities;
using Model.Biomes;
using UnityEngine;

namespace View.Biomes
{
    public class BiomeSpriteLoader : AbstractSingleton<BiomeSpriteLoader>
    {
        private const int PATH_SPRITE_INDEX = 3;
        private const string PATH = "Sprites/CombatMap/";

        private const string DECO_EXTENSION = "_Deco";
        private const string TILE_EXTENSION = "_Tiles";

        public BiomeSpriteLoader() { }

        public Sprite[] GetBiomeBackground(BiomeEnum b)
        {
            var path = StringUtil.PathBuilder(PATH, b.ToString(), TILE_EXTENSION);
            return GetBackgroundSprites(path);
        }

        public Sprite[] GetBiomeBackgroundDeco(BiomeEnum b)
        {
            var path = StringUtil.PathBuilder(PATH, b.ToString(), DECO_EXTENSION);
            return GetBackgroundSprites(path);
        }

        public Sprite GetMoveTileSprite()
        {
            var path = StringUtil.PathBuilder(PATH, "Base", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            return stuff[PATH_SPRITE_INDEX] as Sprite;
        }

        private Sprite[] GetBackgroundDecoSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[8];
                //var sprites = new Sprite[stuff.Length - 1];
                //for (int itr = 1; itr < stuff.Length; itr++)  // TODO
                for (int itr = 1; itr < 9; itr++)
                {
                    sprites[itr - 1] = stuff.GetValue(itr) as Sprite;
                }
                return sprites;
            }
            else
                return null;
        }

        private Sprite[] GetBackgroundSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[8];
                //var sprites = new Sprite[stuff.Length - 1];
                //for (int itr = 1; itr < stuff.Length; itr++)  // TODO
                for (int itr = 1; itr < 9; itr++)
                {
                    sprites[itr - 1] = stuff.GetValue(itr) as Sprite;
                }
                return sprites;
            }
            else
                return null;
        }

    }
}
